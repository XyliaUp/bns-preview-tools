using System.Text;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;
/// <summary>
///  Represents an instant in time
/// </summary>
/// <param name="Ticks"></param>
/// <param name="Publisher"></param>
public struct Time64(long ticks) : IFormattable
{
	#region Properties
	private const int HoursPerDay = 24;
	private const long TicksPerSecond = 1;
	private const long TicksPerMinute = TicksPerSecond * 60;
	private const long TicksPerHour = TicksPerMinute * 60;
	private const long TicksPerDay = TicksPerHour * HoursPerDay;

	// Number of milliseconds per time unit
	private const int MillisPerSecond = 1000;
	private const int MillisPerMinute = MillisPerSecond * 60;
	private const int MillisPerHour = MillisPerMinute * 60;
	private const int MillisPerDay = MillisPerHour * HoursPerDay;

	// Number of days in a non-leap year
	private const int DaysPerYear = 365;
	// Number of days in 4 years
	private const int DaysPer4Years = DaysPerYear * 4 + 1;       // 1461
																 // Number of days in 100 years
	private const int DaysPer100Years = DaysPer4Years * 25 - 1;  // 36524
																 // Number of days in 400 years
	private const int DaysPer400Years = DaysPer100Years * 4 + 1; // 146097
																 // Number of days from 1/1/0001 to 12/31/1969
	internal const int DaysTo1970 = DaysPer400Years * 4 + DaysPer100Years * 3 + DaysPer4Years * 17 + DaysPerYear; // 719,162
																												  // Number of days from 1/1/0001 to 12/31/9999
	private const int DaysTo10000 = DaysPer400Years * 25 - 366;  // 3652059

	internal const long MinTicks = 0;
	internal const long MaxTicks = DaysTo10000 * TicksPerDay - 1;

	// Euclidean Affine Functions Algorithm (EAF) constants

	// Constants used for fast calculation of following subexpressions
	//      x / DaysPer4Years
	//      x % DaysPer4Years / 4
	private const uint EafMultiplier = (int)(((1UL << 32) + DaysPer4Years - 1) / DaysPer4Years);   // 2,939,745
	private const uint EafDivider = EafMultiplier * 4;                                              // 11,758,980

	private const ulong TicksPer6Hours = TicksPerHour * 6;
	private const int March1BasedDayOfNewYear = 306;              // Days between March 1 and January 1


	public readonly ulong Ticks => (ulong)ticks;

	// Returns the year part of this DateTime. The returned value is an
	// integer between 1 and 9999.
	//
	public int Year
	{
		get
		{
			// y100 = number of whole 100-year periods since 1/1/0001
			// r1 = (day number within 100-year period) * 4
			(uint y100, uint r1) = Math.DivRem(((uint)(Ticks / TicksPer6Hours) | 3U), DaysPer400Years);

			return 1970 + (int)(100 * y100 + (r1 | 3u) / DaysPer4Years);
		}
	}

	// Returns the month part of this DateTime. The returned value is an
	// integer between 1 and 12.
	public int Month
	{
		get
		{
			// r1 = (day number within 100-year period) * 4
			uint r1 = (((uint)(Ticks / TicksPer6Hours) | 3U) + 1224) % DaysPer400Years;
			long u2 = Math.BigMul((int)EafMultiplier, (int)(r1 | 3U));
			ushort daySinceMarch1 = (ushort)((uint)u2 / EafDivider);
			int n3 = 2141 * daySinceMarch1 + 197913;
			return (ushort)(n3 >> 16) - (daySinceMarch1 >= March1BasedDayOfNewYear ? 12 : 0);
		}
	}

	// Returns the day-of-month part of this DateTime. The returned
	// value is an integer between 1 and 31.
	public int Day
	{
		get
		{
			// r1 = (day number within 100-year period) * 4
			uint r1 = (((uint)(Ticks / TicksPer6Hours) | 3U) + 1224) % DaysPer400Years;
			long u2 = Math.BigMul((int)EafMultiplier, (int)(r1 | 3U));
			ushort daySinceMarch1 = (ushort)((uint)u2 / EafDivider);
			int n3 = 2141 * daySinceMarch1 + 197913;
			// Return 1-based day-of-month
			return (ushort)n3 / 2141 + 1;
		}
	}

	// Returns the hour part of this DateTime. The returned value is an
	// integer between 0 and 23.
	public int Hour => (int)((uint)(Ticks / TicksPerHour) % 24);

	// Returns the minute part of this DateTime. The returned value is
	// an integer between 0 and 59.
	public int Minute => (int)((Ticks / TicksPerMinute) % 60);

	// Returns the second part of this DateTime. The returned value is
	// an integer between 0 and 59.
	public int Second => (int)((Ticks / TicksPerSecond) % 60);
	#endregion


	#region Interface	
	public string ToString(string format, IFormatProvider formatProvider) => TimeFormat.Format(this + BnsTimeZoneInfo.FromPublisher()!.Offset, format, formatProvider);

	public override string ToString() => ToString(null, null);

	public bool Equals(Time64 other) => Ticks == other.Ticks;

	public override bool Equals(object obj) => obj is Time64 other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(Ticks);

	public static bool operator ==(Time64 a, Time64 b) => a.Ticks == b.Ticks;

	public static bool operator !=(Time64 a, Time64 b) => !(a == b);

	public static Msec operator -(Time64 a, Time64 b) => (int)((a.Ticks - b.Ticks) * 1000);
	public static Msec operator +(Time64 a, Time64 b) => (int)((a.Ticks + b.Ticks) * 1000);

	public static Time64 operator -(Time64 a, Msec b) => (long)(a.Ticks - b.TotalSeconds);
	public static Time64 operator +(Time64 a, Msec b) => (long)(a.Ticks + b.TotalSeconds);
	#endregion


	#region Static Methods
	public static implicit operator long(Time64 time) => (long)time.Ticks;
	public static implicit operator Time64(long ticks) => new Time64(ticks);

	public static implicit operator Time64(DateTime dateTime) => Parse(dateTime);

	public static Time64 Parse(string s) => Parse(DateTime.TryParse(s, out var result) ? result : default);

	public static Time64 Parse(DateTime time)
	{
		return new Time64((time - new DateTime(1970, 1, 1)).Ticks / 10000000) - BnsTimeZoneInfo.FromPublisher()!.Offset;
	}
	#endregion
}


internal static class TimeFormat
{
	public static string Format(Time64 value, string format, IFormatProvider formatProvider)
	{
		if (string.IsNullOrEmpty(format))
		{
			return FormatC(value); // formatProvider ignored, as "c" is invariant
		}

		if (format.Length == 1)
		{
			char c = format[0];

			if (c == 'c' || (c | 0x20) == 't') // special-case to optimize the default TimeSpan format
			{
				return FormatC(value); // formatProvider ignored, as "c" is invariant
			}

			if (c == 'g')
			{
				return FormatG(value);
			}

			throw new FormatException("Format_InvalidString");
		}

		var vlb = new StringBuilder(256);
		FormatCustomized(value, format, ref vlb);
		return vlb.ToString();
	}

	internal static string FormatC(Time64 value)
	{
		return $"{value.Year}/{value.Month}/{value.Day} {value.Hour}:{value.Minute:00}:{value.Second:00}";
	}

	internal static string FormatG(Time64 value)
	{
		return null;
	}

	private static void FormatCustomized(Time64 value, scoped ReadOnlySpan<char> format, ref StringBuilder result)
	{
		for (int i = 0; i < format.Length;)
		{
			char ch = format[i];
			int tokenLen;

			switch (ch)
			{
				case 'h':
					i += tokenLen = ParseRepeatPattern(format, i, ch);
					FormatDigits(ref result, value.Hour, tokenLen, 2);
					break;
				case 'm':
					i += tokenLen = ParseRepeatPattern(format, i, ch);
					FormatDigits(ref result, value.Minute, tokenLen, 2);
					break;
				case 's':
					i += tokenLen = ParseRepeatPattern(format, i, ch);
					FormatDigits(ref result, value.Second, tokenLen, 2);
					break;
				case 'd':
					i += tokenLen = ParseRepeatPattern(format, i, ch);
					FormatDigits(ref result, value.Day, tokenLen, 8);
					break;
				default:
					i++;
					result.Append(ch);
					break;
			}
		}
	}


	internal static void FormatDigits(ref StringBuilder outputBuffer, int value, int length, int maximumLength)
	{
		if (length > maximumLength)
			throw new FormatException("Format_InvalidString");

		outputBuffer.Append(value.ToString(new string('0', length)));
	}

	internal static int ParseRepeatPattern(ReadOnlySpan<char> format, int pos, char patternChar)
	{
		int index = pos + 1;
		while ((uint)index < (uint)format.Length && format[index] == patternChar)
		{
			index++;
		}
		return index - pos;
	}


	public static string AMPM(int hour) => hour < 12 ? "Name.Time.Morning".GetText() : "Name.Time.Afternoon".GetText();
}