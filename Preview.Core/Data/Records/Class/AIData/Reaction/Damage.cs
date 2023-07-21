using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;
public sealed class Damage : Reaction
{
	[Obsolete]
	public Script_obj Target;

	public Script_obj Target2;


	public long Amount;

	public byte Percent;
}