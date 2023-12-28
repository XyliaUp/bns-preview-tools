using System.Runtime.CompilerServices;

using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Objects.Properties;
using CUE4Parse.UE4.Objects.UObject;
using CUE4Parse.UE4.Writers;

namespace CUE4Parse.UE4.Assets.Objects;
public static class FPropertyTagEx
{
	public static void Serialize(this FPropertyTag property, FArchiveWriter writer)
	{
		property.Name.Serialize(writer);
		property.PropertyType.Serialize(writer);
		writer.Write(property.Size);
		writer.Write(property.ArrayIndex);

		// 写属性实际数据
		property.Tag.Serialize(writer);

		//if (Version >= UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)
		//{
		//	HasPropertyGuid = Ar.ReadFlag();
		//	if (HasPropertyGuid)
		//	{
		//		PropertyGuid = Ar.Read<FGuid>();
		//	}
		//}
	}

	public static void Serialize(this FPropertyTagData instance, FArchiveWriter writer)
	{
		//switch (instance.Type)
		{
			//case "StructProperty":
			//{
			//	//StructType = Ar.ReadFName().Text;
			//	//if (Ar.Ver >= UE4Version.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG)
			//	//StructGuid = Ar.Read<FGuid>();
			//}
			//break;
			//case "BoolProperty":
			//	Writer.Write(Bool.Value);
			//	break;
			//case "ByteProperty":
			//case "EnumProperty":
			//	EnumName = Ar.ReadFName().Text;
			//	break;
			//case "ArrayProperty":
			//	if (Ar.Ver >= UE4Version.VAR_UE4_ARRAY_PROPERTY_INNER_TAGS)
			//		InnerType = Ar.ReadFName().Text;
			//	break;
			//// Serialize the following if version is past VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT
			//case "SetProperty":
			//	if (Ar.Ver >= UE4Version.VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT)
			//		InnerType = Ar.ReadFName().Text;
			//	break;
			//case "MapProperty":
			//	if (Ar.Ver >= UE4Version.VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT)
			//	{
			//		InnerType = Ar.ReadFName().Text;
			//		ValueType = Ar.ReadFName().Text;
			//	}
			//	break;
		}
	}

	// is abstract
	public static void Serialize(this FPropertyTagType instance, FArchiveWriter writer)
	{

	}

	public static void Serialize<T>(this FPropertyTagType<T> instance, FArchiveWriter writer)
	{
		var value = instance.Value;

		var size = Unsafe.SizeOf<T>();
		var _data = new byte[size];

		unsafe
		{
			fixed (byte* p = &Unsafe.As<T, byte>(ref value))
			{
				using UnmanagedMemoryStream ms = new UnmanagedMemoryStream((byte*)p, size);
				ms.Read(_data, 0, _data.Length);
			}
		}

		writer.Write(_data);
	}

	public static void Serialize(this EnumProperty instance, FArchiveWriter writer)
	{
		// !HasUnversionedProperties

		instance.Value.Serialize(writer);
	}
}