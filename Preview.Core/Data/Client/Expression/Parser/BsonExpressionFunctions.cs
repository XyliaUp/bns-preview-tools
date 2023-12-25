using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
internal class BsonExpressionFunctions
{
    public static IEnumerable<AttributeValue> MAP(AttributeDocument root, Collation collation, AttributeDocument parameters, IEnumerable<AttributeValue> input, BsonExpression mapExpr)
    {
        foreach (var item in input)
        {
            // execute for each child value and except a first bool value (returns if true)
            var values = mapExpr.Execute(new AttributeDocument[] { root }, root, item, collation);

            foreach (var value in values)
            {
                yield return value;
            }
        }
    }

    public static IEnumerable<AttributeValue> FILTER(AttributeDocument root, Collation collation, AttributeDocument parameters, IEnumerable<AttributeValue> input, BsonExpression filterExpr)
    {
        foreach (var item in input)
        {
            // execute for each child value and except a first bool value (returns if true)
            var c = filterExpr.ExecuteScalar(new AttributeDocument[] { root }, root, item, collation);

            if (c.IsBoolean && c.AsBoolean)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<AttributeValue> SORT(AttributeDocument root, Collation collation, AttributeDocument parameters, IEnumerable<AttributeValue> input, BsonExpression sortExpr, AttributeValue order)
    {
        IEnumerable<Tuple<AttributeValue, AttributeValue>> source()
        {
            foreach (var item in input)
            {
                var value = sortExpr.ExecuteScalar(new AttributeDocument[] { root }, root, item, collation);

                yield return new Tuple<AttributeValue, AttributeValue>(item, value);
            }
        }

        return (order.IsInt32 && order.AsInt32 > 0) || (order.IsString && order.AsString.Equals("asc", StringComparison.OrdinalIgnoreCase)) ?
            source().OrderBy(x => x.Item2, collation).Select(x => x.Item1) :
            source().OrderByDescending(x => x.Item2, collation).Select(x => x.Item1);
    }

    public static IEnumerable<AttributeValue> SORT(AttributeDocument root, Collation collation, AttributeDocument parameters, IEnumerable<AttributeValue> input, BsonExpression sortExpr)
    {
        return SORT(root, collation, parameters, input, sortExpr, order: 1);
    }
}
