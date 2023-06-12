using System.Collections.Generic;

using CUE4Parse.UE4.AssetRegistry.Objects;
using CUE4Parse.UE4.AssetRegistry.Readers;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.AssetRegistry.Objects
{
	public class FAssetData
	{
		public FName ObjectPath;
		public FName PackagePath;
		public FName AssetClass;
		public FName PackageName;
		public FName AssetName;
		public IDictionary<FName, string> TagsAndValues;
		public FAssetBundleData TaggedAssetBundles;
		public int[] ChunkIDs;
		public uint[] PackageFlags;


		public readonly long[] ChunkIDs2;
		public readonly string ObjectPath2;

		public FAssetData(FAssetRegistryArchive Ar)
		{
			ObjectPath = Ar.ReadFName();
			PackagePath = Ar.ReadFName();
			AssetClass = Ar.ReadFName();
			PackageName = Ar.ReadFName();
			AssetName = Ar.ReadFName();

			SerializeTagsAndBundles(Ar , this);

			ChunkIDs2 = Ar.ReadArray<long>();
			ObjectPath2 = Ar.ReadFString();
		}



		public static void SerializeTagsAndBundles(FAssetRegistryArchive baseArchive, FAssetData assetData)
		{
			var size = baseArchive.Read<int>();
			var ret = new Dictionary<FName, string>();
			for (var i = 0; i < size; i++)
			{
				ret[baseArchive.ReadFName()] = baseArchive.ReadFString();
			}
			assetData.TagsAndValues = ret;
			assetData.TaggedAssetBundles = new FAssetBundleData();
		}
	}
}