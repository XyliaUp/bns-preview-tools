using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class ContentQuota : BaseRecord
{
	[Signal("max-value")]
	public long MaxValue;

	[Signal("min-value")]
	public long MinValue;

	[Signal("version")]
	public short Version;

	[Signal("target-type")]
	public TargetTypeSeq TargetType;
	public enum TargetTypeSeq
	{
		Character,

		Account,
	}

	[Signal("expiration-time")]
	public DateTime ExpirationTime;

	[Signal("charge-interval")]
	public ResetType ChargeInterval;

	[Signal("charge-day-of-week")]
	public BDayOfWeek ChargeDayOfWeek;

	[Signal("charge-time")]
	public byte ChargeTime;
}