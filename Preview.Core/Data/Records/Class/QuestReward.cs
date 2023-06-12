using Xylia.Preview.Common.Attribute;
using Xylia.Extension;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class QuestReward : BaseRecord
	{
		public bool QuestFirstProgress => this.Attributes["quest-first-progress"].ToBool();
		public int BasicProductionExp => this.Attributes["basic-production-exp"].ToInt();
		public int BasicFactionReputation => this.Attributes["basic-faction-reputation"].ToInt();
		public int BasicMoney => this.Attributes["basic-money"].ToInt();
		public int BasicExp => this.Attributes["basic-exp"].ToInt();
		public int BasicAccountExp => this.Attributes["basic-account-exp"].ToInt();
		public int BasicDuelPoint => this.Attributes["basic-duel-point"].ToInt();
		public int BasicPartyBattlePoint => this.Attributes["basic-party-battle-point"].ToInt();
		public int BasicFieldPlayPoint => this.Attributes["basic-field-play-point"].ToInt();
	}
}