using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public class UnlocatedStore : ModelElement
{
	public Ref<Store2> Store2 { get; set; }

	public bool NoMoreUse { get; set; }

	public UnlocatedStoreTypeSeq UnlocatedStoreType { get; set; }

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