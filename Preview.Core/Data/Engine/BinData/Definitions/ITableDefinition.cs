namespace Xylia.Preview.Data.Engine.Definitions;
public interface ITableDefinition
{
	string Name { get; set; }
	ushort Size { get; set; }
	short SubclassType { get; }
	List<AttributeDefinition> ExpandedAttributes { get; }
	List<AttributeDefinition> Attributes { get; }
	AttributeDefinition this[string name] { get; }


	void CreateExpandedAttributeMap();
}