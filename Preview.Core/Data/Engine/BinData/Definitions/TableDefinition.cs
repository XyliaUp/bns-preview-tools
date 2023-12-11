using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class TableDefinition
{
	/// <summary>
	/// Name of table
	/// </summary>
    public string Name { get; set; }

	/// <summary>
	/// Identifier of table
	/// </summary>
	/// <remarks>generated automatically according to the sorting of the table name</remarks>
	public short Type { get; set; }

	/// <summary>
	/// major version of table
	/// </summary>
	public ushort MajorVersion { get; set; }

	/// <summary>
	/// minor version of table
	/// </summary>
	public ushort MinorVersion { get; set; }


	/// <summary>
	/// element definitions
	/// </summary>
	public List<ElDefinition> Els { get; } = new();

	/// <summary>
	/// root element
	/// </summary>
	public ElDefinition ElRoot => Els.FirstOrDefault();

	/// <summary>
	/// main element
	/// </summary>
	public ElDefinition ElRecord { get; set; }

	public bool IsEmpty => ElRecord is null;


	#region Static Methods
	public static TableDefinition CreateDefault(short type)
	{
		var definition = new TableDefinition() { Type = type, Name = type.ToString() };
		var autoIdAttr = new AttributeDefinition
		{
			Name = AttributeCollection.s_autoid,
			Size = 8,
			Offset = 8,
			Type = AttributeType.TInt64,
			IsKey = true,
			IsRequired = true,
			Repeat = 1
		};

		var elRecord = new ElDefinition() { Name = "record" };
		elRecord.ExpandedAttributes.Add(autoIdAttr);
		elRecord.Size = 16;

		var elRoot = new ElDefinition() { Name = "table" };
		elRoot.Children.Add(elRecord);

		definition.Els.Add(elRoot);
		definition.Els.Add(definition.ElRecord = elRecord);

		return definition;
	}
	#endregion
}