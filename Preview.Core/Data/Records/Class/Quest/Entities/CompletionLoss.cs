using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData
{
	/// <summary>
	/// Mission的子节点
	/// 完成课题时失去
	/// </summary>
	[Signal("completion-loss")]
	public class CompletionLoss : BaseRecord
	{
		[Signal("job-1")] public JobSeq Job1;
		[Signal("job-2")] public JobSeq Job2;
		[Signal("job-3")] public JobSeq Job3;
		[Signal("job-4")] public JobSeq Job4;
		[Signal("job-5")] public JobSeq Job5;
		[Signal("job-6")] public JobSeq Job6;
		[Signal("job-7")] public JobSeq Job7;
		[Signal("job-8")] public JobSeq Job8;
		[Signal("job-9")] public JobSeq Job9;
		[Signal("job-10")] public JobSeq Job10;
		[Signal("job-11")] public JobSeq Job11;
		[Signal("job-12")] public JobSeq Job12;
		[Signal("job-13")] public JobSeq Job13;
		[Signal("job-14")] public JobSeq Job14;

		[Signal("item-1")] public string Item1;
		[Signal("item-2")] public string Item2;
		[Signal("item-3")] public string Item3;
		[Signal("item-4")] public string Item4;

		[Signal("item-count-1")] public byte ItemCount1;
		[Signal("item-count-2")] public byte ItemCount2;
		[Signal("item-count-3")] public byte ItemCount3;
		[Signal("item-count-4")] public byte ItemCount4;

		public long Money;
	}
}