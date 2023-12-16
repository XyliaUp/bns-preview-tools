using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

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
		[Repeat(42)]
		public Ref<Item>[] SeedItem{ get; set; }
		public Ref<ItemGraphSeedGroup> SeedItemGroup{ get; set; }
		[Repeat(42)]
		public SeedItemSubGroupSeq[] SeedItemSubGroup{ get; set; }
		public NodeTypeSeq NodeType{ get; set; }
		public AttributeGroupSeq AttributeGroup{ get; set; }
		public EquipType ItemEquipType{ get; set; }
		public GrowthCategorySeq GrowthCategory{ get; set; }
		public short Row{ get; set; }
		public short Column{ get; set; }



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
		public EdgeTypeSeq EdgeType{ get; set; }
		public AttributeGroupSeq AttributeGroup{ get; set; }
		public SeedItemSubGroupSeq SeedItemSubGroup{ get; set; }
		public Ref<Item> FeedItem{ get; set; }
		public Ref<ItemTransformRecipe> FeedRecipe{ get; set; }
		public Ref<Item> StartItem{ get; set; }
		public OrientationSeq StartOrientation{ get; set; }
		public Ref<Item> EndItem{ get; set; }
		public OrientationSeq EndOrientation{ get; set; }
		public SuccessProbabilitySeq SuccessProbability{ get; set; }
		public bool HasArrow{ get; set; }



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