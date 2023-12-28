using System.Collections;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;
public class AttributeDocument : AttributeValue, IDictionary<string, AttributeValue>
{
	public AttributeDocument() : base(AttributeType.TNone, new Dictionary<string, AttributeValue>(StringComparer.OrdinalIgnoreCase))
	{
	}


	public AttributeDocument(ConcurrentDictionary<string, AttributeValue> dict) : this()
	{
		ArgumentNullException.ThrowIfNull(dict);

		foreach (var element in dict)
		{
			this.Add(element);
		}
	}

	public AttributeDocument(IDictionary<string, AttributeValue> dict) : this()
	{
		ArgumentNullException.ThrowIfNull(dict);

		foreach (var element in dict)
		{
			this.Add(element);
		}
	}

	public AttributeDocument(AttributeCollection collection) : this()
	{
		ArgumentNullException.ThrowIfNull(collection);

		foreach (var attribute in collection)
		{
			var value = new AttributeValue(attribute.Key.Type, attribute.Value);
			this.Add(attribute.Key.Name, value);
		}
	}


	public new IDictionary<string, AttributeValue> RawValue => base.RawValue as IDictionary<string, AttributeValue>;

	/// <summary>
	/// Get/Set a field for document. Fields are case sensitive
	/// </summary>
	public override AttributeValue this[string key]
	{
		get => this.RawValue.GetOrDefault(key, Null);
		set => this.RawValue[key] = value ?? Null;
	}

	public override int CompareTo(AttributeValue other)
	{
		// if types are different, returns sort type order
		if (!other.IsDocument) return this.Type.CompareTo(other.Type);

		var thisKeys = this.Keys.ToArray();
		var thisLength = thisKeys.Length;

		var otherDoc = other.AsDocument;
		var otherKeys = otherDoc.Keys.ToArray();
		var otherLength = otherKeys.Length;

		var result = 0;
		var i = 0;
		var stop = Math.Min(thisLength, otherLength);

		for (; 0 == result && i < stop; i++)
			result = this[thisKeys[i]].CompareTo(otherDoc[thisKeys[i]]);

		// are different
		if (result != 0) return result;

		// test keys length to check which is bigger
		if (i == thisLength) return i == otherLength ? 0 : -1;

		return 1;
	}

	public override string ToString() => JsonConvert.SerializeObject(this);

	#region IDictionary
	public ICollection<string> Keys => this.RawValue.Keys;

	public ICollection<AttributeValue> Values => this.RawValue.Values;

	public int Count => this.RawValue.Count;

	public bool IsReadOnly => false;

	public bool ContainsKey(string key) => this.RawValue.ContainsKey(key);

	/// <summary>
	/// Get all document elements - Return "_id" as first of all (if exists)
	/// </summary>
	public IEnumerable<KeyValuePair<string, AttributeValue>> GetElements()
	{
		if (this.RawValue.TryGetValue("_id", out var id))
		{
			yield return new KeyValuePair<string, AttributeValue>("_id", id);
		}

		foreach (var item in this.RawValue.Where(x => x.Key != "_id"))
		{
			yield return item;
		}
	}

	public void Add(string key, AttributeValue value) => this.RawValue.Add(key, value ?? Null);

	public bool Remove(string key) => this.RawValue.Remove(key);

	public void Clear() => this.RawValue.Clear();

	public bool TryGetValue(string key, out AttributeValue value) => this.RawValue.TryGetValue(key, out value);

	public void Add(KeyValuePair<string, AttributeValue> item) => this.Add(item.Key, item.Value);

	public bool Contains(KeyValuePair<string, AttributeValue> item) => this.RawValue.Contains(item);

	public bool Remove(KeyValuePair<string, AttributeValue> item) => this.Remove(item.Key);

	public IEnumerator<KeyValuePair<string, AttributeValue>> GetEnumerator() => this.RawValue.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => this.RawValue.GetEnumerator();

	public void CopyTo(KeyValuePair<string, AttributeValue>[] array, int arrayIndex)
	{
		RawValue.CopyTo(array, arrayIndex);
	}

	public void CopyTo(AttributeDocument other)
	{
		foreach (var element in this)
		{
			other[element.Key] = element.Value;
		}
	}
	#endregion
}