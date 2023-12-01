using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public sealed class AliasCollection : List<AliasEntry>
{
	public string Table;
	public bool HasCheck = false;

	public Dictionary<Ref, AliasEntry> ByRef;
	public Dictionary<string, AliasEntry> ByAlias;

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

	public AliasEntry()
	{

	}

	public override string ToString() => Table + ":" + Alias;
}