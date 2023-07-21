using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Xml;

using BnsBinTool.Core.DataStructs;

using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;

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

					add.Invoke(records, Object);
				}

				field.SetValue(this, records);
				continue;
			}




			var name = field.GetSignal().ToLower();
			var repeat = field.GetAttribute<Repeat>()?.Value ?? 1;
			if (repeat == 1)
			{
				_attrs.Add(name);
				field.SetValue(this, ValueConvert.Construct(memberType, Attributes[name]));
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
	public XmlDocument XmlInfo(ReleaseSide side = default)
	{
		var doc = new XmlDocument();

		XmlElement table = doc.CreateElement("table");
		doc.AppendChild(table);
		table.SetAttribute("release-module", "");
		table.SetAttribute("release-side", side.ToString().ToLower());
		table.SetAttribute("type", "");
		table.SetAttribute("version", $"");
		table.AppendChild(Serialize(this, doc));

		return doc;
	}

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

	public static XmlNode Serialize(object T, XmlDocument doc, ReleaseSide side = default, bool HasChild = true, string NodeName = null)
	{
		//如果未传递节点名, 则获取当前节点名称
		NodeName ??= T.GetAttribute<Signal>()?.Description ?? T.GetType().Name.ToLower();
		var Node = doc.CreateElement(NodeName);


		foreach (var field in T.GetType().GetFields(ClassExtension.Flags))
		{
			if (field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
			{
				//if (Field.FieldType.IsValueType) continue;

				if (HasChild)
				{
					var obj = field.GetValue(T);
					if (obj is null) continue;
					if (obj.GetType().GetGenericTypeDefinition() == typeof(Lazy<>))
						obj = obj.GetValue("Value");


					SetData(obj);
					void SetData(object obj)
					{
						if (obj is null) return;
						else if (obj is BaseRecord) Node.AppendChild(Serialize(obj, doc, side));
						else if (obj is IEnumerable enumerable) foreach (var t in enumerable) SetData(t);
						else Trace.WriteLine($"[Serialize Failed] {field.Name} ,{obj.GetType()}");
					}
				}
			}
			else
			{
				//remove server fields
				if (field.ContainAttribute(out Side fside))
				{
					if (side == ReleaseSide.Client && fside.SideType == ReleaseSide.Server) continue;
					else if (side == ReleaseSide.Server && fside.SideType == ReleaseSide.Client) continue;
				}

				var ObjVal = field.GetValue(T);
				if (ObjVal is null) continue;

				#region deault
				//默认值为 Null, 则表示任何值都应该显示
				if (field.ContainAttribute(out DefaultValueAttribute DefVal))
				{
					if (DefVal.Value != null && DefVal.Value.Equals(ObjVal))
						continue;
				}
				else if (field.FieldType == typeof(bool) && !(bool)ObjVal) continue;
				else if (field.FieldType == typeof(int) && (int)ObjVal == 0) continue;
				else if (field.FieldType == typeof(byte) && (byte)ObjVal == 0) continue;
				else if (field.FieldType == typeof(long) && (long)ObjVal == 0) continue;
				else if (field.FieldType == typeof(short) && (short)ObjVal == 0) continue;
				else if (field.FieldType == typeof(float) && (float)ObjVal == 0) continue;
				else if (field.FieldType == typeof(double) && (double)ObjVal == 0) continue;
				else if (field.FieldType.IsEnum)
				{
					var EnumValue = ObjVal.ToString();

					//如果不存在, 则判断枚举对象自身的默认值信息
					if (field.FieldType.ContainAttribute(out DefaultValueAttribute DefVal2))
					{
						if (field.FieldType == DefVal2.Value.GetType() && DefVal2.Value.Equals(ObjVal)) continue;
					}
					else if (EnumValue.Equals("none", StringComparison.OrdinalIgnoreCase)) continue;
				}
				#endregion

				#region value
				string Value = ObjVal.ToString();
				if (field.FieldType == typeof(bool)) Value = (bool)ObjVal ? "y" : "n";
				else if (field.FieldType == typeof(float)) Value = ((float)ObjVal).ToString("0.0001");
				else if (field.FieldType.IsEnum)
				{
					if (ObjVal.ContainAttribute(out Signal EnumDescA) && !string.IsNullOrWhiteSpace(EnumDescA.Description))
						Value = EnumDescA.Description;

					Value = Value.ToLower();
				}
				#endregion

				#region name
				string Key = field.Name.ToLower();
				if (field.ContainAttribute(out Signal descA) && !string.IsNullOrWhiteSpace(descA.Description))
					Key = descA.Description;

				Node.SetAttribute(Key, Value);
				#endregion
			}
		}

		return Node;
	}
	#endregion
}

public class IAttribute
{
	public string Key;

	public string Value;


	public IAttribute(string key, string value)
	{
		this.Key = key;
		this.Value = value;
	}

	public IAttribute(KeyValuePair<string, string> pair)
	{
		this.Key = pair.Key;
		this.Value = pair.Value;
	}
}
