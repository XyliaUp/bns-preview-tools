using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;

namespace CUE4Parse.BNS.Exports;
public class UShowObject : UObject
{
	public ResolvedObject[] EventKeys;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		EventKeys = GetOrDefault<ResolvedObject[]>(nameof(EventKeys));
	}
}