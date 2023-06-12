using System;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData
{
	public sealed class Mission : CaseParent
	{
		//max: 16
		public byte id;

		[Signal("check-tencent-vitality")]
		public bool CheckTencentVitality;

		
		/// <summary>
		/// min: 1
		/// </summary>
		[Signal("required-register-value")]
		public byte RequiredRegisterValue = 1;

		[Signal("reward-1")] 
		public QuestReward Reward1;

		[Signal("reward-2")]
		public QuestReward Reward2;

		[Signal("reset-teleport-recycle-time")]
		public bool ResetTeleportRecycleTime;

		[Signal("required-attraction")]
		public string RequiredAttraction;

		[Signal("tendency-id")]
		public byte TendencyID;

		[Signal("simple-quest-play-section")]
		public string SimpleQuestPlaySection;

		[Signal("variation-required-condition-type")] public string VariationRequiredConditionType;
		[Signal("variation-required-condition-value-1")] public int VariationRequiredConditionValue1;
		[Signal("variation-required-condition-value-2")] public int VariationRequiredConditionValue2;
		[Signal("variation-required-condition-value-3")] public int VariationRequiredConditionValue3;
		[Signal("variation-required-condition-value-4")] public int VariationRequiredConditionValue4;
		[Signal("variation-required-condition-value-5")] public int VariationRequiredConditionValue5;
		[Signal("variation-required-condition-value-6")] public int VariationRequiredConditionValue6;
		[Signal("variation-required-condition-value-7")] public int VariationRequiredConditionValue7;
		[Signal("variation-required-condition-value-8")] public int VariationRequiredConditionValue8;

		[Signal("variation-required-register-value-1")] public int VariationRequiredRegisterValue1;
		[Signal("variation-required-register-value-2")] public int VariationRequiredRegisterValue2;
		[Signal("variation-required-register-value-3")] public int VariationRequiredRegisterValue3;
		[Signal("variation-required-register-value-4")] public int VariationRequiredRegisterValue4;
		[Signal("variation-required-register-value-5")] public int VariationRequiredRegisterValue5;
		[Signal("variation-required-register-value-6")] public int VariationRequiredRegisterValue6;
		[Signal("variation-required-register-value-7")] public int VariationRequiredRegisterValue7;
		[Signal("variation-required-register-value-8")] public int VariationRequiredRegisterValue8;

		[Signal("variation-required-field-play-point-1")] public int VariationRequiredFieldPlayPoint1;
		[Signal("variation-required-field-play-point-2")] public int VariationRequiredFieldPlayPoint2;
		[Signal("variation-required-field-play-point-3")] public int VariationRequiredFieldPlayPoint3;
		[Signal("variation-required-field-play-point-4")] public int VariationRequiredFieldPlayPoint4;
		[Signal("variation-required-field-play-point-5")] public int VariationRequiredFieldPlayPoint5;
		[Signal("variation-required-field-play-point-6")] public int VariationRequiredFieldPlayPoint6;
		[Signal("variation-required-field-play-point-7")] public int VariationRequiredFieldPlayPoint7;
		[Signal("variation-required-field-play-point-8")] public int VariationRequiredFieldPlayPoint8;

		[Signal("variation-reward-account-exp-1")] public int VariationRewardAccountExp1;
		[Signal("variation-reward-account-exp-2")] public int VariationRewardAccountExp2;
		[Signal("variation-reward-account-exp-3")] public int VariationRewardAccountExp3;
		[Signal("variation-reward-account-exp-4")] public int VariationRewardAccountExp4;
		[Signal("variation-reward-account-exp-5")] public int VariationRewardAccountExp5;
		[Signal("variation-reward-account-exp-6")] public int VariationRewardAccountExp6;
		[Signal("variation-reward-account-exp-7")] public int VariationRewardAccountExp7;
		[Signal("variation-reward-account-exp-8")] public int VariationRewardAccountExp8;

		[Signal("variation-reward-faction-score-1")] public int VariationRewardFactionScore1;
		[Signal("variation-reward-faction-score-2")] public int VariationRewardFactionScore2;
		[Signal("variation-reward-faction-score-3")] public int VariationRewardFactionScore3;
		[Signal("variation-reward-faction-score-4")] public int VariationRewardFactionScore4;
		[Signal("variation-reward-faction-score-5")] public int VariationRewardFactionScore5;
		[Signal("variation-reward-faction-score-6")] public int VariationRewardFactionScore6;
		[Signal("variation-reward-faction-score-7")] public int VariationRewardFactionScore7;
		[Signal("variation-reward-faction-score-8")] public int VariationRewardFactionScore8;

		[Signal("variation-reward-field-play-point-1")] public int VariationRewardFieldPlayPoint1;
		[Signal("variation-reward-field-play-point-2")] public int VariationRewardFieldPlayPoint2;
		[Signal("variation-reward-field-play-point-3")] public int VariationRewardFieldPlayPoint3;
		[Signal("variation-reward-field-play-point-4")] public int VariationRewardFieldPlayPoint4;
		[Signal("variation-reward-field-play-point-5")] public int VariationRewardFieldPlayPoint5;
		[Signal("variation-reward-field-play-point-6")] public int VariationRewardFieldPlayPoint6;
		[Signal("variation-reward-field-play-point-7")] public int VariationRewardFieldPlayPoint7;
		[Signal("variation-reward-field-play-point-8")] public int VariationRewardFieldPlayPoint8;

		[Signal("variation-reward-tendency-score-1")] public int VariationRewardTendencyScore1;
		[Signal("variation-reward-tendency-score-2")] public int VariationRewardTendencyScore2;
		[Signal("variation-reward-tendency-score-3")] public int VariationRewardTendencyScore3;
		[Signal("variation-reward-tendency-score-4")] public int VariationRewardTendencyScore4;
		[Signal("variation-reward-tendency-score-5")] public int VariationRewardTendencyScore5;
		[Signal("variation-reward-tendency-score-6")] public int VariationRewardTendencyScore6;
		[Signal("variation-reward-tendency-score-7")] public int VariationRewardTendencyScore7;
		[Signal("variation-reward-tendency-score-8")] public int VariationRewardTendencyScore8;



		[Obsolete]
		[Side(ReleaseSide.Client)]
		public string Name;

		[Side(ReleaseSide.Client)]
		public string Name2;

		[Side(ReleaseSide.Client)]
		[Signal("show-kill-mapunit")]
		public bool ShowKillMapunit;
	}
}