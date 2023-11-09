using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class SkillTrainCategory : Record
{
	public JobSeq Job;

	[Name("view-skill-id")]
	public int ViewSkillId;

	[Name("tree-id")]
	public int TreeId;

	[Name("pc-level")]
	public short PcLevel;

	[Name("pc-mastery-level")]
	public short PcMasteryLevel;

	[Name("complete-quest")]
	public string CompleteQuest;

	[Name("jumping-pc-complete-quest")]
	public string JumpingPcCompleteQuest;

	[Name("consumed-tp")]
	public int ConsumedTp;

	[Name("sort-id")]
	public sbyte SortId;

	[Name("ui-invisible")]
	public bool UiInvisible;

	[Name("context-lock-disable")]
	public bool ContextLockDisable;
}