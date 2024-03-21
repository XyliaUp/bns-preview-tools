using HandyControl.Controls;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Services;
/// <summary>
/// Initialize process
/// </summary>
internal class RegisterService : IService
{
	bool IService.Register()
	{
		// effects 
		UserSettings.Default.UsePerformanceMonitor = UserSettings.Default.UsePerformanceMonitor;
		UserSettings.Default.CopyMode = UserSettings.Default.CopyMode;
		UserSettings.Default.SkinType = UserSettings.Default.SkinType;

		// ask
		if (UserSettings.Default.UseUserDefinition)
		{
			Growl.Ask(StringHelper.Get("Settings_UseUserDefinition_Ask"), isConfirmed =>
			{
				UserSettings.Default.UseUserDefinition = isConfirmed;
				return true;
			});
		}

		return true;
	}
}