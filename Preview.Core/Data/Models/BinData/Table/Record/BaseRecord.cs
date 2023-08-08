using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Helpers;

using Xylia.Extension;
using Xylia.Extension.Class;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
using Xylia.Xml;

using RecordModel = BnsBinTool.Core.Models.Record;

namespace Xylia.Preview.Data.Models.BinData.Table.Record;
public class BaseRecord
{
	public static bool TempSwitch = true;


	#region Fields
	private IAttributeCollection _attributes;

	public IAttributeCollection Attributes
	{
		get => _attributes;
		private set
		{
			_attributes = value;
			if (TempSwitch) this.Init();
		}
	}


	public void LoadData(IAttributeCollection data) => Attributes = data;

	public virtual void LoadData(XmlElement data) => Attributes = new XElementData(data);

	public bool Contains(string Name, out string Value) => Attributes.ContainsKey(Name, out Value) && !string.IsNullOrWhiteSpace(Value);



	public string alias;

	public short Type { get; set; }

	public Ref Ref { get; set; }

	private void Init()
	{
		List<string> _attrs = new();

		if (Attributes.ContainsKey("alias", out var _alias))
			alias = _alias.ToString();


		if (this.GetType() == typeof(BaseRecord)) return;

		foreach (var field in GetType().GetFields(ClassExtension.Flags))
		{
			var memberType = field.GetMemberType();
			if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(List<>))
			{
				var records = Activator.CreateInstance(field.FieldType);
				var recordType = field.FieldType.GetGenericArguments()[0];
				var add = records.GetType().GetMethod("Add", ClassExtension.Flags);

				if (Attributes is not XElementData xe) continue;
				if (!typeof(BaseRecord).IsAssignableFrom(recordType)) continue;

				//TODO: from def
				var subs = GetSub(recordType);
				var NodeName = field.GetAttribute<Signal>()?.Description ?? recordType.Name.ToLower();

				foreach (XmlElement data in xe.data.SelectNodes("./" + NodeName))
				{
					var SubType = recordType;

					//TODO: from def
					var type = data.Attributes["type"]?.Value;
					if (type != null && !subs.TryGetValue(type, out SubType))
					{
						Trace.WriteLine($"cast object failed: {type}");
						continue;
					}


					var Object = (BaseRecord)Activator.CreateInstance(SubType);
					Object.LoadData(data);

					add.Invoke(records, new object[] { Object });
				}

				field.SetValue(this, records);
				continue;
			}


			var name = field.GetSignal().ToLower();
			var repeat = field.GetAttribute<Repeat>()?.Value ?? 1;
			if (repeat == 1)
			{
				_attrs.Add(name);
				field.SetValue(this, ValueConvert.Construct(memberType, Attributes[name], this));
			}
			else
			{
				_attrs.Add($"{name}-{repeat}");

				if (memberType.IsArray)
				{
					var type = memberType.GetElementType();

					var value = Array.CreateInstance(type, repeat);
					Linq.For(repeat, (idx) => value.SetValue(ValueConvert.Construct(type, Attributes[name, idx + 1]), idx));

					field.SetValue(this, value);
				}
				else throw new ConfigurationErrorsException($"Repeatable object must to use array type: {this.GetType()} -> {name}");
			}
		}



		#region Check 
		if (false && Attributes is XElementData Xe)
		{
			foreach (var attr in Xe)
			{
				if (_attrs.Contains(attr.Key)) continue;
				if (attr.Key == "type") continue;

				Trace.WriteLine($"{this.GetType()}: field `{attr.Key}` not set.");
			}
		}
		#endregion
	}
	#endregion

	public Dictionary<string, Type> GetSub(Type BaseType)
	{
		var test = new Dictionary<string, Type>();
		foreach (var itemType in Assembly.GetExecutingAssembly().GetTypes())
		{
			if (!itemType.IsAbstract && BaseType.IsAssignableFrom(itemType) && itemType != BaseType)
			{
				test.Add(itemType.Name.TitleLowerCase(), itemType);
			}
		}

		return test;
	}


	#region Interface
	public override string ToString() => GetType().Name + ":" + (alias ?? Ref.ToString());

	public static bool operator ==(BaseRecord a, BaseRecord b)
	{
		// If both are null, or both are same instance, return true.
		if (ReferenceEquals(a, b)) return true;

		// If one is null, but not both, return false.
		if (a is null || b is null) return false;
		if (a.GetType() != b.GetType()) return false;


		// Return true if the fields match:
		if (a.alias is null && b.alias is null) return false;
		else if (a.alias != null && a.alias.Equals(b.alias, StringComparison.OrdinalIgnoreCase)) return true;


		return false;
	}

	public static bool operator !=(BaseRecord a, BaseRecord b) => !(a == b);

	public override bool Equals(object other) => other is BaseRecord record && this == record;

	public override int GetHashCode() => HashCode.Combine(GetType(), alias);
	#endregion

	#region Static Functions
	public static List<T> LoadChildren<T>(XmlElement xe, string NodeName = null) where T : BaseRecord, new()
	{
		NodeName ??= typeof(T).Name.ToLower();

		var records = new List<T>();
		foreach (XmlElement data in xe.SelectNodes("./" + NodeName))
		{
			var record = new T();
			record.LoadData(data);
			records.Add(record);
		}

		return records;
	}
	#endregion



	public RecordModel Serialize(ITable table = null, RecordBuilder _recordBuilder = null)
	{
		table ??= this.GetType().Name.CastTable();
		ArgumentNullException.ThrowIfNull(table);

		// get attributes from instance if doesn't exist 
		_attributes ??= new XElementData(Serialize(ReleaseSide.Client));
	
		// Create record builder if it doesn't exist
		_recordBuilder ??= table.Owner.converter.Builder;
		_recordBuilder.InitializeRecord();

		// Create record
		var def = table.TableDef;
		var record = new RecordModel
		{
			Data = new byte[def.Size],
			XmlNodeType = 1,
			SubclassType = def.SubclassType,
			DataSize = def.Size,
			StringLookup = _recordBuilder.StringLookup
		};

		// Go through each attribute
		//AttributeDefaultValues.SetRecordDefaults(record, this);
		foreach (var attr in table.TableDef.ExpandedAttributes)
		{
			_recordBuilder.SetAttribute(record, attr, Attributes[attr.Name]);
		}

		_recordBuilder.FinalizeRecord();
		return record;
	}

	public XNode Serialize(ReleaseSide side, ITableDefinition el = null)
	{
		var node = new XElement(el?.Name ??
			this.GetAttribute<Signal>()?.Description ??
			this.GetType().Name.ToLower());

		foreach (var field in this.GetType().GetFields(ClassExtension.Flags | BindingFlags.DeclaredOnly))
		{
			var type = field.FieldType;
			var value = field.GetValue(this);

			// *
			// if (value is null) continue;


			// child
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
			{
				if (value.GetType().GetGenericTypeDefinition() == typeof(Lazy<>))
					value = value.GetValue("Value");

				SetData(value);
				void SetData(object value)
				{
					if (value is null) return;
					else if (value is BaseRecord record) node.Add(record.Serialize(side));
					else if (value is IEnumerable enumerable) foreach (var t in enumerable) SetData(t);
					else Trace.WriteLine($"[Serialize Failed] {field.Name} ,{value.GetType()}");
				}

				continue;
			}



			// attribute
			AttributeDefinition attribute = null;

			//remove server fields
			if (field.ContainAttribute(out Side fside))
			{
				if (side == ReleaseSide.Client && fside.SideType == ReleaseSide.Server) continue;
				else if (side == ReleaseSide.Server && fside.SideType == ReleaseSide.Client) continue;
			}





			var Repeat = type.IsArray ? field.GetAttribute<Repeat>().Value : (ushort)1;


			Linq.For(Repeat,
			(idx) =>
			{
				object _value = value;
				if (Repeat > 1) _value = type.GetMethod("GetValue", new Type[] { typeof(int) }).Invoke(value, new object[] { idx });

				// check default
				if (false)
				{
					var ValueD = attribute?.DefaultValue;
					if (ValueD != null && value.ToString() == ValueD) return;

					if (field.ContainAttribute(out DefaultValueAttribute ValueDA)
						&& ValueDA.Value != null && ValueDA.Value.Equals(value)) return;
				}


				// convert
				string Value = (_value ??= default)?.ToString() ?? "";
				if (field.FieldType == typeof(bool)) Value = (bool)_value ? "y" : "n";
				else if (field.FieldType == typeof(float)) Value = ((float)_value).ToString("F2");
				else if (field.FieldType.IsEnum) Value = _value.GetSignal().TitleLowerCase();

				// key
				string Key = field.GetDescription(true) ?? field.Name;
				if (Repeat > 1) Key += $"-{idx + 1}";

				node.SetAttributeValue(Key.TitleLowerCase(), Value);
			});
		}

		return node;
	}
}