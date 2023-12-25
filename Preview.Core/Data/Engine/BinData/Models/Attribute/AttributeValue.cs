using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;

/// <summary>
/// Represent a Attribute Value
/// </summary>
[JsonConverter(typeof(AttributeValueConverter))]
public class AttributeValue : IComparable<AttributeValue>, IEquatable<AttributeValue>
{
	/// <summary>
	/// Represent a NullValue
	/// </summary>
	public static AttributeValue Null = new AttributeValue(AttributeType.TNone, null);

	/// <summary>
	/// Indicate type of this AttributeValue
	/// </summary>
	public AttributeType Type { get; }

	/// <summary>
	/// Get internal .NET value object
	/// </summary>
	public virtual object RawValue { get; private set; }

	#region Constructor
	internal AttributeValue(AttributeType type, object value)
	{
		this.Type = type;
		this.RawValue = value;
	}

	protected AttributeValue(object value)
	{
		this.RawValue = value;

		if (value == null) this.Type = AttributeType.TNone;
		else if (value is AttributeValue v)
		{
			this.Type = v.Type;
			this.RawValue = v.RawValue;
		}
		else if (value is bool b)
		{
			this.RawValue = (BnsBoolean)b;
			this.Type = AttributeType.TBool;
		}
		else if (AttributeConverter.TypeCode.TryGetValue(value.GetType(), out var code)) this.Type = code;
		else
		{
			// test for array or dictionary (document)
			// test first for dictionary (because IDictionary implements IEnumerable)
			if (value is IDictionary dictionary)
			{
				if (value is not IDictionary<string, AttributeValue>)
				{
					var dict = new Dictionary<string, AttributeValue>();

					foreach (var key in dictionary.Keys)
					{
						dict.Add(key.ToString(), new AttributeValue(dictionary[key]));
					}

					//this.Type = AttributeType.TDocument;
					this.RawValue = dict;
				}
			}
			else if (value is IEnumerable enumerable)
			{
				if (value is not IList<AttributeValue>)
				{
					var list = new List<AttributeValue>();

					foreach (var x in enumerable)
					{
						list.Add(new AttributeValue(x));
					}

					//this.Type = AttributeType.TArray;
					this.RawValue = list;
				}
			}
			else
			{
				throw new InvalidCastException("Value is not a valid attribute type");
			}
		}
	}

	public static AttributeValue Create(object rawValue)
	{
		return new AttributeValue(rawValue);
	}
	#endregion

	#region Index "this" property
	/// <summary>
	/// Get/Set a field for document. Fields are case sensitive - Works only when value are document
	/// </summary>
	public virtual AttributeValue this[string name]
	{
		get => throw new InvalidOperationException("Cannot access non-document type value on " + this.RawValue);
		set => throw new InvalidOperationException("Cannot access non-document type value on " + this.RawValue);
	}

	/// <summary>
	/// Get/Set value in array position. Works only when value are array
	/// </summary>
	public virtual AttributeValue this[int index]
	{
		get => throw new InvalidOperationException("Cannot access non-array type value on " + this.RawValue);
		set => throw new InvalidOperationException("Cannot access non-array type value on " + this.RawValue);
	}

	#endregion


	#region Convert types
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AttributeArray AsArray => this as AttributeArray;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public AttributeDocument AsDocument => this as AttributeDocument;


	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool AsBoolean => (BnsBoolean)this.RawValue;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public string AsString => (string)this.RawValue;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public int AsInt8 => Convert.ToSByte(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public short AsInt16 => Convert.ToInt16(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public int AsInt32 => Convert.ToInt32(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public long AsInt64 => Convert.ToInt64(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public float AsFloat => Convert.ToSingle(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public decimal AsDecimal => Convert.ToDecimal(this.RawValue);

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public DateTime AsDateTime => (DateTime)this.RawValue;
	#endregion

	#region IsTypes
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsNone => this.RawValue is null;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsArray => this is AttributeArray;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsDocument => this is AttributeDocument;


	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsInt8 => this.Type == AttributeType.TInt8;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsInt16 => this.Type == AttributeType.TInt16;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsInt32 => this.Type == AttributeType.TInt32;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsInt64 => this.Type == AttributeType.TInt64;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsFloat32 => this.Type == AttributeType.TFloat32;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsNumeric => this.IsInt8 || this.IsInt16 || this.IsInt32 || this.IsInt64 || this.IsFloat32;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsBoolean => this.Type == AttributeType.TBool;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsString => this.Type == AttributeType.TString;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsSeq => this.Type == AttributeType.TSeq;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsSeq16 => this.Type == AttributeType.TSeq16;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsRef => this.Type == AttributeType.TRef;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsTRef => this.Type == AttributeType.TTRef;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsProp_seq => this.Type == AttributeType.TProp_seq;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsProp_field => this.Type == AttributeType.TProp_field;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsNative => this.Type == AttributeType.TNative;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public bool IsDateTime => this.Type == AttributeType.TXUnknown1;
	#endregion

	#region Implicit Ctor
	// Int32
	public static implicit operator Int32(AttributeValue value) => (Int32)value.RawValue;
	public static implicit operator AttributeValue(Int32 value) => new AttributeValue(value);

	// Int64
	public static implicit operator Int64(AttributeValue value) => (Int64)value.RawValue;
	public static implicit operator AttributeValue(Int64 value) => new AttributeValue(value);

	// Double
	public static implicit operator Double(AttributeValue value) => (Double)value.RawValue;

	public static implicit operator AttributeValue(Double value) => new AttributeValue(value);

	// Decimal
	public static implicit operator Decimal(AttributeValue value) => (Decimal)value.RawValue;
	public static implicit operator AttributeValue(Decimal value) => new AttributeValue(value);

	// UInt64 (to avoid ambigous between Double-Decimal)
	public static implicit operator UInt64(AttributeValue value) => (UInt64)value.RawValue;
	public static implicit operator AttributeValue(UInt64 value) => new AttributeValue(value);

	// String
	public static implicit operator String(AttributeValue value) => (String)value.RawValue;
	public static implicit operator AttributeValue(String value) => new AttributeValue(value);

	// Boolean
	public static implicit operator Boolean(AttributeValue value) => (Boolean)value.RawValue;
	public static implicit operator AttributeValue(Boolean value) => new AttributeValue(value);

	// DateTime
	public static implicit operator DateTime(AttributeValue value) => (DateTime)value.RawValue;
	public static implicit operator AttributeValue(DateTime value) => new AttributeValue(value);

	// +
	public static AttributeValue operator +(AttributeValue left, AttributeValue right)
	{
		if (!left.IsNumeric || !right.IsNumeric) return Null;

		if (left.IsInt8 && right.IsInt8) return left.AsInt8 + right.AsInt8;
		if (left.IsInt16 && right.IsInt16) return left.AsInt16 + right.AsInt16;
		if (left.IsInt32 && right.IsInt32) return left.AsInt32 + right.AsInt32;
		if (left.IsInt64 && right.IsInt64) return left.AsInt64 + right.AsInt64;

		var result = left.AsDecimal + right.AsDecimal;
		var type = (AttributeType)Math.Max((int)left.Type, (int)right.Type);

		return
			type == AttributeType.TInt64 ? new AttributeValue((Int64)result) :
			type == AttributeType.TFloat32 ? new AttributeValue((Single)result) :
			new AttributeValue(result);
	}

	// -
	public static AttributeValue operator -(AttributeValue left, AttributeValue right)
	{
		if (!left.IsNumeric || !right.IsNumeric) return Null;

		if (left.IsInt8 && right.IsInt8) return left.AsInt8 - right.AsInt8;
		if (left.IsInt16 && right.IsInt16) return left.AsInt16 - right.AsInt16;
		if (left.IsInt32 && right.IsInt32) return left.AsInt32 - right.AsInt32;
		if (left.IsInt64 && right.IsInt64) return left.AsInt64 - right.AsInt64;

		var result = left.AsDecimal - right.AsDecimal;
		var type = (AttributeType)Math.Max((int)left.Type, (int)right.Type);

		return
			type == AttributeType.TInt64 ? new AttributeValue((Int64)result) :
			type == AttributeType.TFloat32 ? new AttributeValue((Single)result) :
			new AttributeValue(result);
	}

	// *
	public static AttributeValue operator *(AttributeValue left, AttributeValue right)
	{
		if (!left.IsNumeric || !right.IsNumeric) return Null;

		if (left.IsInt8 && right.IsInt8) return left.AsInt8 * right.AsInt8;
		if (left.IsInt16 && right.IsInt16) return left.AsInt16 * right.AsInt16;
		if (left.IsInt32 && right.IsInt32) return left.AsInt32 * right.AsInt32;
		if (left.IsInt64 && right.IsInt64) return left.AsInt64 * right.AsInt64;

		var result = left.AsDecimal * right.AsDecimal;
		var type = (AttributeType)Math.Max((int)left.Type, (int)right.Type);

		return
			type == AttributeType.TInt64 ? new AttributeValue((Int64)result) :
			type == AttributeType.TFloat32 ? new AttributeValue((Single)result) :
			new AttributeValue(result);
	}

	// /
	public static AttributeValue operator /(AttributeValue left, AttributeValue right)
	{
		if (!left.IsNumeric || !right.IsNumeric) return Null;

		return left.AsFloat / right.AsFloat;
	}

	public override string ToString() => this.RawValue?.ToString();
	#endregion

	#region IComparable, IEquatable		   
	public virtual int CompareTo(AttributeValue other)
	{
		return this.CompareTo(other, Collation.Binary);
	}

	public virtual int CompareTo(AttributeValue other, Collation collation)
	{
		// first, test if types are different
		if (this.Type != other.Type)
		{
			// for compare Null
			if (this.IsNone || other.IsNone)
			{
				return this.IsNone == other.IsNone ? 0 : 1;
			}
			// if either is string, convert them to String
			else if (this.IsString || other.IsString)
			{
				return collation.Compare(this.ToString(), other.ToString());
			}
			// if both values are number, convert them to Decimal (128 bits) to compare
			// it's the slowest way, but more secure
			else if (this.IsNumeric && other.IsNumeric)
			{
				return Convert.ToDecimal(this.RawValue).CompareTo(Convert.ToDecimal(other.RawValue));
			}
			// if not, order by sort type order
			else
			{
				var result = this.Type.CompareTo(other.Type);
				return result < 0 ? -1 : result > 0 ? +1 : 0;
			}
		}

		// for both values with same data type just compare
		switch (this.Type)
		{
			case AttributeType.TNone:
				if (this.IsDocument) return this.AsDocument.CompareTo(other);
				if (this.IsArray) return this.AsArray.CompareTo(other);
				return 0;

			case AttributeType.TInt8: return this.AsInt32.CompareTo(other.AsInt8);
			case AttributeType.TInt16: return this.AsInt32.CompareTo(other.AsInt16);
			case AttributeType.TInt32: return this.AsInt32.CompareTo(other.AsInt32);
			case AttributeType.TInt64: return this.AsInt64.CompareTo(other.AsInt64);
			case AttributeType.TFloat32: return this.AsFloat.CompareTo(other.AsFloat);
			case AttributeType.TBool: return this.AsBoolean.CompareTo(other.AsBoolean);
			case AttributeType.TString: return collation.Compare(this.AsString, other.AsString);

			//case AttributeType.TDateTime:
			//	var d0 = this.AsDateTime;
			//	var d1 = other.AsDateTime;
			//	if (d0.Kind != DateTimeKind.Utc) d0 = d0.ToUniversalTime();
			//	if (d1.Kind != DateTimeKind.Utc) d1 = d1.ToUniversalTime();
			//	return d0.CompareTo(d1);

			default: throw new NotImplementedException();
		}
	}

	public bool Equals(AttributeValue other)
	{
		return this.CompareTo(other) == 0;
	}
	#endregion

	#region Operators
	public static bool operator ==(AttributeValue lhs, AttributeValue rhs)
	{
		if (lhs is null) return rhs is null;
		if (rhs is null) return false; // don't check type because sometimes different types can be ==

		return lhs.Equals(rhs);
	}

	public static bool operator !=(AttributeValue lhs, AttributeValue rhs)
	{
		return !(lhs == rhs);
	}

	public static bool operator >=(AttributeValue lhs, AttributeValue rhs)
	{
		return lhs.CompareTo(rhs) >= 0;
	}

	public static bool operator >(AttributeValue lhs, AttributeValue rhs)
	{
		return lhs.CompareTo(rhs) > 0;
	}

	public static bool operator <(AttributeValue lhs, AttributeValue rhs)
	{
		return lhs.CompareTo(rhs) < 0;
	}

	public static bool operator <=(AttributeValue lhs, AttributeValue rhs)
	{
		return lhs.CompareTo(rhs) <= 0;
	}

	public override bool Equals(object obj)
	{
		if (obj is AttributeValue other)
		{
			return this.Equals(other);
		}

		return false;
	}

	public override int GetHashCode()
	{
		var hash = 17;
		hash = 37 * hash + this.Type.GetHashCode();
		hash = 37 * hash + (this.RawValue?.GetHashCode() ?? 0);
		return hash;
	}
	#endregion
}