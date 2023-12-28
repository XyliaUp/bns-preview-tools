using System;
using HtmlAgilityPack;
using Xylia.Preview.Data.Common.DataStruct;
using static Xylia.Preview.Data.Common.DataStruct.MsecFormat;

namespace Xylia.Preview.UI.Documents;
public class Timer : Element
{
	#region Fields
	public int Id;

	public MsecFormatType Type;
	#endregion


	#region Properties
	public Time64 Value { get; set; }

	public Msec Span => Value - DateTime.Now;
	#endregion


	#region Methods
	protected internal override void Load(HtmlNode node)
	{
		Id = node.GetAttributeValue("id", 0);
		Type = node.GetAttributeValue("type", (MsecFormatType)default);
	}

	public override string ToString() => Span.ToString(Type);

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