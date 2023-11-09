namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class TableDefinition
{
    public string Name { get; set; }

	/// <summary>
	/// elements
	/// </summary>
    public List<ElDefinition> Els { get; } = new();

	public bool IsEmpty => ElRecord is null;


	/// <summary>
	/// model element
	/// </summary>
	public ElDefinition ElRecord { get; set; }
	public short Type { get; set; }
	public ushort MajorVersion { get; set; }
	public ushort MinorVersion { get; set; }
}