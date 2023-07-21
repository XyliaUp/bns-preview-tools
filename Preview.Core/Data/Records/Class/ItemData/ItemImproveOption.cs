using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public sealed class ItemImproveOption : BaseRecord
{
	public int Id;
	public byte Level;



	public MainAbility Ability;

	[Signal("ability-value")]
	public int AbilityValue;

	public Effect Effect;

	[Signal("effect-description")]
	public Text EffectDescription;

	[Signal("skill-modify-info-group"), Repeat(10)]
	public SkillModifyInfoGroup[] SkillModifyInfoGroup;

	public Text Additional;

	[Signal("draw-option-icon")]
	public string DrawOptionIcon;



	#region Functions
	public override string ToString()
	{
		string AdditionalText = Additional.GetText();

		if (this.EffectDescription != null) return $"{this.EffectDescription.GetText()}{AdditionalText}";
		if (this.Ability != MainAbility.None) return this.Ability.GetName(this.AbilityValue) + AdditionalText;

		return SkillModifyInfoGroup?
			.Where(record => record is not null)
			.Aggregate("<font name=\"00008130.UI.Label_Green03_12\">", (sum, now) => sum + "<br/>" + now) + "</font>";
	}
	#endregion
}