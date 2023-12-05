using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class ElDefinition : ITableDefinition
{
	private Dictionary<string, SubtableDefinition> _subtablesDictionary = new();
	private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();


	public short SubclassType { get; } = -1; // always -1 on base table definition
	public string Name { get; set; }
	public bool AutoKey { get; set; }
	public long MaxId { get; set; }
	public ushort Size { get; set; }


	public List<AttributeDefinition> Attributes { get; } = [];
	public List<AttributeDefinition> ExpandedAttributes { get; } = [];
	public List<SubtableDefinition> Subtables { get; } = [];
	public List<ElDefinition> Children { get; } = [];


	#region Methods
	public ITableDefinition SubtableByName(string name)
	{
		bool IsEmpty = string.IsNullOrEmpty(name);
		if (Subtables.Count == 0)
		{
			if (IsEmpty) return this;
		}
		else if (!IsEmpty)
		{
			return _subtablesDictionary.GetValueOrDefault(name, null) ?? 
				Subtables.First();	//throw new ArgumentOutOfRangeException(nameof(name));
		}

		throw new BnsException($"Invalid attribute: 'type'");
	}

	public ITableDefinition SubtableByType(short type)
	{
		if (type == -1) return this;

		// check type is in subtable 
		lock (this.Subtables)
		{
			for (short subIndex = (short)Subtables.Count; subIndex < type + 1; subIndex++)
			{
				var subtable = new SubtableDefinition();
				this.Subtables.Add(subtable);

				subtable.Name = subIndex.ToString();
				subtable.SubclassType = subIndex;

				// Add parent expanded attributes
				subtable.ExpandedAttributes.AddRange(this.ExpandedAttributes);
				subtable.Size = this.Size;
				subtable.CreateExpandedAttributeMap();
			}
		}

		return this.Subtables[type];
	}

	public void CreateSubtableMap() => _subtablesDictionary = Subtables.ToDictionary(x => x.Name);
	public void CreateExpandedAttributeMap() => _expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);
	public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);
	#endregion
}