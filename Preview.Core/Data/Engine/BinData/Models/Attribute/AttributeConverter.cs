using System.Diagnostics;
using System.Globalization;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;
/// <summary>
/// Provides converting attribute text to value, as well
/// </summary>
public class AttributeConverter
{
	/// <summary>
	/// equal AttributeDocument.Get
	/// </summary>
	/// <param name="record"></param>
	/// <param name="attribute"></param>
	/// <param name="provider"></param>
	/// <returns></returns>
	public static object ConvertTo(Record record, AttributeDefinition attribute, IDataProvider provider, bool _noValidate = true)
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
			case AttributeType.TBool: return record.Get<BnsBoolean>(attribute.Offset);
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

			case AttributeType.TRef: return provider.Tables.GetRef(attribute.ReferedTable, record.Get<Ref>(attribute.Offset));
			case AttributeType.TTRef: return provider.Tables.GetRef(record.Get<TRef>(attribute.Offset));
			case AttributeType.TSub: return provider.Tables.GetSub(attribute.ReferedTable, record.Get<Sub>(attribute.Offset));
			case AttributeType.TSu: return record.Get<Su>(attribute.Offset);
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
			case AttributeType.TVersion: return record.Get<BnsVersion>(attribute.Offset);
			case AttributeType.TIcon: return provider.Tables.GetRef(record.Get<IconRef>(attribute.Offset));
			case AttributeType.TTime32: throw new NotSupportedException();
			case AttributeType.TTime64: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown1: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown2: return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			default: throw new Exception($"Unhandled type: '{attribute.Type}'");
		}
	}

	/// <summary>
	/// Converts the specified attribute text into a value.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="attribute"></param>
	/// <param name="provider"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static object ConvertBack(string value, AttributeDefinition attribute, IDataProvider provider) => attribute.Type switch
	{
		AttributeType.TNone => null,
		AttributeType.TInt8 => sbyte.Parse(value),
		AttributeType.TInt16 => short.Parse(value),
		AttributeType.TInt32 => int.Parse(value),
		AttributeType.TInt64 => long.Parse(value),
		AttributeType.TFloat32 => float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture),
		AttributeType.TBool => BnsBoolean.Parse(value),
		AttributeType.TString => value,
		AttributeType.TSeq => value,
		AttributeType.TSeq16 => value,
		AttributeType.TRef => provider.Tables.GetRecord(attribute.ReferedTableName, value),
		AttributeType.TTRef => provider.Tables.GetRecord(value),
		AttributeType.TSub => value,
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
		//AttributeType.TTime32 => value,
		AttributeType.TTime64 => Time64.Parse(value),
		AttributeType.TXUnknown1 => Time64.Parse(value),     //	((DateTime)value).Truncate();
		AttributeType.TXUnknown2 => new ObjectPath(value),
		_ => throw new Exception($"Unhandled type name: '{attribute.Type}'"),
	};



	#region Model Extension Method		

	internal static readonly Dictionary<Type, AttributeType> TypeCode = new()
	{
		[typeof(sbyte)] = AttributeType.TInt8,
		[typeof(short)] = AttributeType.TInt16,
		[typeof(int)] = AttributeType.TInt32,
		[typeof(long)] = AttributeType.TInt64,
		[typeof(float)] = AttributeType.TFloat32,
		[typeof(double)] = AttributeType.TFloat32,
		[typeof(bool)] = AttributeType.TBool,
		[typeof(string)] = AttributeType.TString,

		[typeof(Su)] = AttributeType.TSu,
		[typeof(Sub)] = AttributeType.TSub,

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
		[typeof(BnsVersion)] = AttributeType.TVersion,
		[typeof(Time64)] = AttributeType.TXUnknown1,
		[typeof(ObjectPath)] = AttributeType.TXUnknown2,
	};

	/// <summary>
	/// Converts the attribute value to an object.
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	/// <exception cref="FormatException"></exception>
	internal static object Convert(object value, Type type)
	{
		if (value is null || value.GetType() == type) return value;
		else if (type == typeof(bool))
		{
			if (value is BnsBoolean b) return (bool)b;
		}
		else if (type == typeof(string)) return value.ToString();
		else if (type.IsEnum) return value.ToString().TryParseToEnum(type, out var seq) ? seq : default;
		else if (type.IsGenericType)
		{
			var item = type.GetGenericTypeDefinition();
			if (item == typeof(Ref<>)) return Activator.CreateInstance(type, value);
		}

		//throw new NotSupportedException($"type not supported: {type}");
		Trace.WriteLine($"type not supported: {type}");
		return null;
	}
	#endregion
}