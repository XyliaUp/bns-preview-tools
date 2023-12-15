using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

/// <summary>
/// Class that execute QueryPlan returing results
/// </summary>
internal class QueryExecutor
{
	private readonly BnsDatabase _engine;
	private readonly string _collection;
	private readonly Query _query;
	private readonly IEnumerable<AttributeDocument> _source;

	public QueryExecutor(BnsDatabase engine, string collection, Query query, IEnumerable<AttributeDocument> source)
	{
		_engine = engine;
		_collection = collection;
		_query = query;
		_source = source;
	}

	public DataReader ExecuteQuery()
	{
		return new DataReader(RunQuery(), null);

		IEnumerable<AttributeDocument> RunQuery()
		{
			var records = _engine.Provider.Tables[_collection]?.Records;

			// execute optimization before run query (will fill missing _query properties instance)
			var optimizer = new QueryOptimization(_query, _source, Collation.Binary);

			var queryPlan = optimizer.ProcessQuery();

			var pipe = queryPlan.GetPipe();

			// call safepoint just before return each document
			foreach (var doc in pipe.Pipe(records, queryPlan))
			{
				yield return doc;
			}
		}
	}
}