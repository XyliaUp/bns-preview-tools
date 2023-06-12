using System.Collections.Generic;
using System.Linq;

using Xylia.Preview.Common.Interface;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class SkillTooltipPreview : PreviewControl
	{
		#region Constructor
		public SkillTooltipPreview()
		{
			InitializeComponent();

			this.ContentPanel.FontName = "00008130.UI.Label_Green03_12";
			this.JobStyleSelect.JobStyleChanged += new JobStyleSelect.JobStyleChangedHandle(e => SelectStyle(e));
		}
		#endregion

		#region Functions
		readonly Dictionary<byte, ItemCombat> ItemCombat = new();

		public override void LoadData(BaseRecord record)
		{
			#region Initialize
			var Item = record as Item;
			for (byte idx = 1; idx <= 10; idx++) ItemCombat[idx] = FileCache.Data.ItemCombat[Item.Attributes["item-combat-" + idx]];

			this.JobStyleSelect.LoadStyleIcon(Item.EquipJobCheck1);
			#endregion

			this.Visible = ItemCombat[6] != null;
			this.SelectStyle();
		}

		private void SelectStyle(JobStyleSeq JobStyle = JobStyleSeq.Advanced1)
		{
			#region Initialize
			this.ContentPanel.Text = null;

			var ItemCombat = this.ItemCombat[(byte)(1 + (byte)JobStyle)];
			if (ItemCombat is null) return;
			#endregion


			string Text = FileCache.Data.SkillModifyInfoGroup[ItemCombat.SkillModifyInfoGroup]?.ToString();
			Text = ItemCombat.ItemSkills.Where(v => v.Describe2 != null).Aggregate(Text, (sum, now) => sum + "<br/>" + now.Describe2.GetText());

			this.ContentPanel.Text = Text;
			this.Parent?.Refresh();
		}

		public override void Refresh() => this.ContentPanel.Refresh();
		#endregion
	}
}