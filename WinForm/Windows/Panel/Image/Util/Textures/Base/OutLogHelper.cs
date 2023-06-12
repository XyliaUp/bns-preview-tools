using System;
using System.Collections.Generic;
using System.IO;

namespace Xylia.Match.Util.Paks.Textures
{
	/// <summary>
	/// 日志系统
	/// </summary>
	public class OutLogHelper : IDisposable
	{
		/// <summary>
		/// 文件夹路径
		/// </summary>
		public string Path = null;
		private string m_path = null;

		public OutLogHelper(string FolderPath)
		{
			this.Path = FolderPath + $@"\日志";
			this.m_path = this.Path + $@"\{ DateTime.Now:yyMMdd}";
		}


		public enum LogGroup
		{
			生成日志 = 0,
			错误记录
		}






		#region Functions
		private Dictionary<string, StreamWriter> StreamWriters;
		private bool disposedValue;

		/// <summary>
		/// 获得文本流
		/// </summary>
		/// <param name="LogPath"></param>
		/// <returns></returns>
		public StreamWriter GetStreamWriter(string LogPath)
		{
			if (StreamWriters is null) StreamWriters = new Dictionary<string, StreamWriter>();

			if (StreamWriters.ContainsKey(LogPath)) return StreamWriters[LogPath];
			else
			{
				string Dir = System.IO.Path.GetDirectoryName(LogPath);
				if (!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);

				StreamWriter sw = File.AppendText(LogPath);
				sw.AutoFlush = true;

				StreamWriters.Add(LogPath, sw);

				return sw;
			}
		}

		/// <summary>
		/// 记录信息
		/// </summary>
		/// <param name="Msg"></param>
		/// <param name="logGroup"></param>
		public void Record(string Msg, LogGroup logGroup)
		{
			try
			{
				var sw = GetStreamWriter(m_path + $"_{ logGroup }.txt");
				lock (sw) sw.WriteLine($"[{ DateTime.Now }]  { Msg }");

				if (logGroup == LogGroup.错误记录)
					System.Diagnostics.Debug.WriteLine($"[{ DateTime.Now }]  { Msg }");
			}
			catch
			{

			}
		}

		public void Record(string Msg, object ItemId, string ItemName)
		{
			Record($"物品Id { ItemId } [{ ItemName }], { Msg }。".Replace("。。", "。"), LogGroup.错误记录);
		}
		#endregion


		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					foreach (var s in StreamWriters)
						s.Value?.Dispose();

					StreamWriters?.Clear();
					StreamWriters = null;
				}

				// TODO: 释放未托管的资源(未托管的对象)并重写终结器
				// TODO: 将大型Fields设置为 null
				disposedValue = true;
			}
		}

		// // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
		// ~OutLogHelper()
		// {
		//     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”Functions中
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”Functions中
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

	}
}
