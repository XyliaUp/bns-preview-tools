using System;

using Xylia.Preview.Common.Attribute;


namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class Skill : CaseBase
	{
		[Side(ReleaseSide.Client)]
		[Signal("npc-response")]
		public NpcResponse NpcResponse;

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
		[Signal("object2-16")] public string Object2_16;

		[Obsolete]
		public _Skill skill;

		public Record.Skill Skill3;
	}
}