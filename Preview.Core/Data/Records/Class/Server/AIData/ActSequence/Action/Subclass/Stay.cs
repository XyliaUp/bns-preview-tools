using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	public sealed class Stay : IAction
	{
		public long Duration;

		public Detect detect;

		public byte Repeat;
	}
}