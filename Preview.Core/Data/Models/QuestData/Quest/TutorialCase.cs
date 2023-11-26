using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Models.QuestData.Enums;

using static Xylia.Preview.Data.Models.Decision;
using static Xylia.Preview.Data.Models.Item;

using Skill3Model = Xylia.Preview.Data.Models.Skill3;
using SkillModel = Xylia.Preview.Data.Models.Skill;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models.QuestData;
public partial class TutorialCase : Record
{
	#region Base
	public List<FilterSet> FilterSet { get; set; }
	public List<ReactionSet> ReactionSet { get; set; }



	public sbyte Prob;

	[Repeat(10)]
	public Ref<MapUnit>[] MapUnit;

	public short RangeMax;

	public short RangeMin;

	public ProgressMission ProgressMission = ProgressMission.N;

	[Repeat(2)]
	public Ref<Zone>[] ValidZone;

	[Side(ReleaseSide.Server)]
	public Ref<Zone> Zone;

	[Side(ReleaseSide.Server)]
	public Ref<QuestDecision> QuestDecision;


	public virtual List<Ref<Record>> Attractions { get; }
	#endregion

	#region Sub
	public sealed class AcquireItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item;
	}

	public sealed class EquipItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public GameCategory2Seq ItemCategory;

		[Side(ReleaseSide.Client)]
		public Ref<Item> Item;
	}

	public sealed class UseItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item;
	}

	public sealed class GrowItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> MaterialItem;

		[Side(ReleaseSide.Client)]
		public Ref<Item> PrimaryItem;
	}

	public sealed class TransformItem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<ItemTransformRecipe> ItemTransformRecipe;
	}

	public sealed class PickUpFielditem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<FieldItem> Fielditem;
	}

	public sealed class PickDownFielditem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<FieldItem> Fielditem;
	}

	public sealed class Targeting : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Npc> Npc;
	}

	public sealed class TalkStart : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Npc> Npc;
	}

	public sealed class WindowOpen : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public WindowTypeSeq WindowType;

		[Side(ReleaseSide.Client)]
		public WindowOpenWaySeq WindowOpenWay;


		public enum WindowTypeSeq
		{
			Inverntory,

			[Name("quest-journal")]
			QuestJournal,

			Skill,

			Sandbox,

			Auction,

			[Name("cash-shop")]
			CashShop,

			Wardrobe,

			[Name("account-contents")]
			AccountContents,
		}

		public enum WindowOpenWaySeq
		{
			None,

			[Name("by-npc-seller-button")]
			ByNpcSellerButton,
		}
	}

	public sealed class CompleteSelfRevival : TutorialCase
	{

	}

	public sealed class NpcBleeding : TutorialCase
	{
		public Ref<Npc> Npc;

		public sbyte Percent;
	}

	public sealed class PcBleeding : TutorialCase
	{
		public sbyte Percent;
	}

	public sealed class Exhausted : TutorialCase
	{

	}

	public sealed class Attacked : TutorialCase
	{

	}

	public sealed class AcquireSp : TutorialCase
	{
		public sbyte Sp;
	}


	public sealed class Skill : TutorialCase
	{
		[Repeat(16)]
		public Ref<Record>[] Object2;

		public SkillCheckTypeSeq SkillCheckType;
		public enum SkillCheckTypeSeq
		{
			SkillKey,
			SkillId,
		}

		public Ref<SkillModel> skill;

		public Ref<Skill3Model> Skill3;

		public int Skill3Id;

		public Ref<Effect> TargetRequiredEffect;

		public sbyte TargetEffectCount;
	}

	public sealed class skillSequence : TutorialCase
	{
		[Repeat(16)]
		public Ref<Record>[] Object2;

		public Ref<TutorialSkillSequence> SkillSequence;

		[Deprecated]
		public bool CheckParentSkill;
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
		public Ref<Item> Item;
	}

	public sealed class Teleport : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item;
	}

	public sealed class ExpandInventory : TutorialCase
	{

	}

	public sealed class GemCompose : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> PrimaryItem;

		[Side(ReleaseSide.Client)]
		public Ref<Item> MaterialItem;
	}

	public sealed class GemDecompose : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Item;
	}

	public sealed class WeaponGem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Weapon;

		[Side(ReleaseSide.Client)]
		public Ref<Item> Gem;
	}

	public sealed class DetachWeaponGem : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Item> Weapon;

		[Side(ReleaseSide.Client)]
		public Ref<Item> Gem;
	}

	public sealed class Airdash : TutorialCase
	{
		[Side(ReleaseSide.Client)]
		public Ref<Record> Object2;

		[Side(ReleaseSide.Client)]
		public Ref<EnvResponse> EnvResponse;


		public override List<Ref<Record>> Attractions => new() { Object2 };
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
		public Ref<Npc> LinkNpc;

		[Side(ReleaseSide.Client)]
		public float LocationX;

		[Side(ReleaseSide.Client)]
		public float LocationY;

		[Side(ReleaseSide.Client)]
		public float ApproachRange;
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
		public Ref<Teleport> TeleportID;
	}
	#endregion
}