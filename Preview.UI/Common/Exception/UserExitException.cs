using System.Runtime.Serialization;

namespace Xylia.Preview.UI.Common;

[Serializable]
public class UserExitException : ApplicationException
{
	public UserExitException() { }
	public UserExitException(string message) : base(message) { }
	public UserExitException(string message, Exception inner) : base(message, inner) { }
	public UserExitException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}