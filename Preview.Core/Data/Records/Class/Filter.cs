using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Table;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class Filter : BaseRecord
	{
		
	}


	public class Filter2 : BaseRecord
	{
		public string Field;

		[DefaultValue(null)]
		public Op Op;

		public string Subject;

		public string Subject2;

		public string Type;

		public string Value;

		[Signal("value-1")]
		public string Value1;

		[Signal("value-2")]
		public string Value2;

		[Signal("value-3")]
		public string Value3;

		[Signal("value-4")]
		public string Value4;

		public string Item;

		public string Quest;

		[Signal("count-op")]
		public Op CountOp = Op.ge;

		public bool Flag;

		public Flag FlagType;



		[Side(ReleaseSide.Server)]
		public string Spawn;

		[Side(ReleaseSide.Server)]
		public bool Leader;

		[Side(ReleaseSide.Server)]
		public Script_obj Party;
	}
}