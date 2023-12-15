using System;
using HtmlAgilityPack;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.UI.Documents;
public class Timer : Element
{
	#region Fields
	public int Id;

	public TimerType Type;

	public enum TimerType
	{
		[Name("dhm-plusonesec")]
		dhmPlusonesec,

		[Name("dhms-plusonesec")]
		dhmsPlusonesec,

		[Name("hms-plusonesec")]
		hmsPlusonesec,

		[Name("min-plusonesec")]
		minPlusonesec,

		[Name("dhms-rounddown")]
		dhmsRounddown,

		[Name("hms-rounddown-plusonesec")]
		hmsRounddownPlusonesec,

		[Name("hms-format-colon")]
		hmsFormatColon,

		[Name("hm-format-colon-plusonemin")]
		hmFormatColonPlusonemin,
	}
	#endregion


	#region Properties
	public Time64 Value { get; set; }

	public Msec Span => Value - DateTime.Now;
	#endregion


	#region Methods
	protected internal override void Load(HtmlNode node)
	{
		Id = node.GetAttributeValue("id", 0);
		Type = node.GetAttributeValue("type", (TimerType)default);
	}

	public override string ToString()
	{
		var span = this.Span;

		var format = Type.ToString();
		if (format.Contains("Plusonesec")) span -= new Msec(0, 1);
		else if (format.Contains("Plusonemin")) span -= new Msec(1, 0);

		return span.ToString(default, null);
	}

	public static bool Valid(DayOfWeek DayOfWeek, int ResetTime, out Time64 Time)
	{
		var now = DateTime.Now;

		int days = DayOfWeek - now.DayOfWeek;
		if (days < 0) days += 7;

		// reset hour not arrived
		if (days == 6 && now.Hour < ResetTime) days -= 7;

		// get time range
		var startTime = now.Date.AddDays(days).AddHours(ResetTime);
		var endTime = startTime.AddDays(1);


		bool status = startTime <= now && endTime > now;
		Time = (Time64)(status ? endTime : startTime);
		return status;
	}
	#endregion
}