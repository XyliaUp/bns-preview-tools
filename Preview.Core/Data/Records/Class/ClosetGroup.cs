using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class ClosetGroup : BaseRecord
	{
		[Signal("sort-no")]
		public short SortNo;

		[Signal("charge-of-item-for-instant-payment")]
		public string ChargeOfItemForInstantPayment;

		[Signal("item-to-be-paid")]
		public string ItemToBePaid;

		[Signal("check-equip-characteristics")]
		public bool CheckEquipCharacteristics;
	}
}