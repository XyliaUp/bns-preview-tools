using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public partial class Skill3 : BaseRecord
{
	#region Fields
	[Signal("variation-id")]
	public sbyte VariationId = 1;


	[Signal("revised-effect-equip-probability-in-exec") , Repeat(5)] 
	public short[] RevisedEffectEquipProbabilityInExec1;

	[Signal("damage-rate-pvp")]
	public short DamageRatePvp = 1000;

	[Signal("damage-rate-standard-stats")]
	public short DamageRateStandardStats = 1000;


	public string Name;

	public Text Name2;


	[Signal("short-cut-key")]
	public KeyCommandSeq ShortCutKey;

	[Signal("short-cut-key-classic")]
	public KeyCommandSeq ShortCutKeyClassic;

	[Signal("short-cut-key-simple-context")]
	public KeyCommandSeq ShortCutKeySimpleContext;



	[Signal("main-tooltip-1"), Repeat(5)]
	public SkillTooltip[] MainTooltip1;

	[Signal("main-tooltip-2"), Repeat(5)]
	public SkillTooltip[] MainTooltip2;

	[Signal("sub-tooltip"), Repeat(10)]
	public SkillTooltip[] SubTooltip;

	[Signal("stance-tooltip"), Repeat(5)]
	public SkillTooltip[] StanceTooltip;

	[Signal("condition-tooltip"), Repeat(5)]
	public SkillTooltip[] ConditionTooltip;




	[Signal("icon-texture")]
	public string IconTexture;

	[Signal("icon-index")]
	public short IconIndex;
	#endregion





	#region Properties
	public KeyCommand CurrentShortCutKey => KeyCommand.Cast(this.ShortCutKey);
	#endregion


	#region Sub
	public sealed class PassiveSkill : Skill3
	{

	}

	public sealed class Action : Skill3
	{

	}
	#endregion
}