using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	[DefaultValue(SexNone)]
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
}