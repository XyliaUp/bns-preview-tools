using System.IO;
using System.Windows.Data;
using System.Windows.Input;

using AduSkin.Controls.Metro;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;

using Ookii.Dialogs.Wpf;

using Xylia.Configure;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.FModel.Views;
using Xylia.Preview.UI.Helpers.Output.Items;
using Xylia.Preview.UI.Views.Selector;

using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.UI.ViewModels;
public partial class ItemPageViewModel : ObservableObject
{
	#region List
	[ObservableProperty]
	bool onlyUpdate;

	[ObservableProperty]
	string itemListPath;


	[RelayCommand]
	private async Task CreateItemList()
	{
		#region Check
		if (!Directory.Exists(UserSettings.Default.GameFolder))
		{
			AduMessageBox.Show("Must to set game directory");
			return;
		}
		else if (!Directory.Exists(UserSettings.Default.OutputFolder))
		{
			AduMessageBox.Show("Must to set output directory");
			return;
		}
		else if (OnlyUpdate == true && !File.Exists(ItemListPath))
		{
			AduMessageBox.Show("Select a valid item list, or to cancel");
			return;
		}
		#endregion

		#region Load
		var select2 = new FileModeDialog();
		if (select2.ShowDialog() != true) return;

		var startTime = DateTime.Now;
		using var match = new ItemOut() { OnlyUpdate = this.OnlyUpdate };

		await Task.Run(async () =>
		{
			match.LoadCache(ItemListPath);
			await match.GetData();
			if (match.Count == 0)
			{
				AduMessageBox.Show("没有新增的物品");
				return;
			}

			match.Start(startTime, select2.Result == FileModeDialog.FileMode.Xlsx);
		});

		AduMessageBox.Show($"本次拉取数据共计{match.Count}条, 总耗{(DateTime.Now - startTime).TotalSeconds}秒。", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
		#endregion
	}
	#endregion

	#region Preview
	[RelayCommand]
	private void Preview(string name)
	{
		name = $"Xylia.Preview.UI.Art.GameUI.Scene.{name}";

		var type = Type.GetType(name);
		if (type is null) return;

		(Activator.CreateInstance(type) as Window)?.Show();
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

		AduMessageBox.Show($"output finish, {(DateTime.Now - dt).TotalSeconds:#0}s");
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
			Debug.WriteLine(item.Attributes);

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
			if (MeshId != null)
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

				var temp = models.Where(model => model.Export != null).ToList();
				if (temp.Any())
				{
					models = temp;
					return;
				}
				else if (item is Weapon weapon)
				{
					var pet = item.Attributes["pet"];
					if (pet != null)
					{
						var Pet = FileCache.Data.Pet[pet];
						if (Pet != null)
						{
							models.Add(new ModelData()
							{
								Export = FileCache.Provider.LoadObject<UObject>(Pet.MeshName.Path),
								Cols = Pet.MaterialName.Select(o => o.Path)
							});
						}
					}

					var equipshow = item.Attributes["equip-show"];
					if (equipshow != null)
					{
						//var EquipShow = FileCache.Pakitem.LoadObject<UShowObject>(equipshow);
					}
				}
				else if (item is Accessory accessory)
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
		

		if (models.Count > 0)
		{
			models.First().Run();
		}
		else
		{
			Debug.WriteLine(parameter?.GetType());
		}
	});


	public List<ModelData> GetModels()
	{

		return null;
	}
}