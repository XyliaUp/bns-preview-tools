using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Serialize;

namespace CUE4Parse.BNS.Pack.Objects;
public static class UObjectEx
{
	public static void Serialize(this UObject obj, BinaryWriter writer, SerializeOption option)
	{
		SerializePropertiesTagged(obj.Properties, writer, option);

		//if (!Flags.HasFlag(EObjectFlags.RF_ClassDefaultObject) && Ar.ReadBoolean() && Ar.Position + 16 <= validPos)
		//{
		//	ObjectGuid = Ar.Read<FGuid>();
		//}
	}

	public static void SerializePropertiesTagged(List<FPropertyTag> properties, BinaryWriter writer, SerializeOption option)
	{
		foreach (var property in properties)
		{
			property.Serialize(writer, option);
		}
	}
}