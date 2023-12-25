using System.Diagnostics;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Exceptions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
internal class BsonExpressionOperators
{
    #region Arithmetic
    /// <summary>
    /// Add two number values. If any side are string, concat left+right as string
    /// </summary>
    public static AttributeValue ADD(AttributeValue left, AttributeValue right)
    {
        // if both sides are string, concat
        if (left.IsString && right.IsString)
        {
            return left.AsString + right.AsString;
        }
        // if any sides are string, concat casting both to string
        else if (left.IsString || right.IsString)
        {
            return BsonExpressionMethods.STRING(left).AsString + BsonExpressionMethods.STRING(right).AsString;
        }
        // if any side are DateTime and another is number, add days in date
        else if (left.IsDateTime && right.IsNumeric)
        {
            return left.AsDateTime.AddTicks(right.AsInt64);
        }
        else if (left.IsNumeric && right.IsDateTime)
        {
            return right.AsDateTime.AddTicks(left.AsInt64);
        }
        // if both sides are number, add as math
        else if (left.IsNumeric && right.IsNumeric)
        {
            return left + right;
        }

		return AttributeValue.Null;
	}

    /// <summary>
    /// Minus two number values
    /// </summary>
    public static AttributeValue MINUS(AttributeValue left, AttributeValue right)
    {
        if (left.IsDateTime && right.IsNumeric)
        {
            return left.AsDateTime.AddTicks(-right.AsInt64);
        }
        else if (left.IsNumeric && right.IsDateTime)
        {
            return right.AsDateTime.AddTicks(-left.AsInt64);
        }
        else if (left.IsNumeric && right.IsNumeric)
        {
            return left - right;
        }
		return AttributeValue.Null;
	}

    /// <summary>
    /// Multiply two number values
    /// </summary>
    public static AttributeValue MULTIPLY(AttributeValue left, AttributeValue right)
    {
        if (left.IsNumeric && right.IsNumeric)
        {
            return left * right;
        }

		return AttributeValue.Null;
	}

    /// <summary>
    /// Divide two number values
    /// </summary>
    public static AttributeValue DIVIDE(AttributeValue left, AttributeValue right)
    {
        if (left.IsNumeric && right.IsNumeric)
        {
            return left / right;
        }

		return AttributeValue.Null;
	}

    /// <summary>
    /// Mod two number values
    /// </summary>
    public static AttributeValue MOD(AttributeValue left, AttributeValue right)
    {
        if (left.IsNumeric && right.IsNumeric)
        {
            return left % right;
        }

		return AttributeValue.Null;
	}
    #endregion

    #region Predicates
    /// <summary>
    /// Test if left and right are same value. Returns true or false
    /// </summary>
    public static AttributeValue EQ(Collation collation, AttributeValue left, AttributeValue right) => collation.Equals(left, right);
    public static AttributeValue EQ_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => collation.Equals(x, right));
    public static AttributeValue EQ_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => collation.Equals(x, right));

    /// <summary>
    /// Test if left is greater than right value. Returns true or false
    /// </summary>
    public static AttributeValue GT(AttributeValue left, AttributeValue right) => left > right;
    public static AttributeValue GT_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => collation.Compare(x, right) > 0);
    public static AttributeValue GT_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => collation.Compare(x, right) > 0);

    /// <summary>
    /// Test if left is greater or equals than right value. Returns true or false
    /// </summary>
    public static AttributeValue GTE(AttributeValue left, AttributeValue right) => left >= right;
    public static AttributeValue GTE_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => collation.Compare(x, right) >= 0);
    public static AttributeValue GTE_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => collation.Compare(x, right) >= 0);


    /// <summary>
    /// Test if left is less than right value. Returns true or false
    /// </summary>
    public static AttributeValue LT(AttributeValue left, AttributeValue right) => left < right;
    public static AttributeValue LT_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => collation.Compare(x, right) < 0);
    public static AttributeValue LT_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => collation.Compare(x, right) < 0);

    /// <summary>
    /// Test if left is less or equals than right value. Returns true or false
    /// </summary>
    public static AttributeValue LTE(Collation collation, AttributeValue left, AttributeValue right) => left <= right;
    public static AttributeValue LTE_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => collation.Compare(x, right) <= 0);
    public static AttributeValue LTE_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => collation.Compare(x, right) <= 0);

    /// <summary>
    /// Test if left and right are not same value. Returns true or false
    /// </summary>
    public static AttributeValue NEQ(Collation collation, AttributeValue left, AttributeValue right) => !collation.Equals(left, right);
    public static AttributeValue NEQ_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => !collation.Equals(x, right));
    public static AttributeValue NEQ_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => !collation.Equals(x, right));

    /// <summary>
    /// Test if left is "SQL LIKE" with right. Returns true or false. Works only when left and right are string
    /// </summary>
    public static AttributeValue LIKE(Collation collation, AttributeValue left, AttributeValue right)
    {
        if (left.IsString && right.IsString)
        {
            return left.AsString.SqlLike(right.AsString, collation);
        }
        else
        {
            return false;
        }
	}

    public static AttributeValue LIKE_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => LIKE(collation, x, right));
    public static AttributeValue LIKE_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => LIKE(collation, x, right));

    /// <summary>
    /// Test if left is between right-array. Returns true or false. Right value must be an array. Support multiple values
    /// </summary>
    public static AttributeValue BETWEEN(Collation collation, AttributeValue left, AttributeValue right)
    {
        if (!right.IsArray) throw new InvalidOperationException("BETWEEN expression need an array with 2 values");

        var arr = right.AsArray;

        if (arr.Count != 2) throw new InvalidOperationException("BETWEEN expression need an array with 2 values");

        var start = arr[0];
        var end = arr[1];

        //return left >= start && right <= end;
        return collation.Compare(left, start) >= 0 && collation.Compare(left, end) <= 0;
    }

    public static AttributeValue BETWEEN_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => BETWEEN(collation, x, right));
    public static AttributeValue BETWEEN_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => BETWEEN(collation, x, right));

    /// <summary>
    /// Test if left are in any value in right side (when right side is an array). If right side is not an array, just implement a simple Equals (=). Returns true or false
    /// </summary>
    public static AttributeValue IN(Collation collation, AttributeValue left, AttributeValue right)
    {
        if (right.IsArray)
        {
            return right.AsArray.Contains(left, collation);
        }
        else
        {
            return left == right;
        }
    }

    public static AttributeValue IN_ANY(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.Any(x => IN(collation, x, right) == true);
    public static AttributeValue IN_ALL(Collation collation, IEnumerable<AttributeValue> left, AttributeValue right) => left.All(x => IN(collation, x, right) == true);

    #endregion

    #region Path Navigation
    /// <summary>
    /// Returns value from root document (used in parameter). Returns same document if name are empty
    /// </summary>
    public static AttributeValue PARAMETER_PATH(AttributeDocument doc, string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return doc;
        }

        if (doc.TryGetValue(name, out AttributeValue item))
        {
            return item;
        }
        else
        {
			return AttributeValue.Null;
		}
    }

    /// <summary>
    /// Return a value from a value as document. If has no name, just return values ($). If value are not a document, do not return anything
    /// </summary>
    public static AttributeValue MEMBER_PATH(AttributeValue value, string name)
    {
        // if value is null is because there is no "current document", only "all documents"
        // SELECT COUNT(*), $.pageID FROM $page_list IS invalid!
        if (value == null)
        {
            throw new BnsDataException($"Field '{name}' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause.");
        }

        if (string.IsNullOrEmpty(name))
        {
            return value;
        }
        else if (value.IsDocument)
        {
            var doc = value.AsDocument;

            if (doc.TryGetValue(name, out AttributeValue item))
            {
                return item;
            }
        }

        return AttributeValue.Null;
    }
    #endregion

    #region Array Index/Filter
    /// <summary>
    /// Returns a single value from array according index or expression parameter
    /// </summary>
    public static AttributeValue ARRAY_INDEX(AttributeValue value, int index, BsonExpression expr, AttributeDocument root, Collation collation, AttributeDocument parameters)
    {
        if (!value.IsArray) return AttributeValue.Null;

		var arr = value.AsArray;

        // for expr.Type = parameter, just get value as index (fixed position)
        if (expr.Type == BsonExpressionType.Parameter)
        {
            // get fixed position based on parameter value (must return int value)
            var indexValue = expr.ExecuteScalar(root, collation);

            if (!indexValue.IsNumeric) throw new BnsDataException("Parameter expression must return number when called inside an array");

            index = indexValue.AsInt32;
        }

        var idx = index < 0 ? arr.Count + index : index;

        if (arr.Count > idx)
        {
            return arr[idx];
        }

        return AttributeValue.Null;
	}

    /// <summary>
    /// Returns all values from array according filter expression or all values (index = MaxValue)
    /// </summary>
    public static IEnumerable<AttributeValue> ARRAY_FILTER(AttributeValue value, int index, BsonExpression filterExpr, AttributeDocument root, Collation collation, AttributeDocument parameters)
    {
        if (!value.IsArray) yield break;

        var arr = value.AsArray;

        // [*] - index are all values
        if (index == int.MaxValue)
        {
            foreach (var item in arr)
            {
                yield return item;
            }
        }
        // [<expr>] - index are an expression
        else
        {
            foreach (var item in arr)
            {
                // execute for each child value and except a first bool value (returns if true)
                var c = filterExpr.ExecuteScalar(new AttributeDocument[] { root }, root, item, collation);

                if (c.IsBoolean && c.AsBoolean)
                {
                    yield return item;
                }
            }
        }
    }
    #endregion      

    #region Object Creation
    /// <summary>
    /// Create multi documents based on key-value pairs on parameters. DOCUMENT('_id', 1)
    /// </summary>
    public static AttributeValue DOCUMENT_INIT(string[] keys, AttributeValue[] values)
    {
        Debug.Assert(keys.Length == values.Length, "both keys/value must contains same length");

        // test for special JsonEx data types (date, numberLong, ...)
        if (keys.Length == 1 && keys[0][0] == '$' && values[0].IsString)
        {
            switch (keys[0])
            {
                case "$date": return BsonExpressionMethods.DATE(Collation.Binary, values[0]);
                case "$numberLong": return BsonExpressionMethods.LONG(values[0]);
                case "$numberDecimal": return BsonExpressionMethods.DECIMAL(Collation.Binary, values[0]);
                case "$minValue": return BsonExpressionMethods.MINVALUE();
                case "$maxValue": return BsonExpressionMethods.MAXVALUE();
            }
        }

        var doc = new AttributeDocument();

        for (var i = 0; i < keys.Length; i++)
        {
            doc[keys[i]] = values[i];
        }

        return doc;
    }

    /// <summary>
    /// Return an array from list of values. Support multiple values but returns a single value
    /// </summary>
    public static AttributeValue ARRAY_INIT(AttributeValue[] values)
    {
        return new AttributeArray(values);
    }
    #endregion
}