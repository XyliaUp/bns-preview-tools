using System.Windows;

namespace Xylia.Preview.UI.Resources.Themes;
internal static class SkinHelpers
{
	public static ResourceDictionary GetDayNight(bool? night)
	{
		// Indeterminate represents automatic
		night ??= DateTime.Now.Hour < 6 || DateTime.Now.Hour >= 18;

		var uri = new Uri($"pack://application:,,,/Preview.UI;component/Resources/Themes/Skins/Basic/{(night.Value ? "Dark" : "Day")}.xaml");
		return new ResourceDictionary
		{
			Source = uri
		};
	}

	public static ResourceDictionary GetSkin(SkinType skin)
	{
		try
		{
			var uri = new Uri($"pack://application:,,,/Preview.UI;component/Resources/Themes/Skins/{skin}.xaml");
			return new ResourceDictionary
			{
				Source = uri
			};
		}
		catch
		{
			if (skin == SkinType.Default) throw;

			return GetSkin(SkinType.Default);
		}
	}
}

public enum SkinType
{
	Default,

	Violet,
}