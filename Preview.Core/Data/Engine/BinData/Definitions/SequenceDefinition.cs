using System.Diagnostics;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class SequenceDefinition 
{
    public SequenceDefinition(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public List<string> Sequence { get; } = new List<string>();
    public List<string> OriginalSequence { get; } = new List<string>();
    public string Name { get; set; }
    public int Size { get; set; }

	public string Default { get; set; }



	#region Public Methods
	public static SequenceDefinition LoadFrom(XmlElement element, string name, 
		Dictionary<string, SequenceDefinition> globalSeq = null)
	{
		SequenceDefinition seq;

		var nodes = element.ChildNodes.OfType<XmlElement>();
		if (nodes.Any())
		{
			seq = new SequenceDefinition(name , nodes.Count());

			short key = 0;
			foreach (var node in nodes)
			{
				#region key
				short _key;

				string Key = node.Attributes["key"]?.Value;
				if (string.IsNullOrWhiteSpace(Key)) _key = key++;
				else if (!short.TryParse(Key, out _key)) continue;

				key = (short)Math.Max(key, _key + 1);

				string text = node.Attributes["name"]?.Value?.Trim() ?? "unk" + key;
				#endregion

				seq.Sequence.Add(text);
				if ((node.Attributes["default"]?.Value).ToBool())
				{
					if (seq.Default is not null)
						throw new BnsDefinitionException($"seq `{name}` duplicate default value. (prev:{seq.Default}, now:{text})");

					seq.Default = text;
				}
			}
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

			seq = TSeq.MemberwiseClone() as SequenceDefinition;
			seq.Name = name;
		}

		return seq;
	}


	/// <summary>
	/// Check count of the sequence
	/// </summary>
	/// <param name="type"></param>
	/// <exception cref="BnsDefinitionException"></exception>
	public void Check(AttributeType type)
	{
		if (type == AttributeType.TSeq || type == AttributeType.TProp_seq)
		{
			if (Sequence.Count > sbyte.MaxValue)
				throw new BnsDefinitionException($"{Name} -> seq exceeding maximum size, use `Seq16` instead.");
		}
		else if (type == AttributeType.TSeq16 || type == AttributeType.TProp_field)
		{
			if (Sequence.Count > short.MaxValue)
				throw new BnsDefinitionException($"{Name} -> seq exceeding maximum size, use `Seq32` instead.");
		}
		else throw new BnsDefinitionException($"{Name} -> invalid attribute type, use `Seq` instead.");
	}
	#endregion
}