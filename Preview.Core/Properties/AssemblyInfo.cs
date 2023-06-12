using System.ComponentModel;
using System.Diagnostics;

namespace Xylia.Preview.Properties
{
	public static class Program
    {
        static bool? _designMode;
        public static bool IsDesignMode
        {
            get
            {
                if (_designMode.HasValue)
                    return _designMode.Value;


                bool result = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    result = true;
                }
                else if (Process.GetCurrentProcess().ProcessName == "devenv")
                {
                    result = true;
                }

                return (_designMode = result).Value;
            }
        }
    }
}