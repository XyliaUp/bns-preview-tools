using System.Diagnostics;
using System.Xml;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.Definitions;
public class SequenceDefinition : List<string>
{
    public SequenceDefinition(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; set; }
    public int Size { get; set; }
	public string Default { get; set; }



	#region Public Methods
	public static SequenceDefinition LoadFrom(XmlElement element, string name, 
		Dictionary<string, SequenceDefinition> globalSeq = null)
	{
		SequenceDefinition sequence;

		var nodes = element.ChildNodes.OfType<XmlElement>();
		if (nodes.Any())
		{
			sequence = new SequenceDefinition(name , nodes.Count());

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

				sequence.Add(text);
				if ((node.Attributes["default"]?.Value).ToBool())
				{
					if (sequence.Default is not null)
						throw BnsDataException.InvalidSequence($"duplicate default value. (prev:{sequence.Default}, now:{text})" , name);

					sequence.Default = text;
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

			sequence = TSeq.MemberwiseClone() as SequenceDefinition;
			sequence.Name = name;
		}

		return sequence;
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
			if (this.Count > sbyte.MaxValue)
				throw BnsDataException.InvalidSequence($"seq exceeding maximum size, use `Seq16` instead." , Name);
		}
		else if (type == AttributeType.TSeq16 || type == AttributeType.TProp_field)
		{
			if (this.Count > short.MaxValue)
				throw BnsDataException.InvalidSequence($"seq exceeding maximum size, use `Seq32` instead." , Name);
		}
		else throw BnsDataException.InvalidSequence($"invalid attribute type, use `Seq` instead." , Name);
	}
	#endregion
}