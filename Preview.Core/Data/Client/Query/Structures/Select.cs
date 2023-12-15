namespace Xylia.Preview.Data.Client;

/// <summary>
/// Represent a Select expression
/// </summary>
internal class Select
{
	public BsonExpression Expression { get; }

	public bool All { get; }

	public Select(BsonExpression expression, bool all)
	{
		this.Expression = expression;
		this.All = all;
	}
}
