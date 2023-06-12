using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class Npc : BaseRecord
	{
		public Text Name2;

		public Text Title2;


		[Signal("store2-1")]
		public Store2 Store2_1;

		[Signal("store2-2")]
		public Store2 Store2_2;

		[Signal("store2-3")]
		public Store2 Store2_3;

		[Signal("store2-4")]
		public Store2 Store2_4;

		[Signal("store2-5")]
		public Store2 Store2_5;

		[Signal("store2-6")]
		public Store2 Store2_6;
	}
}