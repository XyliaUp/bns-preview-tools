using System.Globalization;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
internal partial class BsonExpressionMethods
{
	#region NEW_INSTANCE
	/// <summary>
	/// Return a new instance of MINVALUE
	/// </summary>
	public static AttributeValue MINVALUE() => -1; // AttributeValue.MinValue;

	/// <summary>
	/// Return a new DATETIME (Now)
	/// </summary>
	[Volatile]
	public static AttributeValue NOW() => DateTime.Now;

	/// <summary>
	/// Return a new DATETIME (UtcNow)
	/// </summary>
	[Volatile]
	public static AttributeValue NOW_UTC() => DateTime.UtcNow;

	/// <summary>
	/// Return a new DATETIME (Today)
	/// </summary>
	[Volatile]
	public static AttributeValue TODAY() => DateTime.Today;

	/// <summary>
	/// Return a new instance of MAXVALUE
	/// </summary>
	public static AttributeValue MAXVALUE() => -1;// AttributeValue.MaxValue;
	#endregion

	#region DATATYPE

	// ==> MaxValue is a constant
	// ==> "null" are a keyword

	/// <summary>
	/// Convert values into INT32. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue INT32(AttributeValue value)
	{
		if (value.IsNumeric)
		{
			return value.AsInt32;
		}
		else if (value.IsString)
		{
			if (Int32.TryParse(value.AsString, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into INT64. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue INT64(AttributeValue value)
	{
		if (value.IsNumeric)
		{
			return value.AsInt64;
		}
		else if (value.IsString)
		{
			if (Int64.TryParse(value.AsString, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DOUBLE. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DOUBLE(Collation collation, AttributeValue value)
	{
		if (value.IsNumeric)
		{
			return value.AsFloat;
		}
		else if (value.IsString)
		{
			if (Double.TryParse(value.AsString, NumberStyles.Any, collation.Culture.NumberFormat, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DOUBLE. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DOUBLE(AttributeValue value, AttributeValue culture)
	{
		if (value.IsNumeric)
		{
			return value.AsFloat;
		}
		else if (value.IsString && culture.IsString)
		{
			var c = new CultureInfo(culture.AsString); // en-US

			if (Double.TryParse(value.AsString, NumberStyles.Any, c.NumberFormat, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DECIMAL. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DECIMAL(Collation collation, AttributeValue value)
	{
		if (value.IsNumeric)
		{
			return value.AsDecimal;
		}
		else if (value.IsString)
		{
			if (Decimal.TryParse(value.AsString, NumberStyles.Any, collation.Culture.NumberFormat, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DECIMAL. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DECIMAL(AttributeValue value, AttributeValue culture)
	{
		if (value.IsNumeric)
		{
			return value.AsDecimal;
		}
		else if (value.IsString && culture.IsString)
		{
			var c = new CultureInfo(culture.AsString); // en-US

			if (Decimal.TryParse(value.AsString, NumberStyles.Any, c.NumberFormat, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert value into STRING
	/// </summary>
	public static AttributeValue STRING(AttributeValue value)
	{
		return
			value.IsNone ? "" :
			value.IsString ? value.AsString :
			value.ToString();
	}

	// ==> there is no convert to AttributeDocument, must use { .. } syntax 

	/// <summary>
	/// Return an array from list of values. Support multiple values but returns a single value
	/// </summary>
	public static AttributeValue ARRAY(IEnumerable<AttributeValue> values)
	{
		return new AttributeArray(values);
	}


	/// <summary>
	/// Return converted value into BOOLEAN value
	/// </summary>
	public static AttributeValue BOOLEAN(AttributeValue value)
	{
		if (value.IsBoolean)
		{
			return value.AsBoolean;
		}
		else
		{
			var val = false;
			var isBool = false;

			try
			{
				val = Convert.ToBoolean(value.AsString);
				isBool = true;
			}
			catch
			{
			}

			if (isBool) return val;
		}

		return null;
	}

	/// <summary>
	/// Convert values into DATETIME. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DATETIME(Collation collation, AttributeValue value)
	{
		if (value.IsDateTime)
		{
			return value.AsDateTime;
		}
		else if (value.IsString)
		{
			if (DateTime.TryParse(value.AsString, collation.Culture.DateTimeFormat, DateTimeStyles.None, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DATETIME. Returns empty if not possible to convert. Support custom culture info
	/// </summary>
	public static AttributeValue DATETIME(AttributeValue value, AttributeValue culture)
	{
		if (value.IsDateTime)
		{
			return value.AsDateTime;
		}
		else if (value.IsString && culture.IsString)
		{
			var c = new CultureInfo(culture.AsString); // en-US

			if (DateTime.TryParse(value.AsString, c.DateTimeFormat, DateTimeStyles.None, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DATETIME. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DATETIME_UTC(Collation collation, AttributeValue value)
	{
		if (value.IsDateTime)
		{
			return value.AsDateTime;
		}
		else if (value.IsString)
		{
			if (DateTime.TryParse(value.AsString, collation.Culture.DateTimeFormat, DateTimeStyles.AssumeUniversal, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Convert values into DATETIME. Returns empty if not possible to convert
	/// </summary>
	public static AttributeValue DATETIME_UTC(AttributeValue value, AttributeValue culture)
	{
		if (value.IsDateTime)
		{
			return value.AsDateTime;
		}
		else if (value.IsString && culture.IsString)
		{
			var c = new CultureInfo(culture.AsString); // en-US

			if (DateTime.TryParse(value.AsString, c.DateTimeFormat, DateTimeStyles.AssumeUniversal, out var val))
			{
				return val;
			}
		}

		return null;
	}

	/// <summary>
	/// Create a new instance of DATETIME based on year, month, day (local time)
	/// </summary>
	public static AttributeValue DATETIME(AttributeValue year, AttributeValue month, AttributeValue day)
	{
		if (year.IsNumeric && month.IsNumeric && day.IsNumeric)
		{
			return new DateTime(year.AsInt32, month.AsInt32, day.AsInt32);
		}

		return null;
	}

	/// <summary>
	/// Create a new instance of DATETIME based on year, month, day (UTC)
	/// </summary>
	public static AttributeValue DATETIME_UTC(AttributeValue year, AttributeValue month, AttributeValue day)
	{
		if (year.IsNumeric && month.IsNumeric && day.IsNumeric)
		{
			return new DateTime(year.AsInt32, month.AsInt32, day.AsInt32, 0, 0, 0, DateTimeKind.Utc);
		}

		return null;
	}

	#endregion

	#region IS_DATETYPE
	/// <summary>
	/// Return true if value is MINVALUE
	/// </summary>
	public static AttributeValue IS_MINVALUE(AttributeValue value) => false; // value.IsMinValue;

	/// <summary>
	/// Return true if value is NULL
	/// </summary>
	public static AttributeValue IS_NULL(AttributeValue value) => value.IsNone;

	/// <summary>
	/// Return true if value is INT32
	/// </summary>
	public static AttributeValue IS_INT32(AttributeValue value) => value.IsInt32;

	/// <summary>
	/// Return true if value is INT64
	/// </summary>
	public static AttributeValue IS_INT64(AttributeValue value) => value.IsInt64;

	/// <summary>
	/// Return true if value is NUMBER (int, double, decimal)
	/// </summary>
	public static AttributeValue IS_NUMBER(AttributeValue value) => value.IsNumeric;

	/// <summary>
	/// Return true if value is STRING
	/// </summary>
	public static AttributeValue IS_STRING(AttributeValue value) => value.IsString;

	/// <summary>
	/// Return true if value is DOCUMENT
	/// </summary>
	public static AttributeValue IS_DOCUMENT(AttributeValue value) => value.IsDocument;

	/// <summary>
	/// Return true if value is ARRAY
	/// </summary>
	public static AttributeValue IS_ARRAY(AttributeValue value) => value.IsArray;

	/// <summary>
	/// Return true if value is BOOLEAN
	/// </summary>
	public static AttributeValue IS_BOOLEAN(AttributeValue value) => value.IsBoolean;

	/// <summary>
	/// Return true if value is DATETIME
	/// </summary>
	public static AttributeValue IS_DATETIME(AttributeValue value) => value.IsDateTime;

	/// <summary>
	/// Return true if value is DATE (alias to DATETIME)
	/// </summary>
	public static AttributeValue IS_MAXVALUE(AttributeValue value) => false;  //value.IsMaxValue;

	#endregion

	#region ALIAS

	/// <summary>
	/// Alias to INT32(values)
	/// </summary>
	public static AttributeValue INT(AttributeValue value) => INT32(value);

	/// <summary>
	/// Alias to INT64(values)
	/// </summary>
	public static AttributeValue LONG(AttributeValue value) => INT64(value);

	/// <summary>
	/// Alias to BOOLEAN(values)
	/// </summary>
	public static AttributeValue BOOL(AttributeValue value) => BOOLEAN(value);

	/// <summary>
	/// Alias to DATETIME(values) and DATETIME_UTC(values)
	/// </summary>
	public static AttributeValue DATE(Collation collation, AttributeValue value) => DATETIME(collation, value);
	public static AttributeValue DATE(AttributeValue values, AttributeValue culture) => DATETIME(values, culture);
	public static AttributeValue DATE_UTC(Collation collation, AttributeValue value) => DATETIME_UTC(collation, value);
	public static AttributeValue DATE_UTC(AttributeValue values, AttributeValue culture) => DATETIME_UTC(values, culture);

	public static AttributeValue DATE(AttributeValue year, AttributeValue month, AttributeValue day) => DATETIME(year, month, day);
	public static AttributeValue DATE_UTC(AttributeValue year, AttributeValue month, AttributeValue day) => DATETIME_UTC(year, month, day);

	/// <summary>
	/// Alias to IS_INT32(values)
	/// </summary>
	public static AttributeValue IS_INT(AttributeValue value) => IS_INT32(value);

	/// <summary>
	/// Alias to IS_INT64(values)
	/// </summary>
	public static AttributeValue IS_LONG(AttributeValue value) => IS_INT64(value);

	/// <summary>
	/// Alias to IS_BOOLEAN(values)
	/// </summary>
	public static AttributeValue IS_BOOL(AttributeValue value) => IS_BOOLEAN(value);

	/// <summary>
	/// Alias to IS_DATE(values)
	/// </summary>
	public static AttributeValue IS_DATE(AttributeValue value) => IS_DATETIME(value);

	#endregion
}
