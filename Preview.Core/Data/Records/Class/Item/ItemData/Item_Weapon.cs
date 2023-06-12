using Xylia.Preview.Common.Attribute;
using Xylia.Extension;

namespace Xylia.Preview.Data.Record;

public sealed partial class Item
{
	[Signal("weapon-type")]
	public WeaponTypeSeq WeaponType => this.Attributes["weapon-type"]?.ToEnum<WeaponTypeSeq>() ?? 0;
	public enum WeaponTypeSeq
	{
		None,
		BareHand,
		Sword,
		Gauntlet,
		AuraBangle,
		Pistol,
		Rifle,
		TwoHandedAxe,
		Bow,
		Staff,
		Dagger,
		Pet1,
		Pet2,
		Gun,
		GreatSword,
		LongBow,
		Spear,
		Orb,

		[Signal("dual-blade")]
		DualBlade,
	}


	[Signal("weapon-appearance-change-type")]
	public WeaponAppearanceChangeTypeSeq WeaponAppearanceChangeType => this.Attributes["weapon-appearance-change-type"]?.ToEnum<WeaponAppearanceChangeTypeSeq>() ?? 0;
	public enum WeaponAppearanceChangeTypeSeq
	{
		None,

		[Signal("used-only-as-target-weapon")]
		UsedOnlyAsTargetWeapon,

		[Signal("used-only-as-applying-weapon")]
		UsedOnlyAsApplyingWeapon,

		[Signal("both")]
		Both,
	}


	public SkillByEquipment SkillByEquipment => FileCache.Data.SkillByEquipment[this.Attributes["skill-by-equipment"]];
}