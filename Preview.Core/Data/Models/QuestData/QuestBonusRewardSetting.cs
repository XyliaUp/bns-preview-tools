using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class QuestBonusRewardSetting : Record
{
	public string Alias;



	public Ref<Quest> Quest;

	public Ref<QuestBonusReward> Reward;

	public Ref<ContentQuota> BasicQuota;

	[Repeat(10)]
	public Ref<ContentsReset>[] ContentsReset;


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