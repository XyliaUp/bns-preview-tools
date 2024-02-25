using System.IO;
using Serilog;
using Serilog.Events;
using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Services;
internal class LogService : IService
{
	public bool Register()
	{
		Console.SetOut(new ConsoleRedirect());

		var foloder = UserSettings.Default.OutputFolder ?? UserSettings.ApplicationData;
		string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message:lj}{NewLine}{Exception}";
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Debug(LogEventLevel.Warning, outputTemplate: template)
			.WriteTo.File(Path.Combine(foloder, "Logs", $"{DateTime.Now:yyyy-MM-dd}.log"), outputTemplate: template)
			.CreateLogger();


		return true;
	}
}