using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

using Newtonsoft.Json;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Database;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Models;

[JsonConverter(typeof(RecordConverter))]
public unsafe class Record : IDisposable
{
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

	public int SizeWithLookup
	{
		get
		{
			var size = Data.Length;

			if (StringLookup?.Data != null)
				size += StringLookup.Data.Length;

			return size;
		}
	}

	public Table Owner { get; internal set; }

	public Ref Ref => new(RecordId, RecordVariationId);

	public ITableDefinition ElDefinition => Owner.Definition.ElRecord?.SubtableByType(SubclassType);

	public AttributeCollection Attributes { get; internal set; }

	internal Dictionary<string, Record[]> Children { get; set; } = new();
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

	#region Serialize
	public void WriteXml(XmlWriter writer, ElDefinition el)
	{
		Attributes.Synchronize();

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
				if (attributeDef != null && Math.Abs(f - attributeDef.AttributeDefaultValues.DFloat) < 0.001)
					continue;

				value = f.ToString(CultureInfo.InvariantCulture);
			}

			// set value
			if (value?.ToString() != attributeDef?.DefaultValue)
				writer.WriteAttributeString(attribute.Key, value?.ToString());
		}

		// children
		foreach (var child in el.Children)
		{
			var field = this.GetInfo(child.Name, true);
			if (field is null || !field.GetMemberType().IsList()) continue;

			var value = field.GetValue(this);
			if (value is null) continue;

			foreach (var element in (IEnumerable)value)
			{
				if (element is Record record)
					record.WriteXml(writer, child);
			}
		}

		writer.WriteEndElement();
	}

	public void Serialize(RecordBuilder builder)
	{
		Attributes.Synchronize();

		// check definition
		ArgumentNullException.ThrowIfNull(ElDefinition);

		// create record
		builder.InitializeRecord();

		Data = new byte[ElDefinition.Size];
		XmlNodeType = 1;
		SubclassType = ElDefinition.SubclassType;
		DataSize = ElDefinition.Size;
		StringLookup = builder.StringLookup;

		// Go through each attribute
		//AttributeDefaultValues.SetRecordDefaults(record, this);
		foreach (var attr in ElDefinition.ExpandedAttributes)
		{
			try
			{
				builder.SetAttribute(this, attr, Attributes[attr.Name]);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		builder.FinalizeRecord();
	}
	#endregion


	#region Instance
	public Lazy<Record> Model { get; set; }
	#endregion
}