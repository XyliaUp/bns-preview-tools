using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models;
public sealed class ItemRandomAbilitySection : Record
{
	public string Alias;


	[Name("variation-value-min")]
	public int VariationValueMin;

	[Name("variation-value-max")]
	public int VariationValueMax;

	[Name("variation-value-with-special-item-min")]
	public int VariationValueWithSpecialItemMin;

	[Name("variation-value-with-special-item-max")]
	public int VariationValueWithSpecialItemMax;
}