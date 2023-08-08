using System.Data;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Extension;
using Xylia.Preview.UI.Interface;

using static Xylia.Preview.Data.Record.ItemExchange;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class PieceTransformPreview : PreviewControl
{
	#region Constructor
	public PieceTransformPreview()
	{
		this.InitializeComponent();

		this.ProcessTitle.Text = "UI.PieceTransform.ProcessTitle".GetText();
		this.ProcessMaterialTitle.Text = "UI.PieceTransform.ProcessMaterialTitle".GetText();
		this.ProcessComparison.Text = "UI.PieceTransform.ProcessComparison".GetText();
	}
	#endregion


	#region Functions
	private List<ProcessComparisonCell> rules;

	public override void LoadData(BaseRecord record)
	{
		var Item = record as Item;
		var Brand = Item.Brand;


		rules = new List<ProcessComparisonCell>();
		int PosY = 0;

		#region as target
		var temp = FileCache.Data.ItemExchange.Where(record =>
		{
			if (record.NormalItem is null) return false;
			foreach (var item in record.NormalItem)
			{
				if (item == Item) return true;
			}

			return false;
		}).Where(rule => rule.RuleUsage == RuleUsageSeq.Crystallization);
		if (temp.Any())
		{
			this.ProcessMaterialTitle.Visible = true;
			this.ProcessMaterialTitle.Location = new Point(0, PosY);
			PosY = this.ProcessMaterialTitle.Bottom;

			foreach (var rule in temp)
			{
				Linq.For(4, (i) => LoadObject(rule.RequiredItem[i]));
				void LoadObject(BaseRecord data)
				{
					if (data is null) return;

					var cell = new ItemShowCell()
					{
						Location = new Point(5, PosY),
					};

					if (data is Item info)
						cell.LoadData(info);

					this.Controls.Add(cell);
					PosY = cell.Bottom;
				}

				LoadProcessComparisonCell(rule);
			}
		}
		#endregion

		#region as material
		var CrystallTo = FileCache.Data.ItemExchange.Where(record =>
		{
			if (record.RequiredItem is null) return false;
			foreach (var item in record.RequiredItem)
			{
				if (item == Item) return true;
				if (Brand != null && item == Brand) return true;
			}

			return false;
		});
		if (CrystallTo.Any())
		{
			LoadCrystallTo(CrystallTo.Where(rule => rule.RuleUsage == RuleUsageSeq.Crystallization), ref PosY, "UI.PieceTransform.ProcessMaterialTitle".GetText());
			LoadCrystallTo(CrystallTo.Where(rule => rule.RuleUsage == RuleUsageSeq.AntiqueExchange), ref PosY, "UI.ItemCleaner.Exchange".GetText());
		}
		#endregion


		#region rule
		var HasAny = rules.Any();
		if (!HasAny)
		{
			this.Visible = false;
			return;
		}

		ProcessComparison.Location = new Point(0, PosY);
		PosY += ProcessComparison.Height + 2;

		rules.ForEach(cell =>
		{
			this.Controls.Add(cell);
			cell.Location = new Point(5, PosY);

			PosY += cell.Height;
		});

		this.Height = PosY + 5;
		#endregion
	}



	private void LoadCrystallTo(IEnumerable<ItemExchange> Rule, ref int PosY, string Title)
	{
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

			Linq.For(4, (i) => LoadObject(rule.NormalItem[i]));
			void LoadObject(Item data)
			{
				if (data is null) return;

				var cell = new ItemShowCell()
				{
					Location = new Point(PosX2, PosY2),
				};


				cell.LoadData(data);

				TitlePanel.Controls.Add(cell);
				PosX2 = cell.Right + 10;
			}

			PosY2 += 56;

			LoadProcessComparisonCell(rule);
		}

		PosY = TitlePanel.Bottom;
	}


	private void LoadProcessComparisonCell(ItemExchange exchange)
	{
		var items1 = new List<ItemIconCell>();
		var items2 = new List<ItemIconCell>();
		Linq.For(4, (i) => items1.AddItem(exchange.RequiredItem[i].GetObjIcon(exchange.RequiredItemStackCount[i])));
		Linq.For(4, (i) => items2.AddItem(exchange.NormalItem[i].GetObjIcon(exchange.NormalItemStackCount[i])));

		rules.Add(new ProcessComparisonCell(items1, items2));
	}
	#endregion
}