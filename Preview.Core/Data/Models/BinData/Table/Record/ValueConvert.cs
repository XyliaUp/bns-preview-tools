using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Models.BinData.Table.Record;
public static class ValueConvert
{
	public static object Construct(Type type, string value , BaseRecord Source = null)
	{
		if (string.IsNullOrEmpty(value)) return default;

		if (type == typeof(sbyte)) return sbyte.Parse(value);				/// <see cref="AttributeType.TInt8">
		else if (type == typeof(short)) return short.Parse(value);			/// <see cref="AttributeType.TInt16">
		else if (type == typeof(int)) return int.Parse(value);				/// <see cref="AttributeType.TInt32">
		else if (type == typeof(long)) return long.Parse(value);			/// <see cref="AttributeType.TInt64">
		else if (type == typeof(float)) return float.Parse(value);			/// <see cref="AttributeType.TFloat32">
		else if (type == typeof(bool)) return value.ToBool();				/// <see cref="AttributeType.TBool">
		else if (type == typeof(string)) return value;						/// <see cref="AttributeType.TString">

		else if (type.IsEnum)
		{
			if (value.TryParseToEnum(type, out var seq, Extension: true)) return seq;
			throw new FormatException($"Seq `{type.Name}` cast failed: {value}");
		}
		else if (type == typeof(DateTime)) return DateTime.Parse(value);
		else if (type == typeof(BaseRecord)) return value.CastObject();		/// use base class mean as <see cref="AttributeType.TTRef">
		else if (typeof(BaseRecord).IsAssignableFrom(type))      /// use sub class mean as <see cref="AttributeType.TRef">
		{
			// InvalidOperationException: ValueFactory attempted to access the Value property of this instance.
			// this mean object conflicts occurred:	  eg. Item -> Card -> Item   
			// TODO: Delay set attribute ?
			BaseRecord obj = null;

			try 
			{ 
				obj = value.CastObject<BaseRecord>(type.Name);
			}
			catch
			{
				Trace.WriteLine($"Construct Failed , {Source?.GetType()} -> {type.Name}");
			}


			if (obj is null)
			{
				var record = (BaseRecord)Activator.CreateInstance(type);
				if (!record.ContainAttribute<AliasRecord>()) Debug.WriteLine($"warning: target is not AliasRecord ({type.Name})");

				record.alias = value;
				return record;
			}

			return obj;
		}
		else if (type == typeof(Msec)) return new Msec(int.Parse(value));	/// <see cref="AttributeType.TMsec">
		else if (type == typeof(FPath)) return new FPath(value);			/// <see cref="AttributeType.TXUnknown2">


		throw new NotSupportedException($"type not supported: {type}");
	}
}