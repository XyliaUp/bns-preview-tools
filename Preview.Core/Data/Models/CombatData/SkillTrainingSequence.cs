using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class SkillTrainingSequence : Record
{
	public string Alias;
	public sbyte RepeatCount;



	public List<Step> step { get; set; }

	public sealed class Step
	{
		public SequenceStepTypeSeq SequenceStepType;
		public enum SequenceStepTypeSeq
		{
			Skill,
			Effect,
		}

		[Repeat(8)]
		public int[] Skill;
		[Repeat(8)]
		public sbyte[] VariationId;
		public bool IncludeInheritanceSkill;
		public bool CheckOnlyCastSkill;
		public Msec NextStepInterval;
		public Msec CommandActionInputOffset;
		public Ref<Effect> Effect;
		[Repeat(2)]
		public SkillResult[] Result;
		[Repeat(2)]
		public SkillEventType[] ResultEventType;
		public sbyte ResultCount;
		public Flag CasterPassState;
		public Ref<Effect> CasterPassEffect;
		public Flag TargetPassState;
		public sbyte TargetPassStateCount;
		public Ref<Effect> TargetPassEffect;
		public sbyte TargetPassEffectCount;
	}
}