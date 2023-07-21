using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.Table.Config;
public class SeqInfo : List<SeqCell>, ICloneable
{
	#region Constructor
	public SeqInfo() : base()
	{

	}

	public SeqInfo(IEnumerable<SeqCell> collection) : base(collection)
	{

	}
	#endregion

	#region hashmap
	private readonly Hashtable ht_alias = new(StringComparer.Create(CultureInfo.InvariantCulture, true));

	private readonly Hashtable ht_id = new();
	#endregion

	#region Fields
	public string Name;

	public SeqCell DefaultCell;
	#endregion


	#region Functions
	public new void Add(SeqCell item)
	{
		if (string.IsNullOrWhiteSpace(item.Name))
		{
			Trace.WriteLine($"seq `{Name}` missing alias: {item.Key}");
		}



		if (item.Key >= 0 && !string.IsNullOrWhiteSpace(item.Name))
		{
			if (ht_alias.ContainsKey(item.Name))
				throw new ConfigurationErrorsException($"seq `{Name}` duplicate alias: {item.Name}");

			ht_alias.Add(item.Name, item);
		}

		if (ht_id.ContainsKey(item.Key))
			throw new ConfigurationErrorsException($"seq `{Name}` duplicate key: {item.Key}");

		ht_id.Add(item.Key, item);
		base.Add(item);
	}

	public new void AddRange(IEnumerable<SeqCell> items)
	{
		if (items == null) throw new ArgumentNullException(nameof(items));

		foreach (var item in items) Add(item);
	}

	public new bool Remove(SeqCell item)
	{
		if (ht_alias.ContainsKey(item.Name)) ht_alias.Remove(item.Name);
		if (ht_id.ContainsKey(item.Key)) ht_id.Remove(item.Key);

		return base.Remove(item);
	}

	public new void Clear()
	{
		base.Clear();

		ht_id.Clear();
		ht_alias.Clear();
	}


	public bool TryGet(string Name, out SeqCell cell)
	{
		var flag = ht_alias.ContainsKey(Name);

		cell = flag ? ht_alias[Name] as SeqCell : null;
		return flag;
	}

	public bool TryGet(short Key, out SeqCell cell)
	{
		var flag = ht_id.ContainsKey(Key);

		cell = flag ? ht_id[Key] as SeqCell : null;
		return flag;
	}
	#endregion


	#region Functions
	public void Check(AttributeType type)
	{
		if (type == AttributeType.TSeq || type == AttributeType.TProp_seq)
		{
			if (this.Count > sbyte.MaxValue)
				throw new ConfigurationErrorsException($"{Name} -> seq exceeding maximum size, use `Seq16` instead.");
		}
		else if (type == AttributeType.TSeq16 || type == AttributeType.TProp_field)
		{
			if (this.Count > short.MaxValue)
				throw new ConfigurationErrorsException($"{Name} -> seq exceeding maximum size, use `Seq32` instead.");
		}
		else throw new ConfigurationErrorsException($"{Name} -> invalid attribute type, use `Seq` instead.");
	}

	public SequenceDefinition ConvertTo()
	{
		var def = new SequenceDefinition(this.Name, this.Count);
		def.Sequence.AddRange(this.Select(seq => seq.Name));

		return def;
	}


	public static SeqInfo LoadFrom(XmlElement element, string name, Dictionary<string, SeqInfo> globalSeq = null)
	{
		SeqInfo seq;

		var CaseNodes = element.ChildNodes.OfType<XmlElement>();
		if (CaseNodes.Any())
		{
			seq = new() { Name = name };

			short key = 0;
			foreach (var CaseNode in CaseNodes)
			{
				#region key
				short _key;

				string Key = CaseNode.Attributes["key"]?.Value;
				if (string.IsNullOrWhiteSpace(Key)) _key = key++;
				else if (!short.TryParse(Key, out _key)) continue;

				key = (short)Math.Max(key, _key + 1);
				#endregion

				#region save
				var cell = new SeqCell(_key,
					CaseNode.Attributes["name"]?.Value?.Trim() ?? "unk" + key,
					CaseNode.Attributes["desc"]?.Value?.Trim());

				seq.Add(cell);

				if ((CaseNode.Attributes["default"]?.Value).ToBool())
				{
					if (seq.DefaultCell is not null)
						throw new ConfigurationErrorsException($"seq `{name}` duplicate default value. (prev:{seq.DefaultCell.Name}, now:{cell.Name})");

					seq.DefaultCell = cell;
				}
				#endregion
			}


			//seq.DefaultCell ??= seq.FirstOrDefault();
		}
		else
		{
			var SeqName = element.Attributes["seq"]?.Value?.Trim();
			if (string.IsNullOrWhiteSpace(SeqName)) return null;

			if (globalSeq is null || !globalSeq.TryGetValue(SeqName, out var TSeq))
			{
				Trace.WriteLine($"seq `{SeqName}` not defined");
				return null;
			}

			seq = TSeq.Clone() as SeqInfo;
			seq.Name = name;
		}

		return seq;
	}
	#endregion

	#region ICloneable
	public object Clone() => MemberwiseClone();
	#endregion
}