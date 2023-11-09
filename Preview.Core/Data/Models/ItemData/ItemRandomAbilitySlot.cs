using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ItemRandomAbilitySlot : Record
{
	public string Alias;



	public MainAbility Ability;

	[Name("value-min")]
	public int ValueMin;

	[Name("value-max")]
	public int ValueMax;

	[Name("initial-value-max")]
	public int InitialValueMax;

	[Name("item-ability-section-percent") , Repeat(3)]
	public sbyte[] ItemAbilitySectionPercent;

	[Name("item-ability-section"), Repeat(3)]
	public Ref<ItemRandomAbilitySection>[] ItemAbilitySection1;


	public override string ToString() => $"{this.Ability} => {this.ValueMin}~{this.ValueMax} [{this.InitialValueMax}]";
}
