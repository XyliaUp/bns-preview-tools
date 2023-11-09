namespace Xylia.Preview.Data.Common.Exceptions;
public class BnsXmlFileReaderException : BnsException
{
    public BnsXmlFileReaderException(string message, string xml, int line, int pos) : base($"{message} in {xml} at line {line}:{pos}")
    {
    }

    public BnsXmlFileReaderException(string message, string xml, int line, int pos, Exception innerException) : base($"{message} in {xml} at line {line}:{pos}", innerException)
    {
    }
}