using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models.QuestData;
public class Acquisition : ModelElement
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }
}

public class MissionStep : ModelElement
{
	[Name("mission")]
	public List<Mission> Mission { get; set; }

	[Name("mission-step-success")]
	public List<MissionStepSuccess> MissionStepSuccess { get; set; }

	[Name("mission-step-fail")]
	public List<MissionStepFail> MissionStepFail { get; set; }


	public string Text => Attributes["desc"].GetText();
}

public partial class Case
{
	

}

public partial class TutorialCase
{

}


public class Mission : ModelElement
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }


	public sbyte id => Attributes["id"].ToInt8();

	public string Text => Attributes["name2"]?.GetText();

	public short CurrentRegisterValue => 0;

	public string TagName => null;
}

public class MissionStepSuccess : ModelElement
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }
}

public class MissionStepFail : ModelElement
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }



	[Side(ReleaseSide.Client)]
	public Ref<TalkSocial> FailTalksocial;

	[Side(ReleaseSide.Client)]
	public float FailTalksocialDelay;

	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> QuestDecision;

	[Side(ReleaseSide.Server), Repeat(2)]
	public Ref<Zone>[] Zone;
}

public class Completion : ModelElement
{
	public List<NextQuest> NextQuest { get; set; }
}

public class NextQuest : ModelElement
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	public Ref<Quest> Quest;
}

public class Transit : ModelElement
{
	public List<Destination> Destination { get; set; }
	public List<Complete> Complete { get; set; }
	public List<NotAcquire> NotAcquire { get; set; }
}

public class Destination : ModelElement
{
	public sbyte MissionStepId;

	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class Complete : ModelElement
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class NotAcquire : ModelElement
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}