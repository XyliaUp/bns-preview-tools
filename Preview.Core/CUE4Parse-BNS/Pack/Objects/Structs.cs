using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Pack.Objects;
public static class Structs
{
	public static void Serialize(this FName instance, BinaryWriter writer)
	{
		writer.Write(instance.Index);
		writer.Write(instance.Number);
	}
}