using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

internal partial class BsonExpressionMethods
{
    /// <summary>
    /// Parse a JSON string into a new AttributeValue
    /// JSON('{a:1}') = {a:1}
    /// </summary>
    public static AttributeValue JSON(AttributeValue json)
    {
        //if (json.IsString)
        //{
        //    AttributeValue value = null;
        //    var isJson = false;

        //    try
        //    {
        //        value = JsonSerializer.Deserialize(json.AsString);
        //        isJson = true;
        //    }
        //    catch (LiteException ex) when (ex.ErrorCode == BnsException.UNEXPECTED_TOKEN)
        //    {
        //    }

        //    if (isJson) return value;
        //}

        return null;
    }

    /// <summary>
    /// Create a new document and copy all properties from source document. Then copy properties (overritting if need) extend document
    /// Always returns a new document!
    /// EXTEND($, {a: 2}) = {_id:1, a: 2}
    /// </summary>
    public static AttributeValue EXTEND(AttributeValue source, AttributeValue extend)
    {
        //if (source.IsAttributeDocument && extend.IsAttributeDocument)
        //{
        //    // make a copy of source document
        //    var newDoc = new AttributeDocument();

        //    source.AsAttributeDocument.CopyTo(newDoc);
        //    extend.AsAttributeDocument.CopyTo(newDoc);

        //    // copy rawId from source
        //    newDoc.RawId = source.AsAttributeDocument.RawId;

        //    return newDoc;
        //}
        //else if (source.IsAttributeDocument) return source;
        //else if (extend.IsAttributeDocument) return extend;

        return new AttributeDocument();
    }

    /// <summary>
    /// Convert an array into IEnuemrable of values - If not array, returns as single yield value
    /// ITEMS([1, 2, null]) = 1, 2, null
    /// </summary>
    public static IEnumerable<AttributeValue> ITEMS(AttributeValue array)
    {
        if (array.IsArray)
        {
            foreach (var value in array.AsArray)
            {
                yield return value;
            }
        }
        else
        {
            yield return array;
        }
    }

    /// <summary>
    /// Concatenates 2 sequences into a new single sequence
    /// </summary>
    public static IEnumerable<AttributeValue> CONCAT(IEnumerable<AttributeValue> first, IEnumerable<AttributeValue> second)
    {
        return first.Concat(second);
    }

    /// <summary>
    /// Get all KEYS names from a document
    /// </summary>
    public static IEnumerable<AttributeValue> KEYS(AttributeValue document)
    {
        if (document.IsDocument)
        {
            foreach (var key in document.AsDocument)
            {
                yield return key.Key;
            }
        }
    }

    /// <summary>
    /// Get all values from a document
    /// </summary>
    public static IEnumerable<AttributeValue> VALUES(AttributeValue document)
    {
        if (document.IsDocument)
        {
            foreach (var value in document.AsDocument)
            {
                yield return value.Value;
            }
        }
    }

    /// <summary>
    /// Conditional IF statment. If condition are true, returns TRUE value, otherwise, FALSE value
    /// </summary>
    public static AttributeValue IIF(AttributeValue test, AttributeValue ifTrue, AttributeValue ifFalse)
    {
        // this method are not implemented because will use "Expression.Conditional"
        // will execute "ifTrue" only if test = true and will execute "ifFalse" if test = false
        throw new NotImplementedException();
    }

    /// <summary>
    /// Return first values if not null. If null, returns second value.
    /// </summary>
    public static AttributeValue COALESCE(AttributeValue left, AttributeValue right)
    {
        return left.IsNone ? right : left;
    }

    /// <summary>
    /// Return length of variant value (valid only for String, Binary, Array or Document [keys])
    /// </summary>
    public static AttributeValue LENGTH(AttributeValue value)
    {
        if (value.IsString) return value.AsString.Length;
        else if (value.IsArray) return value.AsArray.Count;
        else if (value.IsDocument) return value.AsDocument.Count;
        else if (value.IsNone) return 0;

        return null;
    }

    /// <summary>
    /// Returns the first num elements of values.
    /// </summary>
    public static IEnumerable<AttributeValue> TOP(IEnumerable<AttributeValue> values, AttributeValue num)
    {
        if (num.IsInt32 || num.IsInt64)
        {
            var numInt = num.AsInt32;

            if (numInt > 0)
                return values.Take(numInt);
        }
        return Enumerable.Empty<AttributeValue>();
    }

    /// <summary>
    /// Returns the union of the two enumerables.
    /// </summary>
    public static IEnumerable<AttributeValue> UNION(IEnumerable<AttributeValue> left, IEnumerable<AttributeValue> right)
    {
        return left.Union(right);
    }

    /// <summary>
    /// Returns the set difference between the two enumerables.
    /// </summary>
    public static IEnumerable<AttributeValue> EXCEPT(IEnumerable<AttributeValue> left, IEnumerable<AttributeValue> right)
    {
        return left.Except(right);
    }

    /// <summary>
    /// Returns a unique list of items
    /// </summary>
    public static IEnumerable<AttributeValue> DISTINCT(IEnumerable<AttributeValue> items)
    {
        return items.Distinct();
    }

    private static readonly Random _random = new Random();

    /// <summary>
    /// Return a random int value
    /// </summary>
    [Volatile]
    public static AttributeValue RANDOM()
    {
        return _random.Next();
    }

    /// <summary>
    /// Return a ranom int value inside this min/max values
    /// </summary>
    [Volatile]
    public static AttributeValue RANDOM(AttributeValue min, AttributeValue max)
    {
        if (min.IsNumeric && max.IsNumeric)
        {
            return _random.Next(min.AsInt32, max.AsInt32);
        }
        else
        {
            return null;
        }
    }
}
