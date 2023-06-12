using System;
using System.Collections.Generic;
using System.Linq;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
	public static class Util
	{
		public static IEnumerable<SkillTooltip> GetSkillTooltips(this Data.Record.Skill Skill)
		{
			//返回结果较慢
			if (false) return FileCache.Data.SkillTooltip.Where(tooltip => tooltip.Skill == Skill.alias);


			List<string> temp = new();
			for (int idx = 1; idx <= 10; idx++) temp.AddItem(Skill.Attributes["sub-tooltip-" + idx]);
			for (int idx = 1; idx <= 5; idx++)
			{
				temp.AddItem(Skill.Attributes["main-tooltip-1-" + idx]);
				temp.AddItem(Skill.Attributes["main-tooltip-2-" + idx]);
				temp.AddItem(Skill.Attributes["stance-tooltip-" + idx]);
				temp.AddItem(Skill.Attributes["condition-tooltip-" + idx]);
			}

			return temp.Select(o => FileCache.Data.SkillTooltip[o]);
		}

		public static void LoadArg(this ContentParams Params, SkillTooltipAttribute.ArgType ArgType, string Arg, SkillTooltip Tooltip)
		{
			if (ArgType == SkillTooltipAttribute.ArgType.None) return;

			#region Get param value
			int ArgValue1 = 0;
			int ArgValue2 = 0;

			//防止出现空值导致处理崩溃
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
				SkillTooltipAttribute.ArgType.DamagePercentMinMax => GetDamageInfo(ArgValue1, ArgValue2, Tooltip.SkillAttackAttributeCoefficientPercent),
				SkillTooltipAttribute.ArgType.DamagePercent => GetDamageInfo(ArgValue1, 0, Tooltip.SkillAttackAttributeCoefficientPercent),
				SkillTooltipAttribute.ArgType.Time => (float)ArgValue1 / 1000 + "秒",
				SkillTooltipAttribute.ArgType.StackCount => ArgValue1.ToString(),
				SkillTooltipAttribute.ArgType.Effect => $"<font name=\"00008130.Program.Fontset_ItemGrade_6\">{ FileCache.Data.Effect[Arg]?.Name2.GetText() }</font>",
				SkillTooltipAttribute.ArgType.HealPercent => ArgValue1 + "%",
				SkillTooltipAttribute.ArgType.DrainPercent => ArgValue1 + "%",
				SkillTooltipAttribute.ArgType.Skill => $"<font name=\"00008130.Program.Fontset_ItemGrade_4\">{ FileCache.Data.Skill3[Arg]?.Name2.GetText() }</font>",
				SkillTooltipAttribute.ArgType.ConsumePercent => ArgValue1 + "%",
				SkillTooltipAttribute.ArgType.ProbabilityPercent => ArgValue1 + "%",
				SkillTooltipAttribute.ArgType.StanceType => Arg.ToEnum<StanceSeq>().GetName(),
				SkillTooltipAttribute.ArgType.Percent => ArgValue1 + "%",
				SkillTooltipAttribute.ArgType.Counter => ArgValue1 + "次",
				SkillTooltipAttribute.ArgType.Distance => (float)ArgValue1 / 100 + "米",
				SkillTooltipAttribute.ArgType.KeyCommand => FileCache.Data.Skill3[Arg]?.CurrentShortCutKey.GetImage(),
				SkillTooltipAttribute.ArgType.Number => ArgValue1.ToString(),
				SkillTooltipAttribute.ArgType.TextAlias => Arg.GetText(),

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

				return $"{ MinValue * temp * 0.985:N0}~{ MaxValue * temp * 1.015:N0}";
			}
		}
	}
}