using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class ItemGraph : ModelElement
{
	#region Sequence
	public enum SeedItemSubGroupSeq
	{
		SubGroup1,
		SubGroup2,
	}

	public enum AttributeGroupSeq
	{
		None,
		AttributeGroup1,
		AttributeGroup2,
	}

	public enum EdgeTypeSeq
	{
		Growth,

		Awaken,

		Transform,

		JumpTransform,

		Purification,
	}

	public enum OrientationSeq
	{
		Horizontal,
		Vertical,
	}

	public enum SuccessProbabilitySeq
	{
		Definite,
		Stochastic,
	}
	#endregion

	#region Sub
	public sealed class Seed : ItemGraph
	{
		public Ref<Item>[] SeedItem { get; set; }
		public Ref<ItemGraphSeedGroup> SeedItemGroup { get; set; }
		public SeedItemSubGroupSeq[] SeedItemSubGroup { get; set; }
		public NodeTypeSeq NodeType { get; set; }
		public AttributeGroupSeq AttributeGroup { get; set; }
		public EquipType ItemEquipType { get; set; }
		public GrowthCategorySeq GrowthCategory { get; set; }
		public short Row { get; set; }
		public short Column { get; set; }
		public bool UseImprove { get; set; }
		public Ref<Item> ImproveSuccessionSeed { get; set; }

		public enum NodeTypeSeq
		{
			SeedNormal,
			SeedBlackSky,
		}

		public enum GrowthCategorySeq
		{
			None,
			Dungeon,
			Raid,
			Pvp,
			Attribute,
			Etc1,
			Etc2,
		}

		public void CreateEdgeHelper(GameDataTable<ItemGraph> table)
		{
			if (this.UseImprove)
			{
				var item = this.SeedItem.FirstOrDefault().Instance;
				if (item is null) return;

				var Improve = FileCache.Data.Get<ItemImprove>().FirstOrDefault(x => x.Id == item.ImproveId && x.Level == item.ImproveLevel);
				if (Improve != null)
				{
					var NextItem = item.Attributes.Get<Record>("improve-next-item");
					foreach (var recipe in Improve.CreateRecipe())
					{
						table.Elements.Add(new ItemGraph.Edge()
						{
							StartItem = item,
							EndItem = NextItem,
							SuccessProbability = recipe.SuccessProbability == 1000 ? SuccessProbabilitySeq.Definite : SuccessProbabilitySeq.Stochastic,

							Recipe = recipe
						});
					}
				}

				var Succession = ItemImproveSuccession.FindByFeed(item, ImproveSuccessionSeed);
				if (Succession != null)
				{
					foreach (var recipe in Succession.CreateRecipe(ImproveSuccessionSeed, out var NextItem))
					{
						table.Elements.Add(new ItemGraph.Edge()
						{
							StartItem = item,
							EndItem = NextItem,
							SuccessProbability = SuccessProbabilitySeq.Definite,
							StartOrientation = OrientationSeq.Horizontal,
							EndOrientation = OrientationSeq.Horizontal,

							Recipe = recipe
						});
					}
				}
			}
		}
	}

	public sealed class Edge : ItemGraph
	{
		#region Attributes
		public EdgeTypeSeq EdgeType { get; set; }
		public AttributeGroupSeq AttributeGroup { get; set; }
		public SeedItemSubGroupSeq SeedItemSubGroup { get; set; }
		public Ref<Item> FeedItem { get; set; }
		public Ref<ItemTransformRecipe> FeedRecipe { get; set; }
		public Ref<Item> StartItem { get; set; }
		public OrientationSeq StartOrientation { get; set; } = OrientationSeq.Vertical;
		public Ref<Item> EndItem { get; set; }
		public OrientationSeq EndOrientation { get; set; } = OrientationSeq.Vertical;
		public SuccessProbabilitySeq SuccessProbability { get; set; }
		public bool HasArrow { get; set; }
		#endregion

		#region Helper
		public void CreateRecipeHelper()
		{
			Recipe = FeedRecipe.Instance?.CreateRecipe();
		}

		public ItemRecipeHelper Recipe { get; internal set; }

		public string Title => $"{StartItem.Instance?.ItemName} ➠ {EndItem.Instance?.ItemName}";
		#endregion
	}
	#endregion
}


/// <summary>
/// Provide statistical growth recipe information
/// </summary>
public class ItemRecipeHelper
{
	public Item MainItem { get; set; }

	public short MainItemCount { get; set; }

	public Item[] SubItem { get; set; }

	public short[] SubItemCount { get; set; }

	public int Money { get; set; }

	/// <summary>
	/// range 0 - 1000
	/// the client does not display probability information in CN
	/// </summary>
	public short SuccessProbability { get; set; }


	#region Properies
	public static float DiscountRate = 0.2F;

	public Tuple<Item, short>[] SubItemList => LinqExtensions.Combine(SubItem, SubItemCount);

	public string Price => new Integer(Money).Money;

	public string DiscountPrice => new Integer(Money * (1 - DiscountRate)).Money;

	public string Guide { get; internal set; }
	#endregion
}