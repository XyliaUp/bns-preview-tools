using System.Collections.ObjectModel;

using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public sealed class AliasTable : Collection<AliasEntry>
{
	public AliasTable(string name)
	{
		Name = name;
		ByRef = [];
	}

	public string Name;

	private Dictionary<Ref, AliasEntry> ByRef;

	public AliasEntry this[Ref Ref] => ByRef.GetValueOrDefault(Ref, null);

	protected override void InsertItem(int index, AliasEntry item)
	{
		base.InsertItem(index, item);
		ByRef[item.Ref] = item;
	}

	protected override void ClearItems()
	{
		base.ClearItems();
		ByRef.Clear();
	}


	internal bool HasCheck = false;
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
}