using System.Collections.Concurrent;

using BnsBinTool.Core.Models;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.AliasTable;
public static class AliasTableHelper
{
	public static ConcurrentDictionary<string, AliasCollection> CreateTable(this NameTable source)
	{
		var result = new BlockingCollection<AliasInfo>();
		Parallel.For(source.RootEntry.Begin >> 1, source.Entries.Count, Index =>
		{
			BlockingCollection<AliasInfo> _table = new();
			source.Entries[(int)Index].CreateNodes(source, _table);

			_table.ForEach(o => result.Add(o));
		});
		source.Clear();


		var tables = new ConcurrentDictionary<string, AliasCollection>(StringComparer.OrdinalIgnoreCase);
		Parallel.ForEach(result.GroupBy(o => o.Table), table =>
		{
			var lst = new AliasCollection();
			tables.TryAdd(table.Key, lst);

			foreach (var item in table)
				lst.Add(item);

			lst.Sort();
		});

		return tables;
	}

	private static void CreateNodes(this NameTableEntry entry, NameTable source, BlockingCollection<AliasInfo> table, string CurPath = null)
	{
		CurPath += entry.String;

		if (entry.IsLeaf)
			for (uint Index = entry.Begin >> 1; Index <= entry.End; Index++)
				source.Entries[(int)Index].CreateNodes(source, table, CurPath);
		else table.Add(new AliasInfo(entry.ToRef(), CurPath));
	}
}