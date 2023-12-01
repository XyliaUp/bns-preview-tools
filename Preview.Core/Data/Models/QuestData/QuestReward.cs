using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class QuestReward : Record
{
	public string Alias;



	public QuestFirstProgressSeq QuestFirstProgress;
	public enum QuestFirstProgressSeq
	{
		None,
		Y,
		N,
	}

	public sbyte QuestCompletionCount;
	public Op QuestCompletionCountOp;
	public int BasicMoney;
	public int BasicExp;
	public int BasicAccountExp;
	public sbyte BasicMasteryLevel;
	public short BasicProductionExp;
	public short BasicFactionReputation;
	public short BasicGuildReputation;
	public int BasicDuelPoint;
	public int BasicPartyBattlePoint;
	public int BasicFieldPlayPoint;
}