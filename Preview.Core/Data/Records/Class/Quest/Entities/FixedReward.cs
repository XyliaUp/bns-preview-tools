using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.QuestData
{
	[Signal("fixed-reward")]
	public class FixedReward : BaseRecord
	{
		public Faction Faction;

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
		[Signal("job-15")] public JobSeq Job15;

		[Signal("sex-1")] public SexSeq Sex1;
		[Signal("sex-2")] public SexSeq Sex2;
		[Signal("sex-3")] public SexSeq Sex3;
		[Signal("sex-4")] public SexSeq Sex4;
		[Signal("race-1")] public RaceSeq Race1;
		[Signal("race-2")] public RaceSeq Race2;
		[Signal("race-3")] public RaceSeq Race3;
		[Signal("race-4")] public RaceSeq Race4;


		[Signal("slot-1")] public string Slot1;
		[Signal("slot-2")] public string Slot2;
		[Signal("slot-3")] public string Slot3;
		[Signal("slot-4")] public string Slot4;
		[Signal("item-count-1")] public byte ItemCount1;
		[Signal("item-count-2")] public byte ItemCount2;
		[Signal("item-count-3")] public byte ItemCount3;
		[Signal("item-count-4")] public byte ItemCount4;
	}
}