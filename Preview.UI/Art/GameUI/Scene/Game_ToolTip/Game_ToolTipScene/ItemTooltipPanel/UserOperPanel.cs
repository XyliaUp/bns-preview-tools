using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;

using Xylia.Preview.Art.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.ItemGrowth.Scene;
using Xylia.Preview.UI.Custom;
using Xylia.Preview.UI.FModel.Views;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class UserOperPanel : Form
{
	#region Fields
	public ItemTooltipPanel MyParentForm;

	/// <summary>
	/// 显示更多信息
	/// </summary>
	public bool MoreInformation = true;

	public int BtnCount = 0;
	#endregion


	#region Constructor
	public UserOperPanel(ItemTooltipPanel ParentForm)
	{
		ArgumentNullException.ThrowIfNull(ParentForm);
		ArgumentNullException.ThrowIfNull(ParentForm.ItemInfo);


		InitializeComponent();

		this.MyParentForm = ParentForm;
		this.EquipmentGuideScene = new(MyParentForm.ItemInfo);

		#region Buttons
		var OperBtns = new List<Control>();

		if (this.MoreInformation) OperBtns.Add(this.pictureBox2);
		if (this.EquipmentGuideScene.Pages.Count > 0) OperBtns.Add(this.EquipmentGuide);
		if (true) OperBtns.Add(this.ModelViwer);

		BtnCount = OperBtns.Count;
		#endregion

		#region UI
		int LocY = 7;
		foreach (Control OperBtn in OperBtns)
		{
			OperBtn.Visible = true;
			OperBtn.Location = new Point(8, LocY);

			LocY = OperBtn.Bottom + 8;
		}

		this.Height = LocY - (8 - 7);
		#endregion
	}
	#endregion

	#region Functions
	private void UserOperScene_Load(object sender, EventArgs e)
	{
		this.Width = 50;
	}

	private void UserOperPanel_VisibleChanged(object sender, EventArgs e)
	{
		if (!this.Visible) return;

		var ScreenPoint = this.MyParentForm.PointToScreen(new Point(0, 0));
		this.Left = ScreenPoint.X - this.Width;
		this.Top = ScreenPoint.Y;
	}
	#endregion


	#region View Fields
	private void pictureBox2_Click(object sender, EventArgs e)
	{
		var data = MyParentForm.ItemInfo;

		var grid = new DataGridScene(data.Attributes);
		grid.Text += " " + data.Name2;
		Task.Run(() => grid.ShowDialog());
	}
	#endregion

	#region Equipment Guide
	private readonly EquipmentGuideScene EquipmentGuideScene;

	private void EquipmentGuide_Click(object sender, EventArgs e) => Task.Run(() => this.EquipmentGuideScene.ShowDialog());
	#endregion




	#region ModelViwer
	private void ModelViwer_Click(object sender, EventArgs e)
	{
		var item = MyParentForm.ItemInfo;

		#region Model
		ModelData Model(string Mesh, string Col = null)
		{
			Col ??= Mesh + "-col";

			return new ModelData()
			{
				Name = Mesh,
				Export = FileCache.PakData.LoadObject<UObject>(item.Attributes[Mesh]),
				cols = new string[] { item.Attributes[Col + "-1"], item.Attributes[Col + "-3"], item.Attributes[Col + "-3"] },
			};
		}
		#endregion



		if (item.Contains("mesh-id", out var MeshID))
		{
			//"mesh-id"
			//"mesh-id-2"
			//"mesh-col-1"
			//"mesh-col-2"
			//"mesh-col-3"
			//"mesh-animset"
			//"mesh-attach"
			//"mesh-animtree"

			//"talk-mesh"
			//"talk-animset"

			Model("mesh-id", "mesh-col").Run();
		}
		else
		{
			List<ModelData> Models = new()
			{
				Model("kun-mesh"),
				Model("gon-male-mesh"),
				Model("gon-female-mesh"),
				Model("lyn-male-mesh"),
				Model("lyn-female-mesh"),
				Model("jin-male-mesh"),
				Model("jin-female-mesh"),
				Model("cat-mesh"),
			};

			var _models = Models.Where(model => model.Export != null).ToList();
			if (_models.Any())
			{
				MyTest.ModelViewer.Models = _models;
				MyTest.ModelViewer.TryLoadExport(default);
				MyTest.ModelViewer.Run();
			}
			else if (item is Weapon weapon)
			{
				if (item.Contains("pet", out var pet))
				{
					var Pet = FileCache.Data.Pet[pet];
					if (Pet != null)
					{
						new ModelData()
						{
							Export = FileCache.PakData.LoadObject<UObject>(Pet.MeshName.Path),
							cols = Pet.MaterialName.Select(o => o.Path)
						}.Run();
					}
				}

				if (item.Contains("equip-show", out var equipshow))
				{
					//var EquipShow = FileCache.Pakitem.LoadObject<UShowObject>(equipshow);
				}
			}
			else if (item is Accessory accessory)
			{
				if (item.Contains("vehicle-detail", out var VehicleDetail))
				{
					var Vehicle = FileCache.Data.Vehicle[VehicleDetail];
					var Appearance = Vehicle.Appearance;
					if (Vehicle != null && Appearance != null)
					{
						new ModelData()
						{
							Export = FileCache.PakData.LoadObject<UObject>(Appearance.MeshName.Path),
							AnimSet = FileCache.PakData.LoadObject<UAnimSet>(Appearance.AnimSetName.Path),
							cols = Appearance.MaterialName.Select(o => o.Path),
						}.Run();
					}
				}
			}
		}


		// TODO: move to load
		if (MyTest.ModelViewer.Renderer.Options.Models.Count == 0)
			ModelViwer.Visible = false;
	}
	#endregion
}