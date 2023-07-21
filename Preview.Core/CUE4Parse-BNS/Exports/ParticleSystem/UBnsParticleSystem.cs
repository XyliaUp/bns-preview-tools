using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;

namespace CUE4Parse.BNS.Exports;
public class UBnsParticleSystem : UObject
{
	public bool bShouldResetPeakCounts;

	public ResolvedObject CurveEdSetup;
	public ResolvedObject[] Emitters;

	public int[] LODDistances;
}