using Xylia.Extension;

using static Xylia.Configure.Ini;

namespace Xylia.Preview.Properties;
public static class Settings
{
	const string SEC = "Preview";

	public static LoadMode LoadMode
	{
		get => (LoadMode)ReadValue(SEC, "load-mode").ToInt();
		set => WriteValue(SEC, "load-mode", (int)value);
	}

	public static DumpMode TestMode
	{
		get => (DumpMode)ReadValue(SEC, "test-mode").ToInt();
		set => WriteValue(SEC, "test-mode", (int)value);
	}
}



[Flags]
public enum LoadMode
{
	Default = 0x0000,
	LoadOnInit = 0x0001,
}

public enum DumpMode
{
	None,
	Used,
	Full,
}




[Flags]
public enum Moudle
{
	Client = 4, //??

	LocalizationData = 6,

	AbnormalMoveAnim = 12,

	CombatData = 14,

	CraftRecipe = 38,     

	ArenaPortal = 70,

	Achievement = 262,

	DifficultyType = 326,

	Campfire = 526,

	ContextScript = 2052,

	DuelBotTrainingRoom = 2054,

	Skill = 2062,  //5

	Effect = 2318,

	BoardGacha = 8198,

	Item = 11534,
}