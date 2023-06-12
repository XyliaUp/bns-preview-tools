using System.Collections.Generic;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class EnterZone : CaseBase
	{
		public string Object;

		public override List<string> AttractionObject => new() { Object };
	}
}