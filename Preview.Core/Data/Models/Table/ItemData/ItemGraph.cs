using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.ItemGraph;
using static Xylia.Preview.Data.Models.ItemGraph.Edge;

namespace Xylia.Preview.Data.Models;
public class ItemGraph : ModelElement
{
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
	}

	public sealed class Edge : ItemGraph
	{
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

		internal ItemRecipeHelper Recipe;


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
	}
}


public class ItemGraphRouteHelper
{
	public Edge[] Edges;

	public ItemGraphRouteHelper(Edge[] route)
	{
		Edges = route;
	}


	public override string ToString() => Edges.Aggregate("", (a, n) => n.StartItem.Instance.ItemNameOnly + " -> " + a);



	public IReadOnlyDictionary<Item, int> Ingredients
	{
		get
		{
			var items = new Dictionary<Item, int>();
			void AddItem(Item item, int count)
			{
				if (item is null) return;

				items.TryAdd(item, 0);
				items[item] += count;
			}


			foreach (var edge in Edges)
			{
				var recipe = edge.FeedRecipe.Instance;
				if (recipe != null)
				{
					recipe.FixedIngredient.ForEach(x => x.Instance, (item, i) => AddItem(item, recipe.FixedIngredientStackCount[i]));

					recipe.SubIngredient.ForEach(x => x.Instance, (ingredient, i) =>
					{
						if (ingredient is Item item) AddItem(item, recipe.SubIngredientStackCount[i]);
					});
				}

				var recipe2 = edge.Recipe;
				if (recipe2 != null)
				{
					recipe2.SubItem?.ForEach(x => x, (x, i) => AddItem(x, recipe2.SubItemCount[i]));
				}
			}

			return items;
		}
	}


	public static void CreateEdge(Item item, GameDataTable<ItemGraph> table)
	{
		var Improve = FileCache.Data.Get<ItemImprove>().FirstOrDefault(x => x.Id == item.ImproveId && x.Level == item.ImproveLevel);
		var ImproveSuccession = FileCache.Data.Get<ItemImproveSuccession>().FirstOrDefault(x => x.FeedMainImproveId == item.ImproveId && x.FeedMainImproveLevel == item.ImproveLevel);

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

		if (ImproveSuccession != null)
		{
			foreach (var recipe in ImproveSuccession.CreateRecipe(out var NextItem))
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

public class ItemRecipeHelper
{
	public Item MainItem { get; set; }

	public short MainItemCount { get; set; }

	public Item[] SubItem { get; set; }

	public short[] SubItemCount { get; set; }

	public int Money { get; set; }

	public short SuccessProbability { get; set; }
}