using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;

using RecordModel = BnsBinTool.Core.Models.Record;

namespace Xylia.Preview.Data.Models.BinData.Table.Record.Attributes;
public sealed class DbData : IAttributeCollection
{
	#region Constructor
	private readonly DatafileConverter convert;

	public readonly RecordModel record;

	private readonly ITableDefinition tableDef;

	private readonly Dictionary<AttributeDefinition, string> data;

	public DbData(DatafileConverter convert, TableDefinition tableDef, RecordModel record)
	{
		this.convert = convert;
		this.record = record;

		this.data = new();
		this.tableDef = TableDefinitionEx.GetSubDef(tableDef, record.SubclassType);
	}
	#endregion


	#region Attribute
	public string this[string param, int index, bool convert]
	{
		get
		{
			if (!ContainsKey(index == 0 ? param : $"{param}-{index}", out var def, out var Value)) return null;

			if (convert)
			{
				if (def.CheckRef("Text")) return Value.GetText();
			}

			return Value;
		}
	}

	public bool ContainsKey(string Name, out string Value) => ContainsKey(Name, out _, out Value);

	public bool ContainsKey(string Name, out AttributeDefinition attrDef, out string Value)
	{
		attrDef = tableDef.ExpandedAttributeByName(Name);
		if (attrDef != null)
		{
			//get value
			if (!data.TryGetValue(attrDef, out Value))
				Value = ProcessObject(attrDef);

			return true;
		}

		Value = null;
		return false;
	}

	public override string ToString() => GetEnumerator().ToIEnumerable().Aggregate("<record ", (sum, now) => sum + $"{now.Key}=\"{now.Value}\" ", result => result + "/>");
	#endregion

	#region Functions
	private string ProcessObject(AttributeDefinition attr) => data[attr] = convert.ConvertRecord(record, attr);

	public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
	{
		foreach (var attr in tableDef.ExpandedAttributes)
		{
			var value = ProcessObject(attr);
			if (value != attr.DefaultValue) yield return new(attr.Name, value);
		}

		yield break;
	}
	#endregion
}