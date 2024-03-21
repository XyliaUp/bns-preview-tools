using CUE4Parse.UE4.Assets.Exports;
using Xylia.Preview.Data.Helpers;

namespace FModel.Creator;
public static class Utils
{
	public static bool TryLoadObject<T>(string fullPath, out T export) where T : UObject
	{
		return FileCache.Provider.TryLoadObject(fullPath, out export);
	}
}