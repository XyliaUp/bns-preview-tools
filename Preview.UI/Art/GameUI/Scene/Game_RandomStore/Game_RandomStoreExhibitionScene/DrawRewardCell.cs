using System.ComponentModel;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_RandomStore;

[DesignTimeVisible(false)]
public partial class DrawRewardCell : UserControl
{
	public DrawRewardCell() => InitializeComponent();

	public void LoadData(RandomStoreDrawReward record)
	{
		#region Load Data
		this.label1.Text = record.RequiredDrawCount + "次";

		List<ItemIconCell> FixedRewards = new();
		List<ItemIconCell> OptionalRewards = new();
		for (int idx = 1; idx <= 8; idx++)
		{
			var FixedReward = GetRewardItem(record.Attributes["fixed-reward-" + idx], record.Attributes["fixed-reward-count-" + idx]);
			if (FixedReward != null) FixedRewards.Add(FixedReward);

			var OptionalReward = GetRewardItem(record.Attributes["optional-reward-" + idx], record.Attributes["optional-reward-count-" + idx]);
			if (OptionalReward != null) OptionalRewards.Add(OptionalReward);
		}
		#endregion

		#region UI
		const int StartLocX = 85;
		int LocX = StartLocX, LocY = 0;
		int Padding = 7;

		if (FixedRewards.Any())
		{
			foreach (var o in FixedRewards)
			{
				o.Location = new Point(LocX, LocY);
				LocX = o.Right + Padding;
			}

			LocY += 65;
		}

		if (OptionalRewards.Any())
		{
			OptionTitle.Visible = true;
			OptionTitle.Location = new Point(StartLocX, LocY);

			for (int i = 0; i < OptionalRewards.Count; i++)
			{
				if (i % 4 == 0)
				{
					LocX = StartLocX;
					LocY = i == 0 ? OptionTitle.Bottom + 5 : LocY + 65;
				}

				var o = OptionalRewards[i];
				o.Location = new Point(LocX, LocY);
				LocX = o.Right + Padding;

			}
		}
		#endregion
	}

	private ItemIconCell GetRewardItem(string Item, string Count)
	{
		var ItemInfo = FileCache.Data.Item[Item];
		if (ItemInfo is null) return null;

		byte.TryParse(Count, out byte _count);


		var ItemIconCell = new ItemIconCell()
		{
			StackCount = _count,
			ObjectRef = ItemInfo,
			Image = ItemInfo?.IconExtra(),

			ShowStackCount = true,
			ShowStackCountOnlyOne = false,
		};

		this.Controls.Add(ItemIconCell);
		return ItemIconCell;
	}
}