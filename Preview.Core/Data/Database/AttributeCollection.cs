using System.Collections;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Database;
/// <summary>
/// attributes of data record 
/// </summary>
public class AttributeCollection : DynamicObject, IDisposable, IEnumerable, IEnumerable<KeyValuePair<string, object>>
{
	#region Constructor
	private readonly Record record;
	private readonly Dictionary<string, object> _attributes = new();

	internal AttributeCollection(Record record)
	{
		this.record = record;
	}

	internal AttributeCollection(Record record, XmlElement element, ElDefinition definition)
	{
		this.record = record;

		#region attribute
		foreach (XmlAttribute item in element.Attributes)
		{
			if (!string.IsNullOrEmpty(item.Value))
				_attributes[item.Name] = item.Value;
		}
		#endregion

		#region children
		foreach (var child in definition.Children)
		{
			var table = new Table() { Definition = new TableDefinition() { ElRecord = child } };
			table.LoadXml(element.SelectNodes("./" + table.Definition.ElRecord.Name).OfType<XmlElement>());

			record.Children[child.Name] = table.Records.ToArray();
		}
		#endregion
	}
	#endregion


	#region Interface
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
	{
		if (_attributes.Any())
		{
			foreach (var attribute in _attributes)
				yield return new(attribute.Key, attribute.Value);
			yield break;
		}

		foreach (var attribute in record.ElDefinition.ExpandedAttributes)
			yield return new(attribute.Name, Get(attribute));
	}

	public override string ToString() => GetEnumerator().ToIEnumerable().Aggregate("<record ", (sum, now) => sum + $"{now.Key}=\"{now.Value}\" ", result => result + "/>");

	public void Dispose()
	{
		_attributes.Clear();
		GC.SuppressFinalize(this);
	}
	#endregion

	#region Get
	public override bool TryGetMember(GetMemberBinder binder, out object result)
	{
		result = Get(binder.Name);
		return record.ElDefinition[binder.Name] != null;
	}

	public string this[string name] => Get(name)?.ToString();
	public string this[string name, int index] => this[$"{name}-{index}"];

	public T Get<T>(string name) => (T)Get(name);

	public object Get(string name)
	{
		var attribute = record.ElDefinition[name];

		// from prev
		if (_attributes.Any())
		{
			var value = _attributes.GetValueOrDefault(name);
			if (value is string s && attribute != null) value = AttributeConverter.ConvertTo(s, attribute.Type, record.Owner.Owner);

			return value;
		}

		// from definition
		if (name == "type") return record.ElDefinition.Name;
		return Get(attribute);
	}

	public object Get(AttributeDefinition attribute, bool _noValidate = true)
	{
		if (_noValidate && (attribute is null || attribute.Offset >= record.DataSize))
			return null;

		switch (attribute.Type)
		{
			case AttributeType.TNone: return null;
			case AttributeType.TInt8: return record.Get<sbyte>(attribute.Offset);
			case AttributeType.TInt16: return record.Get<short>(attribute.Offset);
			case AttributeType.TInt32: return record.Get<int>(attribute.Offset);
			case AttributeType.TInt64: return record.Get<long>(attribute.Offset);
			case AttributeType.TFloat32: return record.Get<float>(attribute.Offset);
			case AttributeType.TBool: return record.Get<bool>(attribute.Offset);
			case AttributeType.TString: return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var idx = record.Get<sbyte>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					return attribute.Sequence[idx];
				}
				else
				{
					if (!_noValidate) throw new Exception("Invalid sequence index");
					return idx.ToString();
				}
			}

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
			{
				var idx = record.Get<short>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					return attribute.Sequence[idx];
				}
				else
				{
					if (!_noValidate) throw new Exception("Invalid sequence index");
					return idx.ToString();
				}
			}

			case AttributeType.TRef: return record.GetRef(attribute.ReferedTable, record.Get<Ref>(attribute.Offset));
			case AttributeType.TTRef: return record.GetRef(record.Get<TRef>(attribute.Offset));
			case AttributeType.TSub: return record.Get<short>(attribute.Offset);       // class -> subtype
			case AttributeType.TSu: throw new NotSupportedException();
			case AttributeType.TVector16: throw new NotSupportedException();
			case AttributeType.TVector32: return record.Get<Vector32>(attribute.Offset);
			case AttributeType.TIColor: return record.Get<IColor>(attribute.Offset);
			case AttributeType.TFColor: throw new NotSupportedException();
			case AttributeType.TBox: return record.Get<Box>(attribute.Offset);
			case AttributeType.TAngle: throw new NotSupportedException();
			case AttributeType.TMsec: return record.Get<Msec>(attribute.Offset);
			case AttributeType.TDistance: return record.Get<Distance>(attribute.Offset);
			case AttributeType.TVelocity: return record.Get<Velocity>(attribute.Offset);
			case AttributeType.TScript_obj:
			{
				var scriptObjBytes = record.Data[attribute.Offset..(attribute.Offset + attribute.Size)];
				if (scriptObjBytes.All(x => x == 0))
					return null;

				return Convert.ToBase64String(scriptObjBytes);
			}
			case AttributeType.TNative:
			{
				var n = record.Get<Native>(attribute.Offset);
				return record.StringLookup.GetString(n.Offset);
			}
			case AttributeType.TVersion: return record.Get<Common.DataStruct.Version>(attribute.Offset);
			case AttributeType.TIcon: return record.GetRef(record.Get<IconRef>(attribute.Offset));
			case AttributeType.TTime32: throw new NotSupportedException();
			case AttributeType.TTime64: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown1: return record.Get<Time64>(attribute.Offset);
			case AttributeType.TXUnknown2: return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			default: throw new Exception($"Unhandled type name: '{attribute.Type}'");
		}
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
				value = record.StringLookup.AppendString((string)value);
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
		Set(attribute, AttributeConverter.ConvertTo(value, attribute.Type, record.Owner.Owner));
	}
	#endregion



	#region Methods
	/// <summary>
	/// Sync attributes value from <see langword="Model Record"/> 
	/// </summary>
	/// <param name="side"></param>
	public void Synchronize()
	{
		if (record.GetType() == typeof(Record)) return;

		foreach (var field in record.GetType().GetFields(ClassExtension.Flags | BindingFlags.DeclaredOnly))
		{
			var type = field.FieldType;
			if (type.IsList() || field.ContainAttribute<Deprecated>()) continue;

			ushort repeat = 1;
			if (type.IsArray)
			{
				repeat = field.GetAttribute<Repeat>().Value;
				type = type.GetElementType();
			}

			//remove server fields
			//if (field.ContainAttribute(out Side fside))
			//{
			//	if (side == ReleaseSide.Client && fside.SideType == ReleaseSide.Server) continue;
			//	else if (side == ReleaseSide.Server && fside.SideType == ReleaseSide.Client) continue;
			//}


			var instance = field.GetValue(record);
			for (int i = 0; i < repeat; i++)
			{
				// key
				string key = (field.GetName() ?? field.Name).TitleLowerCase();
				if (repeat > 1) key += $"-{i + 1}";

				// convert
				var value = repeat > 1 ? ((Array)instance).GetValue(i) : instance;
				if (type.IsEnum) value = value?.ToString().TitleLowerCase();

				_attributes[key] = value;
			}
		}
	}
	#endregion
}