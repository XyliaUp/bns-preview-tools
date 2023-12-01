using System.ComponentModel;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class RandomStoreItemDisplay : Record
{
	public string Alias;


	[Name("random-store-type")]
	public RandomStoreTypeSeq RandomStoreType;

	[Name("display-item")]
	public Ref<Item> DisplayItem;

	[Name("draw-group")]
	public DrawGroupSeq DrawGroup;

	[Name("probability-group")]
	public ProbabilityGroupSeq ProbabilityGroup;

	[Name("new-arrival")]
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