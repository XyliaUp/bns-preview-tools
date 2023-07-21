using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.ReactionClass;
public abstract class NpcBase : Reaction
{
	public bool Attackable;

	[Signal("hate-on")]
	public bool HateOn;

	public NpcBrain Brain;
}