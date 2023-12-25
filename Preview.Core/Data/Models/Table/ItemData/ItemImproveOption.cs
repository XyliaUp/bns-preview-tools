using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemImproveOption : ModelElement
{
	public int Id { get; set; }
	public sbyte Level { get; set; }



	public MainAbility Ability { get; set; }

	public int AbilityValue { get; set; }

	public Ref<Effect> Effect { get; set; }

	public Ref<Text> EffectDescription { get; set; }

	public Ref<SkillModifyInfoGroup>[] SkillModifyInfoGroup { get; set; }

	public Ref<Text> Additional { get; set; }

	public string DrawOptionIcon { get; set; }



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