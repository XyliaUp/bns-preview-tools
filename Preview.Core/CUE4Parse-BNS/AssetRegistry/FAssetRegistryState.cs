using CUE4Parse.UE4.AssetRegistry;
using CUE4Parse.UE4.AssetRegistry.Objects;
using CUE4Parse.UE4.AssetRegistry.Readers;
using CUE4Parse.UE4.Readers;

using Newtonsoft.Json;

namespace CUE4Parse.BNS.AssetRegistry;

[JsonConverter(typeof(FAssetRegistryStateConverter))]
public class FAssetRegistryState
{
	public Objects.FAssetData[] PreallocatedAssetDataBuffers;

	public FAssetRegistryState(FArchive Ar)
	{
		FAssetRegistryVersion.TrySerializeVersion(Ar, out var version);

		var nameTableReader = new FNameTableArchiveReader(Ar,null);
		Load(nameTableReader);

		Ar.Dispose();
	}

	private void Load(FAssetRegistryArchive Ar)
	{
		PreallocatedAssetDataBuffers = Ar.ReadArray(() => new Objects.FAssetData(Ar));
	}
}