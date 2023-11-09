using System.Runtime.Serialization;

namespace Xylia.Preview.Data.Common.Exceptions
{
	public class BnsException : Exception
    {
        public BnsException()
        {
        }

        protected BnsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BnsException(string message) : base(message)
        {
        }

        public BnsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}