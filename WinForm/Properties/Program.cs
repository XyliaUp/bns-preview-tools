using System.Collections.Concurrent;
using System.Reflection;

using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.VirtualFileSystem;

using log4net;
using log4net.Config;

using Vanara.PInvoke;
				   using Xylia.Preview.UI.Extension; 
using Xylia.Extension;
using Xylia.Match.Windows.Panel;
using Xylia.Preview.Data.Helper;

namespace Xylia.Match.Properties;
public static partial class Program
{
	public static ILog log;

	[STAThread]
	static void Main(string[] args)
	{
		#region Log
		var res = System.Windows.Application.GetResourceStream(new Uri($"/Match64;component/Properties/log4net.config", UriKind.Relative));
		XmlConfigurator.Configure(res?.Stream);

		//var targetApder = (RollingFileAppender)LogManager.GetRepository().GetAppenders().First(p => p.Name == "LogFileAppender");
		//targetApder.File = "";
		//targetApder.ActivateOptions();

		log = LogManager.GetLogger("LogFileAppender");
		#endregion

		InitializeArgs(args);

		#region WinExe
		if (!_flagValue.TryGetValue("command", out string command))
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Preview.Helper.Register.Main();
			Application.Run(new Windows.MainFrm());
			return;
		}
		#endregion

		#region Command
		Kernel32.AllocConsole();
		Kernel32.SetConsoleCP(65001);
		Kernel32.SetConsoleTitle($"{AssemblyEx.Title} (command line)");
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
		#endregion
	}


	#region Exception
	static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Application_ExceptionHandle(e.ExceptionObject as Exception);
	}

	static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		Application_ExceptionHandle(e.Exception);
	}

	static void Application_ExceptionHandle(Exception error)
	{
		if (error is TargetInvocationException or TypeInitializationException)
			error = error.InnerException;

		string str = $"The program crashed and is about to exit.\n{error.Message};\nat {DateTime.Now}";
		log.Fatal(str + $"\nStackTrace:\n{error.StackTrace}");

		if (error is InvalidOperationException) return;
		if (error is CSCore.MmException) return;

		MessageBox.Show(str);
		Environment.Exit(0);
	}
	#endregion


	#region Command
	private static Dictionary<string, string> _flagValue;

	private static void InitializeArgs(string[] args)
	{
		_flagValue = args
			.Where(x => x[0] == '-' && x.IndexOf('=') > 0)
			.ToDictionary(
				x => x[1..x.IndexOf('=')].ToLower(),
				x => x[(x.IndexOf('=') + 1)..]);
	}

	private static void Command(string command)
	{
		bool pause = false;
		switch (command)
		{
			case "output":
				ItemPage.OutputTable(_flagValue.GetValue("path"));
				return;

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