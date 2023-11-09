using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
public sealed class AliasCollection : List<AliasEntry>
{
    public string Table;

    public bool HasCheck = false;

    public static List<AliasCollection> CreateTable(NameTable source)
    {
        if (source is null) return null;
        var tables = new Dictionary<string, AliasCollection>(StringComparer.OrdinalIgnoreCase);

        CreateNode(source.RootEntry);
        source.Clear();

        foreach (var table in tables.Values)
        {
            table.ByRef = table.ToLookup(x => x.Ref, x => x).ToDictionary(x => x.Key, x => x.First());
            table.ByAlias = table.DistinctBy(x => x.Alias).ToDictionary(x => x.Alias, StringComparer.OrdinalIgnoreCase);
        }

        return tables.Values.ToList();


        void CreateNode(NameTableEntry entry, string path = null)
        {
            path += entry.String;

            if (entry.IsLeaf)
            {
                for (uint i = entry.Begin >> 1; i <= entry.End; i++)
                    CreateNode(source.Entries[(int)i], path);
            }
            else
            {
                var tmp = path.Split(':');
                var table = tmp[0];
                var alias = tmp[1];

                if (!tables.TryGetValue(table, out var infos))
                    tables.TryAdd(table, infos = new() { Table = table });

                infos.Add(new AliasEntry(entry.ToRef(), table, alias));
            }
        }
    }



    private Dictionary<Ref, AliasEntry> ByRef;
    private Dictionary<string, AliasEntry> ByAlias;

    public AliasEntry this[Ref Ref] => ByRef.GetValueOrDefault(Ref, null);
    public AliasEntry this[string alias] => ByAlias.GetValueOrDefault(alias, null);
}

public sealed class AliasEntry
{
	public Ref Ref;
	public string Table;
	public string Alias;

	public AliasEntry(Ref Ref, string table, string alias)
	{
		this.Ref = Ref;
		this.Table = table;
		this.Alias = alias;
	}


	public override string ToString() => Table + ":" + Alias;
}