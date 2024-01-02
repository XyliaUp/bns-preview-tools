using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Xylia.Preview.Data.Engine.Definitions;
public abstract class ElementBaseDefinition
{
	#region Properies
	public string Name { get; set; }
	public ushort Size { get; set; }
	public bool AutoKey { get; set; }
	public long MaxId { get; set; }
	public virtual short SubclassType { get; set; }

	public List<AttributeDefinition> Attributes { get; } = [];
	public List<AttributeDefinition> ExpandedAttributes { get; private set; } = [];
	public List<ElementDefinition> Children { get; } = [];

	public override string ToString() => this.Name;
	#endregion


	#region Helper
	private Dictionary<string, AttributeDefinition> _attributesDictionary = new();
	private Dictionary<string, AttributeDefinition> _expandedAttributesDictionary = new();

	internal void CreateAttributeMap()
	{
		_attributesDictionary = Attributes.ToDictionary(x => x.Name);
		_expandedAttributesDictionary = ExpandedAttributes.ToDictionary(x => x.Name);

		ExpandedAttributes = [.. ExpandedAttributes.OrderBy(o => o.Type == AttributeType.TNative)
			.ThenBy(o => Regex.Replace(o.Name, @"\d+", match => match.Value.PadLeft(4, '0')))];
	}

	public AttributeDefinition this[string name] => _expandedAttributesDictionary.GetValueOrDefault(name, null);

	public AttributeDefinition GetAttribute(string name) => _attributesDictionary.GetValueOrDefault(name, null);
	#endregion
}


public class ElementDefinition : ElementBaseDefinition
{
	// always -1 on base table definition
	public override short SubclassType { get => -1; set => throw new NotSupportedException(); }

	public List<ElementSubDefinition> Subtables { get; } = [];


	#region Helper
	private Dictionary<string, ElementSubDefinition> _subtablesDictionary = new();

	public void CreateSubtableMap() => _subtablesDictionary = Subtables.ToDictionary(x => x.Name);

	public ElementBaseDefinition SubtableByName(string name)
	{
		bool IsEmpty = string.IsNullOrEmpty(name);
		if (Subtables.Count == 0)
		{
			Debug.Assert(IsEmpty);
			return this;
		}
		else
		{
			if (!IsEmpty && _subtablesDictionary.TryGetValue(name, out var definition)) return definition;
			else
			{
				Serilog.Log.Warning($"Invalid attribute: 'type', table: {this.Name}, value: {name}");
				//throw new ArgumentOutOfRangeException(nameof(name));

				return Subtables.First();
			}
		}
	}

	public ElementBaseDefinition SubtableByType(short type)
	{
		if (type == -1) return this;

		// check type is in subtable 
		lock (this.Subtables)
		{
			for (short subIndex = (short)Subtables.Count; subIndex < type + 1; subIndex++)
			{
				var subtable = new ElementSubDefinition();
				this.Subtables.Add(subtable);

				subtable.Name = subIndex.ToString();
				subtable.SubclassType = subIndex;

				// Add parent expanded attributes
				subtable.ExpandedAttributes.AddRange(this.ExpandedAttributes);
				subtable.Size = this.Size;
				subtable.CreateAttributeMap();
			}
		}

		return this.Subtables[type];
	}
	#endregion
}

public class ElementSubDefinition : ElementBaseDefinition
{
	public List<AttributeDefinition> ExpandedAttributesSubOnly { get; } = [];
}