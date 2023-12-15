using System.Collections;
using System.Dynamic;
using System.Xml;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;
/// <summary>
/// attributes of data record 
/// </summary>
public class AttributeCollection : DynamicObject, IDisposable, IReadOnlyDictionary<AttributeDefinition, object>
{
	#region Fields
	internal const string s_autoid = "auto-id";
	internal const string s_type = "type";


	/// <summary>
	/// Owner element
	/// </summary>
	private readonly Record record;

	/// <summary>
	/// for xml element
	/// </summary>
	private readonly Dictionary<string, object> attributes = [];
	#endregion

	#region Ctor
	internal AttributeCollection(Record record)
	{
		this.record = record;
	}

	internal AttributeCollection(Record record, XmlElement element, ElDefinition definition, int index = -1) : this(record)
	{
		#region attribute
		foreach (XmlAttribute item in element.Attributes)
		{
			var name = item.Name;
			if (name == s_type) continue;

			attributes[name] = item.Value;
		}

		// Native
		if (!string.IsNullOrEmpty(element.InnerXml))
		{
			var attr = definition.Attributes.FirstOrDefault(a => a.Type == AttributeType.TNative);
			if (attr != null) attributes[attr.Name] = element.InnerXml;
		}

		attributes[s_autoid] = index;
		#endregion


		#region children
		var provider = record.Owner.Owner;
		foreach (var child in definition.Children)
		{
			var table = new Table() { Owner = provider, Definition = new TableDefinition() { ElRecord = child } };
			table.LoadElement(element, null);

			record.Children[child.Name] = [.. table.Records];
		}
		#endregion
	}
	#endregion


	#region Methods
	internal void CreateData(ITableDefinition definition, bool OnlyKey = false)
	{
		if (OnlyKey)
		{
			definition.ExpandedAttributes.Where(attr => attr.IsKey).ForEach(record.Attributes.Set);
		}
		else
		{
			definition.ExpandedAttributes.ForEach(record.Attributes.Set);
			attributes.Clear();
		}
	}

	public string this[string name] => Get(name)?.ToString();

	public string this[string name, int index] => this[$"{name}-{index}"];
	#endregion


	#region Get
	public override bool TryGetMember(GetMemberBinder binder, out object result) => TryGetValue(binder.Name, out result);

	public bool TryGetValue(string name, out object result)
	{
		result = Get(name);
		return result != null || record.ElDefinition?[name] != null;
	}

	public T Get<T>(string name) => (T)Get(name);

	public object Get(string name)
	{
		if (name == s_type) return record.ElDefinition.Name;
		var attribute = record?.ElDefinition[name];

		// from prev
		if (attributes.Count != 0)
		{
			var value = attributes.GetValueOrDefault(name, attribute?.DefaultValue);
			if (value is string s && attribute != null) value = AttributeConverter.ConvertBack(s, attribute, record.Owner.Owner);

			return value;
		}

		// from definition
		return AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner);
	}
	#endregion

	#region Set
	public override bool TrySetMember(SetMemberBinder binder, object value)
	{
		var attribute = record.ElDefinition[binder.Name];
		if (attribute is null) return false;

		Set(attribute, value);
		return true;
	}

	internal void Set(AttributeDefinition attribute)
	{
		Set(attribute, Get(attribute.Name));
	}

	public void Set(AttributeDefinition attribute, object value)
	{
		switch (attribute.Type)
		{
			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var seqIndex = (sbyte)attribute.Sequence.IndexOf((string)value);
				if (seqIndex == -1)
				{
					Serilog.Log.Warning($"Invalid sequence, name: '{attribute.Name}' value: '{value}'");
					seqIndex = 0;
				}

				value = seqIndex;
				break;
			}

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
			{
				var seqIndex = (short)attribute.Sequence.IndexOf((string)value);
				if (seqIndex == -1)
				{
					Serilog.Log.Warning($"Invalid sequence, name: '{attribute.Name}' value: '{value}'");
					seqIndex = 0;
				}

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
				value = new TRef(record);
				break;
			}
			case AttributeType.TIcon:
			{
				var provider = this.record.Owner.Owner;
				var record = provider.Tables.GetIconRecord((string)value, out var index);
				value = new IconRef(record, index);
				break;
			}

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
		}

		// valid result
		if (value is null) return;
		else if (value is string) throw new InvalidDataException("String is not expected type");
		else if (record.Data.Length < attribute.Offset) throw new InvalidDataException("offset out of range");
		else record.Data.Set(attribute.Offset, value);
	}
	#endregion


	#region Interface
	public void Dispose()
	{
		attributes.Clear();
		GC.SuppressFinalize(this);
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<KeyValuePair<AttributeDefinition, object>> GetEnumerator()
	{
		if (attributes.Count != 0)
		{
			foreach (var attribute in attributes)
			{
				var value = attribute.Value;
				var definition = record?.ElDefinition?[attribute.Key];

				// convert type
				if (value is string s && definition != null)
				{
					var temp = AttributeConverter.ConvertBack(s, definition, record.Owner.Owner);
					if (temp != null) attributes[attribute.Key] = value = temp;
				}

				// virtual definition, ensure the name can be getted
				definition ??= new AttributeDefinition() { Name = attribute.Key, Type = AttributeType.TString };
				yield return new(definition, value);
			}

			yield break;
		}

		foreach (var attribute in record.ElDefinition.ExpandedAttributes)
			yield return new(attribute, AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner));
	}


	public override string ToString() => this.Aggregate("<record ", (sum, now) => sum + $"{now.Key.Name}=\"{now.Value}\" ", result => result + "/>");

	public int Count => this.Count();

	public IEnumerable<AttributeDefinition> Keys => this.Select(x => x.Key);

	public IEnumerable<object> Values => this.Select(x => x.Value);

	public object this[AttributeDefinition key] => Get(key.Name);

	public bool ContainsKey(AttributeDefinition key) => record.ElDefinition[key.Name] != null;

	public bool TryGetValue(AttributeDefinition key, out object value)
	{
		throw new NotImplementedException();
	}
	#endregion
}