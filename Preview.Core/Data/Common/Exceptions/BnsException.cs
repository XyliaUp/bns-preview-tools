using Xylia.Preview.Data.Client;

namespace Xylia.Preview.Data.Common.Exceptions;
public class BnsException : Exception
{
	#region Ctor
	public BnsException()
	{

	}

	public BnsException(string message) : base(message)
	{

	}

	public BnsException(string message, Exception innerException) : base(message, innerException)
	{

	}
	#endregion

	internal static BnsException InvalidExpressionType(BsonExpression expr, BsonExpressionType type)
	{
		return new BnsException($"Expression '{expr.Source}' must be a {type} type.");
	}

	internal static BnsException InvalidExpressionTypePredicate(BsonExpression expr)
	{
		return new BnsException($"Expression '{expr.Source}' are not supported as predicate expression.");
	}

	internal static BnsException UnexpectedToken(Token token, string expected = null)
	{
		var position = (token?.Position - (token?.Value?.Length ?? 0)) ?? 0;
		var str = token?.Type == TokenType.EOF ? "[EOF]" : token?.Value ?? "";
		var exp = expected == null ? "" : $" Expected `{expected}`.";

		return new BnsException($"Unexpected token `{str}` in position {position}.{exp}");
	}

	internal static BnsException UnexpectedToken(string message, Token token)
	{
		var position = (token?.Position - (token?.Value?.Length ?? 0)) ?? 0;

		return new BnsException(message);
	}
}