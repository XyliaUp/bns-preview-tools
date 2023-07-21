using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemSpirit : BaseRecord
{
	#region Fields
	[Signal("main-ingredient")]
	public Item MainIngredient;

	[Signal("applicable-part"), Repeat(4)]
	public EquipType[] ApplicablePart;

	[Signal("use-random-ability-value")]
	public bool UseRandomAbilityValue;

	[Signal("money-cost")]
	public int MoneyCost;

	[Signal("attach-ability"), Repeat(2)]
	public MainAbility[] AttachAbility;

	[Signal("ability-min") , Repeat(2)]
	public int[] AbilityMin;

	[Signal("ability-max"), Repeat(2)]
	public int[] AbilityMax;

	[Signal("once-attach-ability-min"), Repeat(2)]
	public int[] OnceAttachAbilityMin;

	[Signal("once-attach-ability-max"), Repeat(2)]
	public int[] OnceAttachAbilityMax;

	public WarningSeq Warning;
	public enum WarningSeq
	{
		None,
		Fail,
	}
	#endregion
}