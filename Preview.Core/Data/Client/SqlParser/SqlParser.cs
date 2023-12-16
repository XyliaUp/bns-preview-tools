using System.Diagnostics;
using Xylia.Preview.Data.Common.Exceptions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
/// <summary>
/// parse and execute sql commands
/// </summary>
internal partial class SqlParser
{
	private readonly BnsDatabase _engine;
	private readonly Tokenizer _tokenizer;
	private readonly AttributeDocument _parameters;

	public SqlParser(BnsDatabase engine, Tokenizer tokenizer, AttributeDocument parameters)
	{
		_engine = engine;
		_tokenizer = tokenizer;
		_parameters = parameters;


		//var _tree = _parser.Parse(command);

		//var error = _tree.ParserMessages.Where(m => m.Level == Irony.ErrorLevel.Error).Select(m => new Exception(m.Message));
		//if (error.Any()) throw new AggregateException(error);
	}

	public IDataReader Execute()
	{
		var ahead = _tokenizer.LookAhead().Expect(TokenType.Word);

		Debug.WriteLine($"executing `{ahead.Value.ToUpper()}`", "SQL");

		switch (ahead.Value.ToUpper())
		{
			case "SELECT":
			case "EXPLAIN":
				return this.ParseSelect();
			case "INSERT": return this.ParseInsert();
			case "DELETE": return this.ParseDelete();
			case "UPDATE": return this.ParseUpdate();
			case "COMMIT": return this.ParseCommit();

			default: throw BnsDataException.UnexpectedToken(ahead);
		}
	}
}