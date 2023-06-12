using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	public sealed class Select : IAction
	{
		[Signal("enter-prob")]
		public byte EnterProb;
	}
}