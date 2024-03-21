using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.Compression;
using CUE4Parse.UE4.Pak;
using CUE4Parse.Utils;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using Serilog;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using Xylia.Preview.Common;
using Xylia.Preview.UI.Common;
using Xylia.Preview.UI.Helpers.Output.Textures;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Selector;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Xylia.Preview.UI.ViewModels;
internal partial class GameResourcePageViewModel : ObservableObject
{
	#region Asset
	[ObservableProperty]
	ObservableCollection<PackageParam> packages;

	[ObservableProperty]
	PackageParam selectedPackage;

	[ObservableProperty]
	PackageParam.FileParam selectedFile;


	[RelayCommand]
	public void LoadPackageInfo(string? path = null)
	{
		if (path is null)
		{
			var dialog = new VistaOpenFileDialog()
			{
				Filter = "configuration file|*.json",
			};
			if (dialog.ShowDialog() != true) return;

			path = dialog.FileName;
		}

		var pkg = JsonConvert.DeserializeObject<PackageParam[]>(File.ReadAllText(path))!;
		foreach (var p in pkg)
		{
			foreach (var f in p.Files)
				f.Owner = p;
		}

		// update
		this.Packages = new(pkg);
		this.SelectedPackage = pkg.FirstOrDefault();
	}

	[RelayCommand]
	public void SavePackageInfo()
	{
		var dialog = new VistaSaveFileDialog()
		{
			Filter = "configuration file|*.json",
			FileName = "RepackInfo.json",
		};
		if (dialog.ShowDialog() != true) return;

		File.WriteAllText($"{dialog.FileName}", JsonConvert.SerializeObject(Packages, Formatting.Indented));
	}

	[RelayCommand]
	public void AddPackageInfo()
	{
		this.Packages ??= [];
		this.Packages.Add(new());
	}

	[RelayCommand]
	public void RemovePackageInfo()
	{
		this.Packages.Remove(SelectedPackage);
	}


	[RelayCommand]
	public async Task AddFileInfo()
	{
		var file = await Dialog.Show<FileInfoDialog>().GetResultAsync<PackageParam.FileParam>();
		if (file is null) return;

		file.Owner = this.SelectedPackage;
		this.SelectedPackage?.Files.Add(file);
	}

	[RelayCommand]
	public async Task UpdateFileInfo()
	{
		var dialog = new FileInfoDialog() { Result = SelectedFile };
		if (dialog.Result is null) return;

		await Dialog.Show(dialog).GetResultAsync<PackageParam.FileParam>();

		// update
		var temp = SelectedPackage;
		SelectedPackage = null;
		SelectedPackage = temp;
	}

	[RelayCommand]
	public void RemoveFileInfo()
	{
		this.SelectedPackage?.Files.Remove(SelectedFile);
	}



	public async Task UeExporter(string filter, bool ContainType) => await Task.Run(() =>
	{
		using var provider = new GameFileProvider(UserSettings.Default.GameFolder);
		filter = provider.FixPath(filter, false) ?? filter;

		Parallel.ForEach(provider.Files.Values, gamefile =>
		{
			if (gamefile.Extension != "uasset" || !gamefile.Path.Contains(filter, StringComparison.OrdinalIgnoreCase))
				return;

			try
			{
				new Exporter(UserSettings.Default.OutputFolderResource)
					.Run(provider.LoadPackage(gamefile), ContainType);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		});
	});

	public async Task UeRepack(string folder, List<PackageParam> packages) => await Task.Run(() =>
	{
		foreach (var package in packages)
		{
			var reader = new MyPakFileReader(package.MountPoint);
			foreach (var file in package.Files)
			{
				if (!file.IsValid) continue;

				reader.Add(file.Path, file.Vfs, file.Compression);
			}

			reader.WriteToDir(folder, package.Name + ".pak");
		}
	});
	#endregion


	#region Icon
	[ObservableProperty]
	string icon_OutputFolder = UserSettings.Default.OutputFolderResource;

	[ObservableProperty]
	string icon_ItemListPath;


	[RelayCommand]
	public void OpenSettings()
	{
		new SettingsView().ShowDialog();
	}

	[RelayCommand]
	private void Icon_BrowerOutputFolder()
	{
		var dialog = new VistaFolderBrowserDialog() { };
		if (dialog.ShowDialog() == true) Icon_OutputFolder = dialog.SelectedPath;
	}

	[RelayCommand]
	private void Icon_BrowerItemList()
	{
		var dialog = new VistaOpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		if (dialog.ShowDialog() == true) Icon_ItemListPath = dialog.FileName;
	}


	readonly CancellationTokenSource[] Sources = new CancellationTokenSource[20];

	public void Run(IconOutBase Out, string format, int id) => Task.Run(() =>
	{
		#region Token
		var source = this.Sources[id];
		if (source != null)
		{
			if (MessageBox.Show(StringHelper.Get("IconOut_TaskCancel"), StringHelper.Get("Message_Tip"), MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				source?.Cancel();
				source = null;
			}

			return;
		}

		source = this.Sources[id] = new CancellationTokenSource();
		#endregion

		try
		{
			DateTime start = DateTime.Now;

			Out.LoadData(source.Token);
			Out.Output(format, source.Token);
			Out.Dispose();

			Growl.SuccessGlobal(new GrowlInfo()
			{
				Message = StringHelper.Get("IconOut_TaskCompleted", DateTime.Now - start),
				StaysOpen = true,
			});
		}
		catch (Exception ee)
		{
			Growl.Error(StringHelper.Get("IconOut_TaskException", ee.Message));
			Log.Error(ee, "Exception at IconOut");
		}
		finally
		{
			source.Dispose();
			this.Sources[id] = null;

			ProcessFloatWindow.ClearMemory();
		}
	});
	#endregion


	#region Merge
	SKBitmap _mergeIcon_Source;
	public SKBitmap MergeIcon_Source
	{
		get => _mergeIcon_Source;
		set
		{
			_mergeIcon_Source = value;
			MergeIcon();
		}
	}

	bool _mergeIcon_BackgroundMode = true;
	public bool MergeIcon_BackgroundMode
	{
		get => _mergeIcon_BackgroundMode;
		set
		{
			_mergeIcon_BackgroundMode = value;
			MergeIcon();
		}
	}


	NameObject<sbyte> _mergeIcon_Grade;
	public NameObject<sbyte> MergeIcon_Grade
	{
		get => _mergeIcon_Grade;
		set
		{
			_mergeIcon_Grade = value;
			MergeIcon();
		}
	}
	public List<NameObject<sbyte>> GradeList => new()
	{
		{ new(1, StringHelper.Get("MergeIcon_Grade1")) },
		{ new(2, StringHelper.Get("MergeIcon_Grade2")) },
		{ new(3, StringHelper.Get("MergeIcon_Grade3")) },
		{ new(4, StringHelper.Get("MergeIcon_Grade4")) },
		{ new(5, StringHelper.Get("MergeIcon_Grade5")) },
		{ new(6, StringHelper.Get("MergeIcon_Grade6")) },
		{ new(7, StringHelper.Get("MergeIcon_Grade7")) },
		{ new(8, StringHelper.Get("MergeIcon_Grade8")) },
		{ new(9, StringHelper.Get("MergeIcon_Grade9")) },
	};


	NameObject<SKBitmap> _mergeIcon_BottomLeft;
	public NameObject<SKBitmap> MergeIcon_BottomLeft
	{
		get => _mergeIcon_BottomLeft;
		set
		{
			_mergeIcon_BottomLeft = value;
			MergeIcon();
		}
	}

	public List<NameObject<SKBitmap>> BottomLeftList =>
	[
		GetImage("None"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_01"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_02"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_03"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_04"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_05"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_06"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/Weapon_Lock_06"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/unuseable_lock"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/unuseable_lock_2"),
	];


	NameObject<SKBitmap> _mergeIcon_TopRight;
	public NameObject<SKBitmap> MergeIcon_TopRight
	{
		get => _mergeIcon_TopRight;
		set
		{
			_mergeIcon_TopRight = value;
			MergeIcon();
		}
	}
	public List<NameObject<SKBitmap>> TopRightList =>
	[
		GetImage("None"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/SlotItem_marketBusiness"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/SlotItem_privateSale"),
	];



	[ObservableProperty]
	ImageSource mergeIcon_Image;

	public void MergeIcon()
	{
		var grade = _mergeIcon_Grade?.Value ?? 1;
		var info = GetImage(MergeIcon_BackgroundMode ?
			$"Art/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}" :
			$"Art/GameUI/Resource/GameUI_Window/ItemIcon_Bg_Grade_{grade}");

		var bitmap = info.Value;

		if (_mergeIcon_Source != null)
			bitmap = bitmap.Compose(_mergeIcon_Source);

		bitmap = bitmap.Compose(_mergeIcon_BottomLeft?.Value);
		bitmap = bitmap.Compose(_mergeIcon_TopRight?.Value);

		MergeIcon_Image = bitmap.ToWriteableBitmap();
	}

	[RelayCommand]
	public void MergeIcon_Save()
	{
		var dialog = new Microsoft.Win32.SaveFileDialog()
		{
			FileName = "item",
			Filter = "png file|*.png",
		};

		if (dialog.ShowDialog() == true)
		{
			using var fs = new FileStream(dialog.FileName, FileMode.Create);
			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create((BitmapSource)MergeIcon_Image));
			encoder.Save(fs);
			fs.Flush();
		}
	}


	public static NameObject<SKBitmap> GetImage(string path)
	{
		if (path == "None") return new(null, StringHelper.Get("Text_None"));

		var resource = new Uri($"/Preview.UI;component/Content/{path}.png", UriKind.Relative);
		using var stream = Application.GetResourceStream(resource).Stream;
		return new(SKBitmap.Decode(stream), StringHelper.Get(path.SubstringAfterLast('/')));
	}
	#endregion
}

public class PackageParam
{
	public string Name { get; set; } = "Xylia_P";

	public string MountPoint { get; set; } = @"BNSR\Content";

	public ObservableCollection<FileParam> Files { get; set; } = [];


	public class FileParam
	{
		public string Path { get; set; }

		public string Vfs { get; set; }

		public CompressionMethod Compression { get; set; }


		[JsonIgnore]
		public bool IsValid => File.Exists(Path);

		[JsonIgnore]
		internal PackageParam Owner { get; set; }

		public override string ToString() => System.IO.Path.Combine(Owner.MountPoint, Vfs);
	}
}