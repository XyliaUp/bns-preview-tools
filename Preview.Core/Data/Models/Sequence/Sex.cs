using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.Sequence;
public enum SexSeq
{
    [Name("sex-none")]
    SexNone,

	[Name("Name.sex.neuter")]
	남,

	[Name("Name.sex.male")]
	여,

	[Name("Name.sex.female")]
	중,
}