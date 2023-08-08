
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Extension;
public static partial class TimeExtension
{
	public static string GetHourPart(this int hour) => GetHourPart((byte)hour);

	public static string GetHourPart(this sbyte hour) => hour < 12 ? "Name.Time.Morning".GetText() : "Name.Time.Afternoon".GetText();



	public static string ToMyString(this TimeSpan ts)
	{
		string result = null;

		if (ts.Days > 0) result += ts.Days + "天";
		if (ts.Hours > 0) result += ts.Hours + "小时";
		if (ts.Minutes > 0) result += ts.Minutes + "分钟";
		if (ts.Seconds > 0) result += ts.Seconds + "秒";

		return result;
	}
}