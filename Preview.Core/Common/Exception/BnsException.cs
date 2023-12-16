namespace Xylia.Preview.Common;
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
}