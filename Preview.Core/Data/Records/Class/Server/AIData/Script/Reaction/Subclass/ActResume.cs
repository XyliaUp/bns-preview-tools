using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("act-resume")]
	public sealed class ActResume : IReaction
	{
		public Script_obj Target;
	}
}