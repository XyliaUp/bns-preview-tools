namespace Xylia.Preview.Data.Client;

/// <summary>
/// Represent an OrderBy definition
/// </summary>
internal class OrderBy
{
    public BsonExpression Expression { get; }

    public int Order { get; set; }

    public OrderBy(BsonExpression expression, int order)
    {
        this.Expression = expression;
        this.Order = order;
    }
}
