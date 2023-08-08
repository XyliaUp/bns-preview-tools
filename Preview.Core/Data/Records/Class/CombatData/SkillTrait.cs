using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public class SkillTrait : BaseRecord
{
	public JobSeq Job;

	[Signal("job-style")]
	public JobStyleSeq JobStyle;

	public sbyte Tier;

	[Signal("tier-variation")]
	public sbyte TierVariation;




	public bool Enable = true;


	public Text Name2;

	[Signal("icon-texture")]
	public IconTexture IconTexture;

	[Signal("icon-index")]
	public short IconIndex;



	[Signal("tooltip-train-name")]
	public Text TooltipTrainName;

	[Signal("tooltip-train-description")]
	public Text TooltipTrainDescription;

	[Signal("tooltip-effect-description")]
	public Text TooltipEffectDescription;
}