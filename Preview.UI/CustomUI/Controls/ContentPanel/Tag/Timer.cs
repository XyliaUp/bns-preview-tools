using System;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Tag
{
	public sealed class Timer
	{
		public enum TimerType
		{
			[Signal("dhm-plusonesec")]
			DhmPlusonesec,

			[Signal("dhms-plusonesec")]
			DhmsPlusonesec,

			[Signal("hms-plusonesec")]
			HmsPlusonesec,

			[Signal("min-plusonesec")]
			MinPlusonesec,



			[Signal("dhms-rounddown")]
			DhmsRounddown,


			[Signal("hms-rounddown-plusonesec")]
			HmsRounddownPlusonesec,


			[Signal("hms-format-colon")]
			HmsFormatColon,

			[Signal("hm-format-colon-plusonemin")]
			HmFormatColonPlusonemin,
		}


		#region Constructor
		private DateTime Value { get; set; }

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

		public string ToString(string Format)
		{
			var span = this.Span;

			if (Format.Contains("plusonesec")) span = span.Add(new TimeSpan(0, 0, 1));
			else if (Format.Contains("plusonemin")) span = span.Add(new TimeSpan(0, 1, 0));


			if (Format.Contains("dhm")) return string.Format("{0:dd}日 {0:hh}小时 {0:mm}分钟", span);
			else if (Format.Contains("dhms")) return string.Format("{0:dd}日 {0:hh}小时 {0:mm}分钟 {0:ss}秒", span);
			else if (Format.Contains("hms")) return string.Format("{0:hh}小时 {0:mm}分钟 {0:ss}秒", span);
			else return span.ToString();
		}
		#endregion
	}
}