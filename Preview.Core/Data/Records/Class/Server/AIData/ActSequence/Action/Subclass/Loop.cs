using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	public sealed class Loop : IAction
	{
		[Signal("max-count")]
		public int MaxCount;

		[Signal("min-count")]
		public int MinCount;
	}
}