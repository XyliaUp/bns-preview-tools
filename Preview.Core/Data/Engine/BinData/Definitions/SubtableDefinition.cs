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
	private Dictionary<string, AttributeDefinition> _attributesDictionary = new();
	private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();

	public void CreateAttributeMap()
	{
		_attributesDictionary = Attributes.ToDictionary(x => x.Name);
		_expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);
	}

	public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);

	public AttributeDefinition GetAttribute(string name) => _attributesDictionary.GetValueOrDefault(name, null);
	#endregion
}