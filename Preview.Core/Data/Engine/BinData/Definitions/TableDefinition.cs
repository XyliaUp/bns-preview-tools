using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Engine.Definitions;
public class TableDefinition : TableHeader
{
	/// <summary>
	/// element definitions
	/// </summary>
	public List<ElementDefinition> Els { get; internal set; } = new();

	/// <summary>
	/// root element
	/// </summary>
	public ElementDefinition ElRoot => Els.FirstOrDefault();

	/// <summary>
	/// main element
	/// </summary>
	public ElementDefinition ElRecord { get; set; }

	/// <summary>
	/// Has record element
	/// </summary>
	public bool IsEmpty => ElRecord is null;

	/// <summary>
	/// Is default definition 
	/// </summary>
	internal bool IsDefault { get; private set; }

	public TableModule Module { get; set; }




	#region Static Methods
	public override string ToString() => this.Name;

	/// <summary>
	/// create default <see cref="TableDefinition"/> if not found
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static TableDefinition CreateDefault(ushort type)
	{
		var definition = new TableDefinition()
		{
			IsDefault = true,
			Type = type,
			Name = type.ToString(),
		};

		var elRoot = new ElementDefinition() { Name = "table" };
		var elRecord = new ElementDefinition
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