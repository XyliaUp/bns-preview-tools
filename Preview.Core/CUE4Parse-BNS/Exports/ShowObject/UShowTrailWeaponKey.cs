using CUE4Parse.UE4.Assets;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowTrailWeaponKey : ShowKeyBase
{
	public string AttachedWeapon;  // TRAILWEAPON_WEAPON_L(EnumProperty)
	public bool Loop;
	public string PP_Distortion;   // POSTPROC_CASTER_TARGET(EnumProperty)
	public ResolvedObject TrailSys;
}