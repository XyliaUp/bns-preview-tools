using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

using Newtonsoft.Json;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Database;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;

[JsonConverter(typeof(RecordConverter))]
public unsafe class Record : IDisposable
{
	#region Ctor
	public Record()
	{
		Attributes = new(this);
	}
	#endregion


	#region Fields
	[IgnoreDataMember]
	public byte XmlNodeType
	{
		get
		{
			fixed (byte* ptr = Data) return ptr[0];
		}
		set
		{
			fixed (byte* ptr = Data) ptr[0] = value;
		}
	}

	[IgnoreDataMember]
	public short SubclassType
	{
		get
		{
			fixed (byte* ptr = Data) return ((short*)(ptr + 2))[0];
		}
		set
		{
			fixed (byte* ptr = Data) ((short*)(ptr + 2))[0] = value;
		}
	}

	[IgnoreDataMember]
	public ushort DataSize
	{
		get
		{
			fixed (byte* ptr = Data) return ((ushort*)(ptr + 4))[0];
		}
		set
		{
			fixed (byte* ptr = Data) ((ushort*)(ptr + 4))[0] = value;
		}
	}

	[IgnoreDataMember]
	public int RecordId
	{
		get
		{
			fixed (byte* ptr = Data) return ((int*)(ptr + 8))[0];
		}
		set
		{
			fixed (byte* ptr = Data) ((int*)(ptr + 8))[0] = value;
		}
	}

	[IgnoreDataMember]
	public int RecordVariationId
	{
		get
		{
			fixed (byte* ptr = Data) return ((int*)(ptr + 12))[0];
		}
		set
		{
			fixed (byte* ptr = Data) ((int*)(ptr + 12))[0] = value;
		}
	}

	[IgnoreDataMember]
	public byte[] Data { get; set; }

	[IgnoreDataMember]
	public StringLookup StringLookup { get; set; }

	[IgnoreDataMember]
	public Table Owner { get; internal set; }

	[IgnoreDataMember]
	public Ref Ref => Data is null ? default : new(RecordId, RecordVariationId);

	[IgnoreDataMember]
	public ITableDefinition ElDefinition
	{
		get
		{
			var def = Owner?.Definition.ElRecord?.SubtableByType(SubclassType);
			if (def != null) this.CheckSize(def);

			return def;
		}
	}

	[IgnoreDataMember]
	public AttributeCollection Attributes { get; internal set; }

	[IgnoreDataMember]
	internal Dictionary<string, Record[]> Children { get; set; } = new();
	#endregion

	#region Serialize
	/// <summary>
	/// Convert XML text to record
	/// </summary>
	/// <remarks>This method is only used at convert fields</remarks>
	/// <param name="xml"></param>
	/// <returns></returns>
	public static Record Parse(string xml)
	{
		try
		{
			var record = new Record();
			record.Attributes = new AttributeCollection(record, XElement.Parse(xml));

			return record;
		}
		catch
		{
			return default;
		}
	}

	public void WriteXml(XmlWriter writer, ElDefinition el)
	{
		writer.WriteStartElement(el.Name);

		// attribute
		if (SubclassType > -1)
		{
			writer.WriteAttributeString("type", SubclassType < el.Subtables.Count ? el.Subtables[SubclassType].Name : SubclassType.ToString());
		}

		foreach (var attribute in Attributes.OrderBy(o => o.Key))
		{
			// avoid duplicate (only cause when from xml)
			if (SubclassType > -1 && attribute.Key == "type") continue;
			if (attribute.Key == "auto-id") continue;

			// check default
			var attributeDef = el[attribute.Key];

			var value = attribute.Value;
			if (value is bool bol)
			{
				if (attributeDef is null && !bol)
					continue;

				value = bol ? "y" : "n";
			}
			else if (value is float f)
			{
				//if (attributeDef != null && Math.Abs(f - attributeDef.AttributeDefaultValues.DString) < 0.001)
				//	continue;

				value = f.ToString(CultureInfo.InvariantCulture);
			}

			// set value
			if (value?.ToString() != attributeDef?.DefaultValue)
				writer.WriteAttributeString(attribute.Key, value?.ToString());
		}

		// children
		foreach (var el_child in el.Children)
		{
			if (this.Children.TryGetValue(el_child.Name, out var childs))
				childs.ForEach(child => child.WriteXml(writer, el_child));
		}

		writer.WriteEndElement();
	}

	public void Serialize(RecordBuilder builder)
	{
		//Attributes.Synchronize();

		//// check definition
		//ArgumentNullException.ThrowIfNull(ElDefinition);

		//// create record
		//builder.InitializeRecord();

		//Data = new byte[ElDefinition.Size];
		//XmlNodeType = 1;
		//SubclassType = ElDefinition.SubclassType;
		//DataSize = ElDefinition.Size;
		//StringLookup = builder.StringLookup;

		//// Go through each attribute
		////AttributeDefaultValues.SetRecordDefaults(record, this);
		//foreach (var attr in ElDefinition.ExpandedAttributes)
		//{
		//	try
		//	{
		//		builder.SetAttribute(this, attr, Attributes[attr.Name]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Debug.WriteLine(ex.Message);
		//	}
		//}

		//builder.FinalizeRecord();
	}
	#endregion

	#region Interface
	public override string ToString() => this.Attributes["alias"] ?? Ref.ToString();

	public static bool operator ==(Record a, Record b)
	{
		// If both are null, or both are same instance, return true.
		if (ReferenceEquals(a, b)) return true;

		// If one is null, but not both, return false.
		if (a is null || b is null) return false;
		if (a.GetType() != b.GetType()) return false;

		// Return true if the fields match:
		return a.Ref == b.Ref;
	}

	public static bool operator !=(Record a, Record b) => !(a == b);

	public override bool Equals(object other) => other is Record record && this == record;

	public override int GetHashCode() => HashCode.Combine(GetType(), Ref);


	public void Dispose()
	{
		Model = null;

		Data = null;
		StringLookup = null;

		Attributes?.Dispose();
		Attributes = null;

		GC.SuppressFinalize(this);
	}
	#endregion


	#region Instance
	/// XXX: https://zhuanlan.zhihu.com/p/430728295
	[IgnoreDataMember]
	public Lazy<Record> Model { get; set; }
	#endregion
}