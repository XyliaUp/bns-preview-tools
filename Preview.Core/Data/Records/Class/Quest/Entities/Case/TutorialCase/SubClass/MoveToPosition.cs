using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class MoveToPosition : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("link-npc")]
		public Npc LinkNpc;

		[Side(ReleaseSide.Client)]
		[Signal("location-x")]
		public float LocationX;

		[Side(ReleaseSide.Client)]
		[Signal("location-y")]
		public float LocationY;

		[Side(ReleaseSide.Client)]
		[Signal("approach-range")]
		public float ApproachRange;
	}
}