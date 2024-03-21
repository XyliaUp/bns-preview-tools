using System.IO;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.FileProvider;

using Serilog;

using SkiaSharp;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.Helpers.Output.Textures;
public abstract class IconOutBase : IDisposable
{
    #region Constructor
    private readonly string _gameDirectory;
    private readonly string _outputDirectory;
    private readonly char[] _invalidChars = Path.GetInvalidFileNameChars();

    protected BnsDatabase? db;
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
                //.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information)
                .WriteTo.File(Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}.log"), outputTemplate: template))
           // .WriteTo.Logger(lc => lc
           //.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error)
           //.WriteTo.File(Path.Combine(folder, $"{DateTime.Now:yyyyMMdd}_error.log"), outputTemplate: template))
           .CreateLogger();
    }
    #endregion


    #region Methods
    public void LoadData(CancellationToken cancellationToken)
    {
        // init table
        db = new BnsDatabase(DefaultProvider.Load(UserSettings.Default.GameFolder, new DatSelectDialog()));
        _ = db.Provider.GetTable<IconTexture>();

        cancellationToken.ThrowIfCancellationRequested();
    }

    public void Output(string format, CancellationToken cancellationToken)
    {
        using var provider = new GameFileProvider(_gameDirectory, true);
        Directory.CreateDirectory(_outputDirectory);

        cancellationToken.ThrowIfCancellationRequested();
        Output(provider, format, cancellationToken);
    }

    protected abstract void Output(DefaultFileProvider provider, string format, CancellationToken cancellationToken);

    protected void Save(SKBitmap? source, string name)
    {
        if (source is null) return;

        // Invalid chars
        if (name.IndexOfAny(_invalidChars) >= 0)
        {
            foreach (char c in _invalidChars)
                name = name.Replace(c.ToString(), "_");
        }

        source.Save(_outputDirectory + $@"\{name}.png");
        source.Dispose();
    }
    #endregion

    #region Dispose
    public void Dispose()
    {
        db?.Dispose();
        db = null;

        GC.SuppressFinalize(this);
        GC.Collect();
    }
    #endregion
}