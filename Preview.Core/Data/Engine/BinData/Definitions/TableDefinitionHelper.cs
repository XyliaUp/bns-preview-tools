using System.Diagnostics;
using System.Reflection;
using System.Xml;

using CUE4Parse.Utils;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Exceptions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public static class TableDefinitionHelper
{
	public static string GetResource(this string name)
	{
		var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		if (stream is null) return null;

		return new StreamReader(stream).ReadToEnd();
	}

	#region Load Methods
	/// <summary>
	/// load <see cref="TableDefinition"/> from program
	/// </summary>
	public static List<TableDefinition> LoadTableDefinition()
	{
		var _assembly = Assembly.GetExecutingAssembly();

		//public
		var param = ConfigParam.LoadFrom(_assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Definition.Global"))
			.Select(name => new StreamReader(_assembly.GetManifestResourceStream(name)).ReadToEnd())
			.ToArray());

		//result
		var defs = _assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Definition.") && !name.Contains(".Global"))
			.Select(name => name.GetResource())
			.SelectMany(res => LoadTableDefinition(param, res))     //load config
			.ToList();


		// replace
		var UserDefs = new DirectoryInfo(Path.Combine(Settings.Default.OutputFolder, "defs"));
		if (Settings.Default.UseUserDefinition && UserDefs.Exists)
		{
			var temp = LoadTableDefinition(param, UserDefs.GetFiles("*.xml"));
			if (temp.Any())
			{
				Trace.WriteLine("ATTENTION: activated user custome definitions");
				defs.AddRange(temp);
			}
		}


		return defs.DistinctBy(def => def.Name, new TableNameComparer()).ToList();
	}

	/// <summary>
	/// load <see cref="TableDefinition"/> from files
	/// </summary>
	public static List<TableDefinition> LoadTableDefinition(ConfigParam param, params FileInfo[] files) =>
		LoadTableDefinition(param, files.Select(f => File.ReadAllText(f.FullName)).ToArray());

	public static List<TableDefinition> LoadTableDefinition(ConfigParam param, params string[] XmlContents)
	{
		var tables = new List<TableDefinition>();
		foreach (var Content in XmlContents)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(Content);

			var table = TableDefinitionHelper.LoadFrom(param, xmlDoc.DocumentElement);
			if (table is null) continue;

			tables.Add(table);
		}

		return tables;
	}


	public static TableDefinition LoadFrom(ConfigParam param, XmlElement tableNode)
	{
		#region config 
		if ((tableNode.Attributes["retired"]?.Value).ToBool())
			return null;

		var type = (tableNode.Attributes["type"]?.Value).ToInt16();
		var version = tableNode.Attributes["version"]?.Value?.Split('.');
		var major = (ushort)version.ElementAtOrDefault(0).ToInt16();
		var minor = (ushort)version.ElementAtOrDefault(1).ToInt16();
		#endregion


		#region els
		List<ElDefinition> els = new();
		foreach (var el in tableNode.SelectNodes("./el").OfType<XmlElement>())
		{
			var def = new ElDefinition();
			def.Name = el.Attributes["name"]?.Value;

			#region body
			foreach (var attrDef in LoadAttr(el.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "attribute"), param, def))
			{
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
					newAttrDef.Repeat = 1;
					def.ExpandedAttributes.Add(newAttrDef);
				}
			}
			def.Size = GetOffsetAndSize(def.ExpandedAttributes, true);
			def.CreateExpandedAttributeMap();


			short subIndex = 0;
			foreach (var sub in el.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "sub"))
			{
				var subtable = new SubtableDefinition();
				def.Subtables.Add(subtable);

				subtable.Name = sub.Attributes["name"].Value;
				subtable.SubclassType = subIndex++;

				// Add parent expanded attributes
				subtable.ExpandedAttributes.AddRange(def.ExpandedAttributes);

				foreach (var attrDef in LoadAttr(sub.ChildNodes.OfType<XmlElement>(), param, def))
				{
					// HACK: Handle case when there's name conflict in subtable
					if (def.Attributes.Any(x => x.Name == attrDef.Name))
					{
						attrDef.Name += "-rep";
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
						newAttrDef.Repeat = 1;
						subtable.ExpandedAttributes.Add(newAttrDef);
						subtable.ExpandedAttributesSubOnly.Add(newAttrDef);
					}
				}

				subtable.Size = GetOffsetAndSize(subtable.ExpandedAttributesSubOnly, true, def.Size);
				subtable.CreateExpandedAttributeMap();
			}
			def.CreateSubtableMap();
			#endregion

			els.Add(def);
		}

		foreach (var el in els)
		{
			var source = tableNode.SelectSingleNode($"./el[@name='{el.Name}']");
			if (source.Attributes["child"] is null) continue;

			foreach (var child in source.Attributes["child"].Value
				.Split(',').Select(o => o.Trim()))
			{
				ElDefinition child_el = ushort.TryParse(child, out var index) ?
					els.ElementAtOrDefault(index) :
					els.FirstOrDefault(el => el.Name == child);

				if (child_el != null)
					el.Children.Add(child_el);
			}
		}
		#endregion

		#region table
		var table = new TableDefinition();
		table.Els.AddRange(els);
		table.Name = tableNode.Attributes["name"]?.Value;
		//table.Module = tableNode.Attributes["module"]?.Value;


		table.ElRecord = table.Els.FirstOrDefault().Children.FirstOrDefault();
		table.Type = type;
		table.MajorVersion = major;
		table.MinorVersion = minor;
		table.ElRecord.AutoKey = (tableNode.Attributes["autokey"]?.Value).ToBool();
		table.ElRecord.MaxId = (tableNode.Attributes["maxid"]?.Value).ToInt32();

		if (type == 0 && string.IsNullOrWhiteSpace(table.Name))
			throw new BnsDefinitionException("table must set `type` or `name` fields");

		// Add auto key id
		if (table.ElRecord.AutoKey)
		{
			var autoIdAttr = new AttributeDefinition
			{
				Name = "auto-id",
				Size = 8,
				Offset = 8,
				Type = AttributeType.TInt64,
				AttributeDefaultValues = new AttributeDefaultValues(),
				DefaultValue = "0",
				IsKey = true,
				IsRequired = true,
				Repeat = 1
			};

			table.ElRecord.Attributes.Insert(0, autoIdAttr);
			table.ElRecord.ExpandedAttributes.Insert(0, autoIdAttr);
			table.ElRecord.CreateExpandedAttributeMap();
		}

		return table;
		#endregion
	}

	private static List<AttributeDefinition> LoadAttr(IEnumerable<XmlElement> els, ConfigParam param, ElDefinition def)
	{
		var Attributes = new List<AttributeDefinition>();
		foreach (XmlElement node in els)
		{
			try
			{
				string name = node.Attributes["alias"]?.Value;

				var record = AttributeDefinition.LoadFrom(node, def, () => SequenceDefinition.LoadFrom(node, name, param?.PublicSeq));
				if (record is null) continue;

				Attributes.Add(record);
			}
			catch (Exception ee)
			{
				throw new BnsDefinitionException($"attribute load failed: {node.OuterXml}", ee);
			}
		}

		return Attributes;
	}


	private static ushort GetOffsetAndSize(IEnumerable<AttributeDefinition> Attributes, bool is64, int Offset = 16)
	{
		int Offset_Key = 8;
		foreach (var attribute in Attributes)
		{
			if (attribute.IsDeprecated ||
				attribute is AttributeDefinition def && !def.Client)
				continue;


			#region set offset
			int offset = 0;
			if (attribute.Offset != 0) offset = attribute.Offset;
			else if (attribute.IsKey) offset = Offset_Key;
			else offset = Offset;

			// auto align
			var size = AttributeDefinition.GetSize(attribute.Type, is64);
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
	public static void CheckVersion(this Table table, TableDefinition definition)
	{
		if (table.Type == 0 || definition is null) return;
		if (table.MajorVersion == definition.MajorVersion &&
			table.MinorVersion == definition.MinorVersion) return;

		Console.WriteLine($"[{DateTime.Now}] check table `{definition.Name}` type: {table.Type} " +
			$"version: {definition.MajorVersion}.{definition.MinorVersion} <> {table.MajorVersion}.{table.MinorVersion}");
	}

	public static void CheckSize(this Table table)
	{
		foreach (var type in table.Records.GroupBy(o => o.SubclassType).OrderBy(o => o.Key))
		{
			var def = table.Definition.ElRecord.SubtableByType(type.Key);
			CheckSize(type.First(), def);
		}
	}

	public static void CheckSize(this Record record, ITableDefinition definition)
	{
		var size = record.DataSize;
		if (size == 0 || size == definition.Size) return;

		// get block
		lock (definition)
		{
			var block = (size - definition.Size) / 4;
			if (block == 0) return;

			Console.WriteLine($"[{DateTime.Now}] check field size, " +
				$"table: {record.Owner.Name} " +
				$"type: {(record.SubclassType == -1 ? "null" : definition.Name)} " +
				$"size: {definition.Size} <> {size} block: {block}");

			if (block > 0)
			{
				// create unknown attribute
				for (int i = 0; i < block; i++)
				{
					var offset = (ushort)(definition.Size + i * 4);
					definition.ExpandedAttributes.Add(new AttributeDefinition()
					{
						Name = "unk" + offset,
						Size = 4,
						Offset = offset,
						Type = AttributeType.TInt32,
						DefaultValue = "0",
						IsHidden = true,
						Repeat = 1
					});
				}

				definition.Size = size;
				definition.CreateExpandedAttributeMap();
			}
			else
			{
				// prevent duplicate message
				definition.Size = size;
			}
		}
	}
	#endregion
}

public sealed class ConfigParam
{
	public readonly Dictionary<string, SequenceDefinition> PublicSeq = new(StringComparer.OrdinalIgnoreCase);

	public static ConfigParam LoadFrom(params string[] XmlContents)
	{
		var param = new ConfigParam();
		foreach (var content in XmlContents)
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(content);

			foreach (XmlElement record in xmlDoc.SelectNodes("table/record"))
			{
				string name = record.Attributes["name"]?.Value?.Trim();
				if (param.PublicSeq.ContainsKey(name))
					throw new BnsDefinitionException($"seq `{name}` has existed");

				var seq = SequenceDefinition.LoadFrom(record, name);
				if (seq != null) param.PublicSeq[name] = seq;
			}
		}

		return param;
	}
}