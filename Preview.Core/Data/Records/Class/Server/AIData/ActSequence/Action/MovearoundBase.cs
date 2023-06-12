using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	public abstract class MovearoundBase : IAction
	{
		[Signal("max-idle-sec")]
		public int MaxIdleSec;

		[Signal("min-idle-sec")]
		public int MinIdleSec;

		[Signal("max-move-count")]
		public int MaxMoveCount;

		[Signal("min-move-count")]
		public int MinMoveCount;


		public Script_obj Target;
	}
}