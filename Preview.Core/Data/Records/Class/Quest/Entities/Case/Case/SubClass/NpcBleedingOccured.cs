using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class NpcBleedingOccured : CaseBase
	{
		public string Object;

		[Side(ReleaseSide.Server)]
		public byte idx;


		public override List<string> AttractionObject => new() { Object };
	}
}