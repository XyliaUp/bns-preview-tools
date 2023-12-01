#define DEV

using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;
using System.Windows.Threading;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.VirtualFileSystem;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using HandyControl.Controls;

using Serilog;

using Xylia.Configure;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Services;
using Xylia.Preview.UI.ViewModels;

using Kernel32 = Vanara.PInvoke.Kernel32;

namespace Xylia.Preview.UI;
public partial class App : Application
{
	protected override void OnStartup(StartupEventArgs e)
	{
		// Create a jump-list and assign it to the current application
		JumpList.SetJumpList(Current, new JumpList());
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

		// Process the command-line arguments
		InitializeArgs(e.Args);

		#region Log
		var foloder = UserSettings.Default.OutputFolder ?? PathDefine.MainFolder;
		string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}";
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Debug(Serilog.Events.LogEventLevel.Warning, outputTemplate: template)
			.WriteTo.File(Path.Combine(foloder, "Logs", $"{DateTime.Now:yyyy-MM-dd}.log"), outputTemplate: template)
			.CreateLogger();
		#endregion

		new JumpListService().CreateAsync();

#if DEV
		//FileCache.Data = new Data.Engine.DatData.FolderProvider(@"D:\资源\客户端相关\Auto\data");
		//MainWindow = new Views.Editor.PropertyEditor() { Source = FileCache.Data.Store2[80087] };
		//MainWindow = new Xylia.Preview.UI.Art.GameUI.Scene.Game_Broadcasting.Game_BroadcastingScene();

		//MainWindow = new TableView()
		//{
		//	Table = FileCache.Data.Social,
		//};
#endif
		MainWindow = new MainWindow();
		MainWindow.Show();



		//using var provider = DefaultProvider.Load(UserSettings.Default.GameFolder);
		//var o = provider.XmlData.EnumerateFiles("datafile64.bin").FirstOrDefault()?.Data;
		//o = null;

		//GC.Collect();
		//provider.LoadData(null);
	}



	#region Exception	
	private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		e.Handled = true;

		var exception = e.Exception;
		if (exception is TargetInvocationException) exception = exception.InnerException;

		// not to write log
		if (exception is not WarningException)
			Log.Error(exception, "OnUnhandledException");

		Growl.Error(exception.Message);
	}

	private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		var error = e.ExceptionObject as Exception;

		string str = $"The program crashed and is about to exit.\n{error.Message};\nat {DateTime.Now}";

		Log.Fatal(str);
		HandyControl.Controls.MessageBox.Show(str, "Crash", MessageBoxButton.OK, MessageBoxImage.Stop);
	}
	#endregion

	#region Command
	private static Dictionary<string, string> _flagValue;

	private static void InitializeArgs(string[] args)
	{
		// Process the command-line arguments
		_flagValue = args
			.Where(x => x[0] == '-' && x.IndexOf('=') > 0)
			.ToLookup(
				x => x[1..x.IndexOf('=')].ToLower(),
				x => x[(x.IndexOf('=') + 1)..])
			.ToDictionary(x => x.Key, x => x.First());


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
				Console.WriteLine(error is WarningException ? error.Message : error);
				Console.ReadKey();
			}

			Kernel32.FreeConsole();
			Environment.Exit(-1);
		}
	}

	private static void Command(string command)
	{
		if (command == "query")
		{
			var pause = false;
			var type = _flagValue["type"];
			switch (type)
			{
				case "ue":
				case "ue4":
				{
					if (!_flagValue.TryGetValue("path", out var path))
					{
						Console.Clear();
						Console.WriteLine("please enter search rule...");
						path = Console.ReadLine();
					}

					var ext = _flagValue.TryGetValue("class", out var c) ? c : null;
					Console.WriteLine($"starting...");

					// convert
					path = FileCache.Provider.FixPath(path, type != "ue4") ?? path;
					var filter = path.Split('.')[0];

					// filter
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

				default: throw new WarningException();
			}

			if (!pause) Console.WriteLine($"no result!");
			Console.ReadKey();
		}
		else if (command == "soundwave_output")
		{
			var provider = new GameFileProvider(UserSettings.Default.GameFolder, true);
			var assets = provider.AssetRegistryModule.GetAssets(x => x.AssetClass.Text == "SoundWave").ToArray();
			Console.WriteLine($"total: {assets.Length}");

			#region Progress
			int current = 0;
			int cursor = Console.CursorTop;

			var timer = new System.Timers.Timer(1000);
			timer.Elapsed += (_, _) =>
			{
				Console.SetCursorPosition(0, cursor);
				Console.Write(new string(' ', Console.WindowWidth));
				Console.SetCursorPosition(0, cursor);
				Console.Write($"output {(double)current / assets.Length:P0}");
			};
			timer.Start();
			#endregion

			Parallel.ForEach(assets, asset =>
			{
				try
				{
					current++;

					var Object = provider.LoadObject<USoundWave>(asset.ObjectPath.Text);
					if (Object != null)
					{
						Object.Decode(true, out var audioFormat, out var data);
						File.WriteAllBytes(Exporter.FixPath(UserSettings.Default.OutputFolderResource, Object.GetPathName()) + "." + audioFormat, data);
					}
				}
				catch
				{

				}
			});
		}

		else throw new WarningException("bad params: " + command);
	}
	#endregion
}