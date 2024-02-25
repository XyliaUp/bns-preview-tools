namespace Xylia.Preview.Common;
public class BnsException : Exception
{
    #region Constructors
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
}