using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	[DesignTimeVisible(false)]
	public partial class AttributeInfoCell : UserControl
	{
		#region Constructor
		public AttributeInfoCell(ItemRandomAbilitySlot RandomAbilitySlot)
		{
			InitializeComponent();

			this.lbl_MainInfo.Text = RandomAbilitySlot.Ability.GetName(RandomAbilitySlot.ValueMin);
			this.panelContent1.Text = "最大" + RandomAbilitySlot.ValueMax;
		}
		#endregion


		private void AttributeInfoCell_Resize(object sender, EventArgs e)
		{
			this.panelContent1.Location = new Point(this.Width - this.panelContent1.Width - 5, 0);
		}
	}
}