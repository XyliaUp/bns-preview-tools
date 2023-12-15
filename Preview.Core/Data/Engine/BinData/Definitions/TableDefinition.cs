namespace Xylia.Preview.Data.Engine.Definitions;
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

	/// <summary>
	/// Has record element
	/// </summary>
	public bool IsEmpty => ElRecord is null;

	/// <summary>
	/// Is default definition 
	/// </summary>
	internal bool IsDefault { get; private set; } = false;



	#region Static Methods
	/// <summary>
	/// create default <see cref="TableDefinition"/> if not found
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static TableDefinition CreateDefault(short type)
	{
		var definition = new TableDefinition()
		{
			IsDefault = true,
			Type = type, 
			Name = type.ToString() , 
		};

		var elRoot = new ElDefinition() { Name = "table" };
		var elRecord = new ElDefinition
		{
			Name = "record",
			Size = 8
		};

		elRoot.Children.Add(elRecord);

		definition.Els.Add(elRoot);
		definition.Els.Add(definition.ElRecord = elRecord);

		return definition;
	}
	#endregion
}