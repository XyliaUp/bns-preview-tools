using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

/// <summary>
/// This class are result from optimization from QueryBuild in QueryAnalyzer. Indicate how engine must run query - there is no more decisions to engine made, must only execute as query was defined
/// Contains used index and estimate cost to run
/// </summary>
internal class QueryPlan
{
	public QueryPlan(string collection)
	{
		this.Collection = collection;
	}

	/// <summary>
	/// Get collection name (required)
	/// </summary>
	public string Collection { get; set; } = null;

	/// <summary>
	/// Index used on query (required)
	/// </summary>
	public Index Index { get; set; } = default;

	/// <summary>
	/// Index expression that will be used in index (source only)
	/// </summary>
	public string IndexExpression { get; set; } = null;

	/// <summary>
	/// Get index cost (lower is best)
	/// </summary>
	public uint IndexCost { get; internal set; } = 0; // not calculated

	/// <summary>
	/// If true, gereate document result only with IndexNode.Key (avoid load all document)
	/// </summary>
	public bool IsIndexKeyOnly { get; set; } = false;

	/// <summary>
	/// List of filters of documents
	/// </summary>
	public List<BsonExpression> Filters { get; set; } = new List<BsonExpression>();

	/// <summary>
	/// List of includes must be done BEFORE filter (it's not optimized but some filter will use this include)
	/// </summary>
	public List<BsonExpression> IncludeBefore { get; set; } = new List<BsonExpression>();

	/// <summary>
	/// List of includes must be done AFTER filter (it's optimized because will include result only)
	/// </summary>
	public List<BsonExpression> IncludeAfter { get; set; } = new List<BsonExpression>();

	/// <summary>
	/// Expression to order by resultset
	/// </summary>
	public OrderBy OrderBy { get; set; } = null;

	/// <summary>
	/// Expression to group by document results
	/// </summary>
	public GroupBy GroupBy { get; set; } = null;

	/// <summary>
	/// Transaformation data before return - if null there is no transform (return document)
	/// </summary>
	public Select Select { get; set; }

	/// <summary>
	/// Get fields name that will be deserialize from disk
	/// </summary>
	public HashSet<string> Fields { get; set; }

	/// <summary>
	/// Limit resultset
	/// </summary>
	public int Limit { get; set; } = int.MaxValue;

	/// <summary>
	/// Skip documents before returns
	/// </summary>
	public int Offset { get; set; }

	/// <summary>
	/// Indicate this query is for update (lock mode = Write)
	/// </summary>
	public bool ForUpdate { get; set; } = false;


	#region Execution Plan
	public BasePipe GetPipe()
	{
		if (this.GroupBy == null)
		{
			return new QueryPipe();
		}

		throw new NotSupportedException();
	}

	/// <summary>
	/// Get detail about execution plan for this query definition
	/// </summary>
	public AttributeDocument GetExecutionPlan()
	{
		var doc = new AttributeDocument
		{
			["collection"] = this.Collection,
			["snaphost"] = this.ForUpdate ? "write" : "read",
			["pipe"] = this.GroupBy == null ? "queryPipe" : "groupByPipe"
		};


		doc["lookup"] = new AttributeDocument
		{
			["loader"] = this.IsIndexKeyOnly ? "index" : "document",
			["fields"] =
				this.Fields.Count == 0 ? AttributeValue.Create("$") :
				(AttributeValue)new AttributeArray(this.Fields.Select(x => AttributeValue.Create(x))),
		};

		if (this.IncludeBefore.Count > 0)
		{
			doc["includeBefore"] = new AttributeArray(this.IncludeBefore.Select(x => AttributeValue.Create(x.Source)));
		}

		if (this.Filters.Count > 0)
		{
			doc["filters"] = new AttributeArray(this.Filters.Select(x => AttributeValue.Create(x.Source)));
		}

		if (this.OrderBy != null)
		{
			doc["orderBy"] = new AttributeDocument
			{
				["expr"] = this.OrderBy.Expression.Source,
				["order"] = this.OrderBy.Order,
			};
		}

		if (this.Limit != int.MaxValue)
		{
			doc["limit"] = this.Limit;
		}

		if (this.Offset != 0)
		{
			doc["offset"] = this.Offset;
		}

		if (this.IncludeAfter.Count > 0)
		{
			doc["includeAfter"] = new AttributeArray(this.IncludeAfter.Select(x => AttributeValue.Create(x.Source)));
		}

		if (this.GroupBy != null)
		{
			doc["groupBy"] = new AttributeDocument
			{
				["expr"] = this.GroupBy.Expression.Source,
				["having"] = this.GroupBy.Having?.Source,
				["select"] = this.GroupBy.Select?.Source
			};
		}
		else
		{
			doc["select"] = new AttributeDocument
			{
				["expr"] = this.Select.Expression.Source,
				["all"] = this.Select.All
			};
		}

		return doc;
	}
	#endregion
}