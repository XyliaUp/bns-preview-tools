
using System;
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xylia.Preview.Data.Helper;


namespace Xylia.Match.Util.Paks.Textures
{
	public abstract class IconOutBase : IDisposable
	{
		#region Constructor
		/// <summary>
		/// 游戏资源文件夹
		/// </summary>
		private readonly string _gameDirectory;

		public IconOutBase(string GameFolder)
		{
			this._gameDirectory = GameFolder;
		}
		#endregion

		#region	Fields
		protected DataTableSet set;

		/// <summary>
		/// 关联信息
		/// </summary>
		internal BlockingCollection<QuoteInfo> QuoteInfos = new();

		/// <summary>
		/// 结果目录
		/// </summary>
		public string OutputDirectory = null;

		/// <summary>
		/// 日志输出器
		/// </summary>

		protected OutLogHelper LogHelper = null;

		protected Action<string> Action = null;
		#endregion


		#region Functions
		public void LoadData(Action<string> Action)
		{
			#region Load 资源
			this.LogHelper = new OutLogHelper(Path.GetDirectoryName(this.OutputDirectory));  //设置日志输出路径
			this.Action = Action;

			set = new DataTableSet();
			set.LoadData(Folder: this._gameDirectory);
			#endregion

			#region 分析数据
			Action?.Invoke("正在分析图标数据...");
			set.IconTexture.TryLoad();

			//开始分析数据
			this.QuoteInfos = new();
			this.AnalyseSourceData();
			#endregion
		}

		/// <summary>
		/// 分析指定数据
		/// </summary>
		/// <exception cref="Exception"></exception>
		protected virtual void AnalyseSourceData() => throw new Exception("请在引用类中分析资源数据");

		/// <summary>
		/// 生成对应的图标文件
		/// </summary>
		/// <param name="SaveFormat"></param>
		public void SaveAsPicture(string SaveFormat)
		{
			#region	设置存储格式
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

			#region 开始处理
			//必须进行Initialize
			FileCache.PakData.Initialize(this._gameDirectory);

			//多线程处理
			int Count = 0;
			Directory.CreateDirectory(OutputDirectory);
			Parallel.ForEach(this.QuoteInfos, QuoteInfo =>
			{
				Action($"正在生成图标, 进度{ 100 * Count++ / this.QuoteInfos.Count  }%");

				string ItemMsg = $"数据ID { QuoteInfo.MainId } [{ QuoteInfo.Name }] ";
				var IconTexture = set.IconTexture[QuoteInfo.TextureAlias];
				if (IconTexture is null)
				{
					LogHelper.Record(ItemMsg + $"缺少道具图标", OutLogHelper.LogGroup.错误记录);
					return;
				}


				try
				{
					var bitmap = IconTexture.GetIcon(QuoteInfo.IconIndex);
					if (bitmap is null)
					{
						LogHelper.Record(ItemMsg + $"资源获取失败 ({ IconTexture.iconTexture })", OutLogHelper.LogGroup.错误记录);
						return;
					}

					lock (bitmap) bitmap = QuoteInfo.ProcessImage(bitmap);

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

					//判断是否存在非法字符
					if (OutName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
					{
						LogHelper.Record($"{ QuoteInfo.MainId } [{ QuoteInfo.Name }]  由于名称存在非法字符, 生成名称已调整。(非法字符是指<>?*\" |\\/等不可以用于文件名的符号)", OutLogHelper.LogGroup.生成日志);
						foreach (char c in Path.GetInvalidFileNameChars()) OutName = OutName.Replace(c.ToString(), "_");
					}

					//生成最终存储文件名
					string FinalPath = OutputDirectory + @"\" + OutName + ".png";
					#endregion

					#region 存储图片
					bitmap.Save(FinalPath, ImageFormat.Png);
					bitmap.Dispose();
					bitmap = null;
					#endregion
				}
				catch (Exception ee)
				{
					LogHelper.Record($@"{ QuoteInfo.MainId } => { ee.Message }", OutLogHelper.LogGroup.错误记录);
				}
			});
			#endregion


			#region 资源清理 
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
						// TODO: 释放托管状态(托管对象)

					}

					this.LogHelper = null;
					this.set?.Dispose();
					this.set = null;

					// TODO: 释放未托管的资源(未托管的对象)并替代终结器
					// TODO: 将大型Fields设置为 null
					disposedValue = true;
				}
			}
			catch
			{

			}
		}

		// // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
		// ~IconTextureBase()
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
		#endregion
	}
}