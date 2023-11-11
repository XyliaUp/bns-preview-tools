using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public class ItemGraph : Record
{
	public enum SeedItemSubGroupSeq
	{
		[Name("sub-group-1")]
		SubGroup1,

		[Name("sub-group-2")]
		SubGroup2,
	}

	public enum AttributeGroupSeq
	{
		None,

		[Name("attribute-group-1")]
		AttributeGroup1,

		[Name("attribute-group-2")]
		AttributeGroup2,
	}


	public sealed class Seed : ItemGraph
	{
		[Repeat(42)]
		public Ref<Item>[] SeedItem;
		public Ref<ItemGraphSeedGroup> SeedItemGroup;
		[Repeat(42)]
		public SeedItemSubGroupSeq[] SeedItemSubGroup;
		public NodeTypeSeq NodeType;
		public AttributeGroupSeq AttributeGroup;
		public EquipType ItemEquipType;
		public GrowthCategorySeq GrowthCategory;
		public short Row;
		public short Column;



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
		public EdgeTypeSeq EdgeType;
		public AttributeGroupSeq AttributeGroup;
		public SeedItemSubGroupSeq SeedItemSubGroup;
		public Ref<Item> FeedItem;
		public Ref<ItemTransformRecipe> FeedRecipe;
		public Ref<Item> StartItem;
		public OrientationSeq StartOrientation;
		public Ref<Item> EndItem;
		public OrientationSeq EndOrientation;
		public SuccessProbabilitySeq SuccessProbability;
		public bool HasArrow;



		public enum EdgeTypeSeq
		{
			Growth,

			Awaken,

			Transform,

			[Name("jump-transform")]
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