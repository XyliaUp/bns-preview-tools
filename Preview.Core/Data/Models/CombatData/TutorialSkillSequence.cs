using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class TutorialSkillSequence : Record
{
	public string Alias;

	public List<Step> step;

	public sealed class Step
	{
		public SequenceStepTypeSeq SequenceStepType;
		public enum SequenceStepTypeSeq
		{
			Skill,
			Effect,
		}


		[Repeat(20)] public int[] Skill;
		public bool CheckParentSkill;

		[Repeat(2)] 
		public SkillResult[] Result;

		[Repeat(2)]
		public SkillEventType[] ResultEventType;

		public Flag CasterFailState;
		public Ref<Effect> CasterFailEffect;
		public Flag CasterPassState;
		public Flag TargetPassState;
		public sbyte TargetPassStateCount;
		public Ref<Effect> TargetPassEffect;
		public sbyte TargetPassEffectCount;
		public bool CheckOnlyCastSkill;
	}
}