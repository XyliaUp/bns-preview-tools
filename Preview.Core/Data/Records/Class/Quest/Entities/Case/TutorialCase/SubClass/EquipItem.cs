using Xylia.Preview.Common.Attribute;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class EquipItem : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("item-category")]
		public GameCategory2Seq ItemCategory;
		
		[Side(ReleaseSide.Client)]
		public Item Item;
	}
}