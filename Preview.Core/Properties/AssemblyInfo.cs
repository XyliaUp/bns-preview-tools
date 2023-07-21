using System.ComponentModel;
using System.Reflection;

namespace Xylia.Preview.Properties;
public static class Program
{
	static bool? _designMode;

	public static bool IsDesignMode
	{
		get
		{
			if (_designMode.HasValue)
				return _designMode.Value;

			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
				Process.GetCurrentProcess().ProcessName == "devenv")
				return (_designMode = true).Value;


			return (_designMode = false).Value;
		}
	}


	public static bool IsDebugMode => Assembly.GetEntryAssembly().GetName().Name == "Preview.UI";
}