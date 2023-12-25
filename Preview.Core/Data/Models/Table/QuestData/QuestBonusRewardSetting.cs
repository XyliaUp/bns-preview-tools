using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class QuestBonusRewardSetting : ModelElement
{
	#region Sub
	public sealed class SealedLevel : QuestBonusRewardSetting
	{
		public sbyte sealedLevel;
	}

	public sealed class DifficultyType : QuestBonusRewardSetting
	{
		public DifficultyTypeSeq difficultyType;
	}

	public sealed class IgnoreDifficulty : QuestBonusRewardSetting
	{

	}
	#endregion
}