using Xylia.Extension;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class QuestReward : BaseRecord
{
	[Signal("quest-first-progress")]
	public bool QuestFirstProgress;

	public int BasicProductionExp => this.Attributes["basic-production-exp"].ToInt32();
	public int BasicFactionReputation => this.Attributes["basic-faction-reputation"].ToInt32();
	public int BasicMoney => this.Attributes["basic-money"].ToInt32();
	public int BasicExp => this.Attributes["basic-exp"].ToInt32();
	public int BasicAccountExp => this.Attributes["basic-account-exp"].ToInt32();
	public int BasicDuelPoint => this.Attributes["basic-duel-point"].ToInt32();
	public int BasicPartyBattlePoint => this.Attributes["basic-party-battle-point"].ToInt32();
	public int BasicFieldPlayPoint => this.Attributes["basic-field-play-point"].ToInt32();
}