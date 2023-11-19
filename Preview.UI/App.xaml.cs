#define DEV

using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.VirtualFileSystem;

using Serilog;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.ViewModels;

using Kernel32 = Vanara.PInvoke.Kernel32;

namespace Xylia.Preview.UI;
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		InitializeArgs(e.Args);

		#region Log
		string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}";
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Debug(Serilog.Events.LogEventLevel.Warning, outputTemplate: template)
			.WriteTo.File(Path.Combine(UserSettings.Default.OutputFolder, "Logs", $"{DateTime.Now:yyyy-MM-dd}.log"), outputTemplate: template)
			.CreateLogger();
		#endregion

		#region Command
		if (_flagValue.TryGetValue("command", out string command))
		{
			Kernel32.AllocConsole();
			Kernel32.SetConsoleCP(65001);
			Kernel32.SetConsoleTitle(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title);
			try
			{
				Command(command);
			}
			catch (Exception error)
			{
				Console.WriteLine(error);
				Console.ReadKey();
			}

			Kernel32.FreeConsole();
			Environment.Exit(-1);
		}
		#endregion

#if DEV
		//FileCache.Data = new Data.Engine.DatData.FolderProvider(@"D:\资源\客户端相关\Auto\data");
#endif
		MainWindow = new MainWindow();
		//MainWindow = new Xylia.Preview.UI.Art.GameUI.Scene.Game_Broadcasting.Game_BroadcastingScene();
		MainWindow.Show();
	}


	#region Exception	
	private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		e.Handled = true;

		var exception = e.Exception;
		if (exception is TargetInvocationException) exception = exception.InnerException;

		Log.Fatal(exception.ToString());
		MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
	}

	private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		var error = e.ExceptionObject as Exception;

		string str = $"The program crashed and is about to exit.\n{error.Message};\nat {DateTime.Now}";

		Log.Fatal(str);
		MessageBox.Show(str, "Crash");
	}
	#endregion

	#region Command
	private static Dictionary<string, string> _flagValue;

	private static void InitializeArgs(string[] args)
	{
		_flagValue = args
			.Where(x => x[0] == '-' && x.IndexOf('=') > 0)
			.ToLookup(
				x => x[1..x.IndexOf('=')].ToLower(),
				x => x[(x.IndexOf('=') + 1)..])
			.ToDictionary(x => x.Key, x => x.First());
	}

	private static void Command(string command)
	{
		bool pause = false;
		switch (command)
		{
			case "query":
			{
				Console.WriteLine($"starting...");

				var QueryType = _flagValue["type"];
				switch (QueryType)
				{
					case "ue":
					case "ue4":
					{
						var path = _flagValue["path"];
						var ext = _flagValue.TryGetValue("class", out var c) ? c : null;

						path = FileCache.Provider.FixPath(path, QueryType != "ue4") ?? path;


						var filter = path.Split('.')[0];
						var props = new ConcurrentDictionary<string, FPropertyTag>();

						foreach (var _gamefile in FileCache.Provider.Files)
						{
							var vfs = ((VfsEntry)_gamefile.Value).Vfs;
							var package = _gamefile.Value.Path;
							if (package.Contains(".uasset") && package.Contains(filter, StringComparison.OrdinalIgnoreCase))
							{
								if (ext is not null)
								{
									var objs = FileCache.Provider.LoadPackage(_gamefile.Key).GetExports().Where(o => o.ExportType == ext);
									if (!objs.Any()) continue;

									if (true) objs.SelectMany(o => o.Properties).ForEach(prop => props.TryAdd(prop.Name.Text, prop));
								}

								pause = true;
								Console.WriteLine(string.Concat(vfs.Name, "\t", package));
							}
						}


						foreach (var _property in props.OrderBy(o => o.Key))
							Console.WriteLine(_property.Value.Name + " " + _property.Value.Tag.ToString());
					}
					break;
				}


				if (!pause)
				{
					Console.WriteLine($"no result!");
					Console.ReadKey();
				}
			}
			break;

			default: Console.WriteLine("bad param!"); return;
		}

		if (pause) Console.ReadKey();
	}
	#endregion
}