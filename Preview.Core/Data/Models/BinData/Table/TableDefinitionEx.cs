using System.Configuration;

using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table.Config;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Models.BinData.Table;
public static class TableDefinitionEx
{
	#region Load Methods
	public static TableDefinition LoadFrom(ConfigParam param, XmlElement tableNode)
	{
		#region config 
		if ((tableNode.Attributes["retired"]?.Value).ToBool())
			return null;

		var Type = tableNode.Attributes["type"]?.Value.ToInt16();
		var TypeName = tableNode.Attributes["name"]?.Value;
		if (Type is null && string.IsNullOrWhiteSpace(TypeName))
			throw new ConfigurationErrorsException("table must set `type` or `name` fields");

		(tableNode.Attributes["version"]?.Value).GetVersion(out var major, out var minor);
		#endregion


		#region els
		List<ElDefinition> els = new();
		foreach (XmlElement el in tableNode.SelectNodes("./el"))
		{
			#region head
			var name = el.Attributes["name"]?.Value;

			ElDefinition def = name == "record" ? new TableDefinition() : new ElDefinition();
			def.Name = name;

			var child = el.Attributes["child"]?.Value;
			if (child != null) def.Children.AddRange(child.Split(',').Select(ushort.Parse));
			#endregion

			#region body
			foreach (var attrDef in LoadAttr(el.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "attribute")))
			{
				if (attrDef == null)
					continue;

				def.Attributes.Add(attrDef);


				// Expand repeated attributes if needed
				if (attrDef.Repeat == 1)
				{
					def.ExpandedAttributes.Add(attrDef);
					continue;
				}

				for (var i = 1; i <= attrDef.Repeat; i++)
				{
					var newAttrDef = attrDef.Clone();
					newAttrDef.Name += $"-{i}";
					newAttrDef.OriginalName = newAttrDef.Name;
					newAttrDef.Repeat = 1;
					def.ExpandedAttributes.Add(newAttrDef);
				}
			}

			def.Size = GetOffsetAndSize(def.ExpandedAttributes, is64: true);


			short subIndex = 0;
			foreach (var sub in el.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "sub"))
			{
				var subtable = new SubtableDefinition();
				def.Subtables.Add(subtable);


				subtable.Name = sub.Attributes["name"].Value;
				subtable.SubclassType = subIndex++;

				// Add parent expanded attributes
				subtable.ExpandedAttributes.AddRange(def.ExpandedAttributes);

				foreach (var attrDef in LoadAttr(sub.ChildNodes.OfType<XmlElement>()))
				{
					// HACK: Handle case when there's name conflict in subtable
					if (def.Attributes.Any(x => x.Name == attrDef.Name))
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

				subtable.Size = GetOffsetAndSize(subtable.ExpandedAttributesSubOnly, is64: true, def.Size);
				subtable._attributesDictionary = subtable.Attributes.ToDictionary(x => x.Name);
				subtable._expandedAttributesDictionary = subtable.ExpandedAttributes.ToDictionary(x => x.Name);
			}

			def._attributesDictionary = def.Attributes.ToDictionary(x => x.Name);
			def._expandedAttributesDictionary = def.ExpandedAttributes.ToDictionary(x => x.Name);
			def._subtablesDictionary = def.Subtables.ToDictionary(x => x.Name);

			def.IsEmpty = def.Attributes.Count == 0 && (def.Subtables.Count == 0 || def.Subtables.All(x => x.Attributes.Count == 0));

			List<AttributeDef> LoadAttr(IEnumerable<XmlElement> els)
			{
				var Attributes = new List<AttributeDef>();
				foreach (XmlElement node in els)
				{
					try
					{
						string name = node.Attributes["alias"]?.Value;

						var record = AttributeDef.LoadFrom(node, def, () => SeqInfo.LoadFrom(node, name, param?.PublicSeq));
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
			#endregion

			els.Add(def);
		}
		#endregion

		#region table
		var table = (els.FirstOrDefault(el => el.Name == "record") as TableDefinition) ?? new TableDefinition { IsEmpty = true };
		table.Type = Type ?? 0;
		table.Name = TypeName;
		table.OriginalName = TypeName;
		table.MajorVersion = major;
		table.MinorVersion = minor;
		table.AutoKey = (tableNode.Attributes["autokey"]?.Value).ToBool();
		table.MaxId = (tableNode.Attributes["maxid"]?.Value).ToInt32();
		table.Els.AddRange(els);

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

			table.Attributes.Insert(0, autoIdAttr);
			table.ExpandedAttributes.Insert(0, autoIdAttr);
		}

		return table;
		#endregion
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




	#region Check Methods
	/// <summary>
	/// compare config version with game real version
	/// </summary>
	public static void CheckVersion(TableDefinition def, TableModel table)
	{
		if (table.MajorVersion == def.MajorVersion && table.MinorVersion == def.MinorVersion)
			return;

		Debug.WriteLine($"[{DateTime.Now}] check table `{def.Name}` version: {def.MajorVersion}.{def.MinorVersion} <> {table.MajorVersion}.{table.MinorVersion}");
	}

	public static void CheckSize(TableDefinition def, TableModel table, Action<string> message = null)
	{
		foreach (var type in table.Records.GroupBy(o => o.SubclassType).OrderBy(o => o.Key))
		{
			var tableDef = GetSubDef(def, type.Key);

			var _size = tableDef.Size;
			var size = type.First().DataSize;
			if (size == _size) continue;


			var block = (size - _size) / 4;
			if (block > 0)
			{
				tableDef.Size = size;
				for (int i = 0; i < block; i++)
				{
					var offset = (ushort)(_size + i * 4);
					tableDef.ExpandedAttributes.Add(new AttributeDefinition()
					{
						Name = "unk" + offset,
						Size = 4,
						Offset = offset,
						Type = AttributeType.TInt32,
						AttributeDefaultValues = new AttributeDefaultValues(),
						DefaultValue = "0",
						IsHidden = true,
						Repeat = 1
					});
				}
			}


			message?.Invoke($"[{DateTime.Now}] check field size, \t" +
				$"table: {def.Name} " +
				$"{(type.Key == -1 ? string.Empty : "type: " + tableDef.Name)} " +
				$"size: {size} <> {_size} " +
				$"block:{block}");
		}
	}



	public static ITableDefinition GetSubDef(TableDefinition def, short SubclassType)
	{
		if (SubclassType == -1) return def;
		else if (def.Subtables.Count > SubclassType)
			return def.Subtables[SubclassType];

		return def;
	}
	#endregion
}