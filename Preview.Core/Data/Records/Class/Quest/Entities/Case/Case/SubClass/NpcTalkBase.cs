using Xylia.Preview.Common.Attribute;

using static Xylia.Preview.Data.Record.QuestData.Case.DuelFinish;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public abstract class NpcTalkBase : CaseBase
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
		public string ButtonTextAccept;

		[Side(ReleaseSide.Client)]
		[Signal("button-text-cancel")]
		public string ButtonTextCancel;


		[Signal("faction-killed-count-min")]
		public byte FactonKilledCountMin;

		[Signal("faction-killed-count-max")]
		public byte FactonKilledCountMax;

		[Signal("duel-type")]
		public DuelTypeSeq DuelType;

		[Signal("arena-matching-rule-detail")]
		public ArenaMatchingRule ArenaMatchingRuleDetail;

		[Signal("duel-straight-win")]
		public int DuelStraightWin;
	}
}