using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public partial class Skill3
{
	#region Sub
	public sealed partial class ActiveSkill : Skill3
	{
		public FlowTypeSeq FlowType;
		public enum FlowTypeSeq
		{
			KeepMainslot,
			LeaveCaster,
			TransferSimslot,
			DirectlySimslot,
		}


		[Signal("dash-attribute")]
		public string DashAttribute;




		[Signal("target-filter")]
		public string TargetFilter;

		[Signal("gather-range")]
		public string GatherRange;




		[Signal("cast-condition")]
		public string CastCondition;

		[Signal("cast-duration")]
		public int CastDuration;


		[Signal("throw-link-target")]
		public bool ThrowLinkTarget;

		[Signal("casting-delay")]
		public bool CastingDelay = true;

		[Signal("fire-miss")]
		public bool FireMiss;

		[Signal("global-recycle-group")]
		public sbyte GlobalRecycleGroup;

		[Signal("global-recycle-group-duration")]
		public int GlobalRecycleGroupDuration;

		[Signal("recycle-group")]
		public RecycleGroup RecycleGroup;

		[Signal("recycle-group-id")]
		public sbyte RecycleGroupId;

		[Signal("recycle-group-duration")]
		public int RecycleGroupDuration;

		[Signal("bound-recycle-group")]
		public RecycleGroup BoundRecycleGroup;

		[Signal("bound-recycle-group-id")]
		public sbyte BoundRecycleGroupId;


		public enum ConsumeType
		{
			Point,

			[Signal("point-below")]
			PointBelow,

			[Signal("point-above")]
			PointAbove,

			[Signal("base-max-percent")]
			BaseMaxPercent,

			[Signal("total-max-percent")]
			TotalMaxPercent,

			[Signal("current-percent")]
			CurrentPercent,
		}

		[Signal("consume-hp-value")]
		public short ConsumeHpValue;

		[Signal("consume-hp-type")]
		public ConsumeType ConsumeHpType;

		[Signal("consume-sp-value-1")]
		public short ConsumeSpValue1;

		[Signal("consume-sp-value-2")]
		public short ConsumeSpValue2;

		[Signal("consume-sp-type-1")]
		public ConsumeType ConsumeSpType1;

		[Signal("consume-sp-type-2")]
		public ConsumeType ConsumeSpType2;

		[Signal("consume-summoned-hp-value")]
		public short ConsumeSummonedHpValue;

		[Signal("consume-summoned-hp-type")]
		public ConsumeType ConsumeSummonedHpType;





		[Signal("flow-repeat")]
		public sbyte FlowRepeat = 1;

		[Signal("expanded-flow-repeat-count")]
		public sbyte ExpandedFlowRepeatCount;

		[Signal("expanded-flow-repeat-start-flow-step")]
		public sbyte ExpandedFlowRepeatStartFlowStep = 1;




		[Signal("exec-gather-type-1")]
		public GatherType ExecGatherType1;

		[Signal("exec-gather-type-2")]
		public GatherType ExecGatherType2;

		[Signal("exec-gather-type-3")]
		public GatherType ExecGatherType3;

		[Signal("exec-gather-type-4")]
		public GatherType ExecGatherType4;

		[Signal("exec-gather-type-5")]
		public GatherType ExecGatherType5;
	}
	#endregion
}