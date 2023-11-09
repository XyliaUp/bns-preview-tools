using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.Helpers;
public static class Register
{
	public static void Create()
	{
		IDatSelect.Default = new DatSelectDialog();
		UserSettings.Default.UsePerformanceMonitor = UserSettings.Default.UsePerformanceMonitor;
	}
}