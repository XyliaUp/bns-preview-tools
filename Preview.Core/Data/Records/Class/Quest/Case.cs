using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.QuestData.Enums;

using static Xylia.Preview.Data.Record.PartyBattleFieldZone;

namespace Xylia.Preview.Data.Record.QuestData;

[Signal("case")]
public abstract partial class Case : BaseRecord
{
	#region Base
	[Signal("filter-set")]
	public List<FilterSet> FilterSet;

	[DefaultValue(100)]
	public byte Prob = 100;

	[Signal("mapunit"), Repeat(10), Side(ReleaseSide.Client)]
	public MapUnit[] MapUnit;

	[DefaultValue(-1)]
	[Signal("range-max")]
	public short RangeMax = -1;

	[DefaultValue(-1)]
	[Signal("range-min")]
	public short RangeMin = -1;

	[Signal("progress-mission")]
	public ProgressMission ProgressMission = ProgressMission.N;

	[Signal("progress-value")]
	public int ProgressValue;

	[Signal("acquire-quest")]
	public bool AcquireQuest;

	[Signal("gadget-required")]
	public GadgetRequired GadgetRequired;

	public FieldItem Gadget;

	[Side(ReleaseSide.Client)]
	[Signal("unload-map-navigation-object")]
	public string UnloadMapNavigationObject;

	[Signal("valid-zone-1")]
	public Zone ValidZone1;

	[Signal("valid-zone-2")]
	public Zone ValidZone2;

	[Signal("completion-count")]
	public byte CompletionCount;

	[Signal("completion-count-op")]
	public Op CompletionCountOp = Op.ge;

	[Side(ReleaseSide.Client)]
	public Indicator Indicator;

	[Side(ReleaseSide.Client)]
	[Signal("show-in-tooltip")]
	public bool ShowInTooltip;

	[Side(ReleaseSide.Client)]
	[Signal("visible-object")]
	public bool VisibleObject;

	[Obsolete]
	[Side(ReleaseSide.Client)]
	[Signal("case-talksocial")]
	public TalkSocial CaseTalksocial;

	[Side(ReleaseSide.Client)]
	[Signal("case-talksocial-delay")]
	public float CaseTalksocialDelay;




	// 旧版本物品消耗
	[Obsolete]
	[Signal("grocery")]
	public string Grocery;

	[Obsolete]
	[Signal("grocery-count")]
	public short GroceryCount = 1;

	[Obsolete]
	[Signal("remove-grocery")]
	public bool RemoveGrocery;

	// 新版本物品消耗
	[Signal("required-money")]
	public int RequiredMoney;

	[Signal("required-item-depot")]
	public DepotType RequiredItemDepot;

	[Signal("required-item-1")]
	public Item RequiredItem1;

	[Signal("required-item-2")]
	public Item RequiredItem2;

	[Signal("required-item-3")]
	public Item RequiredItem3;

	[Signal("required-item-4")]
	public Item RequiredItem4;

	[Signal("required-item-count-1")]
	public short RequiredItemCount1;

	[Signal("required-item-count-2")]
	public short RequiredItemCount2;

	[Signal("required-item-count-3")]
	public short RequiredItemCount3;

	[Signal("required-item-count-4")]
	public short RequiredItemCount4;

	[Signal("required-item-loss")]
	public bool RequiredItemLoss;

	[Signal("required-item-brand-1")]
	public ItemBrand RequiredItemBrand1;

	[Signal("required-item-brand-2")]
	public ItemBrand RequiredItemBrand2;

	[Signal("required-equip-gem-set")]
	public SetItem RequiredEquipGemSet;




	//public string Object;

	//public string Object2;


	[Signal("faction-killed-count-min")]
	public byte FactionKilledCountMin;

	[Signal("faction-killed-count-max")]
	public byte FactionKilledCountMax;




	[Side(ReleaseSide.Server)]
	public string Zone;

	[Side(ReleaseSide.Server)]
	[Signal("quest-decision")]
	public string QuestDecision;

	[Side(ReleaseSide.Server)]
	[Signal("fail-quest-decision")]
	public string FailQuestDecision;

	/// <summary>
	/// 掉落物品
	/// </summary>
	[Side(ReleaseSide.Server)]
	[Signal("drop-gadget")]
	public string DropGadget;

	[Side(ReleaseSide.Server)]
	[Signal("party-broadcast")]
	public bool PartyBroadcast = false;

	[Side(ReleaseSide.Server)]
	[Signal("team-broadcast")]
	public bool TeamBroadcast = false;



	//去重后返回 .Distinct().ToList()
	public virtual List<string> AttractionObject => new() { /*Object, Object2*/ };
	#endregion


	#region Abstract 
	public abstract class NpcTalkBase : Case
	{
		public string Object;

		public string Object2;


		[Side(ReleaseSide.Client)]
		[Signal("npc-response")]
		public NpcResponse NpcResponse;

		[Side(ReleaseSide.Client)]
		public NpcTalkMessage Msg;

		[Side(ReleaseSide.Client)]
		[Signal("start-msg")]
		public NpcTalkMessage StartMsg;

		[Side(ReleaseSide.Client)]
		[Signal("indicator-social")]
		public IndicatorSocial IndicatorSocial;

		[Side(ReleaseSide.Client)]
		[Signal("indicator-idle")]
		public IndicatorIdle IndicatorIdle;

		[Side(ReleaseSide.Client)]
		[Signal("button-text-accept")]
		public Text ButtonTextAccept;

		[Side(ReleaseSide.Client)]
		[Signal("button-text-cancel")]
		public Text ButtonTextCancel;

		[Signal("faction-killed-count-min")]
		public byte FactonKilledCountMin;

		[Signal("faction-killed-count-max")]
		public byte FactonKilledCountMax;

		[Signal("duel-type")]
		public Duel.DuelType DuelType;

		[Signal("arena-matching-rule-detail")]
		public ArenaMatchingRule ArenaMatchingRuleDetail;

		[Signal("duel-straight-win")]
		public int DuelStraightWin;
	}
	#endregion

	#region Sub
	public sealed class Talk : NpcTalkBase
	{
		[Obsolete]
		[Side(ReleaseSide.Server)]
		[Signal("convoy-member") , Repeat(2)]
		public int[] ConvoyMember;

		// 护送只能在 cave2 定义区域生成
		// cannot start convoy before progress mission
		// 无法在执行任务前开始护卫
		// acquisition 的子对象护卫不生效
		/// </summary>
		[Side(ReleaseSide.Server)]
		public ZoneConvoy Convoy;

		[Signal("check-inventory-full")]
		public bool CheckInventoryFull = false;

		[Signal("check-equiped-durability-below")]
		public byte CheckEquipedDurabilityBelow;

		[Signal("check-exp-boost-normal-below")]
		public byte CheckExpBoostNormalBelow;

	}

	public sealed class TalkToItem : NpcTalkBase
	{
		public Item Item;

		[Signal("check-inventory-full")]
		public bool CheckInventoryFull;

		[Signal("check-equiped-durability-below")]
		public byte CheckEquipedDurabilityBelow;

		[Signal("check-exp-boost-normal-below")]
		public byte CheckExpBoostNormalBelow;


		public override List<string> AttractionObject => new() { "item:" + Item };
	}

	public sealed class TalkToSelf : Case
	{
		[Obsolete]
		public bool Fee;

		[Side(ReleaseSide.Client)]
		public VirtualItem Item;

		[Side(ReleaseSide.Client)]
		public NpcTalkMessage Msg;


		[Signal("check-inventory-full")]
		public bool CheckInventoryFull;

		[Signal("check-equiped-durability-below")]
		public byte CheckEquipedDurabilityBelow;

		[Signal("check-exp-boost-normal-below")]
		public byte CheckExpBoostNormalBelow;

		[Signal("required-jumping-character-state")]
		public JumpingCharacterState RequiredJumpingCharacterState;
	}

	public sealed class Manipulate : Case
	{
		public string Object2;

		[Signal("multi-object"), Repeat(20)]
		public string[] MultiObject;

		[Signal("env-looting")]
		public string EnvLooting;

		[Signal("join-faction2")]
		public string JoinFaction2;

		[Side(ReleaseSide.Server)]
		[Signal("transfer-faction2")]
		public string TransferFaction2;

		public override List<string> AttractionObject
		{
			get
			{
				var result = new List<string>();

				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class NpcManipulate : NpcTalkBase
	{
		[Signal("multi-object"), Repeat(20)]
		public string[] MultiObject;

		public override List<string> AttractionObject => MultiObject.ToList();
	}

	public sealed class Approach : NpcTalkBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("approach-talk")]
		public bool ApproachTalk;

		[Side(ReleaseSide.Client)]
		[Signal("approach-social")]
		public Social ApproachSocial;
	}

	public sealed class Skill : Case
	{
		[Signal("object2"), Repeat(20)]
		public string[] Object2;

		[Side(ReleaseSide.Client)]
		[Signal("npc-response")]
		public NpcResponse NpcResponse;

		[Obsolete]
		public Record.Skill skill;

		public Skill3 Skill3;
	}

	public sealed class Loot : Case
	{
		public string Object2;

		[Signal("multi-object"), Repeat(20)]
		public string[] MultiObject;

		[Signal("quest-symbol-drop-prob")]
		public byte QuestSymbolDropProb;

		[Signal("loot-item")]
		public Item LootItem;

		[Signal("env-looting")]
		public string EnvLooting;

		[Side(ReleaseSide.Client)]
		public VirtualItem Looting;


		public override List<string> AttractionObject
		{
			get
			{
				var result = new List<string>();

				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class Killed : Case
	{
		public string Object2;

		[Signal("multi-object"), Repeat(20)]
		public string[] MultiObject;

		[Signal("specify-object-type")]
		public SpecifyObjectType SpecifyObjectType;

		[Signal("killed-difficulty-type")]
		public DifficultyType KilledDifficultyType;

		public Skill3 Skill3;


		public override List<string> AttractionObject
		{
			get
			{
				var result = new List<string>();

				result.Add(Object2);
				result.AddRange(MultiObject);

				return result;
			}
		}
	}

	public sealed class FinishBlow : Case
	{
		public Npc Npc;

		[Signal("skill3-id"), Repeat(20)]
		public int[] Skill3ID;

		public override List<string> AttractionObject => new() { "npc:" + Npc };
	}

	public sealed class EnvEntered : Case
	{
		public string Object2;

		[Signal("env-response")]
		public EnvResponse EnvResponse;


		public override List<string> AttractionObject => new() { Object2 };
	}

	public sealed class EnterZone : Case
	{
		public string Object;

		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class ConvoyArrived : Case
	{
		public string Object;

		[Side(ReleaseSide.Server)]
		public ZoneConvoy Convoy;

		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class ConvoyFailed : Case
	{
		public string Object;

		public ZoneConvoy Convoy;


		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class NpcBleedingOccured : Case
	{
		public string Object;

		[Side(ReleaseSide.Server)]
		public byte idx;


		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class EnterPortal : Case
	{
		public string Object2;


		public override List<string> AttractionObject => new() { Object2 };
	}

	public sealed class AcquireSummoned : NpcTalkBase
	{
		[Signal("summoned-preset")]
		public SummonedPreset SummonedPreset;


		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class PcSocial : Case
	{
		public string Object2;

		[Side(ReleaseSide.Client)]
		public TalkSocial Social;

		[Side(ReleaseSide.Client)]
		[Signal("state-social")]
		public StateSocial StateSocial;

		[Side(ReleaseSide.Client)]
		[Signal("npc-response")]
		public NpcResponse NpcResponse;


		public override List<string> AttractionObject => new() { Object2 };
	}

	public sealed class JoinFaction : Case
	{
		public string Object;

		public Faction Faction;

		[Signal("npc-response-1"), Repeat(3)]
		public NpcResponse[] NpcResponse;

		public override List<string> AttractionObject => new() { Object };
	}

	public sealed class DuelFinish : Case
	{
		[Signal("duel-grade")]
		public byte DuelGrade;

		[Signal("duel-result")]
		public ResultSeq DuelResult;

		[Signal("duel-type")]
		public Duel.DuelType DuelType;

		[Signal("arena-matching-rule-detail")]
		public ArenaMatchingRule ArenaMatchingRuleDetail;

		[Signal("duel-straight-win")]
		public int DuelStraightWin;
	}

	public sealed class PartyBattle : Case
	{
		[Signal("party-battle-type")]
		public PartyBattleFieldZoneType PartyBattleType;

		[Signal("party-battle-result")]
		public ResultSeq PartyBattleResult;
	}

	public sealed class PartyBattleAction : Case
	{
		[Signal("party-battle-type")]
		public PartyBattleFieldZoneType PartyBattleType;


		[Signal("party-battle-action-type")]
		public PartyBattleActionTypeSeq PartyBattleActionType;
		public enum PartyBattleActionTypeSeq
		{
			None,

			Occupy,
		}
	}

	public sealed class CompleteQuest : Case
	{
		[Signal("complete-quest")]
		public Record.Quest Complete_Quest;
	}

	public sealed class PickUpFielditem : Case
	{
		public FieldItem Fielditem;
	}

	public sealed class BattleRoyal : Case
	{
		[Signal("battle-royal-field")]
		public BattleRoyalField BattleRoyalField;
	}

	public sealed class AttractionPopup : Case
	{
		[Signal("attraction-popup-env")]
		public ZoneEnv2 AttractionPopupEnv;
	}

	public sealed class PublicRaid : Case
	{

	}
	#endregion
}