using HandyControl.Controls;

using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.Services;
internal static class RegisterService
{
	public static void Create()
	{
		IDatSelect.Default = new DatSelectDialog();

		// default effects 
		UserSettings.Default.CopyMode = UserSettings.Default.CopyMode;
		UserSettings.Default.UsePerformanceMonitor = UserSettings.Default.UsePerformanceMonitor;

		// ask
		if (UserSettings.Default.UseUserDefinition)
		{
			Growl.Ask(StringHelper.Get("Settings_UseUserDefinition_Ask"), isConfirmed =>
			{
				UserSettings.Default.UseUserDefinition = isConfirmed;
				return true;
			});
		}
	}
}