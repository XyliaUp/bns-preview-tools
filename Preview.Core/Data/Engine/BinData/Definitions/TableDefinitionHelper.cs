using System.Xml;
using CUE4Parse.Utils;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Exceptions;
using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Properties;

namespace Xylia.Preview.Data.Engine.Definitions;
public static class TableDefinitionHelper
{
	public static string GetResource(this string name)
	{
		var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		if (stream is null) return null;

		return new StreamReader(stream).ReadToEnd();
	}

	#region Load Methods
	internal static DatafileDefinition LoadDefinition()
	{
		#region load from program
		var _assembly = System.Reflection.Assembly.GetExecutingAssembly();

		// public
		var param = ConfigParam.LoadFrom(_assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Definition.Sequence"))
			.Select(name => new StreamReader(_assembly.GetManifestResourceStream(name)).ReadToEnd()).ToArray());

		// result
		var defs = _assembly.GetManifestResourceNames()
			.Where(name => name.StartsWith("Xylia.Preview.Data.Definition.") && !name.Contains(".Sequence"))
			.Select(name => name.GetResource()).SelectMany(res => LoadTableDefinition(param, res));
		#endregion

		#region custom
		var definition = new DatafileDefinition();
		var UserDefs = new DirectoryInfo(Path.Combine(Settings.Default.OutputFolder, "definition"));
		if (Settings.Default.UseUserDefinition && UserDefs.Exists)
		{
			definition.Header = UserDefs.GetFiles("definition.ini").FirstOrDefault();

			var temp = LoadTableDefinition(param, UserDefs.GetFiles("*.xml"));
			if (temp.Count != 0) temp.ForEach(definition.Add);
		}

		// HACK: full defs?
		if (definition.Count < 200) defs.ForEach(definition.Add);
		#endregion

		return definition;
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
		var type = (ushort)(tableNode.Attributes["type"]?.Value).ToInt16();
		var name = tableNode.Attributes["name"]?.Value;
		if (type == 0 && string.IsNullOrWhiteSpace(name))
			throw BnsDataException.InvalidDefinition("table must set `type` or `name` fields");

		var autokey = (tableNode.Attributes["autokey"]?.Value).ToBool();
		var maxid = (tableNode.Attributes["maxid"]?.Value).ToInt32();
		var version = TableHeader.ParseVersion(tableNode.GetAttribute("version"));
		var module = (TableModule)(tableNode.Attributes["module"]?.Value).ToInt32();
		#endregion


		#region els
		List<ElementDefinition> els = [];
		foreach (var source in tableNode.SelectNodes("./el").OfType<XmlElement>())
		{
			var el = new ElementDefinition { Name = source.GetAttribute("name") };
			els.Add(el);

			// HACK: is record element
			if (els.Count == 2)
			{
				el.AutoKey = autokey;
				el.MaxId = maxid;
			}
		}

		foreach (var el in els)
		{
			var source = tableNode.SelectSingleNode($"./el[@name='{el.Name}']");
			var Inherit = (source.Attributes["inherit"]?.Value).ToBool();
			if (Inherit)
			{
				// TODO
				continue;
			}


			#region body
			foreach (var attrDef in LoadAttribute(source.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "attribute"), param, el))
			{
				el.Attributes.Add(attrDef);

				// Expand repeated attributes if needed
				if (attrDef.Repeat == 1)
				{
					el.ExpandedAttributes.Add(attrDef);
					continue;
				}

				for (var i = 1; i <= attrDef.Repeat; i++)
				{
					var newAttrDef = attrDef.Clone();
					newAttrDef.Name += $"-{i}";
					newAttrDef.Repeat = 1;
					el.ExpandedAttributes.Add(newAttrDef);
				}
			}

			// Add auto key id
			if (el.AutoKey)
			{
				var autoIdAttr = new AttributeDefinition
				{
					Name = AttributeCollection.s_autoid,
					Type = AttributeType.TInt64,
					IsKey = true,
					IsHidden = true,
					Offset = 8,
					Repeat = 1,

					CanInput = false,
				};

				el.Attributes.Insert(0, autoIdAttr);
				el.ExpandedAttributes.Insert(0, autoIdAttr);
			}

			// Add type key
			var subs = source.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "sub");
			if (subs.Any())
			{
				var typeAttr = new AttributeDefinition
				{
					Name = AttributeCollection.s_type,
					Type = AttributeType.TSub,
					Offset = 2,
					Repeat = 1,
					ReferedTableName = name,
					ReferedElement = el.Name,
				};

				el.Attributes.Insert(0, typeAttr);
				el.ExpandedAttributes.Insert(0, typeAttr);
			}

			el.Size = GetOffsetAndSize(el.ExpandedAttributes, true);
			el.CreateAttributeMap();
			#endregion

			#region sub
			short subIndex = 0;
			foreach (var sub in subs)
			{
				var subtable = new ElementSubDefinition();
				el.Subtables.Add(subtable);

				subtable.Name = sub.Attributes["name"].Value;
				subtable.SubclassType = subIndex++;

				// Add parent expanded attributes
				subtable.Attributes.AddRange(el.Attributes);
				subtable.ExpandedAttributes.AddRange(el.ExpandedAttributes);
				subtable.Children.AddRange(el.Children);

				foreach (var attrDef in LoadAttribute(sub.ChildNodes.OfType<XmlElement>(), param, el))
				{
					// HACK: Handle case when there's name conflict in subtable
					if (el.Attributes.Any(x => x.Name == attrDef.Name))
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

				subtable.Size = GetOffsetAndSize(subtable.ExpandedAttributesSubOnly, true, el.Size);
				subtable.CreateAttributeMap();
			}

			el.CreateSubtableMap();
			#endregion


			var children = source.Attributes["child"]?.Value.Split(',').Select(o => o.Trim());
			if (children != null)
			{
				foreach (var child in children)
				{
					ElementDefinition child_el = ushort.TryParse(child, out var index) ?
						els.ElementAtOrDefault(index) :
						els.FirstOrDefault(el => el.Name == child);

					if (child_el != null)
						el.Children.Add(child_el);
				}
			}
		}
		#endregion



		#region table
		return new TableDefinition
		{
			Name = name,
			Module = module,
			Type = type,
			MajorVersion = version.Item1,
			MinorVersion = version.Item2,

			Els = els,
			ElRecord = els.FirstOrDefault().Children.FirstOrDefault(),
		};
		#endregion
	}

	private static List<AttributeDefinition> LoadAttribute(IEnumerable<XmlElement> els, ConfigParam param, ElementDefinition def)
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
			catch (Exception ex)
			{
				throw BnsDataException.InvalidDefinition($"attribute load failed: {node.OuterXml}\n{ex.Message}");
			}
		}

		return Attributes;
	}

	private static ushort GetOffsetAndSize(IEnumerable<AttributeDefinition> Attributes, bool is64, int Offset = 16)
	{
		int Offset_Key = 8;
		foreach (var attribute in Attributes.OrderBy(x => !x.IsKey))
		{
			if (attribute.IsDeprecated || !attribute.Side.HasFlag(ReleaseSide.Client))
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

			#region next offset
			offset += attribute.Size;

			if (attribute.IsKey)
			{
				Offset_Key = offset;
				Offset = Math.Max(Offset, offset);
			}
			else Offset = Math.Max(Offset, offset);
			#endregion
		}

		return (ushort)Offset.Align(4);
	}
	#endregion


	#region Check Methods
	public static void CheckSize(this Table table)
	{
		foreach (var type in table.Records.GroupBy(o => o.SubclassType).OrderBy(o => o.Key))
		{
			var def = table.Definition.ElRecord.SubtableByType(type.Key);
			CheckSize(type.First(), def);
		}
	}

	public static void CheckSize(this Record record, ElementBaseDefinition definition)
	{
		var size = record.DataSize;
		if (size == 0 || size == definition.Size) return;

		// get block
		lock (definition)
		{
			var block = (size - definition.Size) / 4;
			if (block == 0) return;

			// skip default definition
			if (size > 8)
			{
				Console.WriteLine($"[{DateTime.Now}] check field size, " +
					 $"table: {record.Owner.Name} " +
					 $"type: {(record.SubclassType == -1 ? "null" : definition.Name)} " +
					 $"size: {definition.Size} <> {size} block: {block}");
			}

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
						Repeat = 1
					});
				}

				definition.Size = size;
				definition.CreateAttributeMap();
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
					throw BnsDataException.InvalidSequence($"has existed", name);

				var seq = SequenceDefinition.LoadFrom(record, name);
				if (seq != null) param.PublicSeq[name] = seq;
			}
		}

		return param;
	}
}