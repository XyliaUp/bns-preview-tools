using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData
{
	/// <summary>
	/// 放弃任务失去
	/// </summary>
	[Signal("giveup-loss")]
	public class GiveupLoss : BaseRecord
	{
		[Signal("item-1")] public string Item1;
		[Signal("item-2")] public string Item2;
		[Signal("item-3")] public string Item3;
		[Signal("item-4")] public string Item4;

		[Signal("item-count-1")] public short ItemCount1;
		[Signal("item-count-2")] public short ItemCount2;
		[Signal("item-count-3")] public short ItemCount3;
		[Signal("item-count-4")] public short ItemCount4;

		[Signal("job-1")] public JobSeq Job1;
		[Signal("job-2")] public JobSeq Job2;
		[Signal("job-3")] public JobSeq Job3;
		[Signal("job-4")] public JobSeq Job4;
	}
}