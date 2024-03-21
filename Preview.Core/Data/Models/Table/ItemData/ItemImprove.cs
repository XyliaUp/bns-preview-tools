using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImprove : ModelElement
{
	#region Attributes
	public int Id { get; set; }

	public sbyte Level { get; set; }

	public int SuccessOptionListId { get; set; }

	public sbyte SuccessOptionSlotNumber { get; set; }
	#endregion

	#region Methods
	internal static Item GetResultItem(Item item, sbyte improvelevel)
	{
		ArgumentNullException.ThrowIfNull(item);

		if (item.ImproveLevel == improvelevel) return item;
		else if (item.ImproveLevel > improvelevel)
		{
			item = item.Attributes.Get<Record>("improve-prev-item")?.As<Item>();
			return GetResultItem(item, improvelevel);
		}
		else if (item.ImproveLevel < improvelevel)
		{
			item = item.Attributes.Get<Record>("improve-next-item")?.As<Item>();
			return GetResultItem(item, improvelevel);
		}
		else throw new NotSupportedException();
	}

	public IEnumerable<ItemRecipeHelper> CreateRecipe()
	{
		var recipes = new List<ItemRecipeHelper>();

		for (sbyte i = 1; i <= 5; i++)
		{
			var CostMainItem = Attributes.Get<Record>("cost-main-item-" + i)?.As<Item>();
			if (CostMainItem is null) continue;

			var CostMainItemCount = Attributes.Get<short>("cost-main-item-count-" + i);
			var CostSubItem = LinqExtensions.For(8, (id) => Attributes.Get<Record>($"cost-sub-item-{i}-{id}")?.As<Item>());
			var CostSubItemCount = LinqExtensions.For(8, (id) => Attributes.Get<short>($"cost-sub-item-count-{i}-{id}"));
			var CostMoney = Attributes.Get<int>("cost-money-" + i);
			var SuccessProbability = Attributes.Get<short>("success-probability-" + i);
			var UseSuccessProbability = Attributes.Get<BnsBoolean>("use-success-probability-" + i);
			var FailLevelDiff = Attributes.Get<sbyte>("fail-level-diff-" + i);

			string Warning = !UseSuccessProbability ? null :
				FailLevelDiff > 0 ? "UI.ItemGrowth.Warning.Improvement.FailProbabilityWithLevelDown" :
				"UI.ItemGrowth.Warning.Improvement.FailProbabilityWithoutLevelDown";

			recipes.Add(new ItemRecipeHelper()
			{
				MainItem = CostMainItem,
				MainItemCount = CostMainItemCount,
				SubItem = CostSubItem,
				SubItemCount = CostSubItemCount,
				Money = CostMoney,
				SuccessProbability = UseSuccessProbability ? SuccessProbability : (short)1000,

				Guide = Warning.GetText(),
			});
		}

		return recipes;
	}
	#endregion
}