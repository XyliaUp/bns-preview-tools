using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class QuestBonusRewardSetting : BaseRecord
	{
		public TypeSeq Type;
		public enum TypeSeq
		{
			[Signal("sealed-level")]
			SealedLevel,

			[Signal("difficulty-type")]
			DifficultyType,

			[Signal("ignore-difficulty")]
			IgnoreDifficulty,
		}




		public Quest Quest;

		public QuestBonusReward Reward;

		[Signal("basic-quota")]
		public ContentQuota BasicQuota;

		[Signal("contents-reset-1")]
		public ContentsReset ContentsReset1;

		[Signal("contents-reset-2")]
		public ContentsReset ContentsReset2;

		[Signal("contents-reset-3")]
		public ContentsReset ContentsReset3;

		[Signal("contents-reset-4")]
		public ContentsReset ContentsReset4;

		[Signal("contents-reset-5")]
		public ContentsReset ContentsReset5;

		[Signal("contents-reset-6")]
		public ContentsReset ContentsReset6;

		[Signal("contents-reset-7")]
		public ContentsReset ContentsReset7;

		[Signal("contents-reset-8")]
		public ContentsReset ContentsReset8;

		[Signal("contents-reset-9")]
		public ContentsReset ContentsReset9;

		[Signal("contents-reset-10")]
		public ContentsReset ContentsReset10;



		[Signal("sealed-level")]
		public byte SealedLevel;

		[Signal("difficulty-type")]
		public DifficultyType DifficultyType;
	}
}