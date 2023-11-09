using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class AccountPostCharge : Record
{
	public string Alias;


	public int ChargeMoney;

	[Repeat(2)]
	public Ref<Item>[] ChargeItem;

	[Repeat(2)]
	public int[] ChargeItemAmount;
}