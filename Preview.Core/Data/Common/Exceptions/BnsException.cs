namespace Xylia.Preview.Data.Common.Exceptions;

public class BnsException : Exception
{
	public BnsException()
	{
	}

	public BnsException(string message) : base(message)
	{
	}

	public BnsException(string message, Exception innerException) : base(message, innerException)
	{
	}
}