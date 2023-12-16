using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Xylia.Preview.Data.Engine.Definitions;
public class DatafileDefinition : Collection<TableDefinition>
{
	/// <summary>
	/// for parse type
	/// </summary>
	public FileInfo Header = null;

	public bool HasHeader => Header != null && Header.Exists;


	#region Helpers
	private Dictionary<string, TableDefinition> _definitionsByName;
	private Dictionary<int, TableDefinition> _definitionsByType;

	public new TableDefinition this[int index] => _definitionsByType.GetValueOrDefault(index, null);
	public TableDefinition this[string index] => _definitionsByName.GetValueOrDefault(index, null);


	public void CreateMap()
	{
		// this.DistinctBy(def => def.Name, new TableNameComparer());
		_definitionsByType = this.ToLookup(x => (int)x.Type, x => x).ToDictionary(o => o.Key, o => o.First());
		_definitionsByName = this.ToLookup(x => x.Name, x => x, new TableNameComparer()).ToDictionary(o => o.Key, o => o.First());
	}
	#endregion
}

public sealed class TableNameComparer : IEqualityComparer<string>
{
	public bool Equals(string x, string y)
	{
		return string.Equals(
			x.Replace("-", null),
			y.Replace("-", null),
			StringComparison.OrdinalIgnoreCase);
	}

	public int GetHashCode([DisallowNull] string obj)
	{
		return string.GetHashCode(obj.Replace("-", null), StringComparison.OrdinalIgnoreCase);
	}
}