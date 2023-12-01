using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class ItemSpirit : Record
{
	public string Alias;



	[Name("main-ingredient")]
	public Ref<Item> MainIngredient;

	[Name("applicable-part"), Repeat(4)]
	public EquipType[] ApplicablePart;

	[Name("use-random-ability-value")]
	public bool UseRandomAbilityValue;

	[Name("money-cost")]
	public int MoneyCost;

	[Name("attach-ability"), Repeat(2)]
	public MainAbility[] AttachAbility;

	[Name("ability-min") , Repeat(2)]
	public int[] AbilityMin;

	[Name("ability-max"), Repeat(2)]
	public int[] AbilityMax;

	[Name("once-attach-ability-min"), Repeat(2)]
	public int[] OnceAttachAbilityMin;

	[Name("once-attach-ability-max"), Repeat(2)]
	public int[] OnceAttachAbilityMax;

	public WarningSeq Warning;
	public enum WarningSeq
	{
		None,
		Fail,
	}
}