using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.QuestData.Enums;

using static Xylia.Preview.Data.Models.Decision;
using static Xylia.Preview.Data.Models.Item;

using Skill3Model = Xylia.Preview.Data.Models.Skill3;
using SkillModel = Xylia.Preview.Data.Models.Skill;

namespace Xylia.Preview.Data.Models.QuestData;
public partial class TutorialCase : ModelElement
{
	#region Base
	public List<FilterSet> FilterSet { get; set; }
	public List<ReactionSet> ReactionSet { get; set; }



	public sbyte Prob { get; set; }

	[Repeat(10)]
	public Ref<MapUnit>[] MapUnit { get; set; }

	public short RangeMax { get; set; }

	public short RangeMin { get; set; }

	public ProgressMission ProgressMission { get; set; }

	[Repeat(2)]
	public Ref<Zone>[] ValidZone { get; set; }

	[Side(ReleaseSide.Server)]
	public Ref<Zone> Zone { get; set; }

	[Side(ReleaseSide.Server)]
	public Ref<QuestDecision> QuestDecision { get; set; }


	public virtual List<Ref<ModelElement>> Attractions { get; }
	#endregion

	#region Sub
	public sealed class AcquireItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class EquipItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public GameCategory2Seq ItemCategory { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class UseItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class GrowItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> MaterialItem { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<Item> PrimaryItem { get; set; }
	}

	public sealed class TransformItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<ItemTransformRecipe> ItemTransformRecipe { get; set; }
	}

	public sealed class PickUpFielditem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<FieldItem> Fielditem { get; set; }
	}

	public sealed class PickDownFielditem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<FieldItem> Fielditem { get; set; }
	}

	public sealed class Targeting : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Npc> Npc { get; set; }
	}

	public sealed class TalkStart : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Npc> Npc { get; set; }
	}

	public sealed class WindowOpen : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public WindowTypeSeq WindowType { get; set; }

		[Side(ReleaseSide.Client)]
		public WindowOpenWaySeq WindowOpenWay { get; set; }


		public enum WindowTypeSeq
		{
			Inverntory,

					QuestJournal,

			Skill,

			Sandbox,

			Auction,

					CashShop,

			Wardrobe,

					AccountContents,
		}

		public enum WindowOpenWaySeq
		{
			None,

					ByNpcSellerButton,
		}
	}

	public sealed class CompleteSelfRevival : TutorialCase
	{

	}

	public sealed class NpcBleeding : TutorialCase
	{
		public Ref<Npc> Npc { get; set; }

		public sbyte Percent { get; set; }
	}

	public sealed class PcBleeding : TutorialCase
	{
		public sbyte Percent { get; set; }
	}

	public sealed class Exhausted : TutorialCase
	{

	}

	public sealed class Attacked : TutorialCase
	{

	}

	public sealed class AcquireSp : TutorialCase
	{
		public sbyte Sp { get; set; }
	}


	public sealed class Skill : TutorialCase
	{
		[Repeat(16)]
		public Ref<ModelElement>[] Object2 { get; set; }

		public SkillCheckTypeSeq SkillCheckType { get; set; }
		public enum SkillCheckTypeSeq
		{
			SkillKey,
			SkillId,
		}

		public Ref<SkillModel> skill { get; set; }

		public Ref<Skill3Model> Skill3 { get; set; }

		public int Skill3Id { get; set; }

		public Ref<Effect> TargetRequiredEffect { get; set; }

		public sbyte TargetEffectCount { get; set; }
	}

	public sealed class skillSequence : TutorialCase
	{
		[Repeat(16)]
		public Ref<ModelElement>[] Object2 { get; set; }

		public Ref<TutorialSkillSequence> SkillSequence { get; set; }
	}

	public sealed class SkillTraining : TutorialCase
	{

	}

	public sealed class QuestTrackingPosition : TutorialCase
	{

	}

	public sealed class RepairWithCampfire : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class Teleport : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class ExpandInventory : TutorialCase
	{

	}

	public sealed class GemCompose : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> PrimaryItem { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<Item> MaterialItem { get; set; }
	}

	public sealed class GemDecompose : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item { get; set; }
	}

	public sealed class WeaponGem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Weapon { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<Item> Gem { get; set; }
	}

	public sealed class DetachWeaponGem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Weapon { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<Item> Gem { get; set; }
	}

	public sealed class Airdash : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<ModelElement> Object2 { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<EnvResponse> EnvResponse { get; set; }


		public override List<Ref<ModelElement>> Attractions => new() { Object2 };
	}

	public sealed class EnlargeMiniMap : TutorialCase
	{

	}

	public sealed class TransparentMiniMap : TutorialCase
	{

	}

	public sealed class ResurrectingSummoned : TutorialCase
	{

	}

	public sealed class MoveToPosition : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Npc> LinkNpc { get; set; }

		[Side(ReleaseSide.Client)]
		public float LocationX { get; set; }

		[Side(ReleaseSide.Client)]
		public float LocationY { get; set; }

		[Side(ReleaseSide.Client)]
		public float ApproachRange { get; set; }
	}

	public sealed class UseHeartCount : TutorialCase
	{

	}

	public sealed class ChargeHeartCount : TutorialCase
	{

	}

	public sealed class TeleportZone : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Teleport> TeleportID { get; set; }
	}
	#endregion
}