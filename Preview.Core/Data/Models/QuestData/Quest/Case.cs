using System.ComponentModel;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models.QuestData.Enums;

using static Xylia.Preview.Data.Models.Decision;
using static Xylia.Preview.Data.Models.Duel;
using static Xylia.Preview.Data.Models.PartyBattleFieldZone;

using Skill3Model = Xylia.Preview.Data.Models.Skill3;
using SkillModel = Xylia.Preview.Data.Models.Skill;

namespace Xylia.Preview.Data.Models.QuestData;
public partial class Case : Record
{
	#region Base
	public List<FilterSet> FilterSet { get; set; }
	public List<ReactionSet> ReactionSet { get; set; }


	public sbyte Prob;

	[Repeat(10), Side(ReleaseSide.Client)]
	public Ref<MapUnit>[] MapUnit;

	public short RangeMax;

	public short RangeMin;

	public ProgressMission ProgressMission = ProgressMission.N;

	[Name("progress-value")]
	public int ProgressValue;

	[Name("acquire-quest")]
	public bool AcquireQuest;

	[Name("gadget-required")]
	public GadgetRequired GadgetRequired;

	public Ref<FieldItem> Gadget;

	[Side(ReleaseSide.Client)]
	public string UnloadMapNavigationObject;

	[Repeat(2)]
	public Ref<Zone>[] ValidZone;

	[Name("completion-count")]
	public sbyte CompletionCount;

	[Name("completion-count-op")]
	public Op CompletionCountOp = Op.ge;

	[Side(ReleaseSide.Client)]
	public Indicator Indicator;

	[Side(ReleaseSide.Client)]
	public bool ShowInTooltip;

	[Side(ReleaseSide.Client)]
	public bool VisibleObject;

	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> CaseTalksocial;

	[Side(ReleaseSide.Client)]
	public float CaseTalksocialDelay;



	[Side(ReleaseSide.Server)]
	public Ref<Zone> Zone;

	[Side(ReleaseSide.Server)]
	public Ref<QuestDecision> QuestDecision;

	[Side(ReleaseSide.Server)]
	public Ref<QuestDecision> FailQuestDecision;

	[Side(ReleaseSide.Server)]
	public Ref<FieldItem> DropGadget;

	[Side(ReleaseSide.Server)]
	public bool PartyBroadcast;

	[Side(ReleaseSide.Server)]
	public bool TeamBroadcast;


	public virtual List<Ref<Record>> Attractions { get; }
	#endregion

	#region Sub
	public sealed class Talk : Case
	{
		public Ref<Record> Object;

		[Repeat(2), Obsolete]
		public int[] ConvoyMember;

		[Side(ReleaseSide.Server)]
		public Ref<ZoneConvoy> Convoy;


		public Ref<NpcResponse> NpcResponse;

		public Ref<NpcTalkMessage> Msg;

		public Ref<NpcTalkMessage> StartMsg;

		public Ref<IndicatorSocial> IndicatorSocial;

		public Ref<IndicatorIdle> IndicatorIdle;

		public Ref<Text> ButtonTextAccept;

		public Ref<Text> ButtonTextCancel;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;

		public sbyte FactionKilledCountMin;

		public sbyte FactionKilledCountMax;

		public DuelType DuelType;

		public ArenaMatchingRuleDetail ArenaMatchingRuleDetail;

		public int DuelStraightWin;

		public int RequiredMoney;       // 新版本物品消耗

		public DepotType RequiredItemDepot;

		[Repeat(4)]
		public Ref<Item>[] RequiredItem;

		[Repeat(4)]
		public short[] RequiredItemCount;

		public bool RequiredItemLoss;

		[Repeat(2)]
		public Ref<ItemBrand>[] RequiredItemBrand;

		public Ref<SetItem> RequiredEquipGemSet;

		public bool CheckInventoryFull;

		public sbyte CheckEquipedDurabilityBelow;

		public sbyte CheckExpBoostNormalBelow;
	}

	public sealed class TalkToItem : Case
	{
		public Ref<Item> Item;

		public Ref<NpcResponse> NpcResponse;

		public Ref<NpcTalkMessage> Msg;

		public Ref<NpcTalkMessage> StartMsg;

		public Ref<IndicatorSocial> IndicatorSocial;

		public Ref<IndicatorIdle> IndicatorIdle;

		public Ref<Text> ButtonTextAccept;

		public Ref<Text> ButtonTextCancel;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;

		public int RequiredMoney;

		public DepotType RequiredItemDepot;

		[Repeat(4)]
		public Ref<Item>[] RequiredItem;

		[Repeat(4)]
		public short[] RequiredItemCount;

		public bool RequiredItemLoss;

		[Repeat(2)]
		public Ref<ItemBrand>[] RequiredItemBrand;

		public Ref<SetItem> RequiredEquipGemSet;

		public bool CheckInventoryFull;

		public sbyte CheckEquipedDurabilityBelow;

		public sbyte CheckExpBoostNormalBelow;

		public override List<Ref<Record>> Attractions => new() { new Ref<Record>("item:" + Item)  };
	}

	public sealed class TalkToSelf : Case
	{
		public Ref<VirtualItem> Item;

		public Ref<NpcTalkMessage> Msg;

		public sbyte FactionKilledCountMin;

		public sbyte FactionKilledCountMax;

		[Obsolete]
		public bool Fee;

		public int RequiredMoney;   

		public DepotType RequiredItemDepot;

		[Repeat(4)]
		public Ref<Item>[] RequiredItem;

		[Repeat(4)]
		public short[] RequiredItemCount;

		public bool RequiredItemLoss;

		[Repeat(2)]
		public Ref<ItemBrand>[] RequiredItemBrand;

		public Ref<SetItem> RequiredEquipGemSet;

		public bool CheckInventoryFull;

		public sbyte CheckEquipedDurabilityBelow;

		public sbyte CheckExpBoostNormalBelow;

		public JumpingCharacterState RequiredJumpingCharacterState;
	}

	public sealed class Manipulate : Case
	{
		public Ref<Record> Object2;

		[Repeat(16)]
		public Ref<Record>[] MultiObject;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;

		public Ref<VirtualItem> EnvLooting;

		public sbyte FactionKilledCountMin;

		public sbyte FactionKilledCountMax;

		public Ref<Faction> JoinFaction2;

		[Side(ReleaseSide.Server)]
		public Ref<Faction> TransferFaction2;

		public int RequiredMoney;

		public DepotType RequiredItemDepot;

		[Repeat(4)]
		public Ref<Item>[] RequiredItem;

		[Repeat(4)]
		public short[] RequiredItemCount;

		public bool RequiredItemLoss;

		public int ConvoyMembers;


		public override List<Ref<Record>> Attractions
		{
			get
			{
				var result = new List<Ref<Record>>();
				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class NpcManipulate : Case
	{
		public Ref<Record> Object;

		[Repeat(16)]
		public Ref<Record>[] MultiObject;

		public Ref<NpcResponse> NpcResponse;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;

		public sbyte FactionKilledCountMin;

		public sbyte FactionKilledCountMax;

		public int RequiredMoney;       

		public DepotType RequiredItemDepot;

		[Repeat(4)]
		public Ref<Item>[] RequiredItem;

		[Repeat(4)]
		public short[] RequiredItemCount;

		public bool RequiredItemLoss;

		public int ConvoyMembers;


		public override List<Ref<Record>> Attractions => MultiObject.ToList();
	}

	public sealed class Approach : Case
	{
		public Ref<Record> Object;

		public Ref<NpcResponse> NpcResponse;

		[Side(ReleaseSide.Client)]
		public Ref<Social> ApproachSocial;

		public Ref<IndicatorSocial> IndicatorSocial;

		public Ref<IndicatorIdle> IndicatorIdle;

		[Side(ReleaseSide.Client)]
		public bool ApproachTalk;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;
	}

	public sealed class Skill : Case
	{
		[Side(ReleaseSide.Client)]
		public Ref<NpcResponse> NpcResponse;

		[Repeat(20)]
		public Ref<Record>[] Object2;

		public Ref<SkillModel> skill;

		public Ref<Skill3Model> skill3;
	}

	public sealed class Loot : Case
	{
		public Ref<Record> Object2;

		[Repeat(16)]
		public Ref<Record>[] MultiObject;

		[Side(ReleaseSide.Client)]
		public Ref<VirtualItem> Looting;

		public sbyte QuestSymbolDropProb;

		public Ref<Item> LootItem;


		public override List<Ref<Record>> Attractions
		{
			get
			{
				var result = new List<Ref<Record>>();
				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class Killed : Case
	{
		public SpecifyObjectType SpecifyObjectType;

		public Ref<Record> Object2;

		[Repeat(16)]
		public Ref<Record>[] MultiObject;

		public DifficultyTypeSeq KilledDifficultyType;

		[Repeat(8), Obsolete]
		public Ref<Skill3Model>[] Skill3;

		public override List<Ref<Record>> Attractions
		{
			get
			{
				var result = new List<Ref<Record>>();
				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class FinishBlow : Case
	{
		public Ref<Npc> Npc;

		[Repeat(20)]
		public int[] Skill3ID;

		public override List<Ref<Record>> Attractions => new() { new Ref<Record>("npc:" + Npc) };
	}

	public sealed class EnvEntered : Case
	{
		public Ref<Record> Object2;

		public Ref<EnvResponse> EnvResponse;


		public override List<Ref<Record>> Attractions => new() { Object2 };
	}

	public sealed class EnterZone : Case
	{
		public Ref<Record> Object;

		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class ConvoyArrived : Case
	{
		public Ref<Record> Object;

		[Side(ReleaseSide.Server)]
		public Ref<ZoneConvoy> Convoy;

		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class ConvoyFailed : Case
	{
		public Ref<Record> Object;

		public Ref<ZoneConvoy> Convoy;


		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class NpcBleedingOccured : Case
	{
		public Ref<Record> Object;

		[Side(ReleaseSide.Server)]
		public sbyte idx;


		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class EnterPortal : Case
	{
		public Ref<Record> Object2;

		public override List<Ref<Record>> Attractions => new() { Object2 };
	}

	public sealed class AcquireSummoned : Case
	{
		public Ref<Record> Object;

		public Ref<SummonedPreset> SummonedPreset;

		public Ref<NpcResponse> NpcResponse;

		public Ref<Text> ButtonTextAccept;

		public Ref<Text> ButtonTextCancel;

		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class PcSocial : Case
	{
		public Ref<Record> Object2;

		[Side(ReleaseSide.Client)]
		public Ref<TalkSocial> Social;

		[Side(ReleaseSide.Client)]
		public Ref<StateSocial> StateSocial;

		[Side(ReleaseSide.Client)]
		public Ref<NpcResponse> NpcResponse;


		public override List<Ref<Record>> Attractions => new() { Object2 };
	}

	public sealed class JoinFaction : Case
	{
		public Ref<Faction> Faction;

		public Ref<Record> Object;

		[Repeat(3)]
		public Ref<NpcResponse>[] NpcResponse;

		public Ref<Text> ButtonTextAccept;

		public Ref<Text> ButtonTextCancel;

		public Ref<Item> Grocery;

		public short GroceryCount;

		public bool RemoveGrocery;


		public override List<Ref<Record>> Attractions => new() { Object };
	}

	public sealed class DuelFinish : Case
	{
		public ResultSeq DuelResult;

		[DefaultValue(All)]
		public enum ResultSeq
		{
			None,

			All,

			Win,

			Lose,
		}



		public DuelType DuelType;

		public ArenaMatchingRuleDetail ArenaMatchingRuleDetail;

		public int DuelStraightWin;

		public sbyte DuelGrade;
	}

	public sealed class PartyBattle : Case
	{
		public PartyBattleFieldZoneType PartyBattleType;

		public DuelFinish.ResultSeq PartyBattleResult;
	}

	public sealed class PartyBattleAction : Case
	{
		public PartyBattleFieldZoneType PartyBattleType;

		public PartyBattleActionTypeSeq PartyBattleActionType;

		public enum PartyBattleActionTypeSeq
		{
			None,

			Occupy,
		}
	}

	public sealed class completeQuest : Case
	{
		public Ref<Quest> CompleteQuest;
	}

	public sealed class PickUpFielditem : Case
	{
		public Ref<FieldItem> Fielditem;
	}

	public sealed class BattleRoyal : Case
	{
		public Ref<BattleRoyalField> BattleRoyalField;
	}

	public sealed class AttractionPopup : Case
	{
		public Ref<ZoneEnv2> AttractionPopupEnv;
	}

	public sealed class PublicRaid : Case
	{

	}
	#endregion
}