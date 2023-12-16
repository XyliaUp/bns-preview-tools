using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public class MapUnit : ModelElement
{
	public short Mapid { get; set; }

	public int ZoneId { get; set; }

	public bool IsPhasingUnit { get; set; }

	public float PositionX { get; set; }

	public float PositionY { get; set; }

	public float PositionZ { get; set; }

	public CategorySeq Category { get; set; }
	public enum CategorySeq
	{
		None,

		Player,

		Party,

		Team,

		Guild,

		Friend,

		RevengeEnemy,

		Faction,

		DuelEnemy,

		Quest,

		Npc,

		Env,

		Teleport,

		Airdash,

		Link,

		Convoy,

		SpawnedEnv,

		Static,

		Auction,

		Store,

		Camp,

			PartyCamp,

		Roulette,

			FieldBoss,

		Gather,

		Craft,

			GatherEnv,

		Heart,

			EnterArena,

			WeaponBox,

		Refiner,

			Dungeon3,

			Dungeon4,

			Dungeon5,

			RaidDungeon,

			ClassicField,

			FactionBattleField,

			GuildBattleField,

			PartyBattleStartpoint,

			PartyBattleEnemy,

			FishingField,
	}


	public MapDepthSeq MapDepth { get; set; }

	public MapDepthSeq ArenaDungeonMapDepth { get; set; }
	public enum MapDepthSeq : byte
	{
		N1,

		N2,

		N3,

		N4,

		N5,
	}



	public bool Zoom { get; set; }

	public bool Rotate { get; set; }

	public bool Click { get; set; }

	public bool Front { get; set; }

	public bool ShowTooltip { get; set; }

	public Ref<Text> Name2 { get; set; }

	public short Opacity { get; set; }

	public short SizeX { get; set; }

	public short SizeY { get; set; }

	public short OufofsightSizeX { get; set; }

	public short OufofsightSizeY { get; set; }

	public string Imageset { get; set; }

	public string OverImageset { get; set; }

	public string PressedImageset { get; set; }

	public string OutofsightImageset { get; set; }

	public string OutofsightOverImageset { get; set; }

	public string OutofsightPressedImageset { get; set; }

	public float CenterPosX { get; set; }

	public float CenterPosY { get; set; }


	#region Sub
	public sealed class Static : MapUnit
	{

	}

	public sealed class Quest : MapUnit
	{

	}

	public sealed class Link : MapUnit
	{

	}

	public sealed class Npc : MapUnit
	{

	}

	public sealed class Boss : MapUnit
	{

	}

	public sealed class Airdash : MapUnit
	{

	}

	public sealed class Env : MapUnit
	{

	}

	public sealed class Attraction : MapUnit
	{

	}

	public sealed class NpcGroup : MapUnit
	{

	}

	public sealed class GuildBattleFieldPortal : MapUnit
	{

	}

	public sealed class PartyBattleStartpointAlpha : MapUnit
	{

	}

	public sealed class PartyBattleStartpointBeta : MapUnit
	{

	}

	public sealed class FishingField : MapUnit
	{

	}
	#endregion
}