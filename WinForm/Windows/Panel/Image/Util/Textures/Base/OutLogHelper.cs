using System.Diagnostics;
using System.IO;

namespace Xylia.Match.Util.Paks.Textures;
public class OutLogHelper : IDisposable
{
	public string Path = null;
	private string m_path = null;

	public OutLogHelper(string FolderPath)
	{
		this.Path = FolderPath + $@"\日志";
		this.m_path = this.Path + $@"\{ DateTime.Now:yyMMdd}";
	}


	public enum LogGroup
	{
		log = 0,
		error
	}






	#region Functions
	private Dictionary<string, StreamWriter> StreamWriters;

	public StreamWriter GetStreamWriter(string LogPath)
	{
		StreamWriters ??= new Dictionary<string, StreamWriter>();

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

	public void Record(string Msg, LogGroup logGroup)
	{
		try
		{
			var sw = GetStreamWriter(m_path + $"_{ logGroup }.txt");
			lock (sw) sw.WriteLine($"[{ DateTime.Now }]  { Msg }");
		}
		catch
		{

		}
	}

	public void Record(string Msg, object ItemId, string ItemName)
	{
		Record($"物品Id { ItemId } [{ ItemName }], { Msg }。", LogGroup.error);
	}
	#endregion


	#region IDisposable
	private bool disposedValue;

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

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}