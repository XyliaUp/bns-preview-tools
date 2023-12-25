using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Writers;

namespace CUE4Parse.UE4.Assets;
public static class PackageEx
{
	public static void Serialize(this Package @this, FArchiveWriter writer)
	{
		@this.Summary.Serialize(writer);
	}
}