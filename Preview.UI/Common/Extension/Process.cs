using Vanara.PInvoke;

namespace Xylia;

public static class ProcessEx
{
	public static void ClearMemory()
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();

		if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			Kernel32.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, SizeT.MinValue, SizeT.MinValue);
	}
}