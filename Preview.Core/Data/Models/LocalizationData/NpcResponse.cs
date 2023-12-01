using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class NpcResponse : Record
{
	public string Alias;



	public FactionCheckTypeSeq FactionCheckType;

	[Repeat(2)]
	public Ref<Faction>[] Faction;

	public Ref<Quest> RequiredCompleteQuest;
	public FactionLevelCheckTypeSeq FactionLevelCheckType;
	public Ref<NpcTalkMessage> TalkMessage;
	public Ref<IndicatorSocial> IndicatorSocial;
	public Ref<Social> ApproachSocial;
	public Ref<IndicatorIdle> Idle;
	public bool IdleVisible;
	public Ref<Social> IdleStart;
	public Ref<Social> IdleEnd;


	public enum FactionCheckTypeSeq : byte
	{
		Is,
		IsNot,
		IsNone,
	}

	public enum FactionLevelCheckTypeSeq : byte
	{
		None,
		CheckForSuccess,
		CheckForFail,
	}
}