namespace Xylia.Preview.Data.Common.Exceptions;
public class BnsDefinitionException : BnsException
{
	public BnsDefinitionException(string message) : base(message)
	{

	}

	public BnsDefinitionException(string message, Exception innerException) : base(message, innerException)
	{

	}
}