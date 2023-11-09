using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class UnlocatedStore : Record
{
	public Ref<Store2> Store2;

	[Name("no-more-use")]
	public bool NoMoreUse;

	[Name("unlocated-store-type")]
	public UnlocatedStoreTypeSeq UnlocatedStoreType;

	public enum UnlocatedStoreTypeSeq
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