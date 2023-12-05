using System.Diagnostics.CodeAnalysis;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class DatafileDefinition
{
    public List<TableDefinition> TableDefinitions { get; private set; }

	private Dictionary<string, TableDefinition> _definitionsByName;
	private Dictionary<int, TableDefinition> _definitionsByType;

	public DatafileDefinition(List<TableDefinition> definitions)
    {
        TableDefinitions = definitions;
        _definitionsByName = definitions.ToDictionary(x => x.Name, new TableNameComparer());
        _definitionsByType = definitions.ToLookup(x => (int)x.Type, x => x).ToDictionary(o => o.Key, o => o.First());
    }

	public TableDefinition this[int index] => _definitionsByType.GetValueOrDefault(index, null);
    public TableDefinition this[string index] => _definitionsByName.GetValueOrDefault(index, null);
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