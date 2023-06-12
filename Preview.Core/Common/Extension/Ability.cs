
using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Extension;

public static class AbilityEx
{
	public static string GetName(this MainAbility ability, long Value = 0)
	{
		var signal = ability.GetSignal();	   
		if (ability == MainAbility.None) return null;
		else if(ability == MainAbility.AttackPowerEquipMinAndMax) signal = "attack-power-equip";
		else if (ability == MainAbility.DefendPowerEquipValue) signal = "defend-power-equip-value";
		else if (ability == MainAbility.PveBossLevelNpcAttackPowerEquipMinAndMax) signal = "boss-attack-power-equip";
		else if (ability == MainAbility.PveBossLevelNpcDefendPowerEquipValue) signal = "boss-defend-power-equip";
		else if (ability == MainAbility.PvpAttackPowerEquipMinAndMax) signal = "pc-attack-power-equip";
		else if (ability == MainAbility.PvpDefendPowerEquipValue) signal = "pc-defend-power-equip";

		return GetName(signal, Value);
	}

	public static string GetName(this AttachAbility ability, long Value = 0)
	{
		var signal = ability.GetSignal();
		if (ability == AttachAbility.None) return null;
		else if (ability == AttachAbility.AttackPowerCreatureMinMax) signal = "attack-power-equip";
		else if (ability == AttachAbility.DefendPowerCreatureValue) signal = "defend-power-equip-value";
		else if (ability == AttachAbility.PveBossLevelNpcAttackPowerCreatureMinMax) signal = "boss-attack-power-equip";
		else if (ability == AttachAbility.PveBossLevelNpcDefendPowerCreatureValue) signal = "boss-defend-power-equip";
		else if (ability == AttachAbility.PvpAttackPowerCreatureMinMax) signal = "pc-attack-power-equip";
		else if (ability == AttachAbility.PvpDefendPowerCreatureValue) signal = "pc-defend-power-equip";

		return GetName(signal, Value);
	}




	private static string GetName(this string AbilitySignal, long Value) => $"Name.Item.{AbilitySignal}".GetText() + 
		(Value == 0 ? null : " " + GetValue(Value, AbilitySignal));


	/// <summary>
	/// 获取值文本
	/// </summary>
	/// <param name="Value"></param>
	/// <param name="AbiltyName"></param>
	/// <returns></returns>
	private static string GetValue(this long Value, string AbiltyName) => AbiltyName != null && AbiltyName.MyEndsWith("percent") ?
		((float)Value / 10).ToString("0.0") + "%" :
		Value.ToString();
}