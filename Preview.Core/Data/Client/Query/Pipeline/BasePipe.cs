using SharpGLTF.Schema2;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;

/// <summary>
/// Abstract class with workflow method to be used in pipeline implementation
/// </summary>
internal abstract class BasePipe
{
	/// <summary>
	/// Abstract method to be implement according pipe workflow
	/// </summary>
	public abstract IEnumerable<AttributeDocument> Pipe(IEnumerable<Record> records, QueryPlan query);

	// load documents from document loader
	protected IEnumerable<AttributeDocument> LoadDocument(IEnumerable<Record> nodes)
	{
		foreach (var node in nodes)
		{
			yield return new AttributeDocument(node.Attributes);
		}
	}					    

	/// <summary>
	/// INCLUDE: Do include in result document according path expression - Works only with DocumentLookup
	/// </summary>
	protected IEnumerable<AttributeDocument> Include(IEnumerable<AttributeDocument> source, BsonExpression path)
	{
		throw new NotImplementedException("include is not implemented!");

		//// cached services
		//string last = null;
		//Snapshot snapshot = null;
		//IndexService indexer = null;
		//DataService data = null;
		//CollectionIndex index = null;
		//IDocumentLookup lookup = null;

		//foreach (var doc in source)
		//{
		//	foreach (var value in path.Execute(doc, _pragmas.Collation)
		//							.Where(x => x.IsAttributeDocument || x.IsAttributeArray)
		//							.ToList())
		//	{
		//		// if value is document, convert this ref document into full document (do another query)
		//		if (value.IsAttributeDocument)
		//		{
		//			DoInclude(value.AsDocument);
		//		}
		//		else
		//		{
		//			// if value is array, do same per item
		//			foreach (var item in value.AsAttributeArray
		//				.Where(x => x.IsAttributeDocument)
		//				.Select(x => x.AsDocument))
		//			{
		//				DoInclude(item);
		//			}
		//		}

		//	}

		//	yield return doc;
		//}

		//void DoInclude(AttributeDocument value)
		//{
		//	// works only if is a document
		//	var refId = value["$id"];
		//	var refCol = value["$ref"];

		//	// if has no reference, just go out
		//	if (refId.IsNull || !refCol.IsString) return;

		//	// do some cache re-using when is same $ref (almost always is the same $ref collection)
		//	if (last != refCol.AsString)
		//	{
		//		last = refCol.AsString;

		//		// initialize services
		//		snapshot = _transaction.CreateSnapshot(LockMode.Read, last, false);
		//		indexer = new IndexService(snapshot, _pragmas.Collation);
		//		data = new DataService(snapshot);

		//		lookup = new DatafileLookup(data, _pragmas.UtcDate, null);

		//		index = snapshot.CollectionPage?.PK;
		//	}

		//	// fill only if index and ref node exists
		//	if (index != null)
		//	{
		//		var node = indexer.Find(index, refId, false, Query.Ascending);

		//		if (node != null)
		//		{
		//			// load document based on dataBlock position
		//			var refDoc = lookup.Load(node);

		//			//do not remove $id
		//			value.Remove("$ref");

		//			// copy values from refDocument into current documet (except _id - will keep $id)
		//			foreach (var element in refDoc.Where(x => x.Key != "_id"))
		//			{
		//				value[element.Key] = element.Value;
		//			}
		//		}
		//		else
		//		{
		//			// set in ref document that was not found
		//			value["$missing"] = true;
		//		}
		//	}
		//}

		return source;
	}

	/// <summary>
	/// WHERE: Filter document according expression. Expression must be an Bool result
	/// </summary>
	protected IEnumerable<AttributeDocument> Filter(IEnumerable<AttributeDocument> source, BsonExpression expr)
	{
		foreach (var doc in source)
		{
			// checks if any result of expression is true
			var result = expr.ExecuteScalar(doc);

			if (result.IsBoolean && result.AsBoolean)
			{
				yield return doc;
			}
		}
	}

	/// <summary>
	/// ORDER BY: Sort documents according orderby expression and order asc/desc
	/// </summary>
	protected IEnumerable<AttributeDocument> OrderBy(IEnumerable<AttributeDocument> source, BsonExpression expr, int order, int offset, int limit)
	{
		throw new NotImplementedException("OrderBy is not implemented!");


		//var keyValues = source
		//	.Select(x => new KeyValuePair<AttributeValue, PageAddress>(expr.ExecuteScalar(x, Collation.Binary), x.RawId));

		//using (var sorter = new SortService(_tempDisk, order, _pragmas))
		//{
		//	sorter.Insert(keyValues);

		//	LOG($"sort {sorter.Count} keys in {sorter.Containers.Count} containers", "SORT");

		//	var result = sorter.Sort().Skip(offset).Take(limit);

		//	foreach (var keyValue in result)
		//	{
		//		var doc = _lookup.Load(keyValue.Value);

		//		yield return doc;
		//	}
		//}


		return source;
	}
}