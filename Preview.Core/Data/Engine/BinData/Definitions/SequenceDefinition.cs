using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.Definitions;
public class SequenceDefinition : List<string>
{
    public SequenceDefinition(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
	public string Default { get; set; }



	#region Public Methods
	public SequenceDefinition Clone() => MemberwiseClone() as SequenceDefinition;

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