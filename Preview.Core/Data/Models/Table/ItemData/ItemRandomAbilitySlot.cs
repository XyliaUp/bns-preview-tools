using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class ItemRandomAbilitySlot : ModelElement
{
	public MainAbility Ability { get; set; }

	[Name("value-min")]
	public int ValueMin { get; set; }

	[Name("value-max")]
	public int ValueMax { get; set; }

	[Name("initial-value-max")]
	public int InitialValueMax { get; set; }

	[Name("item-ability-section-percent") , Repeat(3)]
	public sbyte[] ItemAbilitySectionPercent { get; set; }

	[Name("item-ability-section"), Repeat(3)]
	public Ref<ItemRandomAbilitySection>[] ItemAbilitySection1 { get; set; }


	public override string ToString() => $"{this.Ability} => {this.ValueMin}~{this.ValueMax} [{this.InitialValueMax}]";
}
