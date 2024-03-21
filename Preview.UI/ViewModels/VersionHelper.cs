using System.Reflection;

namespace Xylia.Preview.UI.ViewModels;
internal static class VersionHelper
{
	public static Version InternalVersion => Assembly.GetEntryAssembly()!.GetName().Version!;

	public static Version Version => new(InternalVersion.Major, InternalVersion.Minor, InternalVersion.Build);	  	
	
	public static DateTime Time => new DateTime(2000, 1, 1).AddDays(InternalVersion.Revision);
}