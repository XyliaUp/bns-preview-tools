using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowTrailWeaponKey : ShowKeyBase
{
	[UPROPERTY]
	public string AttachedWeapon;  // TRAILWEAPON_WEAPON_L(EnumProperty)

	[UPROPERTY]
	public bool Loop;

	[UPROPERTY]
	public string PP_Distortion;   // POSTPROC_CASTER_TARGET(EnumProperty)

	[UPROPERTY]
	public FPackageIndex TrailSys;
}