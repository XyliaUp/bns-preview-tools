using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Assets.Exports;
public sealed class UShowPhysicsKey : ShowKeyBase
{
	public string DamageSoundType; // SPhysicsDamage_Heavy(EnumProperty)
	public float fForce;
	public int nBoneIndex;
	public string PhysicsType; // PHYSICS_ATTACK(EnumProperty)
	public FVector vDir;
}