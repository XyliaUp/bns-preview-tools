using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemRandomAbilitySection : BaseRecord
{
	[Signal("variation-value-min")]
	public int VariationValueMin;

	[Signal("variation-value-max")]
	public int VariationValueMax;

	[Signal("variation-value-with-special-item-min")]
	public int VariationValueWithSpecialItemMin;

	[Signal("variation-value-with-special-item-max")]
	public int VariationValueWithSpecialItemMax;
}