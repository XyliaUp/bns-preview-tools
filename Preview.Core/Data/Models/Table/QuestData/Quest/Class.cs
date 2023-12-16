using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
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
	public Ref<TalkSocial> FailTalksocial { get; set; }

	[Side(ReleaseSide.Client)]
	public float FailTalksocialDelay { get; set; }

	[Side(ReleaseSide.Server)]
	public Ref<Decision.QuestDecision> QuestDecision { get; set; }

	[Side(ReleaseSide.Server), Repeat(2)]
	public Ref<Zone>[] Zone { get; set; }
}

public class Completion : ModelElement
{
	public List<NextQuest> NextQuest { get; set; }
}

public class NextQuest : ModelElement
{
	public Ref<Faction> Faction { get; set; }

	[Repeat(15)]
	public JobSeq[] Job { get; set; }

	public Ref<Quest> Quest { get; set; }
}

public class Transit : ModelElement
{
	public List<Destination> Destination { get; set; }
	public List<Complete> Complete { get; set; }
	public List<NotAcquire> NotAcquire { get; set; }
}

public class Destination : ModelElement
{
	public sbyte MissionStepId { get; set; }

	public sbyte ZoneIndex { get; set; }

	[Side(ReleaseSide.Client)]
	public string Kismet { get; set; }
}

public class Complete : ModelElement
{
	public sbyte ZoneIndex { get; set; }

	[Side(ReleaseSide.Client)]
	public string Kismet { get; set; }
}

public class NotAcquire : ModelElement
{
	public sbyte ZoneIndex { get; set; }

	[Side(ReleaseSide.Client)]
	public string Kismet { get; set; }
}