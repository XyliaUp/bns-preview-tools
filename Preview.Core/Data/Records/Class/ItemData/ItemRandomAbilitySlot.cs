using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class ItemRandomAbilitySlot : BaseRecord
{
	public MainAbility Ability;

	[Signal("value-min")]
	public int ValueMin;

	[Signal("value-max")]
	public int ValueMax;

	[Signal("initial-value-max")]
	public int InitialValueMax;

	[Signal("item-ability-section-percent-1")]
	public sbyte ItemAbilitySectionPercent1;

	[Signal("item-ability-section-percent-2")]
	public sbyte ItemAbilitySectionPercent2;

	[Signal("item-ability-section-percent-3")]
	public sbyte ItemAbilitySectionPercent3;

	[Signal("item-ability-section-1")]
	public ItemRandomAbilitySection ItemAbilitySection1;

	[Signal("item-ability-section-2")]
	public ItemRandomAbilitySection ItemAbilitySection2;

	[Signal("item-ability-section-3")]
	public ItemRandomAbilitySection ItemAbilitySection3;


	public override string ToString() => $"{this.Ability} => {this.ValueMin}~{this.ValueMax} [{this.InitialValueMax}]";
}
