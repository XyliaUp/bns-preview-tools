using CUE4Parse.UE4.AssetRegistry.Objects;
using CUE4Parse.UE4.AssetRegistry.Readers;
using CUE4Parse.UE4.Readers;

using Newtonsoft.Json;

namespace CUE4Parse.BNS.AssetRegistry;

[JsonConverter(typeof(FAssetRegistryStateConverter))]
public class FAssetRegistryState
{
	Objects.FAssetData[] PreallocatedAssetDataBuffers;
	public Dictionary<string, string> ObjectRef = [];

	public FAssetRegistryState(FArchive Ar)
	{
		var header = new FAssetRegistryHeader(Ar);

		var nameTableReader = new FNameTableArchiveReader(Ar, header);
		Load(nameTableReader);
	}

	private void Load(FAssetRegistryArchive Ar)
	{
		ObjectRef = new(StringComparer.OrdinalIgnoreCase);

		PreallocatedAssetDataBuffers = Ar.ReadArray(() =>
		{
			var asset = new Objects.FAssetData(Ar);
			ObjectRef[asset.ObjectPath2] = asset.ObjectPath.Text;

			return asset;
		});
	}


	public IEnumerable<Objects.FAssetData> GetAssets(Predicate<Objects.FAssetData> Filter)
	{
		return PreallocatedAssetDataBuffers.Where(asset => Filter(asset));
	}
}