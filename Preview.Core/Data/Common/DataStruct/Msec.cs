using System.Text;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;
public struct Msec : IFormattable
{
	#region Const
	/// <summary>
	/// Represents the number of ticks in 1 millisecond. This field is constant.
	/// </summary>
	public const int TicksPerMillisecond = 1;

	public const int TicksPerSecond = TicksPerMillisecond * 1000;   // 10,000

	public const int TicksPerMinute = TicksPerSecond * 60;         // 600,000

	public const int TicksPerHour = TicksPerMinute * 60;        // 36,000,000

	public const int TicksPerDay = TicksPerHour * 24;          // 864,000,000
	#endregion


	#region Constructors
	private readonly int value;

	public Msec(int value) => this.value = Math.Abs(value);

	public Msec(int minutes, int seconds) : this(0, minutes, seconds) { }

	public Msec(int hours, int minutes, int seconds) => this.value = ((hours * 60 + minutes) * 60 + seconds) * 1000;
	#endregion

	#region Properties
	public readonly int Days => value / TicksPerDay;

	public readonly int Hours => value / TicksPerHour % 24;

	public readonly int Minutes => value / TicksPerMinute % 60;

	public readonly int Seconds => value / TicksPerSecond % 60;

	public readonly int Milliseconds => (value / TicksPerMillisecond % 1000);

	public readonly double TotalDays => ((double)value) / TicksPerDay;

	public readonly double TotalHours => (double)value / TicksPerHour;

	public readonly double TotalMinutes => (double)value / TicksPerMinute;

	public readonly double TotalSeconds => (double)value / TicksPerSecond;
	#endregion


	#region Methods
	public override string ToString() => value.ToString();

	public string ToString(string format, IFormatProvider formatProvider)
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
		FormatCustomized(this, format, ref vlb);
		return vlb.ToString();
	}

	internal static string FormatC(Msec value)
	{
		if (value == 0) return null;


		var MorningName = new Ref<Text>("Name.Time.Morning").GetText();
		var AfternoonName = new Ref<Text>("Name.Time.Afternoon").GetText();

		var DayName = new Ref<Text>("Name.Time.day").GetText() ?? ":";
		var HourName = new Ref<Text>("Name.Time.hour").GetText() ?? ":";
		var MinuteName = new Ref<Text>("Name.Time.minute").GetText() ?? ":";
		var SecondName = new Ref<Text>("Name.Time.second").GetText();


		var sb = new StringBuilder(256);

		if (value.Days > 0) sb.Append(value.Days + DayName);
		if (value.Hours > 0) sb.Append(value.Hours + HourName);
		if (value.Minutes > 0) sb.Append(value.Minutes + MinuteName);
		if (value.Seconds > 0) sb.Append(value.Seconds + SecondName);

		return sb.ToString();
	}

	internal static string FormatG(Msec value)
	{
		return $"{value.Days}.{value.Hours:00}:{value.Minutes:00}:{value.Seconds:00}";
	}

	private static void FormatCustomized(Msec value, scoped ReadOnlySpan<char> format, ref StringBuilder result)
	{
		for (int i = 0; i < format.Length;)
		{
			char ch = format[i];
			int tokenLen;

			switch (ch)
			{
				case 'h':
					i += tokenLen = TimeFormat.ParseRepeatPattern(format, i, ch);
					TimeFormat.FormatDigits(ref result, value.Hours, tokenLen, 2);
					break;
				case 'm':
					i += tokenLen = TimeFormat.ParseRepeatPattern(format, i, ch);
					TimeFormat.FormatDigits(ref result, value.Minutes, tokenLen, 2);
					break;
				case 's':
					i += tokenLen = TimeFormat.ParseRepeatPattern(format, i, ch);
					TimeFormat.FormatDigits(ref result, value.Seconds, tokenLen, 2);
					break;
				case 'd':
					i += tokenLen = TimeFormat.ParseRepeatPattern(format, i, ch);
					TimeFormat.FormatDigits(ref result, value.Days, tokenLen, 8);
					break;
				default:
					i++;
					result.Append(ch);
					break;
			}
		}
	}
	#endregion

	#region Operator
	public static implicit operator Msec(int value) => new(value);

	public static bool operator ==(Msec a, Msec b) => a.value == b.value;

	public static bool operator !=(Msec a, Msec b) => !(a == b);

	public static Msec operator +(Msec a, Msec b) => a.value + b.value;

	public static Msec operator -(Msec a, Msec b) => a.value - b.value;


	public readonly bool Equals(Msec other) => value == other.value;

	public override bool Equals(object obj) => obj is Msec other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(value);
	#endregion
}

public static class TimeFormat
{
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




	public static string GetHourPart(int hour) => hour < 12 ? "Name.Time.Morning".GetText() : "Name.Time.Afternoon".GetText();
}