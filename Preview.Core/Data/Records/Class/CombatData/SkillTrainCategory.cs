using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record;
public class SkillTrainCategory : BaseRecord
{
	public JobSeq Job;

	[Signal("view-skill-id")]
	public int ViewSkillId;

	[Signal("tree-id")]
	public int TreeId;

	[Signal("pc-level")]
	public short PcLevel;

	[Signal("pc-mastery-level")]
	public short PcMasteryLevel;

	[Signal("complete-quest")]
	public string CompleteQuest;

	[Signal("jumping-pc-complete-quest")]
	public string JumpingPcCompleteQuest;

	[Signal("consumed-tp")]
	public int ConsumedTp;

	[Signal("sort-id")]
	public byte SortId;

	[Signal("ui-invisible")]
	public bool UiInvisible;

	[Signal("context-lock-disable")]
	public bool ContextLockDisable;
}