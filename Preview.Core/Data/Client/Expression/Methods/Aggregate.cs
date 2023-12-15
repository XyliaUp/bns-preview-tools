using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

internal partial class BsonExpressionMethods
{
    /// <summary>
    /// Count all values. Return a single value
    /// </summary>
    public static AttributeValue COUNT(IEnumerable<AttributeValue> values)
    {
        return values.Count();
    }

    /// <summary>
    /// Find minimal value from all values (number values only). Return a single value
    /// </summary>
    public static AttributeValue MIN(IEnumerable<AttributeValue> values)
    {
        if (!values.Any()) return "-∞";

		AttributeValue min = long.MaxValue;

        foreach (var value in values)
        {
            if (value.CompareTo(min) <= 0)
            {
                min = value;
            }
        }

        return min;
    }

    /// <summary>
    /// Find max value from all values (number values only). Return a single value
    /// </summary>
    public static AttributeValue MAX(IEnumerable<AttributeValue> values)
	{
		if (!values.Any()) return "+∞";

		AttributeValue max = long.MinValue;

        foreach (var value in values)
        {
            if (value.CompareTo(max) >= 0)
            {
                max = value;
            }
        }

        return max;
    }

    /// <summary>
    /// Returns first value from an list of values (scan all source)
    /// </summary>
    public static AttributeValue FIRST(IEnumerable<AttributeValue> values)
    {
        return values.FirstOrDefault();
    }

    /// <summary>
    /// Returns last value from an list of values
    /// </summary>
    public static AttributeValue LAST(IEnumerable<AttributeValue> values)
    {
        return values.LastOrDefault();
    }

    /// <summary>
    /// Find average value from all values (number values only). Return a single value
    /// </summary>
    public static AttributeValue AVG(IEnumerable<AttributeValue> values)
    {
		var sum = AttributeValue.Create(0);
		var count = 0;

        foreach (var value in values.Where(x => x.IsNumeric))
        {
            sum += value;
            count++;
        }

        if (count > 0)
        {
            return sum / count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Sum all values (number values only). Return a single value
    /// </summary>
    public static AttributeValue SUM(IEnumerable<AttributeValue> values)
    {
        var sum = AttributeValue.Create(0);

        foreach (var value in values.Where(x => x.IsNumeric))
        {
            sum += value;
        }

        return sum;
	}

    /// <summary>
    /// Return "true" if inner collection contains any result
    /// ANY($.items[*])
    /// </summary>
    public static AttributeValue ANY(IEnumerable<AttributeValue> values)
    {
        return values.Any(); 
    }
}