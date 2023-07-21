using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Seq;

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



public static partial class Extension
{
	public static string GetName(this RaceSeq Seq) => Seq switch
	{
		RaceSeq.건 => "Name.race.Kun".GetText(),
		RaceSeq.곤 => "Name.race.Gon".GetText(),
		RaceSeq.린 => "Name.race.Lyn".GetText(),
		RaceSeq.진 => "Name.race.Jin".GetText(),

		_ => Seq.ToString(),
	};

	public static string GetName(this RaceSeq2 Seq) => $"Name.race.{Seq.GetSignal()}".GetText(true) ?? Seq.ToString();

	//public static Job Convert(this JobSeq seq) => FileCache.Data.Job.FirstOrDefault(o => o.job == seq);
}
