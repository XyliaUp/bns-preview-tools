using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class FinishBlow : CaseBase
	{
		public Npc Npc;

		[Signal("skill3-id-1")] public int Skill3ID1;
		[Signal("skill3-id-2")] public int Skill3ID2;
		[Signal("skill3-id-3")] public int Skill3ID3;
		[Signal("skill3-id-4")] public int Skill3ID4;
		[Signal("skill3-id-5")] public int Skill3ID5;
		[Signal("skill3-id-6")] public int Skill3ID6;
		[Signal("skill3-id-7")] public int Skill3ID7;
		[Signal("skill3-id-8")] public int Skill3ID8;
		[Signal("skill3-id-9")] public int Skill3ID9;
		[Signal("skill3-id-10")] public int Skill3ID10;
		[Signal("skill3-id-11")] public int Skill3ID11;
		[Signal("skill3-id-12")] public int Skill3ID12;
		[Signal("skill3-id-13")] public int Skill3ID13;
		[Signal("skill3-id-14")] public int Skill3ID14;
		[Signal("skill3-id-15")] public int Skill3ID15;
		[Signal("skill3-id-16")] public int Skill3ID16;
		[Signal("skill3-id-17")] public int Skill3ID17;
		[Signal("skill3-id-18")] public int Skill3ID18;
		[Signal("skill3-id-19")] public int Skill3ID19;
		[Signal("skill3-id-20")] public int Skill3ID20;
	
	
		public override List<string> AttractionObject => new() { "npc:" + Npc };
	}
}