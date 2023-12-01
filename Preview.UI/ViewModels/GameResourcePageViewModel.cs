using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CUE4Parse.BNS.Conversion;
using CUE4Parse.Utils;

using HandyControl.Controls;
using HandyControl.Data;

using Ookii.Dialogs.Wpf;

using Serilog;

using SkiaSharp;
using SkiaSharp.Views.WPF;

using Xylia.Preview.UI.Common;
using Xylia.Preview.UI.Helpers.Output.Textures;
using Xylia.Preview.UI.Views;

using MessageBox = HandyControl.Controls.MessageBox;

namespace Xylia.Preview.UI.ViewModels;
public partial class GameResourcePageViewModel : ObservableObject
{
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

	public void Run(IconOutBase Out, string format, int id) => Task.Run(async () =>
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

			await Out.LoadData(source.Token);
			await Out.Output(format, source.Token);
			Out.Dispose();

			Growl.Success(new GrowlInfo()
			{
				Message = string.Format(StringHelper.Get("IconOut_TaskCompleted"), DateTime.Now - start),
				StaysOpen = true,
			});
		}
		catch (Exception ee)
		{
			Growl.Error(string.Format(StringHelper.Get("IconOut_TaskException"), ee.Message));
			Log.Error(ee, "Exception at IconOut");
		}
		finally
		{
			ProcessEx.ClearMemory();
		}

		source.Dispose();
		this.Sources[id] = source = null;
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


	QuoteItem<sbyte> _mergeIcon_Grade;
	public QuoteItem<sbyte> MergeIcon_Grade
	{
		get => _mergeIcon_Grade;
		set
		{
			_mergeIcon_Grade = value;
			MergeIcon();
		}
	}
	public List<QuoteItem<sbyte>> GradeList => new()
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


	QuoteItem<SKBitmap> _mergeIcon_BottomLeft;
	public QuoteItem<SKBitmap> MergeIcon_BottomLeft
	{
		get => _mergeIcon_BottomLeft;
		set
		{
			_mergeIcon_BottomLeft = value;
			MergeIcon();
		}
	}
	public List<QuoteItem<SKBitmap>> BottomLeftList => new()
	{
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
	};


	QuoteItem<SKBitmap> _mergeIcon_TopRight;
	public QuoteItem<SKBitmap> MergeIcon_TopRight
	{
		get => _mergeIcon_TopRight;
		set
		{
			_mergeIcon_TopRight = value;
			MergeIcon();
		}
	}
	public List<QuoteItem<SKBitmap>> TopRightList => new()
	{
		GetImage("None"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/SlotItem_marketBusiness"),
		GetImage("Art/GameUI/Resource/GameUI_Icon3_R/SlotItem_privateSale"),
	};



	[ObservableProperty]
	ImageSource mergeIcon_Image;

	public void MergeIcon()
	{
		var grade = _mergeIcon_Grade?.Value ?? 1;
		var info = Application.GetResourceStream(new Uri(MergeIcon_BackgroundMode ?
			 $"/Preview.UI;component/Art/GameUI/Resource/GameUI_Window_R/ItemIcon_Bg_Grade_{grade}.png" :
			 $"/Preview.UI;component/Resources/Images/ue3/ItemIcon_Bg_Grade_{grade}.png", UriKind.Relative));

		var bitmap = SKBitmap.Decode(info.Stream);

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




	public static QuoteItem<SKBitmap> GetImage(string path)
	{
		if (path == "None") return new(null, Application.Current.TryFindResource("Text_None"));

		var info = Application.GetResourceStream(new Uri($"/Preview.UI;component/{path}.png", UriKind.Relative));
		return new(SKBitmap.Decode(info.Stream), StringHelper.Get(path.SubstringAfterLast('/')));
	}
	#endregion
}