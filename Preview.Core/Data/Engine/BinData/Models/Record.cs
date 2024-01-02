using System.Xml;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.Data.Models;
[JsonConverter(typeof(RecordConverter))]
public sealed unsafe class Record : IDisposable
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

	public byte[] Data { get; internal set; }

	public StringLookup StringLookup { get; set; }

	public Table Owner { get; internal set; }

	public ElementBaseDefinition Definition
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
	#endregion

	#region Serialize
	public void WriteXml(XmlWriter writer, ElementDefinition el)
	{
		writer.WriteStartElement(el.Name);

		// attribute
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
	public override string ToString() => this.Attributes.Get<string>("alias") ?? ((Ref)this).ToString();

	public T As<T>(Type type = null) where T : ModelElement
	{
		// NOTE: create new object
		var subs = ModelTypeHelper.Get(type ?? typeof(T), this.Owner.Name);
		var subtype = this.Attributes[AttributeCollection.s_type]?.ToString();

		return ModelElement.As(this, subs.CreateInstance<T>(subtype));
	}

	public void Dispose()
	{
		Data = null;
		StringLookup = null;
		Attributes = null;

		GC.SuppressFinalize(this);
	}
	#endregion
}