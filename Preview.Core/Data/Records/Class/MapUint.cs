
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class MapUnit : BaseRecord
	{
		public TypeSeq Type;


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

		[Signal("map-depth")]
		public MapDepthSeq MapDepth;

		[Signal("arena-dungeon-map-depth")]
		public MapDepthSeq ArenaDungeonMapDepth;


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



		#region Enums
		public enum TypeSeq
		{
			Static,

			/// <summary>
			/// 任务范围圈
			/// </summary>
			Quest,

			Link,

			Npc,

			Boss,

			Airdash,

			Env,

			Attraction,

			[Signal("npc-group")]
			NpcGroup,

			[Signal("guild-battle-field-portal")]
			GuildBattleFieldPortal,

			[Signal("party-battle-startpoint-alpha")]
			PartyBattleStartpointAlpha,

			[Signal("party-battle-startpoint-beta")]
			PartyBattleStartpointBeta,

			[Signal("fishing-field")]
			FishingField,
		}

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

		public enum MapDepthSeq : byte
		{
			 N1,

			 N2,

			 N3,

			 N4,

			 N5,
		}
		#endregion
	}
}