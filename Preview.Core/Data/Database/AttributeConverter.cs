using System.Diagnostics;
using System.Globalization;

using Xylia.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Models;

using Version = Xylia.Preview.Data.Common.DataStruct.Version;

namespace Xylia.Preview.Data.Database;
/// <summary>
/// Provides converting attribute text to value, as well
/// </summary>
public class AttributeConverter
{
	/// <summary>
	/// equal AttributeCollection.Get
	/// </summary>
	/// <param name="record"></param>
	/// <param name="attribute"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	public static object ConvertTo(Record record, AttributeDefinition attribute, BnsDatabase database, bool _noValidate = true)
	{
		if (_noValidate && (attribute is null || attribute.Offset >= record.DataSize)) return null;

		switch (attribute.Type)
		{
			case AttributeType.TNone: return null;
			case AttributeType.TInt8: return record.Get<sbyte>(attribute.Offset);
			case AttributeType.TInt16: return record.Get<short>(attribute.Offset);
			case AttributeType.TInt32: return record.Get<int>(attribute.Offset);
			case AttributeType.TInt64: return record.Get<long>(attribute.Offset);
			case AttributeType.TFloat32: return record.Get<float>(attribute.Offset);
			case AttributeType.TBool: return record.Get<bool>(attribute.Offset);
			case AttributeType.TString: return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var idx = record.Get<sbyte>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					return attribute.Sequence[idx];
				}
				else
				{
					if (!_noValidate) throw new Exception("Invalid sequence index");
					return idx.ToString();
				}
			}

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
			{
				var idx = record.Get<short>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					return attribute.Sequence[idx];
				}
				else
				{
					if (!_noValidate) throw new Exception("Invalid sequence index");
					return idx.ToString();
				}
			}

			case AttributeType.TRef: return database.Provider.Tables.GetRef(attribute.ReferedTable, record.Get<Ref>(attribute.Offset));
			case AttributeType.TTRef: return database.Provider.Tables.GetRef(record.Get<TRef>(attribute.Offset));
			case AttributeType.TSub: return record.Get<short>(attribute.Offset);       // class -> subtype
			case AttributeType.TSu: throw new NotSupportedException();
			case AttributeType.TVector16: throw new NotSupportedException();
			case AttributeType.TVector32: return record.Get<Vector32>(attribute.Offset);
			case AttributeType.TIColor: return record.Get<IColor>(attribute.Offset);
			case AttributeType.TFColor: throw new NotSupportedException();
			case AttributeType.TBox: return record.Get<Box>(attribute.Offset);
			case AttributeType.TAngle: throw new NotSupportedException();
			case AttributeType.TMsec: return record.Get<Msec>(attribute.Offset);
			case AttributeType.TDistance: return record.Get<Distance>(attribute.Offset);
			case AttributeType.TVelocity: return record.Get<Velocity>(attribute.Offset);
			case AttributeType.TScript_obj:
			{
				var scriptObjBytes = record.Data[attribute.Offset..(attribute.Offset + attribute.Size)];
				if (scriptObjBytes.All(x => x == 0))
					return null;

				return System.Convert.ToBase64String(scriptObjBytes);
			}
			case AttributeType.TNative:
			{
				var n = record.Get<Native>(attribute.Offset);
				return record.StringLookup.GetString(n.Offset);
			}
			case AttributeType.TVersion: return record.Get<Version>(attribute.Offset);
			case AttributeType.TIcon: return database.Provider.Tables.GetRef(record.Get<IconRef>(attribute.Offset));
			case AttributeType.TTime32: throw new NotSupportedException();
			case AttributeType.TTime64: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown1: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown2: return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			default: throw new Exception($"Unhandled type name: '{attribute.Type}'");
		}
	}

	/// <summary>
	/// Converts the specified attribute text into a value.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="attribute"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static object ConvertBack(string value, AttributeDefinition attribute, BnsDatabase database) => attribute.Type switch
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
		AttributeType.TRef => database.Provider.Tables.GetRecord(attribute.ReferedTableName, value),
		AttributeType.TTRef => database.Provider.Tables.GetRecord(value),
		AttributeType.TSub => short.Parse(value),
		//AttributeType.TSu => value,
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
		AttributeType.TIcon => database.Provider.Tables.GetIconRecord(value, out var index),
		//AttributeType.TTime32 => value,
		AttributeType.TTime64 => Time64.Parse(value),
		AttributeType.TXUnknown1 => Time64.Parse(value),
		AttributeType.TXUnknown2 => new ObjectPath(value),
		_ => throw new Exception($"Unhandled type name: '{attribute.Type}'"),
	};



	#region Model Extension Method
	/// <summary>
	/// Converts the specified text into an object.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <param name="database"></param>
	/// <returns></returns>
	/// <exception cref="FormatException"></exception>
	public static object Convert(string value, Type type, BnsDatabase database)
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


		if (TypeCode.TryGetValue(type, out var code)) return ConvertBack(value, new AttributeDefinition() { Type = code }, database);

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
	#endregion
}