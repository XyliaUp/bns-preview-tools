using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public class MapUnit : BaseRecord
{
	public short Mapid;

	[Signal("zone-id")]
	public int ZoneId;

	[Signal("is-phasing-unit")]
	public bool IsPhasingUnit;

	[Signal("position-x")]
	public float PositionX;

	[Signal("position-y")]
	public float PositionY;

	[Signal("position-z")]
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

		[Signal("revenge-enemy")]
		RevengeEnemy,

		Faction,

		[Signal("duel-enemy")]
		DuelEnemy,

		Quest,

		Npc,

		Env,

		Teleport,

		Airdash,

		Link,

		Convoy,

		[Signal("spawned-env")]
		SpawnedEnv,

		Static,

		Auction,

		Store,

		Camp,

		[Signal("party-camp")]
		PartyCamp,

		Roulette,

		[Signal("field-boss")]
		FieldBoss,

		Gather,

		Craft,

		[Signal("gather-env")]
		GatherEnv,

		Heart,

		[Signal("enter-arena")]
		EnterArena,

		[Signal("weapon-box")]
		WeaponBox,

		Refiner,

		[Signal("dungeon-3")]
		Dungeon3,

		[Signal("dungeon-4")]
		Dungeon4,

		[Signal("dungeon-5")]
		Dungeon5,

		[Signal("raid-dungeon")]
		RaidDungeon,

		[Signal("classic-field")]
		ClassicField,

		[Signal("faction-battle-field")]
		FactionBattleField,

		[Signal("guild-battle-field")]
		GuildBattleField,

		[Signal("party-battle-startpoint")]
		PartyBattleStartpoint,

		[Signal("party-battle-enemy")]
		PartyBattleEnemy,

		[Signal("fishing-field")]
		FishingField,
	}


	[Signal("map-depth")]
	public MapDepthSeq MapDepth;

	[Signal("arena-dungeon-map-depth")]
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

	[Signal("show-tooltip")]
	public bool ShowTooltip;

	public Text Name2;

	public short Opacity;

	[Signal("size-x")]
	public short SizeX;

	[Signal("size-y")]
	public short SizeY;

	[Signal("oufofsight-size-x")]
	public short OufofsightSizeX;

	[Signal("oufofsight-size-y")]
	public short OufofsightSizeY;

	public string Imageset;

	[Signal("over-imageset")]
	public string OverImageset;

	[Signal("pressed-imageset")]
	public string PressedImageset;

	[Signal("outofsight-imageset")]
	public string OutofsightImageset;

	[Signal("outofsight-over-imageset")]
	public string OutofsightOverImageset;

	[Signal("outofsight-pressed-imageset")]
	public string OutofsightPressedImageset;

	[Signal("center-pos-x")]
	public float CenterPosX;

	[Signal("center-pos-y")]
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