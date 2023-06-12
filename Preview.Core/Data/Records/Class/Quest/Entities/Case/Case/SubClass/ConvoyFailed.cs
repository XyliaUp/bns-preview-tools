using System.Collections.Generic;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	/// <summary>
	/// 护送失败
	/// </summary>
	public sealed class ConvoyFailed : CaseBase
	{
		public string Object;

		public ZoneConvoy Convoy;


		public override List<string> AttractionObject => new() { Object };
	}
}