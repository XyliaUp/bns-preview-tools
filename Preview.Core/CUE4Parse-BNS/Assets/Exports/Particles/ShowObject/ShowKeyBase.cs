using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public abstract class ShowKeyBase : USerializeObject
{
	[UPROPERTY]
	public float StartTime = 0;

	[UPROPERTY]
	public int SpawnId;

	[UPROPERTY]
	public float Duration;

	[UPROPERTY]
	public bool FixByTime;
}