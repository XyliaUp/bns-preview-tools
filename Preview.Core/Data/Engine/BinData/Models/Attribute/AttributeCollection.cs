using System.Collections;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
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
public class AttributeCollection : IReadOnlyDictionary<AttributeDefinition, object>, IDynamicMetaObjectProvider
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

	#region Ctor
	internal void CreateData(ITableDefinition definition, bool OnlyKey = false)
	{
		void SetData(AttributeDefinition attribute) =>
			record.Attributes.Set(attribute, record.Attributes.Get(attribute.Name));

		if (OnlyKey)
		{
			definition.ExpandedAttributes.Where(attr => attr.IsKey).ForEach(SetData);
		}
		else
		{
			definition.ExpandedAttributes.ForEach(SetData);
			attributes.Clear();
		}
	}

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
	public object this[string name, int index] => this[$"{name}-{index}"];

	public object this[string name] 
	{ 
		get => Get(name); 
		set => Set(name, value); 
	}

	public object this[AttributeDefinition key] { get => Get(key.Name); set => Set(key, value); }
	#endregion


	#region Get
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
	public object Set(string name, object value)
	{
		var attribute = record?.ElDefinition[name];
		if (attribute is null) return value;

		if (value is string s)
			value = AttributeConverter.ConvertBack(s, attribute, record.Owner.Owner);

		Set(attribute, value);
		return value;
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


	#region IReadOnlyDictionary
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
		}
		else
		{
			foreach (var attribute in record.ElDefinition.ExpandedAttributes)
				yield return new(attribute, AttributeConverter.ConvertTo(record, attribute, record.Owner.Owner));
		}
	}

	public int Count => this.Count();

	public IEnumerable<AttributeDefinition> Keys => this.Select(x => x.Key);

	public IEnumerable<object> Values => this.Select(x => x.Value);

	public bool ContainsKey(AttributeDefinition key) => record.ElDefinition[key.Name] != null;

	public bool TryGetValue(AttributeDefinition key, out object value)
	{
		throw new NotImplementedException();
	}
	#endregion

	#region IDynamicMetaObjectProvider
	public DynamicMetaObject GetMetaObject(Expression parameter) => new MetaDynamic(parameter, this);

	class MetaDynamic : DynamicMetaObject
	{
		MethodInfo _getMethod = typeof(AttributeCollection).GetMethods().First(x => !x.IsGenericMethod && x.Name == nameof(Get));
		MethodInfo _setMethod = typeof(AttributeCollection).GetMethods().First(x => !x.IsGenericMethod && x.Name == nameof(Set));

		internal MetaDynamic(Expression expression, AttributeCollection value)
			: base(expression, BindingRestrictions.Empty, value)
		{

		}

		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			// setup the binding restrictions.
			var restrictions = BindingRestrictions.GetTypeRestriction(Expression, LimitType);

			// setup the parameters
			var args = new Expression[] { Expression.Constant(binder.Name) };
			Expression call = Expression.Call(Expression.Convert(Expression, LimitType), _getMethod, args);

			var fallback = new DynamicMetaObject(call, restrictions);
			return binder.FallbackGetMember(this, fallback);
		}

		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			// setup the binding restrictions.
			var restrictions = BindingRestrictions.GetTypeRestriction(Expression, LimitType);

			// setup the parameters
			var args = new Expression[]
			{
				Expression.Constant(binder.Name),
				Expression.Convert(value.Expression, typeof(object))
			};

			// Setup the method call expression
			Expression call = Expression.Call(Expression.Convert(Expression, LimitType), _setMethod, args);

			// Create a meta object to invoke Set later
			var fallback = new DynamicMetaObject(call, restrictions);
			return binder.FallbackSetMember(this, value, fallback);
		}
	}
	#endregion


	#region Interface
	public override string ToString() => this.Aggregate("<record ", (sum, now) => sum + $"{now.Key.Name}=\"{now.Value}\" ", result => result + "/>");
	#endregion
}