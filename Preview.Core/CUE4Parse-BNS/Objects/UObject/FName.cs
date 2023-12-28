using CUE4Parse.UE4.Writers;

namespace CUE4Parse.UE4.Objects.UObject;
public static partial class FNameEx
{
	public static void Serialize(this FName @this, FArchiveWriter writer)
	{
		writer.Write(@this.Index);
		writer.Write(@this.Number);
	}
}