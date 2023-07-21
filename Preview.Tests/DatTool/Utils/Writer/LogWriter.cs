using System.Diagnostics;

namespace Xylia.Preview.Data.Models.Util.Writer;

public static class LogWriter
{
	public static string RecordPath = Xylia.Configure.PathDefine.MainFolder + $@"\log\{ DateTime.Now:yyMMdd}.rec";

	public static void CreateLog(this object Txt)
	{
		if (Txt is null || Txt.ToString() is null) return;


		try
		{
			File.AppendAllText(RecordPath, Txt.ToString() + '\n');
		}
		catch
		{

		}
	}


	/// <summary>
	/// 日志输出级别（0:不输出  1:只输出必要内容  2:完全输出)
	/// </summary>
	public static int LogLevel = 2;

	public static void WriteLine(object Txt, int Level = 1, bool HasTimeInfo = true)
	{
		//如果日志定义级别大于等于当前消息级别，输出日志
		if (LogLevel < Level) return;

		Txt = (HasTimeInfo ? $"[{DateTime.Now}] " : null) + Txt;

#if (true)
		Trace.WriteLine(Txt);
#endif

		Txt.CreateLog();
	}
}