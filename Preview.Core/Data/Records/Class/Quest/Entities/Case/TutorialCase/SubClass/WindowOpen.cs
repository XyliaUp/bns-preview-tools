using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class WindowOpen : TutorialCaseBase
	{
		[Signal("window-type")]
		[Side(ReleaseSide.Client)]
		public WindowTypeSeq WindowType;

		public enum WindowTypeSeq
		{
			Inverntory,

			[Signal("quest-journal")]
			QuestJournal,

			Skill,

			Sandbox,

			Auction,

			[Signal("cash-shop")]
			CashShop,

			Wardrobe,

			[Signal("account-contents")]
			AccountContents,
		}



		[Signal("window-open-way")]
		[Side(ReleaseSide.Client)]
		public WindowOpenWaySeq WindowOpenWay;

		public enum WindowOpenWaySeq
		{
			None,

			[Signal("by-npc-seller-button")]
			ByNpcSellerButton,
		}
	}
}