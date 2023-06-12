using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class JoinFaction : CaseBase
	{
		public Faction Faction;

		public string Object;

		[Signal("npc-response-1")]
		public NpcResponse NpcResponse1;

		[Signal("npc-response-2")]
		public NpcResponse NpcResponse2;

		[Signal("npc-response-3")]
		public NpcResponse NpcResponse3;


		public override List<string> AttractionObject => new() { Object };
	}
}