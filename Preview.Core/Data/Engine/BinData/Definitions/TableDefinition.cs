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
}