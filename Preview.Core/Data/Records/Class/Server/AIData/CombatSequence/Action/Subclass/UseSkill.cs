using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	/// <summary>
	/// 发动技能
	/// </summary>
	[Signal("use-skill")]
	public class UseSkill : SkillBase
	{
		[Signal("target-area")]
		public ZoneArea TargetArea;

		[Signal("multi-skill3-1")] public Skill Multi_Skill3_1;
		[Signal("multi-skill3-2")] public Skill Multi_Skill3_2;
		[Signal("multi-skill3-3")] public Skill Multi_Skill3_3;
		[Signal("multi-skill3-4")] public Skill Multi_Skill3_4;
		[Signal("multi-skill3-5")] public Skill Multi_Skill3_5;
		[Signal("multi-skill3-6")] public Skill Multi_Skill3_6;
		[Signal("multi-skill3-7")] public Skill Multi_Skill3_7;
		[Signal("multi-skill3-8")] public Skill Multi_Skill3_8;
		[Signal("multi-skill3-9")] public Skill Multi_Skill3_9;
		[Signal("multi-skill3-10")] public Skill Multi_Skill3_10;
		[Signal("multi-skill3-11")] public Skill Multi_Skill3_11;
		[Signal("multi-skill3-12")] public Skill Multi_Skill3_12;

		[Signal("multi-target-1")] public Script_obj MultiTarget1;
		[Signal("multi-target-2")] public Script_obj MultiTarget2;
		[Signal("multi-target-3")] public Script_obj MultiTarget3;
		[Signal("multi-target-4")] public Script_obj MultiTarget4;
		[Signal("multi-target-5")] public Script_obj MultiTarget5;
		[Signal("multi-target-6")] public Script_obj MultiTarget6;
		[Signal("multi-target-7")] public Script_obj MultiTarget7;
		[Signal("multi-target-8")] public Script_obj MultiTarget8;
		[Signal("multi-target-9")] public Script_obj MultiTarget9;
		[Signal("multi-target-10")] public Script_obj MultiTarget10;
		[Signal("multi-target-11")] public Script_obj MultiTarget11;
		[Signal("multi-target-12")] public Script_obj MultiTarget12;
	}
}