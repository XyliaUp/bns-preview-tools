
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class FieldZone : BaseRecord, Attraction
	{
		public AttractionGroup Group;

		[Signal("attraction-quest-1")]
		public string AttractionQuest1;

		[Signal("attraction-quest-2")]
		public string AttractionQuest2;

		[Signal("attraction-quest-3")]
		public string AttractionQuest3;

		[Signal("attraction-quest-4")]
		public string AttractionQuest4;

		[Signal("attraction-quest-5")]
		public string AttractionQuest5;

		[Signal("ui-filter-attraction-quest-only")]
		public bool UiFilterAttractionQuestOnly;

		[Signal("respawn-confirm-text")]
		public Text RespawnConfirmText;

		public Text Name2;

		public Text Desc;

		[Signal("ui-text-grade")]
		public byte UiTextGrade;

		[Signal("reward-summary")]
		public AttractionRewardSummary RewardSummary;


		[Signal("guild-battle-field-zone")]
		public string GuildBattleFieldZone;

		[Signal("min-fixed-channel")]
		public int MinFixedChannel;


		#region Interface
		public string GetName() => this.Name2.GetText();

		public string GetDescribe() => this.Desc.GetText();
		#endregion
	}
}