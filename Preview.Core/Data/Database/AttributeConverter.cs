using System.Diagnostics;

using Xylia.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;

using Version = Xylia.Preview.Data.Common.DataStruct.Version;

namespace Xylia.Preview.Data.Database;
/// <summary>
/// Provides converting attribute text to value, as well
/// </summary>
public class AttributeConverter 
{
	private static readonly Dictionary<Type, AttributeType> TypeCode = new()
	{
		[typeof(sbyte)] = AttributeType.TInt8,
		[typeof(short)] = AttributeType.TInt16,
		[typeof(int)] = AttributeType.TInt32,
		[typeof(long)] = AttributeType.TInt64,
		[typeof(float)] = AttributeType.TFloat32,
		[typeof(bool)] = AttributeType.TBool,
		[typeof(string)] = AttributeType.TString,

		[typeof(Vector16)] = AttributeType.TVector16,
		[typeof(Vector32)] = AttributeType.TVector32,
		[typeof(IColor)] = AttributeType.TIColor,
		//[typeof(FColor)] = AttributeType.TFColor,
		[typeof(Box)] = AttributeType.TBox,
		[typeof(Msec)] = AttributeType.TMsec,
		[typeof(Distance)] = AttributeType.TDistance,
		[typeof(Velocity)] = AttributeType.TVelocity,
		[typeof(Distance)] = AttributeType.TDistance,

		[typeof(Version)] = AttributeType.TVersion,
		[typeof(Time64)] = AttributeType.TXUnknown1,
		[typeof(ObjectPath)] = AttributeType.TXUnknown2,
	};


	/// <summary>
	/// Converts the specified attribute text into a value.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static object ConvertTo(string value, AttributeType type, BnsDatabase database)
	{
		switch (type)
		{
			case AttributeType.TNone: return null;
			case AttributeType.TInt8: return sbyte.Parse(value);
			case AttributeType.TInt16: return short.Parse(value);
			case AttributeType.TInt32: return int.Parse(value);
			case AttributeType.TInt64: return long.Parse(value);
			case AttributeType.TFloat32: return float.Parse(value);
			case AttributeType.TBool: return value.ToBool();
			case AttributeType.TString: return value;
			case AttributeType.TSeq: return value;
			case AttributeType.TSeq16: return value;
			case AttributeType.TRef: return value;
			case AttributeType.TTRef: return value;
			case AttributeType.TSub: return value;
			case AttributeType.TSu: return value;
			case AttributeType.TVector16: return Vector16.Parse(value);
			case AttributeType.TVector32: return Vector32.Parse(value);
			case AttributeType.TIColor: return IColor.Parse(value);
			//case AttributeType.TFColor: return FColor.Parse(value);
			case AttributeType.TBox: return Box.Parse(value);
			//case AttributeType.TAngle: return Angle.Parse(value);
			case AttributeType.TMsec: return (Msec)int.Parse(value);
			case AttributeType.TDistance: return (Distance)short.Parse(value);
			case AttributeType.TVelocity: return (Velocity)ushort.Parse(value);
			case AttributeType.TProp_seq: return value;
			case AttributeType.TProp_field: return value;
			case AttributeType.TScript_obj: return new Script_obj(value);
			case AttributeType.TNative: return value;
			case AttributeType.TVersion: return new Version(value);
			case AttributeType.TIcon: return value;
			case AttributeType.TTime32: return value;
			case AttributeType.TTime64: return Time64.Parse(value);
			case AttributeType.TXUnknown1: return Time64.Parse(value);
			case AttributeType.TXUnknown2: return new ObjectPath(value);

			default: throw new Exception($"Unhandled type name: '{type}'");
		}
	}

	/// <summary>
	/// Converts the specified text into an object.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	/// <exception cref="FormatException"></exception>
	public static object ConvertTo(string value, Type type, BnsDatabase database)
	{
		if (string.IsNullOrEmpty(value)) return default;
		else if (type.IsEnum)
		{
			if (value.TryParseToEnum(type, out var seq, Extension: true)) return seq;
			throw new FormatException($"Seq `{type.Name}` cast failed: {value}");
		}
		else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Ref<>)) return Activator.CreateInstance(type, value, database);
		else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Sub<>)) return Activator.CreateInstance(type, value, database);


		if (TypeCode.TryGetValue(type, out var code)) return ConvertTo(value, code, database);

		//throw new NotSupportedException($"type not supported: {type}");
		Trace.WriteLine($"== WARNING == type not supported: {type}");
		return null;
	}
}