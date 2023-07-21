using BnsBinTool.Core.Definitions;

using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Models.BinData.Table;
public static class TableDefinitionEx
{
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
				$"{(type.Key == -1 ? string.Empty : "type:" + type.Key)} " +
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
}