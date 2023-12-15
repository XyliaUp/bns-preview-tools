using System.Text.RegularExpressions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

internal partial class BsonExpressionMethods
{
    /// <summary>
    /// Return lower case from string value
    /// </summary>
    public static AttributeValue LOWER(AttributeValue value)
    {
        if (value.IsString)
        {
            return value.AsString.ToLowerInvariant();
        }

        return null;
    }

    /// <summary>
    /// Return UPPER case from string value
    /// </summary>
    public static AttributeValue UPPER(AttributeValue value)
    {
        if (value.IsString)
        {
            return value.AsString.ToUpperInvariant();
        }

        return null;
    }

    /// <summary>
    /// Apply Left TRIM (start) from string value
    /// </summary>
    public static AttributeValue LTRIM(AttributeValue value)
    {
        if (value.IsString)
        {
            return value.AsString.TrimStart();
        }

        return null;
    }

    /// <summary>
    /// Apply Right TRIM (end) from string value
    /// </summary>
    public static AttributeValue RTRIM(AttributeValue value)
    {
        if (value.IsString)
        {
            return value.AsString.TrimEnd();
        }

        return null;

    }

    /// <summary>
    /// Apply TRIM from string value
    /// </summary>
    public static AttributeValue TRIM(AttributeValue value)
    {
        if (value.IsString)
        {
            return value.AsString.Trim();
        }

        return null;
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in this instance
    /// </summary>
    public static AttributeValue INDEXOF(AttributeValue value, AttributeValue search)
    {
        if (value.IsString && search.IsString)
        {
            return value.AsString.IndexOf(search.AsString);
        }

        return null;
    }

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified string in this instance
    /// </summary>
    public static AttributeValue INDEXOF(AttributeValue value, AttributeValue search, AttributeValue startIndex)
    {
        if (value.IsString && search.IsString && startIndex.IsNumeric)
        {
            return value.AsString.IndexOf(search.AsString, startIndex.AsInt32);
        }

        return null;
    }

    /// <summary>
    /// Returns substring from string value using index and length (zero-based)
    /// </summary>
    public static AttributeValue SUBSTRING(AttributeValue value, AttributeValue startIndex)
    {
        if (value.IsString && startIndex.IsNumeric)
        {
            return value.AsString.Substring(startIndex.AsInt32);
        }

        return null;
    }

    /// <summary>
    /// Returns substring from string value using index and length (zero-based)
    /// </summary>
    public static AttributeValue SUBSTRING(AttributeValue value, AttributeValue startIndex, AttributeValue length)
    {
        if (value.IsString && startIndex.IsNumeric && length.IsNumeric)
        {
            return value.AsString.Substring(startIndex.AsInt32, length.AsInt32);
        }

        return null;
    }

    /// <summary>
    /// Returns replaced string changing oldValue with newValue
    /// </summary>
    public static AttributeValue REPLACE(AttributeValue value, AttributeValue oldValue, AttributeValue newValue)
    {
        if (value.IsString && oldValue.IsString && newValue.IsString)
        {
            return value.AsString.Replace(oldValue.AsString, newValue.AsString);
        }

        return null;
    }

    /// <summary>
    /// Return value string with left padding
    /// </summary>
    public static AttributeValue LPAD(AttributeValue value, AttributeValue totalWidth, AttributeValue paddingChar)
    {
        if (value.IsString && totalWidth.IsNumeric && paddingChar.IsString)
        {
            var c = paddingChar.AsString;

            if (string.IsNullOrEmpty(c)) throw new ArgumentOutOfRangeException(nameof(paddingChar));

            return value.AsString.PadLeft(totalWidth.AsInt32, c[0]);
        }

        return null;
    }

    /// <summary>
    /// Return value string with right padding
    /// </summary>
    public static AttributeValue RPAD(AttributeValue value, AttributeValue totalWidth, AttributeValue paddingChar)
    {
        if (value.IsString && totalWidth.IsNumeric && paddingChar.IsString)
        {
            var c = paddingChar.AsString;

            if (string.IsNullOrEmpty(c)) throw new ArgumentOutOfRangeException(nameof(paddingChar));

            return value.AsString.PadRight(totalWidth.AsInt32, c[0]);
        }

        return null;
    }

    /// <summary>
    /// Slit value string based on separator 
    /// </summary>
    public static IEnumerable<AttributeValue> SPLIT(AttributeValue value, AttributeValue separator)
    {
        if (value.IsString && separator.IsString)
        {
            var values = value.AsString.Split(new string[] { separator.AsString }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in values)
            {
                yield return str;
            }
        }
    }

    /// <summary>
    /// Slit value string based on regular expression pattern
    /// </summary>
    public static IEnumerable<AttributeValue> SPLIT(AttributeValue value, AttributeValue pattern, AttributeValue useRegex)
    {
        if (useRegex.IsBoolean && useRegex.AsBoolean)
        {
            if (value.IsString && pattern.IsString)
            {
                var values = Regex.Split(value.AsString, pattern.AsString, RegexOptions.Compiled);

                foreach (var str in values)
                {
                    yield return str;
                }
            }
        }
        else
        {
            foreach(var str in SPLIT(value, pattern))
            {
                yield return str;
            }
        }
    }

    /// <summary>
    /// Return format value string using format definition (same as String.Format("{0:~}", values)).
    /// </summary>
    public static AttributeValue FORMAT(AttributeValue value, AttributeValue format)
    {
        if (format.IsString)
        {
            return string.Format("{0:" +  format.AsString + "}", value.RawValue);
        }

        return null;
    }

    /// <summary>
    /// Join all values into a single string with ',' separator.
    /// </summary>
    public static AttributeValue JOIN(IEnumerable<AttributeValue> values)
    {
        return JOIN(values, "");
    }

    /// <summary>
    /// Join all values into a single string with a string separator
    /// </summary>
    public static AttributeValue JOIN(IEnumerable<AttributeValue> values, AttributeValue separator)
    {
        if (separator.IsString)
        {
            return string.Join(
                separator.AsString,
                values.Select(x => STRING(x).AsString).ToArray()
            );
        }

        return null;
    }

    /// <summary>
    /// Test if value is match with regular expression pattern
    /// </summary>
    public static AttributeValue IS_MATCH(AttributeValue value, AttributeValue pattern)
    {
        if (value.IsString == false || pattern.IsString == false) return false;

        return Regex.IsMatch(value.AsString, pattern.AsString);
    }

    /// <summary>
    /// Apply regular expression pattern over value to get group data. Return null if not found
    /// </summary>
    public static AttributeValue MATCH(AttributeValue value, AttributeValue pattern, AttributeValue group)
    {
        if (value.IsString == false || pattern.IsString == false) return null;

        var match = Regex.Match(value.AsString, pattern.AsString);

        if (match.Success == false) return null;

        if (group.IsNumeric)
        {
            return match.Groups[group.AsInt32].Value;
        }
        else if (group.IsString)
        {
            return match.Groups[group.AsString].Value;
        }
        else
        {
            return null;
        }
    }
}