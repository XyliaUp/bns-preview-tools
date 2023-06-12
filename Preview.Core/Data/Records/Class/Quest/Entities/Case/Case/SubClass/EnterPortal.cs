using System.Collections.Generic;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class EnterPortal : CaseBase
	{
		public string Object2;


		public override List<string> AttractionObject => new() { Object2 };
	}
}