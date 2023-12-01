using CUE4Parse.UE4.Assets.Exports;

namespace CUE4Parse.BNS.Assets.Exports;
public abstract class ShowKeyBase : USerializeObject
{
	//[UPROPERTY]
	public float StartTime => this.GetOrDefault<float>(nameof(StartTime));

	//[UPROPERTY]
	public int SpawnId => this.GetOrDefault<int>(nameof(SpawnId));

	//[UPROPERTY]
	public float Duration => this.GetOrDefault<int>(nameof(Duration));

	//[UPROPERTY]
	public bool FixByTime => this.GetOrDefault<bool>(nameof(FixByTime));
}