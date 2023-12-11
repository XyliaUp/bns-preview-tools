using System.Xml;

using Newtonsoft.Json;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;

[JsonConverter(typeof(RecordConverter))]
public unsafe class Record : IDisposable
{
	#region Ctor
	internal Record()
	{
		Attributes = new(this);
	}
	#endregion


	#region Fields
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

	public byte[] Data { get; set; }

	public StringLookup StringLookup { get; set; }

	public Table Owner { get; internal set; }

	public Ref Ref => Data is null ? default : new(RecordId, RecordVariationId);

	public ITableDefinition ElDefinition
	{
		get
		{
			var def = Owner.Definition.ElRecord.SubtableByType(SubclassType);
			if (def != null) this.CheckSize(def);

			return def;
		}
	}

	public AttributeCollection Attributes { get; internal set; }

	internal Dictionary<string, Record[]> Children { get; set; } = new();


	public bool HasChildren => Children.Count > 0;

	internal Lazy<ModelElement> Model { get; set; }
	#endregion

	#region Serialize
	public void WriteXml(XmlWriter writer, ElDefinition el)
	{
		writer.WriteStartElement(el.Name);

		// attribute
		if (SubclassType > -1)
		{
			writer.WriteAttributeString(AttributeCollection.s_type, SubclassType < el.Subtables.Count ? el.Subtables[SubclassType].Name : SubclassType.ToString());
		}

		foreach (var attribute in Attributes)
		{
			if (attribute.Key.Name == AttributeCollection.s_autoid) continue;

			// set value, it seem that WriteRaw must be last  
			var value = AttributeDefinition.ToString(attribute.Key, attribute.Value);
			if (value is null) continue;

			if (attribute.Key.Type == AttributeType.TNative) writer.WriteRaw(value);
			else writer.WriteAttributeString(attribute.Key.Name, value);
		}

		// children
		foreach (var el_child in el.Children)
		{
			if (this.Children.TryGetValue(el_child.Name, out var childs))
				childs.ForEach(child => child.WriteXml(writer, el_child));
		}

		writer.WriteEndElement();
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
}