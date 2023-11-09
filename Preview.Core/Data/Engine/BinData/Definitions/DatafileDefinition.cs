using System.Diagnostics.CodeAnalysis;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class DatafileDefinition
{
    public List<TableDefinition> TableDefinitions { get; protected set; }
    public short IconTextureTableId { get; protected set; }
    public short TextTableId { get; protected set; }
    public List<SequenceDefinition> SequenceDefinitions { get; } = new List<SequenceDefinition>();

    protected Dictionary<string, TableDefinition> _definitionsByName;
    protected Dictionary<int, TableDefinition> _definitionsByType;

    public DatafileDefinition(List<TableDefinition> definitions)
    {
        TableDefinitions = definitions;
        _definitionsByName = definitions.ToDictionary(x => x.Name, new TableNameComparer());
        _definitionsByType = definitions.ToLookup(x => (int)x.Type, x => x).ToDictionary(o => o.Key, o => o.First());

        if (TryGetValue("icontexture", out var iconTexture))
            IconTextureTableId = iconTexture.Type;

        if (TryGetValue("text", out var text))
            TextTableId = text.Type;
    }

    public TableDefinition this[int index] => _definitionsByType.GetValueOrDefault(index, null);
    public TableDefinition this[string index] => _definitionsByName.GetValueOrDefault(index, null);

    public bool TryGetValue(string index, out TableDefinition tableDef)
    {
        return _definitionsByName.TryGetValue(index, out tableDef);
    }

    public bool TryGetValue(int index, out TableDefinition tableDef)
    {
        return _definitionsByType.TryGetValue(index, out tableDef);
    }
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