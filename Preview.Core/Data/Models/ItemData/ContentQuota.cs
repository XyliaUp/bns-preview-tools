using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ContentQuota : Record
{
	public string Alias;



	[Name("max-value")]
	public long MaxValue;

	[Name("min-value")]
	public long MinValue;

	[Name("version")]
	public short Version;

	[Name("target-type")]
	public TargetTypeSeq TargetType;
	public enum TargetTypeSeq
	{
		Character,

		Account,
	}

	[Name("expiration-time")]
	public Time64 ExpirationTime;

	[Name("charge-interval")]
	public ResetType ChargeInterval;

	[Name("charge-day-of-week")]
	public BDayOfWeek ChargeDayOfWeek;

	[Name("charge-time")]
	public sbyte ChargeTime;
}