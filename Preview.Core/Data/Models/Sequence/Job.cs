using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models.Sequence;
public enum JobSeq
{
	[Name("job-none")]
	JobNone,

	[Name("blade-master")]
	검사,

	[Name("kung-fu-fighter")]
	권사,

	[Name("force-master")]
	기공사,

	[Name("shooter")]
	격사,

	[Name("destroyer")]
	역사,

	[Name("summoner")]
	소환사,

	[Name("assassin")]
	암살자,

	[Name("sword-master")]
	귀검사,

	[Name("warlock")]
	주술사,

	[Name("soul-fighter")]
	기권사,

	[Name("warroir")]
	투사,

	[Name("archer")]
	궁사,

	[Name("spear-master")]
	창술사,

	[Name("thunderer")]
	뇌전술사,

	[Name("dual-blader")]
	쌍검사,

	[Name("bard")]
	악사,

	[Name("pc-max")]
	PcMax,


	[Name("소환수-루키")]
	소환수루키,

	[Name("소환수-striker")]
	소환수striker,

	[Name("소환수-defender")]
	소환수defender,

	[Name("소환수-controller")]
	소환수controller,

	COUNT
}

public static partial class SequenceExtensions
{
	public static Job Convert(this JobSeq seq) => FileCache.Data.Get<Job>().FirstOrDefault(o => o.job == seq);
}