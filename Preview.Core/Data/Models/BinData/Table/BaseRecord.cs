using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Attributes;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record
{
    public class BaseRecord : IArgParam
	{
		#region Fields
		public IAttributeCollection Attributes { get; set; }

		public virtual int Key() => int.TryParse(this.Attributes?["id"], out var @int) ? @int : (int)(this.TableIndex + 1);

		public uint TableIndex { get; set; }

		public string alias;


		public bool ContainsAttribute(string Name, out string Value) => this.Attributes.ContainsKey(Name, out Value) && Value != null;

		private void Init()
		{
			if (Attributes.ContainsKey("alias", out var _alias))
				this.alias = _alias.ToString();

			var convert = new RefConvert();
			if (Attributes is XElementData xe)
			{
				foreach (XmlAttribute attr in xe.data.Attributes)
				{
					if (attr.Name == "type") continue;

					this.SetMember(attr.Name, attr.Value, true, convert);
				}

				foreach (var field in this.GetType().GetFields(ClassExtension.Flags))
				{
					if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
					{
						var type = field.FieldType.GetGenericArguments()[0];
						if (type.HasImplementedRawGeneric(typeof(BaseRecord)) && !type.IsAbstract)
						{
							var records = Activator.CreateInstance(field.FieldType);
							var add = records.GetType().GetMethod("Add", ClassExtension.Flags);

							var NodeName = field.GetAttribute<Signal>()?.Description ?? type.Name.ToLower();
							foreach (XmlElement data in xe.data.SelectNodes("./" + NodeName))
							{
								var record = (BaseRecord)Activator.CreateInstance(type);
								record.LoadData(data);

								add.Invoke(records, record);
							}

							field.SetValue(this, records);
						}
					}
				}
			}
			else
			{
				foreach (var field in this.GetType().GetFields(ClassExtension.Flags))
				{
					var Name = field.GetSignal();
					if (this.Attributes.ContainsKey(Name, out var Value))
						this.SetMember(field, Value, convert);
				}
			}
		}



		public bool INVALID
		{
			get
			{
				if (this.ContainAttribute<AliasRecord>()) return string.IsNullOrWhiteSpace(this.alias);


				return false;
			}
		}
		#endregion

		#region Interface
		public override string ToString() => this.GetType().Name + ":" + (this.alias ?? this.Key().ToString());

		object IArgParam.ParamTarget(string ParamName) => this.GetParam(ParamName) ?? this.Attributes[ParamName];


		public static bool operator ==(BaseRecord a, BaseRecord b)
		{
			// If both are null, or both are same instance, return true.
			if (ReferenceEquals(a, b)) return true;

			// If one is null, but not both, return false.
			if (a is null || b is null) return false;
			if (a.GetType() != b.GetType()) return false;

			// Return true if the fields match:
			if (a.alias != null && a.alias != b.alias) return false;


			return true;
		}

		public static bool operator !=(BaseRecord a, BaseRecord b)
		{
			return !(a == b);
		}

		public override bool Equals(object other) => other is BaseRecord record && this == record;

		public override int GetHashCode() => HashCode.Combine(this.GetType(), this.alias);
		#endregion



		public virtual void LoadData(IAttributeCollection data)
		{
			this.Attributes = data;
			this.Init();
		}

		public virtual void LoadData(XmlElement data)
		{
			this.Attributes = new XElementData(data);
			this.Init();
		}

		public XmlDocument XmlInfo(ReleaseSide side = default)
		{
			var doc = new XmlDocument();

			XmlElement table = doc.CreateElement("table");
			doc.AppendChild(table);
			table.SetAttribute("release-module", "");
			table.SetAttribute("release-side", side.ToString().ToLower());
			table.SetAttribute("type", "");
			table.SetAttribute("version", $"");
			table.AppendChild(BaseRecord.Serialize(this, doc));

			return doc;
		}


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
							else Trace.WriteLine($"[Serialize Failed] { field.Name } ,{ obj.GetType() }");
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

						else if (EnumValue.MyEquals("none")) continue;
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

	public abstract class TypeBaseRecord<SubType> : BaseRecord where SubType : Enum
	{
		public SubType Type;
	}
}