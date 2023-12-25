namespace Xylia.Preview.Data.Engine.Definitions;
public interface ITableDefinition
{
	string Name { get; set; }
	ushort Size { get; set; }
	short SubclassType { get; }

	List<AttributeDefinition> Attributes { get; }
	List<AttributeDefinition> ExpandedAttributes { get; }
	AttributeDefinition this[string name] { get; }


	void CreateAttributeMap();

	AttributeDefinition GetAttribute(string name);
}