using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Xylia.Preview.Common.Extension;

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
	private Dictionary<ushort, TableDefinition> _definitionsByType;

	public TableDefinition this[ushort index] =>_definitionsByType.GetValueOrDefault(index, null);
	public TableDefinition this[string index] => _definitionsByName.GetValueOrDefault(index, null);


	internal void CreateMap()
	{
		// this.DistinctBy(def => def.Name, new TableNameComparer());
		_definitionsByType = this.ToDistinctDictionary(x => x.Type, null);
		_definitionsByName = this.ToDistinctDictionary(x => x.Name, new TableNameComparer());
	}
	#endregion
}

/// <summary>
/// Defines methods to support the comparison of camel hump naming convention
/// </summary>
internal sealed class TableNameComparer : IEqualityComparer<string>
{
	public static TableNameComparer Instance => new();

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