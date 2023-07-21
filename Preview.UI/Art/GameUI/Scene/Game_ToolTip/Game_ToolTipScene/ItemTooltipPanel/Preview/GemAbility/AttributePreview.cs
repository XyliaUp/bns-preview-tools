using Xylia.Extension;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
    public partial class AttributePreview : PreviewControl
	{
		#region Constructor
		public AttributePreview() => InitializeComponent();
		#endregion


		#region Interface Functions
		public override void LoadData(BaseRecord record)
		{
			#region Load Property
			var MainAbilityFixed = FileCache.Data.ItemRandomAbilitySlot[record.Attributes["main-ability-fixed"]];
			var SubAbilityFixed = FileCache.Data.ItemRandomAbilitySlot[record.Attributes["sub-ability-fixed"]];

			var SubAbilityRandomCount = record.Attributes["sub-ability-random-count"].ToByte();
			var SubAbilityRandoms = new List<ItemRandomAbilitySlot>();
			for (int i = 1; i <= 5; i++)
			{
				if (record.Contains("sub-ability-random-" + i, out string SubAbilityRandom))
					SubAbilityRandoms.AddItem(FileCache.Data.ItemRandomAbilitySlot[SubAbilityRandom]);
			}
			#endregion


			#region UI
			int NextTop = 0;
			void AddAbilitySlot(ItemRandomAbilitySlot slot)
			{
				if (slot is null) return;
				AddControl(new AttributeInfoCell(slot));
			}
			void AddControl(Control c)
			{
				c.Location = new Point(0, NextTop);
				this.Controls.Add(c);

				NextTop = c.Bottom;
			}

			AddAbilitySlot(MainAbilityFixed);
			AddAbilitySlot(SubAbilityFixed);
			if (SubAbilityRandomCount > 0)
			{
				AddControl(new ContentPanel($"从以下属性中随机获得{SubAbilityRandomCount}个"));

				SubAbilityRandoms.ForEach(AddAbilitySlot);
			}


			this.Height = NextTop;
			this.Visible = NextTop != 0;
			#endregion
		}

		private void AttributePreview_Resize(object sender, EventArgs e)
		{
			foreach (var c in this.Controls.OfType<AttributeInfoCell>()) 
				c.Width = this.Width;
		}
		#endregion
	}
}