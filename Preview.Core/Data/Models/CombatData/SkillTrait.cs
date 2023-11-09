using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class SkillTrait : Record
{
	public string Alias;



	public JobSeq Job;

	[Name("job-style")]
	public JobStyleSeq JobStyle;

	public sbyte Tier;

	[Name("tier-variation")]
	public sbyte TierVariation;




	public bool Enable = true;


	public Ref<Text> Name2;

	[Name("icon-texture")]
	public Ref<IconTexture> IconTexture;

	[Name("icon-index")]
	public short IconIndex;



	[Name("tooltip-train-name")]
	public Ref<Text> TooltipTrainName;

	[Name("tooltip-train-description")]
	public Ref<Text> TooltipTrainDescription;

	[Name("tooltip-effect-description")]
	public Ref<Text> TooltipEffectDescription;
}