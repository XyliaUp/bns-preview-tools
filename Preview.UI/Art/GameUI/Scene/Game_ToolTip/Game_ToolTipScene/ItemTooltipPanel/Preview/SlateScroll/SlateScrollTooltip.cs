using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class SlateScrollTooltip : TitlePanel
	{
		#region Constructor
		public SlateScrollTooltip() => InitializeComponent();
		#endregion

		#region Functions
		private void SlateScrollTooltip_SizeChanged(object sender, EventArgs e)
		{
			foreach (var Cell in this.Controls.OfType<SlateScrollCell>())
			{
				Cell.Width = this.Width - 12;
			}
		}
		#endregion


		#region Interface Functions
		public override void LoadData(BaseRecord record)
		{
			#region get data
			var cells = new List<SlateScrollCell>();
			foreach (var Stone in FileCache.Data.SlateScrollStone.Where(info => info.scroll == record)
				.OrderByDescending(o => o.recommend).Take(20)
				.Select(s => FileCache.Data.SlateStone[s.stone]))
			{
				#region get attr
				char SplitChar = '，';   //设置分隔符号
				string AbilityInfo = null;

				void GetAbility(AttachAbility ability, int max_ability_value)
				{
					if (ability == 0) return;
					AbilityInfo += ability.GetName(max_ability_value) + SplitChar;
				}

				GetAbility(Stone.ModifyAbility1, Stone.MaxAbilityValue1);
				GetAbility(Stone.ModifyAbility2, Stone.MaxAbilityValue2);
				GetAbility(Stone.ModifyAbility3, Stone.MaxAbilityValue3);
				GetAbility(Stone.ModifyAbility4, Stone.MaxAbilityValue4);

				AbilityInfo = AbilityInfo?.RemoveSuffixString(SplitChar);
				#endregion

				cells.Add(new SlateScrollCell()
				{
					Icon = Stone.Icon.GetIcon(),
					Text = Stone.Name.GetText(),
					Grade = Stone.Grade,

					AbilityInfo = AbilityInfo,
				});
			}
			#endregion

			#region UI
			int LoY = 21 + 8;
			foreach (var Cell in cells.OrderByDescending(o => o.Grade))
			{
				Cell.Location = new Point(0, LoY - 8);
				Cell.Width = this.Width - 12;
				this.Controls.Add(Cell);

				Cell.BringToFront();
				LoY = Cell.Bottom;
			}

			this.Guide.BringToFront();
			this.Guide.Location = new Point(this.Guide.Location.X, LoY);
			this.Height = this.Guide.Bottom;
			#endregion
		}
		#endregion
	}
}
