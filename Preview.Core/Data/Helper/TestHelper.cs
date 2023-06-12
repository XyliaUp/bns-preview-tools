
using System;
using System.Collections.Generic;
using System.Diagnostics;

using BnsBinTool.Core.Definitions;
using Xylia.Preview.Data.Models.BinData.AliasTable;

namespace Xylia.Preview.Data.Helper;

public sealed class TestHelper
{
	public short Type;

	public string TypeName;

	public AliasCollection Aliases;

	public TableDefinition Definition;



	public void CheckVersion(BnsBinTool.Core.Models.Table Table)
	{
		if (Table.MajorVersion != Definition.MajorVersion || Table.MinorVersion != Definition.MinorVersion)
			Trace.WriteLine($"[{ DateTime.Now }] 版本不一致: { TypeName } -> {Definition.MajorVersion}.{Definition.MinorVersion} <> {Table.MajorVersion}.{Table.MinorVersion}");
	}

	public Dictionary<short, Dictionary<string, AttributeDefinition>> GetAttrDef()
	{
		if (Definition is null || Definition.IsEmpty)
			throw new ArgumentNullException(nameof(Definition));


		Dictionary<short, Dictionary<string, AttributeDefinition>> attrDef = new();

		attrDef[-1] = new(StringComparer.OrdinalIgnoreCase);
		Definition.ExpandedAttributes.ForEach(attr => attrDef[-1][attr.Name] = attr);

		Definition.Subtables.ForEach(subtable =>
		{
			var group = attrDef[subtable.SubclassType] = new(StringComparer.OrdinalIgnoreCase);

			group["type"] = new AttributeDefinition() { Name = "type", OriginalName = subtable.Name };
			subtable.ExpandedAttributes.ForEach(attr => group[attr.Name] = attr);
		});

		return attrDef;
	}
}