
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SlateScroll : BaseRecord
	{
		[Signal("secondary-cash-enable")]
		public bool SecondaryCashEnable;

		[Signal("ingredient-money")]
		public int IngredientMoney;

		[Signal("secondary-cash")]
		public string SecondaryCash;
	}
}