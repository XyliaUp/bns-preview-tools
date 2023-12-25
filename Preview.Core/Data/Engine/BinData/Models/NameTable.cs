using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class NameTable
{
	public NameTableEntry RootEntry { get; } = new NameTableEntry();
	public virtual List<NameTableEntry> Entries { get; } = new List<NameTableEntry>();


	public virtual void Clear()
	{
		Entries.Clear();
	}


	public List<AliasTable> CreateTable()
	{
		var tables = new Dictionary<string, AliasTable>(StringComparer.OrdinalIgnoreCase);
		CreateNode(this.RootEntry, String.Empty, tables);

		return tables.Values.ToList();
	}

	private void CreateNode(NameTableEntry entry, string path, Dictionary<string, AliasTable> tables = null)
	{
		path += entry.String;

		if (entry.IsLeaf)
		{
			for (uint i = entry.Begin >> 1; i <= entry.End; i++)
				CreateNode(Entries[(int)i], path, tables);
		}
		else
		{
			var ls = path.Split(':', 2);
			if (ls.Length < 2) return;

			var table = ls[0];
			var alias = ls[1];

			if (!tables.TryGetValue(table, out var collection))
				collection = tables[table] = new(table);

			collection.Add(new AliasEntry(entry.ToRef(), table, alias));
		}
	}
}

public class NameTableEntry
{
	public string String;
	public long StringOffset;
	public uint Begin;
	public uint End;

	public bool IsLeaf => (Begin & 1) == 0;

	public Ref ToRef() => Ref.From((Begin | (ulong)End << 32) >> 1);

	public override string ToString() => $"{Begin >> 1}-{End} IsLeaf:{IsLeaf}";
}