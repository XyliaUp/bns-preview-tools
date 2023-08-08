using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class SlateStone : BaseRecord
{
	public Text Name;

	public sbyte Group;

	public sbyte Grade;

	[Signal("modify-ability-1")]
	public AttachAbility ModifyAbility1;

	[Signal("modify-ability-2")]
	public AttachAbility ModifyAbility2;

	[Signal("modify-ability-3")]
	public AttachAbility ModifyAbility3;

	[Signal("modify-ability-4")]
	public AttachAbility ModifyAbility4;

	[Signal("duplication-permission")]
	public bool DuplicationPermission;

	[Signal("init-ability-value-1")]
	public int InitAbilityValue1;

	[Signal("init-ability-value-2")]
	public int InitAbilityValue2;

	[Signal("init-ability-value-3")]
	public int InitAbilityValue3;

	[Signal("init-ability-value-4")]
	public int InitAbilityValue4;

	[Signal("unit-ability-value-1")]
	public int UnitAbilityValue1;

	[Signal("unit-ability-value-2")]
	public int UnitAbilityValue2;

	[Signal("unit-ability-value-3")]
	public int UnitAbilityValue3;

	[Signal("unit-ability-value-4")]
	public int UnitAbilityValue4;

	[Signal("max-ability-value-1")]
	public int MaxAbilityValue1;

	[Signal("max-ability-value-2")]
	public int MaxAbilityValue2;

	[Signal("max-ability-value-3")]
	public int MaxAbilityValue3;

	[Signal("max-ability-value-4")]
	public int MaxAbilityValue4;

	public string Icon;

	[Signal("icon-case")]
	public string IconCase;
}