using System.Collections.Generic;
using System.Linq;

using BnsBinTool.Core.Definitions;

using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;

namespace Xylia.Preview.Data.Definition;

public class MyTableDefinition : TableDefinition
{
	public string TypeName;

	private Dictionary<string, AttributeDefinition> _attributesDictionary =
		new Dictionary<string, AttributeDefinition>();

	public new AttributeDefinition this[string name] => _attributesDictionary.GetValueOrDefault(name, null);

	public static MyTableDefinition LoadFrom(TableInfo TableDef)
	{
		var table = new MyTableDefinition
		{
			Name = TableDef.TypeName,
			OriginalName = TableDef.TypeName,
			Type = TableDef.Type,
			TypeName = TableDef.TypeName,

			MajorVersion = TableDef.ConfigMajorVersion,
			MinorVersion = TableDef.ConfigMinorVersion,
			AutoKey = false,  //TableDef.AutoStart;
			MaxId = 0
		};


		// If we don't find record definition then we just return empty table
		if (TableDef.Records == null)
		{
			table.IsEmpty = true;
			return table;
		}

		var HasSubclass = TableDef.RecordTables.Count > 1;
		foreach (var records in TableDef.RecordTables)
		{
			ITableDefinition def = table;
			if (HasSubclass && records.Key != -1)
			{
				var subtable = new SubtableDefinition();
				def = subtable;

				subtable.Name = TableDef.TypeInfo.GetCell(records.Key)?.Alias ?? records.Key.ToString();
				subtable.SubclassType = records.Key;

				table.Subtables.Add(subtable);
			}

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

				def.ExpandedAttributes.Add(autoIdAttr);
			}

			foreach (var attr in records.Value.Records)
			{
				var attrDef = MyAttributeDefinition.LoadFrom(attr);

				if (attrDef == null)
					continue;

				// Expand repeated attributes if needed
				if (attrDef.Repeat == 1)
				{
					def.ExpandedAttributes.Add(attrDef);
					continue;
				}

				for (var i = 1; i <= attrDef.Repeat; i++)
				{
					var newAttrDef = attrDef.DuplicateOffseted((i - 1) * attrDef.Size);
					newAttrDef.Name += $"-{i}";
					newAttrDef.OriginalName = newAttrDef.Name;
					newAttrDef.Repeat = 1;

					def.ExpandedAttributes.Add(newAttrDef);
				}
			}
		}

		table._attributesDictionary = table.ExpandedAttributes.ToDictionary(x => x.Name);

		return table;
	}
}