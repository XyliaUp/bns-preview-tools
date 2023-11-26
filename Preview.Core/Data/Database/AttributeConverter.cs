using System.Diagnostics;
using System.Globalization;

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
	/// <summary>
	/// Converts the specified attribute text into a value.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static object ConvertTo(string value, AttributeType type, BnsDatabase database) => type switch
	{
		AttributeType.TNone => null,
		AttributeType.TInt8 => sbyte.Parse(value),
		AttributeType.TInt16 => short.Parse(value),
		AttributeType.TInt32 => int.Parse(value),
		AttributeType.TInt64 => long.Parse(value),
		AttributeType.TFloat32 => float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture),
		AttributeType.TBool => value.ToBool(),
		AttributeType.TString => value,
		AttributeType.TSeq => value,
		AttributeType.TSeq16 => value,
		AttributeType.TRef => value,
		AttributeType.TTRef => value,
		AttributeType.TSub => short.Parse(value),
		AttributeType.TSu => value,
		AttributeType.TVector16 => Vector16.Parse(value),
		AttributeType.TVector32 => Vector32.Parse(value),
		AttributeType.TIColor => IColor.Parse(value),
		//case AttributeType.TFColor: return FColor.Parse(value);
		AttributeType.TBox => Box.Parse(value),
		//case AttributeType.TAngle: return Angle.Parse(value);
		AttributeType.TMsec => (Msec)int.Parse(value),
		AttributeType.TDistance => (Distance)short.Parse(value),
		AttributeType.TVelocity => (Velocity)ushort.Parse(value),
		AttributeType.TProp_seq => value,
		AttributeType.TProp_field => value,
		AttributeType.TScript_obj => new Script_obj(value),
		AttributeType.TNative => value,
		AttributeType.TVersion => new Version(value),
		AttributeType.TIcon => value,
		AttributeType.TTime32 => value,
		AttributeType.TTime64 => Time64.Parse(value),
		AttributeType.TXUnknown1 => Time64.Parse(value),
		AttributeType.TXUnknown2 => new ObjectPath(value),
		_ => throw new Exception($"Unhandled type name: '{type}'"),
	};

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

			Trace.WriteLine($"Seq `{type.Name}` cast failed: {value}");
			return default;
		}
		else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Ref<>)) return Activator.CreateInstance(type, value, database);
		else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Sub<>)) return Activator.CreateInstance(type, value, database);


		if (TypeCode.TryGetValue(type, out var code)) return ConvertTo(value, code, database);

		//throw new NotSupportedException($"type not supported: {type}");
		Trace.WriteLine($"type not supported: {type}");
		return null;
	}


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

		[typeof(Script_obj)] = AttributeType.TScript_obj,
		[typeof(Version)] = AttributeType.TVersion,
		[typeof(Time64)] = AttributeType.TXUnknown1,
		[typeof(ObjectPath)] = AttributeType.TXUnknown2,
	};


	// throw new Exception($"Invalid typed reference, refered table doesn't exist: '{split[0]}'");
	// throw new Exception($"Invalid typed reference string: '{value}'");

	//private void SetIcon(Record record, AttributeDefinition attrDef, string value)
	//{
	//	if (value != null)
	//	{
	//		var colon = value.LastIndexOf(':');

	//		if (colon != -1)
	//		{
	//			var split = new[] { value[..colon], value[(colon + 1)..] };
	//			var i32 = int.Parse(split[1]);

	//			if (_resolvedAliases.ByAlias[_definitions.IconTextureTableId].TryGetValue(split[0], out var @ref))
	//			{
	//				record.Set(attrDef.Offset, new IconRef(@ref.Id, @ref.Variant, i32));
	//				return;
	//			}

	//			@ref = Ref.From(split[0]);
	//			record.Set(attrDef.Offset, new IconRef(@ref.Id, @ref.Variant, i32));
	//			return;
	//		}

	//		throw new Exception($"Invalid icon reference string: '{value}'");
	//		return;
	//	}

	//	record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DIconRef);
	//}
}