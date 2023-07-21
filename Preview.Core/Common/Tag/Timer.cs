using HtmlAgilityPack;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Tag;
public sealed class Timer : ITag
{
	#region Fields
	public readonly int id;

	public readonly TimerType type;

	public enum TimerType
	{
		[Signal("dhm-plusonesec")]
		dhmPlusonesec,

		[Signal("dhms-plusonesec")]
		dhmsPlusonesec,

		[Signal("hms-plusonesec")]
		hmsPlusonesec,

		[Signal("min-plusonesec")]
		minPlusonesec,



		[Signal("dhms-rounddown")]
		dhmsRounddown,


		[Signal("hms-rounddown-plusonesec")]
		hmsRounddownPlusonesec,


		[Signal("hms-format-colon")]
		hmsFormatColon,

		[Signal("hm-format-colon-plusonemin")]
		hmFormatColonPlusonemin,
	}
	#endregion

	#region Properties
	private DateTime Value { get; set; }
	#endregion



	#region Constructor
	public Timer(HtmlNode node)
	{
		id = (node.Attributes["id"]?.Value).ToInt();
		type = (node.Attributes["id"]?.Value).ToEnum<TimerType>();
	}


	public Timer(DateTime Value) => this.Value = Value;

	public Timer(DayOfWeek DayOfWeek, int ResetTime)
	{
		var TodayDate = DateTime.Now;

		//获取到指定重置日期
		var DiffDay = (int)DayOfWeek - (int)TodayDate.DayOfWeek;
		if (DiffDay < 0) DiffDay += 7;

		//获取到指定重置时间的剩余时间
		this.Value = TodayDate.Date.AddDays(DiffDay).AddHours(ResetTime);
	}
	#endregion




	#region Functions
	public TimeSpan Span => Value - DateTime.Now;

	public override string ToString()
	{
		var span = this.Span;


		var Format = type.ToString();
		if (Format.Contains("Plusonesec")) span = span.Add(new TimeSpan(0, 0, 1));
		else if (Format.Contains("Plusonemin")) span = span.Add(new TimeSpan(0, 1, 0));


		if (Format.Contains("dhm")) return string.Format("{0:dd}日 {0:hh}小时 {0:mm}分钟", span);
		else if (Format.Contains("dhms")) return string.Format("{0:dd}日 {0:hh}小时 {0:mm}分钟 {0:ss}秒", span);
		else if (Format.Contains("hms")) return string.Format("{0:hh}小时 {0:mm}分钟 {0:ss}秒", span);
		else return span.ToString();
	}
	#endregion
}