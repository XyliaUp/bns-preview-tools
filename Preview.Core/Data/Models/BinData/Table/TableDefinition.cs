using System.Configuration;
using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Config;

namespace Xylia.Preview.Data.Models.BinData.Table;
public sealed class DataTableDefinition : TableDefinition
{
	#region Load Methods
	public static DataTableDefinition LoadFrom(ConfigParam param, XmlElement tableNode)
	{
		#region config 
		if ((tableNode.Attributes["retired"]?.Value).ToBool())
			return null;

		var Type = tableNode.Attributes["type"]?.Value.ToShort();
		var TypeName = tableNode.Attributes["name"]?.Value;
		if (Type is null && string.IsNullOrWhiteSpace(TypeName))
			throw new ConfigurationErrorsException("table must set `type` or `name` fields");

		(tableNode.Attributes["version"]?.Value).GetVersion(out var major, out var minor);


		var table = new DataTableDefinition
		{
			Type = Type ?? 0,
			Name = TypeName,
			OriginalName = TypeName,
			MajorVersion = major,
			MinorVersion = minor,

			AutoKey = (tableNode.Attributes["autokey"]?.Value).ToBool(),
			MaxId = (tableNode.Attributes["maxid"]?.Value).ToInt(),
		};
		#endregion

		var els = tableNode.SelectNodes("./el");
		foreach (XmlElement el in els)
		{
			ElDefinition elDef = new ElDefinition() { Name = el.Attributes["name"]?.Value };

			var child = el.Attributes["child"]?.Value;
			if (child != null) elDef.Children.AddRange(child.Split(',').Select(ushort.Parse).ToList());


			table.Els.Add(elDef);
		}

		LoadMain(table, els[1].ChildNodes.OfType<XmlElement>(), param);
		

		return table;
	}

	public static void LoadMain(DataTableDefinition table, IEnumerable<XmlElement> XmlElements, ConfigParam ConfigInfo = null)
	{
		// Add auto key id
		if (table.AutoKey)
		{
			var autoIdAttr = new AttributeDefinition
			{
				Name = "auto-id",
				OriginalName = "auto-id",
				Size = 8,
				Offset = 8,
				Type = AttributeType.TInt64,
				OriginalTypeName = "int64",
				TypeName = "int64",
				AttributeDefaultValues = new AttributeDefaultValues(),
				DefaultValue = "0",
				IsKey = true,
				IsRequired = true,
				Repeat = 1
			};

			table.Attributes.Add(autoIdAttr);
			table.ExpandedAttributes.Add(autoIdAttr);
		}

		foreach (var attrDef in LoadAttr(XmlElements.Where(el => el.Name == "record")))
		{
			if (attrDef == null)
				continue;

			table.Attributes.Add(attrDef);


			// Expand repeated attributes if needed
			if (attrDef.Repeat == 1)
			{
				table.ExpandedAttributes.Add(attrDef);
				continue;
			}

			for (var i = 1; i <= attrDef.Repeat; i++)
			{
				var newAttrDef = attrDef.Clone();
				newAttrDef.Name += $"-{i}";
				newAttrDef.OriginalName = newAttrDef.Name;
				newAttrDef.Repeat = 1;
				table.ExpandedAttributes.Add(newAttrDef);
			}
		}

		table.Size = GetOffsetAndSize(table.ExpandedAttributes, is64: true);


		short subIndex = 0;
		foreach (var sub in XmlElements.Where(el => el.Name == "sub"))
		{
			var subtable = new SubtableDefinition();
			subtable.Name = sub.Attributes["name"].Value;
			subtable.SubclassType = subIndex++;

			// Add parent expanded attributes
			subtable.ExpandedAttributes.AddRange(table.ExpandedAttributes);

			foreach (var attrDef in LoadAttr(sub.ChildNodes.OfType<XmlElement>()))
			{
				// HACK: Handle case when there's name conflict in subtable
				if (table.Attributes.Any(x => x.Name == attrDef.Name))
				{
					attrDef.Name += "-rep";
					attrDef.OriginalName = attrDef.Name;
				}

				subtable.Attributes.Add(attrDef);

				// Expand repeated attributes if needed
				if (attrDef.Repeat == 1)
				{
					subtable.ExpandedAttributes.Add(attrDef);
					subtable.ExpandedAttributesSubOnly.Add(attrDef);
					continue;
				}

				for (var i = 1; i <= attrDef.Repeat; i++)
				{
					var newAttrDef = attrDef.Clone();
					newAttrDef.Name += $"-{i}";
					newAttrDef.OriginalName = newAttrDef.Name;
					newAttrDef.Repeat = 1;
					subtable.ExpandedAttributes.Add(newAttrDef);
					subtable.ExpandedAttributesSubOnly.Add(newAttrDef);
				}
			}

			subtable.Size = GetOffsetAndSize(subtable.ExpandedAttributesSubOnly, is64: true, table.Size);

			subtable._attributesDictionary = subtable.Attributes.ToDictionary(x => x.Name);
			subtable._expandedAttributesDictionary = subtable.ExpandedAttributes.ToDictionary(x => x.Name);


			if (subtable != null) table.Subtables.Add(subtable);
		}


		table._attributesDictionary = table.Attributes.ToDictionary(x => x.Name);
		table._expandedAttributesDictionary = table.ExpandedAttributes.ToDictionary(x => x.Name);
		table._subtablesDictionary = table.Subtables.ToDictionary(x => x.Name);

		table.IsEmpty = table.Attributes.Count == 0 && (table.Subtables.Count == 0 || table.Subtables.All(x => x.Attributes.Count == 0));



		List<AttributeDef> LoadAttr(IEnumerable<XmlElement> els)
		{
			var Attributes = new List<AttributeDef>();
			foreach (XmlElement node in els)
			{
				try
				{
					string name = node.Attributes["alias"]?.Value;


					var record = AttributeDef.LoadFrom(node, table, () => SeqInfo.LoadFrom(node, name, ConfigInfo?.PublicSeq));
					if (record is null) continue;

					Attributes.Add(record);
				}
				catch (Exception ee)
				{
					throw new ConfigurationErrorsException($"attribute load failed: {node.OuterXml}", ee);
				}
			}

			return Attributes;
		}
	}


	private static ushort GetOffsetAndSize(IEnumerable<AttributeDefinition> Attributes, bool is64, int Offset = 16)
	{
		int Offset_Key = 8;
		foreach (var attribute in Attributes)
		{
			if (attribute is AttributeDef def && !def.Client)
				continue;


			#region set offset
			int offset = 0;
			if (attribute.Offset != 0) offset = attribute.Offset;
			else if (attribute.IsKey) offset = Offset_Key;
			else offset = Offset;

			// auto align
			var size = AttributeDef.GetSize(attribute.Type, is64);
			if (size == 2) offset = offset.Align(2);
			else if (size != 1) offset = offset.Align(4);

			attribute.Offset = (ushort)offset;
			attribute.Size = size;

			// create new alias
			if (attribute.Name.Equals("unk-")) attribute.Name = "unk" + attribute.Offset;
			#endregion


			#region next start offset
			offset += attribute.Size;

			if (attribute.IsKey) Offset_Key = offset;
			else Offset = offset;
			#endregion
		}

		return (ushort)Offset.Align(4);
	}
	#endregion
}