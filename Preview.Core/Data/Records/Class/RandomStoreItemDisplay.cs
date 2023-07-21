using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class RandomStoreItemDisplay : BaseRecord
{
	[Signal("random-store-type")]
	public RandomStoreTypeSeq RandomStoreType;

	[Signal("display-item")]
	public Item DisplayItem;

	[Signal("draw-group")]
	public DrawGroupSeq DrawGroup;

	[Signal("probability-group")]
	public ProbabilityGroupSeq ProbabilityGroup;

	[Signal("new-arrival")]
	public bool NewArrival;



	#region Enums
	public enum RandomStoreTypeSeq : byte
	{
		None,

		Paid,

		Free,
	}

	public enum DrawGroupSeq : byte
	{
		None,

		[Description("鸿运专属宝物")]
		Premium,

		[Description("宝物")]
		Normal,
	}

	public enum ProbabilityGroupSeq : byte
	{
		None,

		[Description("稀有概率")]
		Rare,

		[Description("一般概率")]
		Normal,
	}
	#endregion
}