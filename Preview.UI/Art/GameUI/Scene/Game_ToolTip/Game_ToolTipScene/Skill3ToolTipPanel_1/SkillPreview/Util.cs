using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.SkillTooltipAttribute;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTipScene.Skill3ToolTipPanel_1.SkillPreview
{
    public static class Util
    {
        public static IEnumerable<SkillTooltip> GetSkillTooltips(this Skill3 Skill)
        {
            //返回结果较慢
            if (false) return FileCache.Data.SkillTooltip.Where(tooltip => tooltip.Skill == Skill.alias);


            List<SkillTooltip> tooltips = new();
            tooltips.AddRange(Skill.MainTooltip1);
            tooltips.AddRange(Skill.MainTooltip2);
            tooltips.AddRange(Skill.SubTooltip);
            tooltips.AddRange(Skill.StanceTooltip);
            tooltips.AddRange(Skill.ConditionTooltip);
            return tooltips.Where(tooltip => tooltip is not null);
        }

        public static void LoadArg(this ContentParams Params, ArgType ArgType, string Arg, SkillTooltip Tooltip)
        {
            if (ArgType == ArgType.None) return;

            #region Get param value
            int ArgValue1 = 0;
            int ArgValue2 = 0;
            if (Arg != null)
            {
                var v = Arg.Split(',');
                if (v.Length >= 1 && int.TryParse(v[0], out var v1)) ArgValue1 = v1;
                if (v.Length >= 2 && int.TryParse(v[1], out var v2)) ArgValue2 = v2;
            }
            #endregion

            #region param
            Params.Add(ArgType switch
            {
                ArgType.DamagePercentMinMax => GetDamageInfo(ArgValue1, ArgValue2, Tooltip.SkillAttackAttributeCoefficientPercent),
                ArgType.DamagePercent => GetDamageInfo(ArgValue1, 0, Tooltip.SkillAttackAttributeCoefficientPercent),
                ArgType.Time => (float)ArgValue1 / 1000 + "秒",
                ArgType.StackCount => ArgValue1.ToString(),
                ArgType.Effect => $"<font name=\"00008130.Program.Fontset_ItemGrade_6\">{FileCache.Data.Effect[Arg]?.Name2.GetText()}</font>",
                ArgType.HealPercent => ArgValue1 + "%",
                ArgType.DrainPercent => ArgValue1 + "%",
                ArgType.Skill => $"<font name=\"00008130.Program.Fontset_ItemGrade_4\">{FileCache.Data.Skill3[Arg]?.Name2.GetText()}</font>",
                ArgType.ConsumePercent => ArgValue1 + "%",
                ArgType.ProbabilityPercent => ArgValue1 + "%",
                ArgType.StanceType => Arg.ToEnum<StanceSeq>().GetText(),
                ArgType.Percent => ArgValue1 + "%",
                ArgType.Counter => ArgValue1 + "次",
                ArgType.Distance => (float)ArgValue1 / 100 + "米",
                ArgType.KeyCommand => FileCache.Data.Skill3[Arg]?.CurrentShortCutKey.GetImage(),
                ArgType.Number => ArgValue1.ToString(),
                ArgType.TextAlias => Arg.GetText(),

                _ => null,
            });
            #endregion
        }





        public static string GetDuration(this int Duration) => Duration == 0 ? "即时" : TimeSpan.FromMilliseconds(Duration).ToMyString();





        public static int AttackPower = 6464;

        public static double AttackAttributePercent = 3.8837;

        public static string GetDamageInfo(int Value) => GetDamageInfo(Value, Value, 100);

        public static string GetDamageInfo(int MinValue, int MaxValue, int AttributePercent)
        {
            if (false)
            {
                string result = MaxValue == 0 ? $"{MinValue}%" : $"{MinValue}~{MaxValue}%";
                return AttributePercent > 0 ? result + " [功力]" : result;
            }
            else
            {
                var temp = AttackPower * 0.01;
                if (AttributePercent > 0) temp = temp * (AttributePercent * 0.01) * (AttackAttributePercent * 0.97);

                return $"{MinValue * temp * 0.985:N0}~{MaxValue * temp * 1.015:N0}";
            }
        }
    }
}