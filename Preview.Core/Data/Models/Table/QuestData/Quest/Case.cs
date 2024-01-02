using System.ComponentModel;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.QuestData.Enums;
using static Xylia.Preview.Data.Models.Duel;
using static Xylia.Preview.Data.Models.PartyBattleFieldZone;

using Skill3Model = Xylia.Preview.Data.Models.Skill3;
using SkillModel = Xylia.Preview.Data.Models.Skill;

namespace Xylia.Preview.Data.Models.QuestData;
public partial class Case : ModelElement
{
	#region Base
	public List<FilterSet> FilterSet { get; set; }
	public List<ReactionSet> ReactionSet { get; set; }


	[Side(ReleaseSide.Client)]
	public Indicator Indicator { get; set; }

	[Side(ReleaseSide.Client)]
	public bool ShowInTooltip { get; set; }

	[Side(ReleaseSide.Client)]
	public bool VisibleObject { get; set; }

	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> CaseTalksocial { get; set; }

	[Side(ReleaseSide.Client)]
	public float CaseTalksocialDelay { get; set; }



	[Side(ReleaseSide.Server)]
	public Ref<Zone> Zone { get; set; }

	//[Side(ReleaseSide.Server)]
	//public Ref<QuestDecision> QuestDecision { get; set; }

	//[Side(ReleaseSide.Server)]
	//public Ref<QuestDecision> FailQuestDecision { get; set; }

	[Side(ReleaseSide.Server)]
	public Ref<FieldItem> DropGadget { get; set; }

	[Side(ReleaseSide.Server)]
	public bool PartyBroadcast { get; set; }

	[Side(ReleaseSide.Server)]
	public bool TeamBroadcast { get; set; }


	public virtual List<Record> Attractions { get; }
	#endregion

	#region Sub
	public sealed class Talk : Case
	{
		public Ref<ModelElement> Object { get; set; }
	}

	public sealed class TalkToItem : Case
	{
		public Ref<Item> Item { get; set; }

		public override List<Record> Attractions => new() { Item.Instance?.Source };
	}

	public sealed class TalkToSelf : Case
	{

	}

	public sealed class Manipulate : Case
	{
		public Ref<ModelElement> Object2 { get; set; }
		public Ref<ModelElement>[] MultiObject { get; set; }


		public override List<Record> Attractions
		{
			get
			{
				var result = new List<Record>();
				result.Add(Object2.Instance?.Source);
				MultiObject.ForEach(x => result.Add(x.Instance?.Source));

				return result;
			}
		}
	}

	public sealed class NpcManipulate : Case
	{
		public Ref<ModelElement> Object { get; set; }
		public Ref<ModelElement>[] MultiObject { get; set; }

		public override List<Record> Attractions
		{
			get
			{
				var result = new List<Record>();
				MultiObject.ForEach(x => result.Add(x.Instance?.Source));

				return result;
			}
		}
	}

	public sealed class Approach : Case
	{
	}

	public sealed class Skill : Case
	{
		[Side(ReleaseSide.Client)]
		public Ref<NpcResponse> NpcResponse { get; set; }

		[Repeat(20)]
		public Ref<ModelElement>[] Object2 { get; set; }

		public Ref<SkillModel> skill { get; set; }

		public Ref<Skill3Model> skill3 { get; set; }
	}

	public sealed class Loot : Case
	{
		public Ref<ModelElement> Object2 { get; set; }
		public Ref<ModelElement>[] MultiObject { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<VirtualItem> Looting { get; set; }

		public override List<Record> Attractions
		{
			get
			{
				var result = new List<Record>();
				result.Add(Object2.Instance?.Source);
				MultiObject.ForEach(x => result.Add(x.Instance?.Source));

				return result;
			}
		}
	}

	public sealed class Killed : Case
	{
		public Ref<ModelElement> Object2 { get; set; }
		public Ref<ModelElement>[] MultiObject { get; set; }


		public override List<Record> Attractions
		{
			get
			{
				var result = new List<Record>();
				result.Add(Object2.Instance?.Source);
				MultiObject.ForEach(x => result.Add(x.Instance?.Source));

				return result;
			}
		}
	}

	public sealed class FinishBlow : Case
	{
		public Ref<Npc> Npc { get; set; }

		public override List<Record> Attractions => new() { Npc.Instance?.Source };
	}

	public sealed class EnvEntered : Case
	{
		public Ref<ModelElement> Object2 { get; set; }

		public Ref<EnvResponse> EnvResponse { get; set; }


		public override List<Record> Attractions => new() { Object2.Instance?.Source };
	}

	public sealed class EnterZone : Case
	{
		public Ref<ModelElement> Object { get; set; }

		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class ConvoyArrived : Case
	{
		public Ref<ModelElement> Object { get; set; }

		[Side(ReleaseSide.Server)]
		public Ref<ZoneConvoy> Convoy { get; set; }

		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class ConvoyFailed : Case
	{
		public Ref<ModelElement> Object { get; set; }

		public Ref<ZoneConvoy> Convoy { get; set; }


		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class NpcBleedingOccured : Case
	{
		public Ref<ModelElement> Object { get; set; }

		[Side(ReleaseSide.Server)]
		public sbyte idx { get; set; }


		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class EnterPortal : Case
	{
		public Ref<ModelElement> Object2 { get; set; }

		public override List<Record> Attractions => new() { Object2.Instance?.Source };
	}

	public sealed class AcquireSummoned : Case
	{
		public Ref<ModelElement> Object { get; set; }

		public Ref<SummonedPreset> SummonedPreset { get; set; }

		public Ref<NpcResponse> NpcResponse { get; set; }

		public Ref<Text> ButtonTextAccept { get; set; }

		public Ref<Text> ButtonTextCancel { get; set; }

		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class PcSocial : Case
	{
		public Ref<ModelElement> Object2 { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<TalkSocial> Social { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<StateSocial> StateSocial { get; set; }

		[Side(ReleaseSide.Client)]
		public Ref<NpcResponse> NpcResponse { get; set; }


		public override List<Record> Attractions => new() { Object2.Instance?.Source };
	}

	public sealed class JoinFaction : Case
	{
		public Ref<Faction> Faction { get; set; }

		public Ref<ModelElement> Object { get; set; }

		public Ref<NpcResponse>[] NpcResponse { get; set; }

		public Ref<Text> ButtonTextAccept { get; set; }

		public Ref<Text> ButtonTextCancel { get; set; }

		public Ref<Item> Grocery { get; set; }

		public short GroceryCount { get; set; }

		public bool RemoveGrocery { get; set; }


		public override List<Record> Attractions => new() { Object.Instance?.Source };
	}

	public sealed class DuelFinish : Case
	{
		public ResultSeq DuelResult { get; set; }

		[DefaultValue(All)]
		public enum ResultSeq
		{
			None,

			All,

			Win,

			Lose,
		}



		public DuelType DuelType { get; set; }

		public ArenaMatchingRuleDetail ArenaMatchingRuleDetail { get; set; }

		public int DuelStraightWin { get; set; }

		public sbyte DuelGrade { get; set; }
	}

	public sealed class PartyBattle : Case
	{
		public PartyBattleFieldZoneType PartyBattleType { get; set; }

		public DuelFinish.ResultSeq PartyBattleResult { get; set; }
	}

	public sealed class PartyBattleAction : Case
	{
		public PartyBattleFieldZoneType PartyBattleType { get; set; }

		public PartyBattleActionTypeSeq PartyBattleActionType { get; set; }

		public enum PartyBattleActionTypeSeq
		{
			None,

			Occupy,
		}
	}

	public sealed class completeQuest : Case
	{
		public Ref<Quest> CompleteQuest { get; set; }
	}

	public sealed class PickUpFielditem : Case
	{
		public Ref<FieldItem> Fielditem { get; set; }
	}

	public sealed class BattleRoyal : Case
	{
		public Ref<BattleRoyalField> BattleRoyalField { get; set; }
	}

	public sealed class AttractionPopup : Case
	{
		public Ref<ZoneEnv2> AttractionPopupEnv { get; set; }
	}

	public sealed class PublicRaid : Case
	{

	}
	#endregion
}