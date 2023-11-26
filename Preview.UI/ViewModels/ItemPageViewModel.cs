using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Data;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports;

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;

using Ookii.Dialogs.Wpf;

using Xylia.Configure;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Art.GameUI.Scene.Game_Auction;
using Xylia.Preview.UI.FModel.Views;
using Xylia.Preview.UI.Helpers.Output.Items;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Editor;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.ViewModels;
public partial class ItemPageViewModel : ObservableObject
{
	#region List
	[ObservableProperty]
	bool onlyUpdate;

	[ObservableProperty]
	string itemListPath;


	[RelayCommand]
	public void OpenSettings()
	{
		new SettingsView().ShowDialog();
	}

	[RelayCommand]
	private void BrowerItemList()
	{
		var dialog = new VistaOpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		if (dialog.ShowDialog() == true) ItemListPath = dialog.FileName;
	}

	[RelayCommand]
	private async Task CreateItemList()
	{
		#region Check
		if (!Directory.Exists(UserSettings.Default.GameFolder)) throw new WarningException("Must to set game directory");
		else if (!Directory.Exists(UserSettings.Default.OutputFolder)) throw new WarningException("Must to set output directory");
		else if (OnlyUpdate == true && !File.Exists(ItemListPath)) throw new WarningException("Select a valid item list");
		#endregion

		#region Load
		var fileMode = await Dialog.Show<FileModeDialog>().GetResultAsync<FileModeDialog.FileMode>();
		if (fileMode == FileModeDialog.FileMode.None) return;

		int span = 0;
		using var Out = new ItemOut() { OnlyUpdate = this.OnlyUpdate };
		await Task.Run(() =>
		{
			var startTime = DateTime.Now;

			Out.LoadCache(ItemListPath);
			Out.GetData();
			if (Out.Count == 0)
			{
				Growl.Error("没有新增的物品");
				return;
			}

			Out.Start(startTime, fileMode == FileModeDialog.FileMode.Xlsx);
			span = (int)(DateTime.Now - startTime).TotalSeconds;
		});
		#endregion


		Growl.Success(new GrowlInfo()
		{
			Message = string.Format("本次拉取数据共计{0}条, 总耗{1}秒。", Out.Count, span),
			StaysOpen = true,
		});
	}
	#endregion

	#region Preview
	[RelayCommand]
	private void Preview(string name)
	{
		name = $"Xylia.Preview.UI.Art.GameUI.Scene.{name}";

		var type = Type.GetType(name);
		if (type is null) return;

		(Activator.CreateInstance(type) as System.Windows.Window)?.Show();
	}

	[RelayCommand]
	private void PreviewItem(string rule)
	{
		var scene = new Game_AuctionScene();
		scene.NameFilter = rule;
		scene.Show();
	}

	[RelayCommand]
	private void PreviewList(string name)
	{
		IEnumerable<Record> records = null;
		if (name == "Npc") records = FileCache.Data.Npc;

		if (records != null)
		{
			var window = new SearcherResult();
			window.Source = new CollectionView(records);
			window.Show();
		}
	}


	public static async void StartOutput<T>() where T : OutSet, new()
	{
		var instance = new T();

		var save = new VistaSaveFileDialog
		{
			Filter = "Excel Files|*.xlsx",
			FileName = $"{instance.Name} ({DateTime.Now:yyyyMM}).xlsx",

			InitialDirectory = Ini.Instance.ReadValue("Folder", "OutputExcel")
		};
		if (save.ShowDialog() != true) return;

		DateTime dt = DateTime.Now;
		await instance.Output(new FileInfo(save.FileName));

		int span = (int)(DateTime.Now - dt).TotalSeconds;
		Growl.Success(string.Format("output finish, {0}s" , span));
	}
	#endregion
}

public class PreviewModel : ICommand
{
	public static PreviewModel Command { get; } = new();


	public event EventHandler? CanExecuteChanged;

	public bool CanExecute(object parameter) => true;

	public void Execute(object parameter) => Task.Run(async () =>
	{
		List<ModelData> models = new();
		await Load(parameter, models);

		// show
		var view = MyTest.ModelViewer;
		view.Models = models.ToArray();
		if (view.TryLoadExport(default)) view.Run();
		else Debug.WriteLine(parameter?.GetType());
	});


	private static async Task Load(object parameter, List<ModelData> models)
	{
		if (parameter is null) return;
		else if (parameter is Npc npc)
		{
			var _appearance = npc?.Appearance.Instance;
			if (_appearance is null) return;

			models.Add(new()
			{
				Export = await FileCache.Provider.LoadObjectAsync<UObject>(_appearance.BodyMeshName.Path),
				Cols = new string[] { _appearance.BodyMaterialName.Path },
				AnimSet = await FileCache.Provider.LoadObjectAsync<UAnimSet>(npc.Animset.Path),
			});
		}
		else if (parameter is Item item)
		{
			void LoadModel(string Mesh, string Col = null)
			{
				Col ??= Mesh + "-col";
				models.Add(new ModelData()
				{
					DisplayName = Mesh,
					Export = FileCache.Provider.LoadObject<UObject>(item.Attributes[Mesh]),
					Cols = new string[] { item.Attributes[Col, 1], item.Attributes[Col, 2], item.Attributes[Col, 3] },
				});
			}

			var MeshId = item.Attributes["mesh-id"];
			if (!string.IsNullOrEmpty(MeshId))
			{
				//"mesh-id"
				//"mesh-id-2"
				//"mesh-col-1"
				//"mesh-col-2"
				//"mesh-col-3"
				//"mesh-animset"
				//"mesh-attach"
				//"mesh-animtree"

				LoadModel("mesh-id", "mesh-col");

				models.Add(new ModelData()
				{
					Export = FileCache.Provider.LoadObject<UObject>(item.Attributes["talk-mesh"]),
					AnimSet = FileCache.Provider.LoadObject<UAnimSet>(item.Attributes["talk-animset"]),
				});
			}
			else
			{
				LoadModel("kun-mesh");
				LoadModel("gon-male-mesh");
				LoadModel("gon-female-mesh");
				LoadModel("lyn-male-mesh");
				LoadModel("lyn-female-mesh");
				LoadModel("jin-male-mesh");
				LoadModel("jin-female-mesh");
				LoadModel("cat-mesh");

				var temp = models.Where(model => model.Export != null);
				if (temp.Any())
				{
					models = temp.ToList();
					return;
				}

				else if (item is Item.Weapon weapon)
				{
					var pet = item.Attributes["pet"];
					await Load(pet, models);

					var equipshow = item.Attributes["equip-show"];
					if (!string.IsNullOrEmpty(equipshow))
					{
						//var EquipShow = FileCache.Pakitem.LoadObject<UShowObject>(equipshow);
					}
				}
				else if (item is Item.Accessory accessory)
				{
					var VehicleDetail = item.Attributes["vehicle-detail"];
					if (VehicleDetail != null)
					{
						var Vehicle = FileCache.Data.Vehicle[VehicleDetail];
						var Appearance = Vehicle.Appearance.Instance;
						if (Vehicle != null && Appearance != null)
						{
							models.Add(new ModelData()
							{
								Export = FileCache.Provider.LoadObject<UObject>(Appearance.MeshName.Path),
								AnimSet = FileCache.Provider.LoadObject<UAnimSet>(Appearance.AnimSetName.Path),
								Cols = Appearance.MaterialName.Select(o => o.Path),
							});
						}
					}
				}
			}
		}
		else if (parameter is Pet pet)
		{
			models.Add(new()
			{
				Export = await FileCache.Provider.LoadObjectAsync<UObject>(pet.MeshName.Path),
				Cols = pet.MaterialName.Select(o => o.Path),
				AnimSet = await FileCache.Provider.LoadObjectAsync<UAnimSet>(pet.AnimSetName.Path),
			});
		}
	}
}

public class PreviewRaw : ICommand
{
	public static PreviewRaw Command { get; } = new();


	public event EventHandler? CanExecuteChanged;

	public bool CanExecute(object parameter) => true;

	public void Execute(object parameter) => Execute(parameter, false);

	public void Execute(object parameter, bool mode)
	{
		if (parameter is Record record)
		{
			// TODO: valid children
			// Warning: is not original text
			if (parameter is Quest || mode)
			{
				var editor = new TextEditor();
				editor.Text = record.Owner.WriteXml(record);
				editor.Show();
			}
			else
			{
				var editor = new PropertyEditor();
				editor.Source = record;
				editor.Show();
			}
		}

		if (parameter is Table table)
		{
			var editor = new TextEditor();
			editor.Text = table.WriteXml();
			editor.Show();
		}
	}
}