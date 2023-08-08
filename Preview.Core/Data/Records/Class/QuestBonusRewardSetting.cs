using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public class QuestBonusRewardSetting : BaseRecord
{
	public Quest Quest;

	public QuestBonusReward Reward;

	[Signal("basic-quota")]
	public ContentQuota BasicQuota;

	[Signal("contents-reset") , Repeat(10)]
	public ContentsReset[] ContentsReset;



	#region Sub
	public sealed class SealedLevel : QuestBonusRewardSetting
	{
		[Signal("sealed-level")]
		public sbyte sealedLevel;
	}

	public sealed class DifficultyType : QuestBonusRewardSetting
	{
		[Signal("difficulty-type")]
		public Common.Seq.DifficultyType difficultyType;
	}

	public sealed class IgnoreDifficulty : QuestBonusRewardSetting
	{

	}
	#endregion
}