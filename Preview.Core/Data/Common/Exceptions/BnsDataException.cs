using Xylia.Preview.Common;
using Xylia.Preview.Data.Client;

namespace Xylia.Preview.Data.Common.Exceptions;
internal class BnsDataException : BnsException
{
	#region Constructors
	public BnsDataException(string message) : base(message)
	{

	}

	public BnsDataException(string message, Exception innerException) : base(message, innerException)
	{

	}
	#endregion

	#region	Definition
	internal static BnsDataException InvalidGame(int game = 0)
	{
		return new BnsDataException($"invalid game (code: {game})");
	}

	internal static BnsDataException InvalidDefinition(string message)
	{
		return new BnsDataException(message);
	}

	internal static BnsDataException InvalidSequence(string message, string name)
	{
		return new BnsDataException($"seq `{name}` {message}");
	}
	#endregion

	#region Expression
	internal static BnsDataException InvalidExpressionType(BsonExpression expr, BsonExpressionType type)
	{
		return new BnsDataException($"Expression '{expr.Source}' must be a {type} type.");
	}

	internal static BnsDataException InvalidExpressionTypePredicate(BsonExpression expr)
	{
		return new BnsDataException($"Expression '{expr.Source}' are not supported as predicate expression.");
	}

	internal static BnsDataException UnexpectedToken(Token token, string expected = null)
	{
		var position = token?.Position - (token?.Value?.Length ?? 0) ?? 0;
		var str = token?.Type == TokenType.EOF ? "[EOF]" : token?.Value ?? "";
		var exp = expected == null ? "" : $" Expected `{expected}`.";

		return new BnsDataException($"Unexpected token `{str}` in position {position}.{exp}");
	}

	internal static BnsDataException UnexpectedToken(string message, Token token)
	{
		var position = token?.Position - (token?.Value?.Length ?? 0) ?? 0;

		return new BnsDataException(message);
	}		
	#endregion
}