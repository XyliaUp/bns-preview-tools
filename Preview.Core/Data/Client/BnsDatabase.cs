using CUE4Parse.UE4.Pak;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Data.Client;
public class BnsDatabase : IEngine, IDisposable
{
	#region Constructorss
	public BnsDatabase(IDataProvider provider = null)
	{
		_provider = provider ?? DefaultProvider.Load(Settings.Default.GameFolder);
		ArgumentNullException.ThrowIfNull(_provider);
	}
	#endregion

	#region Database
	public void Initialize()
	{
		lock (this)
		{
			if (IsInitialized) return;

			IPlatformFilePak.DoSignatureCheck();

			var definitions = TableDefinitionHelper.LoadDefinition();
			_provider.LoadData(definitions);
			definitions.CreateMap();

			// Bind definitions to tables
			foreach (var table in _provider.Tables)
			{
				table.Owner = _provider;

				if (table.Type == 0)
				{
					// represents from xml
					ArgumentException.ThrowIfNullOrEmpty(table.Name);
					table.Definition = definitions[table.Name];
					table.Type = table.Definition.Type;
				}
				else
				{
					table.Definition = definitions[table.Type];
				}
			}

			IsInitialized = true;
		}
	}


	public IDataReader Execute(string command, AttributeDocument parameters = null)
	{
		ArgumentNullException.ThrowIfNull(command);

		var tokenizer = new Tokenizer(command);
		var sql = new SqlParser(this, tokenizer, parameters);
		var reader = sql.Execute();

		return reader;
	}

	public IDataReader Execute(string command, params AttributeValue[] args)
	{
		var p = new AttributeDocument();
		var index = 0;

		foreach (var arg in args)
		{
			p[index.ToString()] = arg;
			index++;
		}

		return this.Execute(command, p);
	}

	public bool Commit()
	{
		throw new NotImplementedException();
	}

	public bool Rollback()
	{
		throw new NotImplementedException();
	}

	public IDataReader Query(string collection, Query query)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(collection);
		ArgumentNullException.ThrowIfNull(query);

		IEnumerable<AttributeDocument> source = null;

		// test if is an system collection
		//if (collection.StartsWith("$"))
		//{
		//	SqlParser.ParseCollection(new Tokenizer(collection), out var name, out var options);

		//	// get registered system collection to get data source
		//	var sys = this.GetSystemCollection(name);

		//	source = sys.Input(options);
		//	collection = sys.Name;
		//}

		var exec = new QueryExecutor(this, collection, query, source);

		return exec.ExecuteQuery();
	}

	public int Insert(string collection, IEnumerable<AttributeDocument> docs)
	{
		throw new NotImplementedException();
	}

	public int Update(string collection, IEnumerable<AttributeDocument> docs)
	{
		throw new NotImplementedException();
	}

	public int UpdateMany(string collection, BsonExpression transform, BsonExpression predicate)
	{
		throw new NotImplementedException();
	}

	public int Upsert(string collection, IEnumerable<AttributeDocument> docs)
	{
		throw new NotImplementedException();
	}

	public int Delete(string collection, IEnumerable<AttributeValue> ids)
	{
		throw new NotImplementedException();
	}

	public int DeleteMany(string collection, BsonExpression predicate)
	{
		throw new NotImplementedException();
	}
	#endregion


	#region Interface
	public void Dispose()
	{
		_provider.Dispose();
		_provider = null;

		GC.SuppressFinalize(this);
		GC.Collect();
	}
	#endregion

	#region Data
	public bool IsInitialized { get; private set; }

	private IDataProvider _provider;

	public IDataProvider Provider
	{
		protected set => _provider = value;
		get
		{
			Initialize();
			return _provider;
		}
	}
	#endregion
}