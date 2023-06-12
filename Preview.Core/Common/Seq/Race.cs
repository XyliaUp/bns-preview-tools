using System.ComponentModel;

using Xylia.Preview.Common.Attribute;


namespace Xylia.Preview.Common.Seq
{
	[DefaultValue(RaceNone)]
	public enum RaceSeq
	{
		[Signal("race-none")]
		RaceNone,

		건,

		곤,

		린,

		진,

		나쁜몹,

		더나쁜몹,

		무서운몹,

		더무서운몹,

		더더무서운몹,

		고양이,

		강림체,

		악귀
	}

	[DefaultValue(All)]
	public enum RaceSeq2
	{
		[Signal("race-none")]
		RaceNone,

		All,

		Jin,

		Gon,

		Lyn,

		Kun,

		[Description("召唤兽")]
		[Signal("summoned-all")]
		SummonedAll,

		[Description("喵喵")]
		[Signal("summoned-cat")]
		SummonedCat,
	}
}