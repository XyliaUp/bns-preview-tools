using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Seq;

[DefaultValue(JobNone)]
public enum JobSeq : byte
{
	[Signal("job-none")]
	JobNone,

	[Signal("blade-master")]
	[Description("剑士")]
	검사,

	[Signal("kung-fu-fighter")]
	[Description("拳师")]
	권사,

	[Signal("force-master")]
	[Description("气功师")]
	기공사,

	[Signal("shooter")]
	[Description("枪手")]
	격사,

	[Signal("destroyer")]
	[Description("力士")]
	역사,

	[Signal("summoner")]
	[Description("召唤师")]
	소환사,

	[Signal("assassin")]
	[Description("刺客")]
	암살자,

	[Signal("sword-master")]
	[Description("灵剑士")]
	귀검사,

	[Signal("warlock")]
	[Description("咒术师")]
	주술사,

	[Signal("soul-fighter")]
	[Description("气宗")]
	기권사,

	[Signal("warroir")]
	[Description("斗士")]
	투사,

	[Signal("archer")]
	[Description("弓手")]
	궁사,

	[Signal("spear-master")]
	[Description("矛手")]
	창술사,

	[Signal("thunderer")]
	[Description("星术师")]
	뇌전술사,

	[Signal("dual-blader")]
	[Description("双剑")]
	쌍검사,

	[Signal("bard")]
	[Description("乐师")]
	악사,

	[Signal("pc-max")]
	PcMax,



	[Signal("소환수-루키")]
	소환수루키,

	[Signal("소환수-striker")]
	소환수striker,

	[Signal("소환수-defender")]
	소환수defender,

	[Signal("소환수-controller")]
	소환수controller
}

public static partial class Extension
{
	public static string GetName(this JobSeq Seq) => Seq.GetDescription();

	public static Job Convert(this JobSeq seq) => FileCache.Data.Job.FirstOrDefault(o => o.job == seq);
}
