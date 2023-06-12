using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;

using static Xylia.Preview.Data.Record.ItemExchange;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class ExchangePreview : PreviewControl
	{
		#region Constructor
		public ExchangePreview()
		{
			this.InitializeComponent();
		}
		#endregion


		#region Functions
		public override void LoadData(BaseRecord record)
		{
			var Item = record as Item;

			int PosY = 0;
			var CrystallRules = new List<ItemExchange>();

			#region 查询当前物品为加工目标物品 
			var CrystallFrom = LoadNormalItem(Item.alias);

			//对于加工来源, 需要限制Load 类型
			var temp = CrystallFrom.Where(rule => rule.RuleUsage == RuleUsageSeq.Crystallization);
			CrystallRules.AddRange(temp);

			if (temp.Any())
			{
				this.ProcessMaterialTitle.Visible = true;
				this.ProcessMaterialTitle.Location = new Point(0, PosY);
				PosY = this.ProcessMaterialTitle.Bottom;

				foreach (var rule in temp)
				{
					void LoadObject(BaseRecord data)
					{
						ItemShowCell itemShowCell = null;

						if (data is null) return;
						else if (data is Item info) itemShowCell = new ItemShowCell(info, false);
						else itemShowCell = new ItemShowCell();

						this.Controls.Add(itemShowCell);
						itemShowCell.Location = new Point(5, PosY);

						PosY = itemShowCell.Bottom;
					}

					LoadObject(rule.RequiredItem1?.CastObject());
					LoadObject(rule.RequiredItem2?.CastObject());
					LoadObject(rule.RequiredItem3?.CastObject());
					LoadObject(rule.RequiredItem4?.CastObject());
				}
			}
			#endregion

			#region 查询当前物品为加工起始物品
			var CrystallTo = LoadRequiredItem(Item.alias, Item.Brand);
			if (CrystallTo.Any())
			{
				CrystallRules.AddRange(CrystallTo);

				LoadCrystallTo(CrystallTo.Where(rule => rule.RuleUsage == RuleUsageSeq.Crystallization), ref PosY, "加工品");
				LoadCrystallTo(CrystallTo.Where(rule => rule.RuleUsage == RuleUsageSeq.AntiqueExchange), ref PosY, "旧物交换");
			}
			#endregion


			#region 生成兑换关系
			var HasAny = CrystallRules.Any();
			if (!HasAny)
			{
				this.Visible = false;
				return;
			}

			ProcessComparison.Location = new Point(0, PosY);
			PosY += ProcessComparison.Height + 2;

			CrystallRules.ForEach(rule =>
			{
				var CrystallizationCell = new ProcessComparisonCell(rule);

				this.Controls.Add(CrystallizationCell);
				CrystallizationCell.Location = new Point(5, PosY);

				PosY += CrystallizationCell.Height;
			});

			this.Height = PosY + 5;
			#endregion
		}

		private void LoadCrystallTo(IEnumerable<ItemExchange> Rule, ref int PosY, string Title)
		{
			//校验是否存在内容
			if (!Rule.Any()) return;


			var TitlePanel = new TitlePanel()
			{
				Title = Title,
				AutoSize = true,
				Location = new Point(0, PosY),
			};
			this.Controls.Add(TitlePanel);

			int PosY2 = 21;
			foreach (var rule in Rule)
			{
				int PosX2 = 5;

				void LoadObject(Item data)
				{
					if (data is null) return;

					var itemShowCell = new ItemShowCell(data, false);
					itemShowCell.Location = new Point(PosX2, PosY2);

					TitlePanel.Controls.Add(itemShowCell);


					PosX2 = itemShowCell.Right + 10;
				}

				LoadObject(FileCache.Data.Item[rule.NormalItem1]);
				LoadObject(FileCache.Data.Item[rule.NormalItem2]);
				LoadObject(FileCache.Data.Item[rule.NormalItem3]);
				LoadObject(FileCache.Data.Item[rule.NormalItem4]);

				PosY2 += 56;
			}

			PosY = TitlePanel.Bottom;
		}
		#endregion
	}
}