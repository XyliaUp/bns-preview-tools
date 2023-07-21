using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class SkillTooltipPreview : PreviewControl
{
	#region Constructor
	public SkillTooltipPreview()
	{
		InitializeComponent();
		this.ContentPanel.FontName = "00008130.UI.Label_Green03_12";
	}
	#endregion

	#region Functions
	public override void LoadData(BaseRecord record)
	{
		var item = record as Item;
		if (!(this.Visible = item.ItemCombat[5] != null)) return;

		this.JobStyleSelect.LoadStyleIcon(item.EquipJobCheck1);
		this.JobStyleSelect.JobStyleChanged += new((JobStyle) =>
		{
			this.ContentPanel.Text = null;

			var ItemCombat = item.ItemCombat[(byte)JobStyle];
			if (ItemCombat is null) return;

			this.ContentPanel.Text = ItemCombat.ToString();
			this.Parent?.Refresh();
		});
		this.JobStyleSelect.SelectDefault();
	}

	public override void Refresh() => this.ContentPanel.Refresh();
	#endregion
}