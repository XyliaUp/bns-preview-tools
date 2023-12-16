using System.ComponentModel;

using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreItemDisplay : ModelElement
{
	[Name("random-store-type")]
	public RandomStoreTypeSeq RandomStoreType { get; set; }

	[Name("display-item")]
	public Ref<Item> DisplayItem { get; set; }

	[Name("draw-group")]
	public DrawGroupSeq DrawGroup { get; set; }

	[Name("probability-group")]
	public ProbabilityGroupSeq ProbabilityGroup { get; set; }

	[Name("new-arrival")]
	public bool NewArrival { get; set; }



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