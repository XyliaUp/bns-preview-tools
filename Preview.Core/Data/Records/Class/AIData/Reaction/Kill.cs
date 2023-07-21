using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;
public sealed class Kill : Reaction
{
	[Obsolete] 
	[Signal("target")] 
	public Script_obj Target;

	[Signal("target-1")] 
	public Script_obj Target1;

	[Signal("target-2")] 
	public Script_obj Target2;
}