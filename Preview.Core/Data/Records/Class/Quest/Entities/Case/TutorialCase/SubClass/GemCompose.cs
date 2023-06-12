using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class GemCompose : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("primary-item")]
		public Item PrimaryItem;

		[Side(ReleaseSide.Client)]
		[Signal("material-item")]
		public Item MaterialItem;
	}
}