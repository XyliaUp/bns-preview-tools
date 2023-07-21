using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public class NpcTalkMessage : BaseRecord
{
	public Text Name2;

	[Signal("required-faction")]
	public Faction RequiredFaction;

	[Signal("required-complete-quest")]
	public string RequiredCompleteQuest;

	[Signal("function-step")]
	public byte FunctionStep;


	[Signal("step-text"), Repeat(30)]
	public Text[] StepText;
	[Signal("step-subtext"), Repeat(30)]
	public Text[] StepSubtext;
	[Signal("step-next"), Repeat(30)]
	public Text[] StepNext;
	[Signal("step-kismet"), Repeat(30)]
	public string[] StepKismet;
	[Signal("step-cinematic"), Repeat(30)]
	public Cinematic[] StepCinematic;
	[Signal("step-show"), Repeat(30)]
	public FPath[] StepShow;
	[Signal("step-camera-show"), Repeat(30)]
	public FPath[] StepCameraShow;

	[Signal("end-talk-social")]
	public TalkSocial EndTalkSocial;
	[Signal("end-talk-sound")]
	public string EndTalkSound;


	#region Sub	
	[Signal("branch")]
	public sealed class Branch : NpcTalkMessage 
	{ 
	
	}

	[Signal("questmessage")]
	public sealed class QuestMessage : NpcTalkMessage
	{

	}

	[Signal("teleport")]
	public sealed class Teleport : NpcTalkMessage
	{

	}

	[Signal("craft")]
	public sealed class Craft : NpcTalkMessage
	{

	}

	[Signal("faction-coin-exchange")]
	public sealed class FactionCoinExchange : NpcTalkMessage
	{

	}

	[Signal("store")]
	public sealed class Store : NpcTalkMessage
	{

	}
	
	[Signal("warehouse")]
	public sealed class Warehouse : NpcTalkMessage
	{

	}

	[Signal("auction")]
	public sealed class Auction : NpcTalkMessage
	{

	}

	[Signal("delivery")]
	public sealed class Delivery : NpcTalkMessage
	{

	}

	[Signal("make-summoned")]
	public sealed class MakeSummoned : NpcTalkMessage
	{

	}

	[Signal("summoned-beauty-shop")]
	public sealed class SummonedBeautyShop : NpcTalkMessage
	{

	}

	[Signal("summoned-name-change")]
	public sealed class SummonedNameChange : NpcTalkMessage
	{

	}

	[Signal("create-guild")]
	public sealed class CreateGuild : NpcTalkMessage
	{

	}

	[Signal("join-faction")]
	public sealed class JoinFaction : NpcTalkMessage
	{

	}

	[Signal("transfer-faction")]
	public sealed class TransferFaction : NpcTalkMessage
	{

	}

	[Signal("contribute-guild-reputation")]
	public sealed class ContributeGuildReputation : NpcTalkMessage
	{

	}

	[Signal("dungeon-progress")]
	public sealed class DungeonProgress : NpcTalkMessage
	{

	}

	[Signal("select-join-faction")]
	public sealed class SelectJoinFaction : NpcTalkMessage
	{

	}
	
	[Signal("guild-customize")]
	public sealed class GuildCustomize : NpcTalkMessage
	{

	}
	#endregion
}