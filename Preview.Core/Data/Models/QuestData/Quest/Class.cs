using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models.QuestData;
public class Acquisition : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }
}

public class MissionStep : Record
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


public class Mission : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }


	public sbyte id => Attributes["id"].ToInt8();

	public string Text => Attributes["name2"]?.GetText();

	public short CurrentRegisterValue => 0;

	public string TagName => null;
}

public class MissionStepSuccess : Record
{
	public List<Case> Case { get; set; }

	public List<TutorialCase> TutorialCase { get; set; }
}

public class MissionStepFail : Record
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

public class Completion : Record
{
	public List<NextQuest> NextQuest { get; set; }
}

public class NextQuest : Record
{
	public Ref<Faction> Faction;

	[Repeat(15)]
	public JobSeq[] Job;

	public Ref<Quest> Quest;
}

public class Transit : Record
{
	public List<Destination> Destination { get; set; }
	public List<Complete> Complete { get; set; }
	public List<NotAcquire> NotAcquire { get; set; }
}

public class Destination : Record
{
	public sbyte MissionStepId;

	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class Complete : Record
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}

public class NotAcquire : Record
{
	public sbyte ZoneIndex;

	[Side(ReleaseSide.Client)]
	public string Kismet;
}