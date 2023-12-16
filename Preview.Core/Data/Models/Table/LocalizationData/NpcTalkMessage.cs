using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public class NpcTalkMessage : ModelElement
{
	#region Sub	
	public sealed class Branch : NpcTalkMessage 
	{ 
	
	}

	public sealed class QuestMessage : NpcTalkMessage
	{

	}

	public sealed class Teleport : NpcTalkMessage
	{

	}

	public sealed class Craft : NpcTalkMessage
	{

	}

	public sealed class FactionCoinExchange : NpcTalkMessage
	{

	}

	public sealed class Store : NpcTalkMessage
	{

	}
	
	public sealed class Warehouse : NpcTalkMessage
	{

	}

	public sealed class Auction : NpcTalkMessage
	{

	}

	public sealed class Delivery : NpcTalkMessage
	{

	}

	public sealed class MakeSummoned : NpcTalkMessage
	{

	}

	public sealed class SummonedBeautyShop : NpcTalkMessage
	{

	}

	public sealed class SummonedNameChange : NpcTalkMessage
	{

	}

	public sealed class CreateGuild : NpcTalkMessage
	{

	}

	public sealed class JoinFaction : NpcTalkMessage
	{

	}

	public sealed class TransferFaction : NpcTalkMessage
	{

	}

	public sealed class ContributeGuildReputation : NpcTalkMessage
	{

	}

	public sealed class DungeonProgress : NpcTalkMessage
	{

	}

	public sealed class SelectJoinFaction : NpcTalkMessage
	{

	}
	
	public sealed class GuildCustomize : NpcTalkMessage
	{

	}
	#endregion
}