using CUE4Parse.UE4.AssetRegistry.Objects;
using CUE4Parse.UE4.AssetRegistry.Readers;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Versions;

namespace CUE4Parse.BNS.AssetRegistry.Objects;
public class FAssetData
{
	public readonly FName PackageName;
	public readonly FName PackagePath;
	public readonly FName AssetName;
	public readonly FName AssetClass;
	public IDictionary<FName, string> TagsAndValues;
	public FAssetBundleData TaggedAssetBundles;
	public readonly int[] ChunkIDs;
	public readonly uint PackageFlags;

	public readonly string ObjectPath2;

	public FAssetData(FAssetRegistryArchive Ar)
	{
		if (Ar.Header.Version < FAssetRegistryVersionType.RemoveAssetPathFNames)
		{
			var oldObjectPath = Ar.ReadFName();
		}
		PackagePath = Ar.ReadFName();
		AssetClass = Ar.Header.Version >= FAssetRegistryVersionType.ClassPaths ? new FTopLevelAssetPath(Ar).AssetName : Ar.ReadFName();
		if (Ar.Header.Version < FAssetRegistryVersionType.RemovedMD5Hash)
		{
			var oldGroupNames = Ar.ReadFName();
		}
		PackageName = Ar.ReadFName();
		AssetName = Ar.ReadFName();

		SerializeTagsAndBundles(Ar, this);

		if (Ar.Ver >= EUnrealEngineObjectUE4Version.CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
		{
			ChunkIDs = Ar.ReadArray<int>();
		}
		else if (Ar.Ver >= EUnrealEngineObjectUE4Version.ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
		{
			ChunkIDs = new[] { Ar.Read<int>() };
		}
		else
		{
			ChunkIDs = Array.Empty<int>();
		}

		if (Ar.Ver >= EUnrealEngineObjectUE4Version.COOKED_ASSETS_IN_EDITOR_SUPPORT)
		{
			PackageFlags = Ar.Read<uint>();
		}

		ObjectPath2 = Ar.ReadFString();
	}

	public static void SerializeTagsAndBundles(FAssetRegistryArchive baseArchive, FAssetData assetData)
	{
		var size = baseArchive.Read<int>();
		var ret = new Dictionary<FName, string>(size);
		for (var i = 0; i < size; i++)
		{
			ret[baseArchive.ReadFName()] = baseArchive.ReadFString();
		}

		// I don't know why this can improve efficiency
		//assetData.TagsAndValues = ret;
		//assetData.TaggedAssetBundles = new FAssetBundleData();
	}

	public string ObjectPath => $"{PackageName}.{AssetName}";
}