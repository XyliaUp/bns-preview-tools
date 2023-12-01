using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class SlateStone : Record
{
	public string Alias;



	public Ref<Text> Name;

	public short Group;

	public sbyte Grade;

	[Repeat(4)]
	public AttachAbility[] ModifyAbility;

	[Repeat(4)]
	public int[] InitAbilityValue;

	[Repeat(4)]
	public int[] UnitAbilityValue;

	[Repeat(4)]
	public int[] MaxAbilityValue;


	public string Icon;

	public string IconCase;
}