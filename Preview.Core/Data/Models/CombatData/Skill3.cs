using SkiaSharp;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public partial class Skill3 : Record
{
	#region Fields
	[Name("variation-id")]
	public sbyte VariationId = 1;


	public string Alias;



	[Name("revised-effect-equip-probability-in-exec") , Repeat(5)] 
	public short[] RevisedEffectEquipProbabilityInExec1;

	[Name("damage-rate-pvp")]
	public short DamageRatePvp = 1000;

	[Name("damage-rate-standard-stats")]
	public short DamageRateStandardStats = 1000;


	public string Name;

	public Ref<Text> Name2;


	[Name("short-cut-key")]
	public KeyCommandSeq ShortCutKey;

	[Name("short-cut-key-classic")]
	public KeyCommandSeq ShortCutKeyClassic;

	[Name("short-cut-key-simple-context")]
	public KeyCommandSeq ShortCutKeySimpleContext;



	[Name("main-tooltip-1"), Repeat(5)]
	public Ref<SkillTooltip>[] MainTooltip1;

	[Name("main-tooltip-2"), Repeat(5)]
	public Ref<SkillTooltip>[] MainTooltip2;

	[Name("sub-tooltip"), Repeat(10)]
	public Ref<SkillTooltip>[] SubTooltip;

	[Name("stance-tooltip"), Repeat(5)]
	public Ref<SkillTooltip>[] StanceTooltip;

	[Name("condition-tooltip"), Repeat(5)]
	public Ref<SkillTooltip>[] ConditionTooltip;



	[Name("icon-texture")]
	public string IconTexture;

	[Name("icon-index")]
	public short IconIndex;
	#endregion

	#region Properties
	public KeyCommand CurrentShortCutKey => KeyCommand.Cast(this.ShortCutKey);

	public SKBitmap Icon => IconTexture.GetIcon(IconIndex);
	#endregion


	#region Sub
	public sealed class ActiveSkill : Skill3
	{
		public FlowTypeSeq FlowType;
		public enum FlowTypeSeq
		{
			KeepMainslot,
			LeaveCaster,
			TransferSimslot,
			DirectlySimslot,
		}




		[Name("target-filter")]
		public Ref<Filter> TargetFilter;

		[Name("gather-range")]
		public Ref<SkillGatherRange3> GatherRange;


		[Name("cast-condition")]
		public Ref<SkillCastCondition3> CastCondition;

		[Name("cast-duration")]
		public Msec CastDuration;


		[Name("throw-link-target")]
		public bool ThrowLinkTarget;

		[Name("casting-delay")]
		public bool CastingDelay = true;

		[Name("fire-miss")]
		public bool FireMiss;

		[Name("global-recycle-group")]
		public sbyte GlobalRecycleGroup;

		[Name("global-recycle-group-duration")]
		public Msec GlobalRecycleGroupDuration;

		[Name("recycle-group")]
		public RecycleGroup RecycleGroup;

		[Name("recycle-group-id")]
		public sbyte RecycleGroupId;

		[Name("recycle-group-duration")]
		public Msec RecycleGroupDuration;

		[Name("bound-recycle-group")]
		public RecycleGroup BoundRecycleGroup;

		[Name("bound-recycle-group-id")]
		public sbyte BoundRecycleGroupId;


		public enum ConsumeType
		{
			Point,

			[Name("point-below")]
			PointBelow,

			[Name("point-above")]
			PointAbove,

			[Name("base-max-percent")]
			BaseMaxPercent,

			[Name("total-max-percent")]
			TotalMaxPercent,

			[Name("current-percent")]
			CurrentPercent,
		}

		[Name("consume-hp-value")]
		public short ConsumeHpValue;

		[Name("consume-hp-type")]
		public ConsumeType ConsumeHpType;

		[Name("consume-sp-value"), Repeat(2)]
		public short[] ConsumeSpValue;

		[Name("consume-sp-type"), Repeat(2)]
		public ConsumeType[] ConsumeSpType;


		[Name("consume-summoned-hp-value")]
		public short ConsumeSummonedHpValue;

		[Name("consume-summoned-hp-type")]
		public ConsumeType ConsumeSummonedHpType;





		[Name("flow-repeat")]
		public sbyte FlowRepeat = 1;

		[Name("expanded-flow-repeat-count")]
		public sbyte ExpandedFlowRepeatCount;

		[Name("expanded-flow-repeat-start-flow-step")]
		public sbyte ExpandedFlowRepeatStartFlowStep = 1;



		[Name("exec-gather-type") , Repeat(5)]
		public GatherType[] ExecGatherType;
	}

	public sealed class PassiveSkill : Skill3
	{

	}

	public sealed class Action : Skill3
	{

	}
	#endregion
}