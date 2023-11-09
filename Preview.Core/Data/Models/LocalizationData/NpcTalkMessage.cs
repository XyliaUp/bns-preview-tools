using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public class NpcTalkMessage : Record
{
	public string Alias;



	public Ref<Text> Name2;

	[Name("required-faction")]
	public Ref<Faction> RequiredFaction;

	[Name("required-complete-quest")]
	public string RequiredCompleteQuest;

	[Name("function-step")]
	public sbyte FunctionStep;


	[Name("step-text"), Repeat(30)]
	public Ref<Text>[] StepText;
	[Name("step-subtext"), Repeat(30)]
	public Ref<Text>[] StepSubtext;
	[Name("step-next"), Repeat(30)]
	public Ref<Text>[] StepNext;
	[Name("step-kismet"), Repeat(30)]
	public string[] StepKismet;
	[Name("step-cinematic"), Repeat(30)]
	public Ref<Cinematic>[] StepCinematic;
	[Name("step-show"), Repeat(30)]
	public ObjectPath[] StepShow;
	[Name("step-camera-show"), Repeat(30)]
	public ObjectPath[] StepCameraShow;

	[Name("end-talk-social")]
	public Ref<TalkSocial>EndTalkSocial;
	[Name("end-talk-sound")]
	public string EndTalkSound;


	#region Sub	
	[Name("branch")]
	public sealed class Branch : NpcTalkMessage 
	{ 
	
	}

	[Name("questmessage")]
	public sealed class QuestMessage : NpcTalkMessage
	{

	}

	[Name("teleport")]
	public sealed class Teleport : NpcTalkMessage
	{

	}

	[Name("craft")]
	public sealed class Craft : NpcTalkMessage
	{

	}

	[Name("faction-coin-exchange")]
	public sealed class FactionCoinExchange : NpcTalkMessage
	{

	}

	[Name("store")]
	public sealed class Store : NpcTalkMessage
	{

	}
	
	[Name("warehouse")]
	public sealed class Warehouse : NpcTalkMessage
	{

	}

	[Name("auction")]
	public sealed class Auction : NpcTalkMessage
	{

	}

	[Name("delivery")]
	public sealed class Delivery : NpcTalkMessage
	{

	}

	[Name("make-summoned")]
	public sealed class MakeSummoned : NpcTalkMessage
	{

	}

	[Name("summoned-beauty-shop")]
	public sealed class SummonedBeautyShop : NpcTalkMessage
	{

	}

	[Name("summoned-name-change")]
	public sealed class SummonedNameChange : NpcTalkMessage
	{

	}

	[Name("create-guild")]
	public sealed class CreateGuild : NpcTalkMessage
	{

	}

	[Name("join-faction")]
	public sealed class JoinFaction : NpcTalkMessage
	{

	}

	[Name("transfer-faction")]
	public sealed class TransferFaction : NpcTalkMessage
	{

	}

	[Name("contribute-guild-reputation")]
	public sealed class ContributeGuildReputation : NpcTalkMessage
	{

	}

	[Name("dungeon-progress")]
	public sealed class DungeonProgress : NpcTalkMessage
	{

	}

	[Name("select-join-faction")]
	public sealed class SelectJoinFaction : NpcTalkMessage
	{

	}
	
	[Name("guild-customize")]
	public sealed class GuildCustomize : NpcTalkMessage
	{

	}
	#endregion
}