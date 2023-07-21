using CUE4Parse.BNS;
using CUE4Parse.UE4.Assets.Exports;

namespace Xylia.Preview.Data.Helper;
public static class FileCache
{
    #region Data
    public static DataTableSet Data = new();

    public static PakData PakData = new();

    public static void Clear()
    {
        Data?.Dispose();
        Data = new();

        PakData?.Dispose();
        PakData = new();
    }
    #endregion


    #region Extension 
    public static UObject GetUObject(this string Path) => PakData.LoadObject<UObject>(Path);
    #endregion
}