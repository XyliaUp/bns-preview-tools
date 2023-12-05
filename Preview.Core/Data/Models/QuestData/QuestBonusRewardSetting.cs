using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public class QuestBonusRewardSetting : Record
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