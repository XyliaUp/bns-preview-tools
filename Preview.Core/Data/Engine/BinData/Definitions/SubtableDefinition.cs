namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class SubtableDefinition : ITableDefinition
{
    private Dictionary<string, AttributeDefinition> _attributesDictionary = new();
    private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();

    public string Name { get; set; }
    public ushort Size { get; set; }
    public short SubclassType { get; set; }

    public List<AttributeDefinition> Attributes { get; } = new List<AttributeDefinition>();
    public List<AttributeDefinition> ExpandedAttributes { get; } = new List<AttributeDefinition>();
    public List<AttributeDefinition> ExpandedAttributesSubOnly { get; } = new List<AttributeDefinition>();

    public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);

    public void CreateExpandedAttributeMap() => _expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);
}