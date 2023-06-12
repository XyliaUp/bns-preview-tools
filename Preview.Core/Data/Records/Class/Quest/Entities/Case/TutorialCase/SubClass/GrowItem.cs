using Xylia.Preview.Common.Attribute;
using  Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class GrowItem : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("material-item")]
		public Item MaterialItem;

		[Side(ReleaseSide.Client)]
		[Signal("primary-item")]
		public Item PrimaryItem;
	}
}