using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum ArenaMatchingRule
{
	None,

	Normal,

	[Signal("death-match-1vs1")]
	N2,

	[Signal("death-match-1vs1")]
	N3,

	[Signal("death-match-1vs1")]
	N4,
}


[DefaultValue(All)]
public enum ResultSeq
{
	None,

	All,

	Win,

	Lose,
}