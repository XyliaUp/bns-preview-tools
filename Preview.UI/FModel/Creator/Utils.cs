using CUE4Parse.UE4.Assets.Exports;
using Xylia.Preview.Data.Helper;

namespace FModel.Creator;
public static class Utils
{
	public static bool TryLoadObject<T>(string fullPath, out T export) where T : UObject
	{
		export = FileCache.PakData.LoadObject<T>(fullPath);
		return export != null;
	}
}