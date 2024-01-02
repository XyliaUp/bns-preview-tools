using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models.Creature;
public static class AbilityEx
{
	public static string GetName(this MainAbility ability, long Value = 0)
	{
		if (ability == MainAbility.None) return null;

		return ability.GetText().Combine(Value);
	}

	public static string GetName(this AttachAbility ability, long Value = 0)
	{
		if (ability == AttachAbility.None) return null;

		return ability.GetText().Combine(Value);
	}


	private static string Combine(this string text, long Value)
	{
		if (Value == 0) return text;
		return text + " " + Value.GetValue(text);
	}

	private static string GetValue(this long Value, string AbiltyName) => AbiltyName != null && AbiltyName.EndsWith("percent", StringComparison.OrdinalIgnoreCase) ?
		((float)Value / 10).ToString("0.0") + "%" :
		Value.ToString();
}