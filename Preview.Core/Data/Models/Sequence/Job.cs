using System.ComponentModel;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models.Sequence;
public enum JobSeq
{
	[Description("job-none")]
	JobNone,

	[Description("blade-master")]
	[Name("Name.job.BladeMaster")]
	검사,

	[Description("kung-fu-fighter")]
	[Name("Name.job.KungFuFighter")]
	권사,

	[Description("force-master")]
	[Name("Name.job.ForceMaster")]
	기공사,

	[Description("shooter")]
	[Name("Name.job.Shooter")]
	격사,

	[Description("destroyer")]
	[Name("Name.job.Destroyer")]
	역사,

	[Description("summoner")]
	[Name("Name.job.Summoner")]
	소환사,

	[Description("assassin")]
	[Name("Name.job.Assassin")]
	암살자,

	[Description("sword-master")]
	[Name("Name.job.SwordMaster")]
	귀검사,

	[Description("warlock")]
	[Name("Name.job.Warlock")]
	주술사,

	[Description("soul-fighter")]
	[Name("Name.job.SoulFighter")]
	기권사,

	[Description("warroir")]
	[Name("Name.job.Warrior")]
	투사,

	[Description("archer")]
	[Name("Name.job.Archer")]
	궁사,

	[Description("spear-master")]
	창술사,

	[Description("thunderer")]
	[Name("Name.job.Thunderer")]
	뇌전술사,

	[Description("dual-blader")]
	[Name("Name.job.Dual-Blader")]
	쌍검사,

	[Description("bard")]
	[Name("Name.job.Bard")]
	악사,

	[Description("pc-max")]
	PcMax,

	[Description("소환수-루키")]
	소환수루키,

	[Description("소환수-striker")]
	소환수striker,

	[Description("소환수-defender")]
	소환수defender,

	[Description("소환수-controller")]
	소환수controller,

	COUNT
}

public static partial class SequenceExtensions
{
	public static Job Convert(this JobSeq seq) => FileCache.Data.Get<Job>().FirstOrDefault(o => o.job == seq);
}