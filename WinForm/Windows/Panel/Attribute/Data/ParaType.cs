using System.ComponentModel;

namespace Xylia.Match.Windows.Attribute.Data;
public enum ParaType
{
	None,

	[Description("功力")]
	Attribute = 1,

	[Description("暴击")]
	Critical,

	[Description("暴击伤害")]
	CriticalDamage,

	[Description("状态异常伤害")]
	AbnormalAttack,

	[Description("状态异常防御")]
	AbnormalDefend,


	[Description("命中")]
	Hit,

	[Description("治疗")]
	Heal,


	[Description("暴击防御")]
	CriticalDefence,

	[Description("暴击防御-伤害减免")]
	CriticalDefence2,



	[Description("防御穿刺")]
	Pierce1,

	[Description("格挡穿刺")]
	Pierce2,

	[Description("防御")]
	Defence,



	[Description("闪避")]
	Dodge1,

	[Description("闪避-反击武功强化")]
	Dodge2,



	[Description("格挡-伤害减免")]
	Parry1,

	[Description("格挡-防御武功强化")]
	Parry2,

	[Description("格挡")]
	Parry3,
}
