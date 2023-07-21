using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class NpcResponse : BaseRecord
{
	[Signal("faction-check-type")]
	public FactionCheckTypeSeq FactionCheckType;

	[Signal("faction-1")]
	public Faction Faction1;

	[Signal("faction-2")]
	public Faction Faction2;

	[Signal("required-complete-quest")]
	public string RequiredCompleteQuest;

	[Signal("faction-level-check-type")]
	public FactionLevelCheckTypeSeq FactionLevelCheckType;

	[Signal("talk-message")]
	public NpcTalkMessage TalkMessage;

	[Signal("indicator-social")]
	public IndicatorSocial IndicatorSocial;

	[Signal("approach-social")]
	public Social ApproachSocial;

	[Signal("idle")]
	public IndicatorSocial Idle;

	[Signal("idle-visible")]
	public bool IdleVisible;

	[Signal("idle-start")]
	public Social IdleStart;

	[Signal("idle-end")]
	public Social IdleEnd;


	public enum FactionCheckTypeSeq : byte
	{
		[Signal("is")]
		Is,

		[Signal("is-not")]
		IsNot,

		[Signal("is-none")]
		IsNone,
	}

	public enum FactionLevelCheckTypeSeq : byte
	{
		None,

		[Signal("check-for-success")]
		CheckForSuccess,

		[Signal("check-for-fail")]
		CheckForFail,
	}
}