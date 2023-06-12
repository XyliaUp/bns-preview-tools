using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using HZH_Controls.Controls;

using Xylia.Match.Windows.Forms;

namespace Xylia.Match.Windows.Forms
{
	public partial class LoggerFrm : Form
	{
		#region Fields
		public List<object> lstSource = new();
		public static string LogPath = Xylia.Configure.PathDefine.MainFolder + $@"\Log\{ DateTime.UtcNow:yyMMdd}.log";
		#endregion

		#region Constructor
		/// <summary>
		/// 日志系统
		/// </summary>
		public LoggerFrm()
		{
			this.InitializeComponent();
			ClearOverdue();

			#region Initialize
			var lstCulumns = new List<DataGridViewColumnEntity>
			{
				new DataGridViewColumnEntity() { DataField = "ID", HeadText = "ID", Width = 50, WidthType = SizeType.Absolute },
				new DataGridViewColumnEntity() { DataField = "Time", HeadText = "时间", Width = 60, WidthType = SizeType.Percent, Format = (a) => { return ((DateTime)a).ToString("yyyy-MM-dd HH:mm:ss"); } },
				new DataGridViewColumnEntity() { DataField = "Level", HeadText = "级别", Width = 40, WidthType = SizeType.Percent },
				new DataGridViewColumnEntity() { DataField = "Content", HeadText = "内容", Width = 70, WidthType = SizeType.Percent }
			};

			this.ucDataGridView2.Columns = lstCulumns;
			this.ucDataGridView2.IsShowCheckBox = false;
			#endregion
		}
		#endregion


		#region Functions
		/// <summary>
		/// 自动清理时间过久日志
		/// </summary>											  
		private static void ClearOverdue()
		{
			int DayInterval = 15;
			string DirPath = Xylia.Configure.PathDefine.MainFolder + $@"\Log";

			if (Directory.Exists(DirPath))
			{
				foreach (FileInfo fi in new DirectoryInfo(DirPath).GetFiles())
				{
					try
					{
						if (fi.Name == "Updata-dev.log") fi.Delete();
						else
						{
							int y = int.Parse(DateTime.Now.Year.ToString()[..2] + fi.Name[..2]);
							int m = int.Parse(fi.Name.Substring(2, 2));
							int d = int.Parse(fi.Name.Substring(4, 2));

							if ((DateTime.Now - new DateTime(y, m, d)).Days > DayInterval) fi.Delete();
						}
					}
					catch
					{
						continue;
					}
				}
			}
		}

		public void Write(object Msg, MsgInfo.MsgLevel msgLevel = MsgInfo.MsgLevel.信息)
		{
			if (Msg is Array msg)
			{
				foreach (var M in msg) Write(M, msgLevel);
				return;
			}

			if (Msg.ToString().Contains("由于权限不足")) return;

			#region 保存到内存
			lstSource.Add(new MsgInfo()
			{
				ID = lstSource.Count + 1,
				Level = msgLevel,
				Content = Msg.ToString(),
				Backup = "null",
				Time = DateTime.Now
			});

			this.ucDataGridView2.DataSource = lstSource;
			#endregion

			#region 写出日志文件
			if (!Directory.Exists(Path.GetDirectoryName(LogPath))) Directory.CreateDirectory(Path.GetDirectoryName(LogPath));
			bool isNew = !File.Exists(LogPath);

			using StreamWriter sw = new(new FileStream(LogPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite));
			var Assemly = Assembly.GetExecutingAssembly().GetName();
			sw.BaseStream.Seek(0, SeekOrigin.End);

			if (isNew) sw.WriteLine($"-------------------------------------------------------------------\n" +
									$"由 { Assemly.FullName } Initialize创建\n" +
									$"-------------------------------------------------------------------");

			string OutMsg = $"{ DateTime.Now } || { Assemly.Name + "(" + Assemly.Version + ")" } || { msgLevel } || {  Msg }";

			sw.WriteLine(OutMsg);
			sw.Flush();

			#endregion
		}

		private void Logger_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		private void LoggerFrm_Load(object sender, EventArgs e)
		{

		}
		#endregion
	}

	public class MsgInfo
	{
		public int ID { get; set; }
		public DateTime Time { get; set; }
		public MsgLevel Level { get; set; }
		public string Content { get; set; }
		public string Backup { get; set; }

		public enum MsgLevel
		{
			调试 = 0,
			信息,
			警告,
			错误,
			严重,
			崩溃
		}
	}
}


public static class Log
{
	private static readonly LoggerFrm m_Logger = new();

	public static void Show() => m_Logger.Show();

	public static void Write(object Msg, MsgInfo.MsgLevel msgLevel = MsgInfo.MsgLevel.信息)
	{
		if (m_Logger == null) return;

		m_Logger.Write(Msg, msgLevel);
		Console.WriteLine(Msg);
	}
}