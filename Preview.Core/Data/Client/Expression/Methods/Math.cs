using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
internal partial class BsonExpressionMethods
{
    /// <summary>
    /// Apply absolute value (ABS) method in all number values
    /// </summary>
    public static AttributeValue ABS(AttributeValue value)
    {
        switch (value.Type)
        {
            case AttributeType.TInt32: return Math.Abs(value.AsInt32); 
            case AttributeType.TInt64: return Math.Abs(value.AsInt64); 
            case AttributeType.TFloat32: return Math.Abs(value.AsFloat); 
        }

        return null;
    }

    /// <summary>
    /// Round number method in all number values
    /// </summary>
    public static AttributeValue ROUND(AttributeValue value, AttributeValue digits)
    {
        if (digits.IsNumeric)
        {
            switch (value.Type)
            {
                case AttributeType.TInt32: return value.AsInt32;
                case AttributeType.TInt64: return value.AsInt64;
                case AttributeType.TFloat32: return Math.Round(value.AsFloat, digits.AsInt32);
            }
        }

        return null;
    }

    /// <summary>
    /// Implement POWER (x and y)
    /// </summary>
    public static AttributeValue POW(AttributeValue x, AttributeValue y)
    {
        if (x.IsNumeric && y.IsNumeric)
        {
            return Math.Pow(x.AsFloat, y.AsFloat);
        }

        return null;
    }
}