using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Common.Seq;
public enum SexSeq
{
	[Signal("sex-none")]
	SexNone,

	남,

	여,

	중,
}

public static partial class Extension
{
	public static string GetName(this SexSeq2 Seq) => $"Name.sex.{Seq.GetSignal()}".GetText(true) ?? Seq.ToString();
}