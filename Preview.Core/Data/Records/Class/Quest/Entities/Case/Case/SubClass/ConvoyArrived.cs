using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	/// <summary>
	/// 护送成功
	/// </summary>
	public sealed class ConvoyArrived : CaseBase
	{
		public string Object;

		[Side(ReleaseSide.Server)]
		public ZoneConvoy Convoy;


		public override List<string> AttractionObject => new() { Object };
	}
}