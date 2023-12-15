using System.Linq.Expressions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

internal class ExpressionContext
{
    public ExpressionContext()
    {
        this.Source = Expression.Parameter(typeof(IEnumerable<AttributeDocument>), "source");
        this.Root = Expression.Parameter(typeof(AttributeDocument), "root");
        this.Current = Expression.Parameter(typeof(AttributeValue), "current");
        this.Collation = Expression.Parameter(typeof(Collation), "collation");
        this.Parameters = Expression.Parameter(typeof(AttributeDocument), "parameters");
    }

    public ParameterExpression Source { get; }
    public ParameterExpression Root { get; }
    public ParameterExpression Current { get; }
    public ParameterExpression Collation { get; }
    public ParameterExpression Parameters { get; }
}