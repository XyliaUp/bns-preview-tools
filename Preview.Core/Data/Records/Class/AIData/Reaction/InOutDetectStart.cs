using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;

[Signal("in-out-detect-start")]
public sealed class InOutDetectStart : Reaction
{
	public int Duration;

	[Signal("filter") , Repeat(4)]
	public Filter Filter1;

	public short Radius;


	[Signal("ref-type")]
	public RefType RefType;

	[Signal("ref-object")]
	public Script_obj RefObject;

	[Signal("ref-area")]
	public ZoneArea RefArea;



	public Script_obj Subscriber;

	[Signal("gather-count")]
	public byte GatherCount;

	public byte Index;
}


public enum RefType
{
	Area,

	Object,
}