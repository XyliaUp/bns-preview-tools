
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;


namespace Xylia.Preview.Data.Record
{
	public sealed class FactionLevel : BaseRecord
	{
		#region Fields
		public short Level;

		public int Reputation;

		[Signal("grade-name-1")]
		public string GradeName1;

		[Signal("grade-name-2")]
		public string GradeName2;

		[Signal("max-faction-score")]
		public int MaxFactionScore;
		#endregion
	}
}