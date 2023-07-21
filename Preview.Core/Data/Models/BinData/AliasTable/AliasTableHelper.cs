using System.Collections.Concurrent;

using BnsBinTool.Core.Models;

namespace Xylia.Preview.Data.Models.BinData.AliasTable;
public static class AliasTableHelper
{
	public static ConcurrentDictionary<string, AliasCollection> CreateTable(this List<NameTableEntry> entries)
	{
		#region find start index
		int startIdx = -1;
		for (int idx = entries.Count - 1; idx >= 0; idx--)
		{
			//存在别名的数据表名
			var entry = entries[idx];
			if (entry.IsLeaf && entry.String == "account-post-charge:")
			{
				startIdx = idx;
				break;
			}
		}
		if (startIdx == -1) throw new Exception($"start index not found!");
		#endregion

		var array = entries.ToArray();
		var result = new ConcurrentDictionary<string, AliasCollection>(StringComparer.OrdinalIgnoreCase);

		Parallel.For(startIdx, array.Length, idx =>
		{
			BlockingCollection<AliasInfo> table = new();
			array[idx].CreateNodes(array, table);

			foreach (var record in table)
			{
				string ParentTable = record.Table;
				if (!result.TryGetValue(ParentTable, out var infos))
					infos = result[ParentTable] = new AliasCollection();

				infos.Add(record);
			}
		});

		Parallel.ForEach(result, table => table.Value.Sort());
		return result;
	}

	private static void CreateNodes(this NameTableEntry entry, NameTableEntry[] entries, BlockingCollection<AliasInfo> NodesList, string CurPath = null)
	{
		CurPath += entry.String;

		if (entry.IsLeaf)
		{
			var IndexA = entry.Begin / 2;
			var IndexB = entry.End;

			var Children = new NameTableEntry[IndexB - IndexA + 1];
			Array.Copy(entries, (int)IndexA, Children, 0, Children.Length);

			foreach (var node in Children)
				node.CreateNodes(entries, NodesList, CurPath);
		}
		else NodesList.Add(new AliasInfo(entry.ToRef(), CurPath));
	}
}