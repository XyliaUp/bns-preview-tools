using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models.Creature;
public static class AbilityEx
{
    public static string GetName(this MainAbility ability, long Value = 0)
    {
        var signal = ability.GetName();
        if (ability == MainAbility.None) return null;
        else if (ability == MainAbility.AttackPowerEquipMinAndMax) signal = "attack-power-equip";
        else if (ability == MainAbility.DefendPowerEquipValue) signal = "defend-power-equip-value";
        else if (ability == MainAbility.PveBossLevelNpcAttackPowerEquipMinAndMax) signal = "boss-attack-power-equip";
        else if (ability == MainAbility.PveBossLevelNpcDefendPowerEquipValue) signal = "boss-defend-power-equip";
        else if (ability == MainAbility.PvpAttackPowerEquipMinAndMax) signal = "pc-attack-power-equip";
        else if (ability == MainAbility.PvpDefendPowerEquipValue) signal = "pc-defend-power-equip";

        return signal.GetName(Value);
    }

    public static string GetName(this AttachAbility ability, long Value = 0)
    {
        var signal = ability.GetName();
        if (ability == AttachAbility.None) return null;
        else if (ability == AttachAbility.AttackPowerCreatureMinMax) signal = "attack-power-equip";
        else if (ability == AttachAbility.DefendPowerCreatureValue) signal = "defend-power-equip-value";
        else if (ability == AttachAbility.PveBossLevelNpcAttackPowerCreatureMinMax) signal = "boss-attack-power-equip";
        else if (ability == AttachAbility.PveBossLevelNpcDefendPowerCreatureValue) signal = "boss-defend-power-equip";
        else if (ability == AttachAbility.PvpAttackPowerCreatureMinMax) signal = "pc-attack-power-equip";
        else if (ability == AttachAbility.PvpDefendPowerCreatureValue) signal = "pc-defend-power-equip";

        return signal.GetName(Value);
    }



    private static string GetName(this string AbilitySignal, long Value) => $"Name.Item.{AbilitySignal}".GetText() +
        (Value == 0 ? null : " " + Value.GetValue(AbilitySignal));

    private static string GetValue(this long Value, string AbiltyName) => AbiltyName != null && AbiltyName.EndsWith("percent", StringComparison.OrdinalIgnoreCase) ?
        ((float)Value / 10).ToString("0.0") + "%" :
        Value.ToString();
}