
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Extension;

public static partial class TimeExtension
{
	public static string GetHourPart(this int hour) => GetHourPart((byte)hour);

	public static string GetHourPart(this byte hour) => hour < 12 ? "Name.Time.Morning".GetText() : "Name.Time.Afternoon".GetText();
}