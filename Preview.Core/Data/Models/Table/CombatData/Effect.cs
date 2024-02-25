namespace Xylia.Preview.Data.Models;
public sealed class Effect : ModelElement
{
	protected internal override void LoadHiddenField()
	{
		if (this.Attributes["power-percent-max"] is not null)
			return;

		var type = this.Attributes.Get<string>("type");
		if (type == "melee-physical-attack" ||
			type == "melee-physical-attack-hate" ||
			type == "melee-physical-attack-drain" ||
			type == "force-attack-hp-drain" ||
			type == "force-attack-sp-drain" ||
			type == "melee-physical-attack-sp-drain" ||
			type == "melee-physical-attack-hp-sp-drain" ||
			type == "force-attack-hp-sp-drain" ||
			type == "range-physical-attack" ||
			type == "force-attack" ||
			type == "force-attack-hate"
			)
		{
			this.Attributes["power-percent-max"] = "100";
			this.Attributes["power-percent-min"] = "100";
		}
	}
}