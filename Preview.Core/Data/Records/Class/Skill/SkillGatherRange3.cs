using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class SkillGatherRange3 : BaseRecord
	{
		#region Fields	
		public short RangeCastMin;
		public short RangeCastMax;

		public short RangeCastDepth;
		public short RangeCastHeight;

		public short GatherRadiusMax1;
		public short GatherRadiusMax2;
		public short GatherRadiusMax3;
		public short GatherRadiusMax4;
		public short GatherRadiusMax5;
		public short GatherRadiusMin1;
		public short GatherRadiusMin2;
		public short GatherRadiusMin3;
		public short GatherRadiusMin4;
		public short GatherRadiusMin5;
		#endregion




		#region 测试
		public short RadiusMax => (short)(this.GatherRadiusMax1 * 4 * 2);

		public short CastMax => (short)(this.RangeCastMax * 4 * 2);
		#endregion
	}
}