using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveOption : ModelElement
{
	public int Id;
	public sbyte Level;



	public MainAbility Ability;

	[Name("ability-value")]
	public int AbilityValue;

	public Ref<Effect> Effect;

	[Name("effect-description")]
	public Ref<Text> EffectDescription;

	[Name("skill-modify-info-group"), Repeat(10)]
	public Ref<SkillModifyInfoGroup>[] SkillModifyInfoGroup;

	public Ref<Text> Additional;

	[Name("draw-option-icon")]
	public string DrawOptionIcon;



	#region Functions
	public override string ToString()
	{
		string AdditionalText = Additional.GetText();

		if (this.EffectDescription.Instance != null) return $"{this.EffectDescription.GetText()}{AdditionalText}";
		if (this.Ability != MainAbility.None) return this.Ability.GetName(this.AbilityValue) + AdditionalText;

		return SkillModifyInfoGroup.Skip(5)
			.Select(record => record.Instance).Where(record => record is not null)
			.Aggregate("<font name=\"00008130.UI.Label_Green03_12\">", (sum, now) => sum + "<br/>" + now) + "</font>";
	}
	#endregion
}