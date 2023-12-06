using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Xml;
using System.Xml.Linq;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Database;
/// <summary>
/// attributes of data record 
/// </summary>
public class AttributeCollection : DynamicObject, IDisposable, IEnumerable, IReadOnlyDictionary<AttributeDefinition, object>
{
	#region Constructor
	private readonly Record record;

	/// <summary>
	/// for xml element
	/// </summary>
	private readonly Dictionary<string, object> _attributes = new();

	internal AttributeCollection(Record record)
	{
		this.record = record;
	}

	internal AttributeCollection(Record record, XElement element) : this(record)
	{
		#region attribute
		foreach (var item in element.Attributes())
		{
			if (!string.IsNullOrEmpty(item.Value))
				_attributes[item.Name.LocalName] = item.Value;
		}

		// text record
		var reader = element.CreateReader();
		reader.MoveToContent();

		var inner = reader.ReadInnerXml();
		if (inner != null) _attributes["text"] = inner;
		#endregion
	}

	internal AttributeCollection(Record record, XmlElement element, ElDefinition definition, int index = -1) : this(record)
	{
		#region attribute
		foreach (XmlAttribute item in element.Attributes)
		{
			if (!string.IsNullOrEmpty(item.Value))
				_attributes[item.Name] = item.Value;
		}

		_attributes["auto-id"] = index;
		#endregion

		#region children
		var db = record.Owner.Owner;
		foreach (var child in definition.Children)
		{
			var table = new Table() { Owner = db, Definition = new TableDefinition() { ElRecord = child } };
			table.LoadXml(element.SelectNodes("./" + table.Definition.ElRecord.Name).OfType<XmlElement>());

			record.Children[child.Name] = table.Records.ToArray();
		}
		#endregion
	}
	#endregion


	#region Get
	public bool TryGetMember(string name, bool ignoreCase, out object result)
	{
		result = Get(name);

		// parse-record does not have definition 
		return record.ElDefinition is null ?
		result != null :
			record.ElDefinition![name] != null;
	}

	public override bool TryGetMember(GetMemberBinder binder, out object result) => TryGetMember(binder.Name, binder.IgnoreCase, out result);

	public T Get<T>(string name) => (T)Get(name);

	public object Get(string name)
	{
		var attribute = record.ElDefinition?[name];

		// from prev
		if (_attributes.Count != 0)
		{
			var value = _attributes.GetValueOrDefault(name);
			if (value is string s && attribute != null) value = AttributeConverter.ConvertBack(s, attribute, record.Owner.Owner);

			return value;
		}

		// from definition
		if (name == "type") return record.ElDefinition.Name;
		return AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner);
	}


	public string this[string name] => Get(name)?.ToString();
	public string this[string name, int index] => this[$"{name}-{index}"];
	#endregion

	#region Set
	public override bool TrySetMember(SetMemberBinder binder, object value)
	{
		var attribute = record.ElDefinition[binder.Name];
		if (attribute is null) return false;

		Set(attribute, value);
		return true;
	}

	public void Set(AttributeDefinition attribute, object value)
	{
		// HACK: not implement xml
		// NOTE: String is not expected type
		switch (attribute.Type)
		{
			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var seqIndex = (sbyte)attribute.Sequence.IndexOf((string)value);
				if (seqIndex == -1)
					throw new Exception($"Invalid sequence value: '{value}'");

				value = seqIndex;
				break;
			}

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
			{
				var seqIndex = (short)attribute.Sequence.IndexOf((string)value);
				if (seqIndex == -1)
					throw new Exception($"Invalid sequence value: '{value}'");

				value = seqIndex;
				break;
			}

			case AttributeType.TRef:
			{
				var record = value as Record;
				value = record?.Ref ?? new Ref();
				break;
			}
			case AttributeType.TTRef:
			{
				var record = value as Record;
				value = new TRef(record.Owner.Type, record?.Ref ?? new Ref());
				break;
			}

			case AttributeType.TScript_obj:
				// Ignore
				break;

			// HACK: 显然这种方式有严重问题 
			case AttributeType.TString:
			{
				value = record.StringLookup.AppendString((string)value, out _);
				break;
			}
			case AttributeType.TNative:
			{
				var offset = record.StringLookup.AppendString((string)value, out var size);
				value = new Native(size, offset);
				break;
			}

			default:
				if (value is string) throw new InvalidDataException();
				break;
		}

		// valid
		if (record.Data.Length > attribute.Offset) record.Set(attribute.Offset, value);
		else Debug.WriteLine("offset out of range");
	}

	/// <summary>
	/// Create xml record data
	/// </summary>
	/// <param name="attribute"></param>
	internal void Set(AttributeDefinition attribute)
	{
		var value = this[attribute.Name] ?? attribute.DefaultValue;
		Set(attribute, AttributeConverter.ConvertBack(value, attribute, record.Owner.Owner));
	}
	#endregion


	#region Interface
	public void Dispose()
	{
		_attributes.Clear();
		GC.SuppressFinalize(this);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<KeyValuePair<AttributeDefinition, object>> GetEnumerator()
	{
		if (_attributes.Count != 0)
		{
			foreach (var attribute in _attributes)
			{
				var definition = record.ElDefinition?[attribute.Key] ?? new AttributeDefinition() { Name = attribute.Key };
				yield return new(definition, attribute.Value);
			}

			yield break;
		}

		foreach (var attribute in record.ElDefinition.ExpandedAttributes)
			yield return new(attribute, AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner));
	}

	public override string ToString() => GetEnumerator().ToIEnumerable().Aggregate("<record ", (sum, now) => sum + $"{now.Key}=\"{now.Value}\" ", result => result + "/>");

	public IEnumerable<AttributeDefinition> Keys => throw new NotImplementedException();

	public IEnumerable<object> Values => throw new NotImplementedException();

	public int Count => throw new NotImplementedException();

	public object this[AttributeDefinition key] => throw new NotImplementedException();

	public bool ContainsKey(AttributeDefinition key)
	{
		throw new NotImplementedException();
	}

	public bool TryGetValue(AttributeDefinition key, [MaybeNullWhen(false)] out object value)
	{
		throw new NotImplementedException();
	}
	#endregion

}