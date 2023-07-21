using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData;
public abstract class CaseParent : BaseRecord
{
	[Signal("case")]
	public List<Case> Case;

	[Signal("tutorial-case")]
	public List<TutorialCase> TutorialCase;

	[Signal("basic-reward")]
	public List<BasicReward> BasicReward;

	[Signal("fixed-reward")]
	public List<FixedReward> FixedReward;

	[Signal("optional-reward")]
	public List<OptionalReward> OptionalReward;

	[Signal("acquisition-loss")]
	public List<AcquisitionLoss> AcquisitionLoss;

	[Signal("completion-loss")]
	public List<CompletionLoss> CompletionLoss;
}