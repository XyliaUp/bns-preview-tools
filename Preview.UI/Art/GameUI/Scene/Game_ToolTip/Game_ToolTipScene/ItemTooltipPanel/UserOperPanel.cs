using System.Xml;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Definition;
using Xylia.Preview.GameUI.Scene.ItemGrowth.Scene;

using static Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.DataGridScene;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
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
			InitializeComponent();

			this.MyParentForm = ParentForm;
			this.EquipmentGuideScene = new(MyParentForm.ItemInfo);

			#region 创建操作按钮
			var OperBtns = new List<Control>();

			if (this.MoreInformation) OperBtns.Add(this.pictureBox2);
			if (this.EquipmentGuideScene.Pages.Count > 0) OperBtns.Add(this.pictureBox1);

			BtnCount = OperBtns.Count;
			#endregion

			#region 设置位置
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
			var ItemData = MyParentForm?.ItemInfo;
			if (ItemData is null) return;

			new DataGridScene(ParamTable, ItemData.Attributes) { Text = $"查看字段 {ItemData.Name2}" }
			   .MyShowDialog();
		}


		private static ParamTable m_ParamTable;

		public static ParamTable ParamTable
		{
			get
			{
				if (m_ParamTable is null)
				{
					m_ParamTable = new ParamTable();

					var res = "Xylia.Preview.Data.Records.TableDef.ItemData.xml".GetResource();
					if (res is null) return null;

					var doc = new XmlDocument();
					doc.LoadXml(res);
					foreach (XmlNode test in doc.SelectNodes("//list//record[@alias!=''][@name!='']"))
					{
						string Alias = test.Attributes["alias"]?.Value;
						string Name = test.Attributes["name"]?.Value;

						m_ParamTable.ParamDef[Alias] = Name;
					}
				}

				return m_ParamTable;
			}
		}
		#endregion

		#region 查看装备管理
		private readonly EquipmentGuideScene EquipmentGuideScene;

		private void pictureBox1_Click(object sender, EventArgs e) => Execute.MyShowDialog(this.EquipmentGuideScene);
		#endregion
	}
}