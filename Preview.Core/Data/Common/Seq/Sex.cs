using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Models;

using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.Data.Common.Seq;
public enum SexSeq
{
	[Name("sex-none")]
	SexNone,

	남,

	여,

	중,
}

public static partial class Extension
{
	public static string GetName(this SexSeq2 Seq) => $"Name.sex.{Seq.GetName()}".GetText() ?? Seq.ToString();
}