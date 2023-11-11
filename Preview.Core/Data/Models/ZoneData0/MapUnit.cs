using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public class MapUnit : Record
{
	public string Alias;



	public short Mapid;

	[Name("zone-id")]
	public int ZoneId;

	[Name("is-phasing-unit")]
	public bool IsPhasingUnit;

	[Name("position-x")]
	public float PositionX;

	[Name("position-y")]
	public float PositionY;

	[Name("position-z")]
	public float PositionZ;

	public CategorySeq Category;
	public enum CategorySeq
	{
		None,

		Player,

		Party,

		Team,

		Guild,

		Friend,

		[Name("revenge-enemy")]
		RevengeEnemy,

		Faction,

		[Name("duel-enemy")]
		DuelEnemy,

		Quest,

		Npc,

		Env,

		Teleport,

		Airdash,

		Link,

		Convoy,

		[Name("spawned-env")]
		SpawnedEnv,

		Static,

		Auction,

		Store,

		Camp,

		[Name("party-camp")]
		PartyCamp,

		Roulette,

		[Name("field-boss")]
		FieldBoss,

		Gather,

		Craft,

		[Name("gather-env")]
		GatherEnv,

		Heart,

		[Name("enter-arena")]
		EnterArena,

		[Name("weapon-box")]
		WeaponBox,

		Refiner,

		[Name("dungeon-3")]
		Dungeon3,

		[Name("dungeon-4")]
		Dungeon4,

		[Name("dungeon-5")]
		Dungeon5,

		[Name("raid-dungeon")]
		RaidDungeon,

		[Name("classic-field")]
		ClassicField,

		[Name("faction-battle-field")]
		FactionBattleField,

		[Name("guild-battle-field")]
		GuildBattleField,

		[Name("party-battle-startpoint")]
		PartyBattleStartpoint,

		[Name("party-battle-enemy")]
		PartyBattleEnemy,

		[Name("fishing-field")]
		FishingField,
	}


	[Name("map-depth")]
	public MapDepthSeq MapDepth;

	[Name("arena-dungeon-map-depth")]
	public MapDepthSeq ArenaDungeonMapDepth;
	public enum MapDepthSeq : byte
	{
		N1,

		N2,

		N3,

		N4,

		N5,
	}



	public bool Zoom;

	public bool Rotate;

	public bool Click;

	public bool Front;

	[Name("show-tooltip")]
	public bool ShowTooltip;

	public Ref<Text> Name2;

	public short Opacity;

	[Name("size-x")]
	public short SizeX;

	[Name("size-y")]
	public short SizeY;

	[Name("oufofsight-size-x")]
	public short OufofsightSizeX;

	[Name("oufofsight-size-y")]
	public short OufofsightSizeY;

	public string Imageset;

	[Name("over-imageset")]
	public string OverImageset;

	[Name("pressed-imageset")]
	public string PressedImageset;

	[Name("outofsight-imageset")]
	public string OutofsightImageset;

	[Name("outofsight-over-imageset")]
	public string OutofsightOverImageset;

	[Name("outofsight-pressed-imageset")]
	public string OutofsightPressedImageset;

	[Name("center-pos-x")]
	public float CenterPosX;

	[Name("center-pos-y")]
	public float CenterPosY;


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