using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
public interface IDataReader : IDisposable
{
	AttributeValue this[string field] { get; }

    AttributeValue Current { get; }

    bool HasValues { get; }

    bool Read();
}