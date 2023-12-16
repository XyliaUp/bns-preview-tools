using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public class SkillTrait : ModelElement
{
	public JobSeq Job { get; set; }

	public JobStyleSeq JobStyle { get; set; }

	public sbyte Tier { get; set; }

	public sbyte TierVariation { get; set; }




	public bool Enable { get; set; } 


	public Ref<Text> Name2 { get; set; }

	public Ref<IconTexture> IconTexture { get; set; }

	public short IconIndex { get; set; }



	public Ref<Text> TooltipTrainName { get; set; }

	public Ref<Text> TooltipTrainDescription { get; set; }

	public Ref<Text> TooltipEffectDescription { get; set; }
}