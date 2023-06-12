using Xylia.Preview.Common.Attribute;

using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction.Base
{
	public abstract class NpcBase : IReaction
	{
		public bool Attackable;

		[Signal("hate-on")]
		public bool HateOn;

		public NpcBrain Brain;
	}
}