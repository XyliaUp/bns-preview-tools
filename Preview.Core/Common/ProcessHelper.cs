using System.Diagnostics;

using Vanara.PInvoke;

namespace Xylia.Preview.Common;
public static class ProcessHelper
{
    public static void ClearMemory()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();

        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            Kernel32.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, SizeT.MinValue, SizeT.MinValue);
    }
}