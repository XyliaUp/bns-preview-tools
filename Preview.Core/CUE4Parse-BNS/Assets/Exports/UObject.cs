using CUE4Parse.BNS.Pack.Objects;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Writers;

namespace CUE4Parse.UE4.Assets.Exports;
public static class UObjectEx
{
	public static void Serialize(this UObject obj, FArchiveWriter writer)
	{
		SerializePropertiesTagged(obj.Properties, writer);

		//if (!Flags.HasFlag(EObjectFlags.RF_ClassDefaultObject) && Ar.ReadBoolean() && Ar.Position + 16 <= validPos)
		//{
		//	ObjectGuid = Ar.Read<FGuid>();
		//}

		//if (Ar.Game >= EGame.GAME_UE5_0 && (Flags.HasFlag(EObjectFlags.RF_ClassDefaultObject) || Flags.HasFlag(EObjectFlags.RF_DefaultSubObject)))
		//{
		//	Ar.Position += 4L;
		//}
	}

	public static void SerializePropertiesTagged(List<FPropertyTag> properties, FArchiveWriter writer)
	{
		foreach (var property in properties)
		{
			property.Serialize(writer);
		}
	}
}