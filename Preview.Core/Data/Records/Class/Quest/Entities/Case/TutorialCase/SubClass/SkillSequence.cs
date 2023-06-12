using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class SkillSequence : TutorialCaseBase
	{
		[Signal("object-1")] public string Object_1;
		[Signal("object-2")] public string Object_2;
		[Signal("object-3")] public string Object_3;
		[Signal("object-4")] public string Object_4;
		[Signal("object-5")] public string Object_5;
		[Signal("object-6")] public string Object_6;
		[Signal("object-7")] public string Object_7;
		[Signal("object-8")] public string Object_8;
		[Signal("object-9")] public string Object_9;
		[Signal("object-10")] public string Object_10;
		[Signal("object-11")] public string Object_11;
		[Signal("object-12")] public string Object_12;
		[Signal("object-13")] public string Object_13;
		[Signal("object-14")] public string Object_14;
		[Signal("object-15")] public string Object_15;

		[Signal("object2-1")] public string Object2_1;
		[Signal("object2-2")] public string Object2_2;
		[Signal("object2-3")] public string Object2_3;
		[Signal("object2-4")] public string Object2_4;
		[Signal("object2-5")] public string Object2_5;
		[Signal("object2-6")] public string Object2_6;
		[Signal("object2-7")] public string Object2_7;
		[Signal("object2-8")] public string Object2_8;
		[Signal("object2-9")] public string Object2_9;
		[Signal("object2-10")] public string Object2_10;
		[Signal("object2-11")] public string Object2_11;
		[Signal("object2-12")] public string Object2_12;
		[Signal("object2-13")] public string Object2_13;
		[Signal("object2-14")] public string Object2_14;
		[Signal("object2-15")] public string Object2_15;
	
	
		[Side(ReleaseSide.Client)]
		[Signal("skill-sequence")]
		public string skillSequence;
	}
}