using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class SlateStone : ModelElement
{
	public short Group { get; set; }

	public sbyte Grade { get; set; }

	[Repeat(4)]
	public AttachAbility[] ModifyAbility { get; set; }

	[Repeat(4)]
	public int[] InitAbilityValue { get; set; }

	[Repeat(4)]
	public int[] UnitAbilityValue { get; set; }

	[Repeat(4)]
	public int[] MaxAbilityValue { get; set; }


	public string Icon { get; set; }

	public string IconCase { get; set; }
}