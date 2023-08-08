using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

using CUE4Parse.BNS;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Properties;

namespace Xylia.Match.Util.Paks.Textures;
public abstract class IconOutBase : IDisposable
{
	#region Constructor
	private readonly string _gameDirectory;

	public IconOutBase(string GameFolder)
	{
		this._gameDirectory = GameFolder;
	}
	#endregion

	#region	Fields
	protected TableSet set;

	internal BlockingCollection<QuoteInfo> QuoteInfos = new();

	public string OutputDirectory = null;

	protected OutLogHelper LogHelper = null;

	protected Action<string> Action = null;
	#endregion



	#region Functions
	public void LoadData(Action<string> action)
	{
		#region res
		action?.Invoke($"正在初始化数据");

		this.LogHelper = new OutLogHelper(Path.GetDirectoryName(this.OutputDirectory));  
		this.Action = action;

		set = new TableSet();
		set.LoadData(Folder: this._gameDirectory);
		#endregion

		#region analyse
		action?.Invoke("正在分析图标数据...");
		set.IconTexture.TryLoad();

		action?.Invoke("正在分析物品数据...");
		this.QuoteInfos = new();
		this.AnalyseSourceData();
		#endregion
	}

	protected virtual void AnalyseSourceData() => throw new NotImplementedException();

	public void SaveAsPicture(string SaveFormat)
	{
		#region	format
		//去除格式中括号内空格
		bool HasSaveFormat = false;
		if (!string.IsNullOrWhiteSpace(SaveFormat))
		{
			HasSaveFormat = true;

			SaveFormat = SaveFormat.ToLower();
			SaveFormat = new Regex(@"\[\s+", RegexOptions.Singleline).Replace(SaveFormat, "[");
			SaveFormat = new Regex(@"\s+\]", RegexOptions.Singleline).Replace(SaveFormat, "]");
		}
		#endregion

		#region Data
		var mode = Settings.LoadMode;
		Settings.LoadMode = LoadMode.LoadOnInit;

		var PakData = new GameFileProvider(this._gameDirectory);
		Settings.LoadMode = mode;

		int Count = 0;
		Directory.CreateDirectory(OutputDirectory);
		Parallel.ForEach(this.QuoteInfos, QuoteInfo =>
		{
			Action($"正在生成图标, { 100 * Count++ / this.QuoteInfos.Count  }%");

			string ItemMsg = $"id: { QuoteInfo.MainId } [{ QuoteInfo.Name }]";


			try
			{
				var bitmap = QuoteInfo.Icon.GetIcon(set , PakData);
				if (bitmap is null)
				{
					LogHelper.Record(ItemMsg + $" 资源获取失败 ({QuoteInfo.Icon})", OutLogHelper.LogGroup.error);
					return;
				}

#pragma warning disable CS0728 
				lock (bitmap) bitmap = QuoteInfo.ProcessImage(bitmap);
#pragma warning restore CS0728 

				#region 输出名称处理
				string MainId = QuoteInfo.MainId.ToString();
				string OutName = $"{ MainId }_{ QuoteInfo.Name }";

				if (!HasSaveFormat) OutName = MainId;
				else
				{
					//由于正则对中文支持问题, 其他中文情况单独列出
					OutName = SaveFormat
					   .Replace("[名称]", QuoteInfo.Name)
					   .Replace("[name]", QuoteInfo.Name).Replace("[name2]", QuoteInfo.Name)
					   .Replace("[alias]", QuoteInfo.Alias).Replace("[别名]", QuoteInfo.Alias)
					   .Replace("[id]", MainId).Replace("[编号]", MainId);
				}

				// Invalid chars
				if (OutName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				{
					LogHelper.Record($"{ QuoteInfo.MainId } [{ QuoteInfo.Name }]  由于名称存在非法字符, 生成名称已调整。(非法字符是指<>?*\" |\\/等不可以用于文件名的符号)", OutLogHelper.LogGroup.log);
					foreach (char c in Path.GetInvalidFileNameChars()) OutName = OutName.Replace(c.ToString(), "_");
				}
				#endregion


				bitmap.Save(OutputDirectory + @"\" + OutName + ".png", ImageFormat.Png);
				bitmap.Dispose();
				bitmap = null;
			}
			catch (Exception ee)
			{
				LogHelper.Record($@"{ QuoteInfo.MainId } => { ee.Message }", OutLogHelper.LogGroup.error);
			}
		});
		#endregion


		#region Dispose 
		this.QuoteInfos = null;
		GC.Collect();
		#endregion
	}
	#endregion

	#region Dispose
	private bool disposedValue;

	protected virtual void Dispose(bool disposing)
	{
		try
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.LogHelper = null;
					this.set?.Dispose();
					this.set = null;
				}

				disposedValue = true;
			}
		}
		catch
		{

		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}

public class OutLogHelper : IDisposable
{
	public string Path = null;
	private string m_path = null;

	public OutLogHelper(string FolderPath)
	{
		this.Path = FolderPath + $@"\logs";
		this.m_path = this.Path + $@"\{DateTime.Now:yyMMdd}";
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
			var sw = GetStreamWriter(m_path + $"_{logGroup}.txt");
			lock (sw) sw.WriteLine($"[{DateTime.Now}]  {Msg}");
		}
		catch
		{

		}
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