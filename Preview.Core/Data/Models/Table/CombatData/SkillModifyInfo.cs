namespace Xylia.Preview.Data.Models;
public class SkillModifyInfo : ModelElement
{
	#region Methods
	private enum TextType
	{
		Percent,
		Value,
		Second1,
		Second2,
	}

	//public override string ToString()
	//{
	//	#region skill
	//	string SkillPart = null;
	//	if (this is Skill)
	//	{
	//		var Skill = ParentSkill3Id.Where(a => a != 0).Select(skill => FileCache.Data.Skill3[skill, 1]?.Name2.GetText()).Aggregate(", ");
			
	//		SkillPart = $"<font name=\"00008130.UI.Vital_LightBlue\">{Skill}</font> ";
	//	}
	//	#endregion

	//	#region text
	//	List<string> Text = new();
	//	void AddText(string name, float value, TextType type)
	//	{
	//		//if (value == 0) return;

	//		//var param = new ContentParams();
	//		//param[2] = name.GetText();
	//		//param[3] = Math.Abs(type == TextType.Percent ? (value / 10) : value);

	//		//Text.Add(param.Handle(type switch
	//		//{
	//		//	TextType.Percent => value > 0 ? "Name.SkillModifyByEquipment.Plus.Percent" : "Name.SkillModifyByEquipment.Minus.Percent",
	//		//	TextType.Value => value > 0 ? "Name.SkillModifyByEquipment.Plus.Value" : "Name.SkillModifyByEquipment.Minus.Value",
	//		//	TextType.Second1 => value > 0 ? "Name.SkillModifyByEquipment.Plus.Second" : "Name.SkillModifyByEquipment.Minus.Second",
	//		//	TextType.Second2 => value > 0 ? "Name.SkillModifyByEquipment.Plus.Second.Integer" : "Name.SkillModifyByEquipment.Minus.Second.Integer",

	//		//	_ => null,
	//		//}));
	//	}

	//	AddText("Name.SkillModifyByEquipment.recycle-duration", this.RecycleDurationModifyPercent, TextType.Second1);
	//	AddText("Name.SkillModifyByEquipment.recycle-duration", this.RecycleDurationModifyDiff / 1000, TextType.Second2);
	//	AddText("Name.SkillModifyByEquipment.damage-power-percent", this.DamagePowerPercentModifyPercent, TextType.Percent);
	//	AddText("Name.SkillModifyByEquipment.damage-power-percent", this.DamagePowerPercentModifyDiff, TextType.Value);
	//	AddText("Name.SkillModifyByEquipment.sp-consume", this.SpConsumeModifyDiff[0], TextType.Value);
	//	AddText("Name.SkillModifyByEquipment.sp-consume", this.SpConsumeModifyDiff[1], TextType.Value);
	//	AddText("Name.SkillModifyByEquipment.hp-drain-percent", this.HpDrainPercentModifyPercent, TextType.Percent);
	//	AddText("Name.SkillModifyByEquipment.hp-drain-percent", this.HpDrainPercentModifyDiff, TextType.Value);
	//	AddText("Name.SkillModifyByEquipment.heal-percent", this.HealPercentModifyPercent, TextType.Percent);
	//	AddText("Name.SkillModifyByEquipment.heal-percent", this.HealPercentModifyDiff, TextType.Value);

	//	if (!Text.Any()) return null;
	//	#endregion


	//	return Text.Select(o => SkillPart + o).Aggregate("<br/>");
	//}
	#endregion


	#region Sub
	public sealed class Normal : SkillModifyInfo
	{

	}

	public sealed class Skill : SkillModifyInfo
	{

	}

	public sealed class SkillSystematization : SkillModifyInfo
	{

	}
	#endregion
}