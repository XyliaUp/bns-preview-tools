using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Seq;
public enum SexSeq
{
	[Signal("sex-none")]
	SexNone,

	남,

	여,

	중,
}

[DefaultValue(All)]
public enum SexSeq2
{
	[Signal("sex-none")]
	SexNone,

	All,

	Male,

	Female,
}


public static partial class Extension
{
	public static string GetName(this SexSeq2 Seq) => $"Name.sex.{Seq.GetSignal()}".GetText(true) ?? Seq.ToString();


	//public static Sex Convert(this SexSeq2 seq) => FileCache.Data.Sex.FirstOrDefault(o => o.job == seq);
}