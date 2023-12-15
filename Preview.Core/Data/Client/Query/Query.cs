namespace Xylia.Preview.Data.Client;

/// <summary>
/// Represent full query options
/// </summary>
public partial class Query
{
	public BsonExpression Select { get; set; } = BsonExpression.Root;

	public List<BsonExpression> Includes { get; } = new List<BsonExpression>();
	public List<BsonExpression> Where { get; } = new List<BsonExpression>();

	public BsonExpression OrderBy { get; set; } = null;
	public int Order { get; set; } = Query.Ascending;

	public BsonExpression GroupBy { get; set; } = null;
	public BsonExpression Having { get; set; } = null;

	public int Offset { get; set; } = 0;
	public int Limit { get; set; } = int.MaxValue;
	public bool ForUpdate { get; set; } = false;

	public bool ExplainPlan { get; set; }
}