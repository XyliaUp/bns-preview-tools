using Xylia.Configure;
using Xylia.Extension;

namespace Xylia.Preview.Properties;
public static class CommonPath
{
	public static string OutputFolder
	{
		get => Ini.ReadValue("Folder", "Output");
		set
		{
			if (Directory.Exists(value))
				Ini.WriteValue("Folder", "Output", value);
		}
	}

	public static string GameFolder
	{
		get => Ini.ReadValue("Folder", "Game_Bns");
		set
		{
			if (Directory.Exists(value))
				Ini.WriteValue("Folder", "Game_Bns", value);
		}
	}

	public static string DataFiles => OutputFolder + @"\data";
}