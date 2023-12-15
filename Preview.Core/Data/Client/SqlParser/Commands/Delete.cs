namespace Xylia.Preview.Data.Client;
internal partial class SqlParser
{
	/// <summary>
	/// DELETE {collection} WHERE {whereExpr}
	/// </summary>
	private DataReader ParseDelete()
	{
		_tokenizer.ReadToken().Expect("DELETE");

		var collection = _tokenizer.ReadToken().Expect(TokenType.Word).Value;

		BsonExpression where = null;

		if (_tokenizer.LookAhead().Is("WHERE"))
		{
			// read WHERE
			_tokenizer.ReadToken();

			where = BsonExpression.Create(_tokenizer, BsonExpressionParserMode.Full, _parameters);
		}

		_tokenizer.ReadToken().Expect(TokenType.EOF, TokenType.SemiColon);

		_tokenizer.ReadToken();

		//var result = _engine.DeleteMany(collection, where);
		//var test = new BsonDataReader(result);
		return null;
	}
}