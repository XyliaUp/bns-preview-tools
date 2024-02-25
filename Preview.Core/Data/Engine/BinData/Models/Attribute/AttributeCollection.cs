using System.Collections;
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
public class AttributeCollection : IReadOnlyDictionary<AttributeDefinition, object>
{
	#region Fields
	internal const string s_autoid = "auto-id";
	internal const string s_type = "type";

	/// <summary>
	/// Owner element
	/// </summary>
	protected readonly Record record;

	/// <summary>
	/// for xml element
	/// </summary>
	protected readonly Dictionary<string, object> attributes = [];
	#endregion

	#region Constructors
	internal void BuildData(ElementBaseDefinition definition, bool OnlyKey = false)
	{
		void SetData(AttributeDefinition attribute) =>
			record.Attributes.Set(attribute, record.Attributes.Get(attribute.Name));

		// implement IGameDataKeyParser
		if (OnlyKey)
		{
			var keys = definition.ExpandedAttributes.Where(attr => attr.IsKey);
			// TODO: ignore children element
			// if (!keys.Any()) throw new Exception("invalid key attribute");

			keys.ForEach(SetData);
		}
		else
		{
			// create data
			definition.ExpandedAttributes.ForEach(SetData);
			attributes.Clear();
		}
	}

	internal AttributeCollection(Record record)
	{
		this.record = record;
	}

	internal AttributeCollection(Record record, XmlElement element, ElementDefinition definition, int index = -1) : this(record)
	{
		#region attribute
		foreach (XmlAttribute item in element.Attributes)
		{
			var name = item.Name;
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
	public object this[string name] { get => Get(name); set => Set(name, value); }

	public object this[AttributeDefinition key] { get => Get(key.Name); set => Set(key, value); }


	public void CheckAttribute(params string[] attrNames)
	{
		foreach (string text in attrNames)
		{
			if (this[text] == null)
			{
				throw new Exception(string.Format("{0} Attribute is Required Field", text));
			}
		}
	}
	#endregion


	#region Get
	public bool TryGetValue(string name, out object result)
	{
		result = Get(name);
		return result != null || record.Definition?[name] != null;
	}

	// TODO: Value convert
	public T Get<T>(string name) => (T)Get(name);

	public object Get(string name)
	{
		var definition = record.Definition;
		var attribute = definition[name];

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
	public void Set(string name, object value)
	{
		var attribute = record?.Definition[name];
		if (attribute is null) return;

		if (value is string s)
			value = AttributeConverter.ConvertBack(s, attribute, record.Owner.Owner);

		Set(attribute, value);
		return;
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
				value = record.PrimaryKey;
				break;
			}
			case AttributeType.TTRef:
			{
				var provider = this.record.Owner.Owner;
				var record = provider.Tables.GetRecord((string)value);
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
			case AttributeType.TXUnknown2:
			{
				value = record.StringLookup.AppendString((ObjectPath)value, out _);
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


	#region IReadOnlyDictionary
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<KeyValuePair<AttributeDefinition, object>> GetEnumerator()
	{
		if (attributes.Count != 0)
		{
			foreach (var attribute in attributes)
			{
				var value = attribute.Value;
				var definition = record?.Definition?[attribute.Key];

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
		}
		else
		{
			foreach (var attribute in record.Definition.ExpandedAttributes)
				yield return new(attribute, AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner));
		}
	}

	public int Count => this.Count();

	public IEnumerable<AttributeDefinition> Keys => this.Select(x => x.Key);

	public IEnumerable<object> Values => this.Select(x => x.Value);

	public bool ContainsKey(AttributeDefinition key) => record.Definition[key.Name] != null;

	public bool TryGetValue(AttributeDefinition key, out object value)
	{
		throw new NotImplementedException();
	}
	#endregion

	#region Interface
	public override string ToString() => this.Aggregate("<record ", (sum, now) => sum + $"{now.Key.Name}=\"{now.Value}\" ", result => result + "/>");
	#endregion
}