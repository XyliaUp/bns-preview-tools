using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class AccountPostCharge : BaseRecord
	{
		[Signal("charge-money")]
		public int ChargeMoney;

		[Signal("charge-item-1")]
		public Item ChargeItem1;

		[Signal("charge-item-2")]
		public Item ChargeItem2;

		[Signal("charge-item-amount-1")]
		public int ChargeItemAmount1;

		[Signal("charge-item-amount-2")]
		public int ChargeItemAmount2;
	}
}