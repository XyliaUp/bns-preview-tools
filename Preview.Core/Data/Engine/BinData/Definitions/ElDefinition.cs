namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class ElDefinition : ITableDefinition
{
	private Dictionary<string, SubtableDefinition> _subtablesDictionary = new();
	private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();


	public short Type { get; set; }
	public short SubclassType { get; set; } = -1; // always -1 on base table definition
	public string Name { get; set; }
	public string OriginalName { get; set; }
	public ushort MajorVersion { get; set; }
	public ushort MinorVersion { get; set; }
	public bool AutoKey { get; set; }
	public long MaxId { get; set; }

	public ushort Size { get; set; }
	public bool IsEmpty { get; set; }

	public List<AttributeDefinition> Attributes { get; } = new List<AttributeDefinition>();
	public List<SubtableDefinition> Subtables { get; } = new List<SubtableDefinition>();
	public List<AttributeDefinition> ExpandedAttributes { get; } = new List<AttributeDefinition>();

	public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);
	public SubtableDefinition SubtableByName(string name) => _subtablesDictionary.GetValueOrDefault(name, null);
	public ITableDefinition SubtableByType(short type) 
	{
		if (type == -1) return this;
		else if (this.Subtables.Count > type) return this.Subtables[type];

		return this;
	}




	public List<ElDefinition> Children = new();

	public void CreateSubtableMap() => _subtablesDictionary = Subtables.ToDictionary(x => x.Name);
	public void CreateExpandedAttributeMap() => _expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);
}