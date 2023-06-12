using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class UnlocatedStore : BaseRecord
	{
		public Store2 Store2;

		[Signal("no-more-use")]
		public bool NoMoreUse;

		[Signal("unlocated-store-type")]
		public Type UnlocatedStoreType;

		public enum Type
		{
			UnlocatedNone,
			UnlocatedStore,
			AccountStore,
			SoulBoostStore1,
			SoulBoostStore2,
			SoulBoostStore3,
			SoulBoostStore4,
			SoulBoostStore5,
			SoulBoostStore6,
		}
	}
}