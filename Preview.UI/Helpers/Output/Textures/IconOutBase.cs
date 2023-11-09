using System.IO;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;

using Serilog;
using Serilog.Events;

using SkiaSharp;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Helpers.Output.Textures;
public abstract class IconOutBase : IDisposable
{
	#region Constructor
	private readonly string _gameDirectory;
	private readonly string _outputDirectory;

	protected BnsDatabase set;
	protected readonly ILogger logger;

	public IconOutBase(string GameFolder, string OutputFolder)
	{
		// path
		_gameDirectory = GameFolder;
		_outputDirectory = OutputFolder;

		// log
		string template = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Message:lj}{NewLine}";
		string folder = Path.GetDirectoryName(_outputDirectory) + "\\Log";

		logger = new LoggerConfiguration()
			.WriteTo.Logger(lc => lc
				.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information)
				.WriteTo.File(Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}.log"), outputTemplate: template))
		   .WriteTo.Logger(lc => lc
				.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error)
				.WriteTo.File(Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}_error.log"), outputTemplate: template))
		   .CreateLogger();
	}
	#endregion


	#region Methods
	public async Task LoadData(CancellationToken cancellationToken)
	{
		logger.Information("正在分析图标数据...");
		set = new BnsDatabase(DefaultProvider.Load(UserSettings.Default.GameFolder));
		await set.IconTexture.LoadAsync();
	}

	public async Task Output(string format, CancellationToken cancellationToken)
	{
		using var provider = new GameFileProvider(_gameDirectory, true);
		Directory.CreateDirectory(_outputDirectory);

		await Task.Run(() => AnalyseSourceData(provider, format, cancellationToken) , cancellationToken);
	}

	protected abstract void AnalyseSourceData(DefaultFileProvider provider, string format, CancellationToken cancellationToken);

	protected void Save(ref SKBitmap source, string name)
	{
		if (source is null) return;

		// Invalid chars
		if (name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
		{
			logger.Error($"{name} 由于名称存在非法字符, 生成名称已调整。(非法字符是指<>?*\" |\\/等不可以用于文件名的符号)");
			foreach (char c in Path.GetInvalidFileNameChars()) name = name.Replace(c.ToString(), "_");
		}

		source.Save(_outputDirectory + @"\" + name + ".png");
		source.Dispose();
		source = null;
	}
	#endregion

	#region Dispose
	public void Dispose()
	{
		set?.Dispose();
		set = null;

		GC.SuppressFinalize(this);
		GC.Collect();
	}
	#endregion
}