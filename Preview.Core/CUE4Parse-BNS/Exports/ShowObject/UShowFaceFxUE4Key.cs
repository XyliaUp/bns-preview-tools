using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Exports;
public sealed class UShowFaceFxUE4Key : ShowKeyBase
{
	public FSoftObjectPath FaceFXAnimObj;

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		FaceFXAnimObj = GetOrDefault<FSoftObjectPath>(nameof(FaceFXAnimObj));
	}
}