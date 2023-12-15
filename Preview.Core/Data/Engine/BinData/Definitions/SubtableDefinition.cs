namespace Xylia.Preview.Data.Engine.Definitions;
public class SubtableDefinition : ITableDefinition
{
    public string Name { get; set; }
    public ushort Size { get; set; }
    public short SubclassType { get; set; }

    public List<AttributeDefinition> Attributes { get; } = [];
    public List<AttributeDefinition> ExpandedAttributes { get; } = [];
    public List<AttributeDefinition> ExpandedAttributesSubOnly { get; } = [];


	#region Helper

	private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();

	public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);

    public void CreateExpandedAttributeMap() => _expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);
	#endregion
}