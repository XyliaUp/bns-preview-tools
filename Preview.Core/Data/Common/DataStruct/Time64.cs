using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Common.DataStruct;
public struct Time64
{
	const long epoch = 621355968000000000;
	public long Ticks;

	public Time64(long Ticks) => this.Ticks = Ticks;

	public DateTime Time => new(epoch + Ticks * 10000000);

	public DateTime LocalTime => TimeZoneInfo.ConvertTimeFromUtc(Time, ZoneInfo());


	public static Time64 Parse(string s) => DateTime.Parse(s);


	public static implicit operator Time64(long ticks) => new(ticks);

	public static implicit operator Time64(DateTime dateTime)
	{
		var time = TimeZoneInfo.ConvertTimeToUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified), ZoneInfo());
		return (time.Ticks - epoch) / 10000000;
	}


	public static bool operator ==(Time64 a, Time64 b) => a.Ticks == b.Ticks;

	public static bool operator !=(Time64 a, Time64 b) => !(a == b);

	public static Msec operator -(Time64 a, Time64 b) => (int)((a.Ticks - b.Ticks) * 1000);


	public bool Equals(Time64 other) => Ticks == other.Ticks;

	public override bool Equals(object obj) => obj is Time64 other && Equals(other);

	public override string ToString() => this.LocalTime.ToString();

	public override int GetHashCode() => HashCode.Combine(Ticks);



	#region ZoneInfo
	private static TimeZoneInfo ZoneInfo()
	{
		var publisher = FileCache.Data.Provider?.Locale?.Publisher;
		var offset = publisher switch
		{
			Publisher.Default => new TimeSpan(9, 0, 0),   // Korea Standard Time
			Publisher.Tencent => new TimeSpan(8, 0, 0),   // China Standard Time
			Publisher.Innova => new TimeSpan(0, 0, 0),    //
			Publisher.NcJapan => new TimeSpan(9, 0, 0),   // Tokyo Standard Time
			Publisher.Sea => new TimeSpan(0, 0, 0),        //
			Publisher.NcTaiwan => new TimeSpan(8, 0, 0),  // Taipei Standard Time
			Publisher.NcWest => new TimeSpan(-5, 0, 0),   // Eastern Standard Time
			Publisher.Garena => new TimeSpan(7, 0, 0),    // SE Asia Standard Time
			_ => TimeZoneInfo.Local.BaseUtcOffset,
		};

		return TimeZoneInfo.CreateCustomTimeZone("BnsZoneInfo", offset, publisher.ToString(), publisher.ToString());
	}
	#endregion
}