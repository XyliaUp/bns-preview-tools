using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Client;
public class DataReader : IDataReader
{
	private readonly IEnumerator<AttributeValue> _source = null;
	private readonly string _collection = null;
	private readonly bool _hasValues;

	private AttributeValue _current = null;
	private bool _isFirst;

	/// <summary>
	/// Initialize with no value
	/// </summary>
	internal DataReader()
	{
		_hasValues = false;
	}

	/// <summary>
	/// Initialize with a single value
	/// </summary>
	internal DataReader(AttributeValue value, string collection = null)
	{
		_current = value;
		_isFirst = _hasValues = true;
		_collection = collection;
	}

	/// <summary>
	/// Initialize with an IEnumerable data source
	/// </summary>
	internal DataReader(IEnumerable<AttributeValue> values, string collection)
	{
		_source = values.GetEnumerator();
		_collection = collection;

		if (_source.MoveNext())
		{
			_hasValues = _isFirst = true;
			_current = _source.Current;
		}
	}

	/// <summary>
	/// Return if has any value in result
	/// </summary>
	public bool HasValues => _hasValues;

	/// <summary>
	/// Return current value
	/// </summary>
	public AttributeValue Current => _current;

	/// <summary>
	/// Return collection name
	/// </summary>
	public string Collection => _collection;

	/// <summary>
	/// Move cursor to next result. Returns true if read was possible
	/// </summary>
	public bool Read()
	{
		if (!_hasValues) return false;

		if (_isFirst)
		{
			_isFirst = false;
			return true;
		}
		else
		{
			if (_source != null)
			{
				var read = _source.MoveNext();
				_current = _source.Current;
				return read;
			}
			else
			{
				return false;
			}
		}
	}

	public AttributeValue this[string field]
	{
		get
		{
			return (AttributeValue)_current.AsDocument[field] ?? null; 
		}
	}

	public void Dispose()
	{
		_source?.Dispose();
		GC.SuppressFinalize(this);
	}
}