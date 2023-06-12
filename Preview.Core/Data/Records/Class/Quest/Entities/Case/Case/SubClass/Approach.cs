using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class Approach : NpcTalkBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("approach-talk")]
		public bool ApproachTalk;

		[Side(ReleaseSide.Client)]
		[Signal("approach-social")]
		public Social ApproachSocial;
	}
}