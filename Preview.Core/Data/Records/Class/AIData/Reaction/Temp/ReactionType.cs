using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;
public enum ReactionType
{
	[Signal("act-resume")]
	ActResume,

	[Signal("copy-npc-hate")]
	CopyNpcHate,

	[Signal("damage")]
	Damage,

	[Signal("debug-print")]
	DebugPrint,

	[Signal("debug-trace")]
	DebugTrace,


	[Signal("despawn-npc")]
	DespawnNpc,

	[Signal("despawn-npc-groups")]
	DespawnNpcGroups,

	[Signal("despawn-npc-index")]
	DespawnNpcIndex,


	[Signal("diff-npc-hate")]
	DiffNpcHate,

	[Signal("diff-npc-number")]
	DiffNpcNumber,

	[Signal("diff-party-number")]
	DiffPartyNumber,


	[Signal("dispel-by-attr")]
	DispelByAttr,

	[Signal("dispel-by-type")]
	DispelByType,

	[Signal("dispel-buff")]
	DispelBuff,

	[Signal("dispel-debuff")]
	DispelDebuff,


	#region	Env
	[Signal("set-env-enable")]
	SetEnvEnable,

	[Signal("set-env-init-enable")]
	SetEnvInitEnable,

	[Signal("set-env-state")]
	SetEnvState,
	#endregion


	#region	FieldItem
	[Signal("acquire-field-item")]
	AcquireFieldItem,

	[Signal("remove-field-item")]
	RemoveFieldItem,

	[Signal("spawn-field-item")]
	SpawnFieldItem,
	#endregion

	#region Heal
	[Signal("heal")]
	Heal,

	[Signal("heal-max")]
	HealMax,
	#endregion


	[Signal("in-out-detect-start")]
	InOutDetectStart,

	[Signal("in-out-detect-stop")]
	InOutDetectStop,


	[Signal("invoke-effect")]
	InvokeEffect,


	[Signal("kill")]
	Kill,


	[Signal("npc-fire-special")]
	NpcFireSpecial,

	[Signal("npc-talk-finish")]
	NpcTalkFinish,


	[Signal("pattern-start")]
	PatternStart,

	[Signal("pattern-success")]
	PatternSuccess,



	[Signal("play-cinematic")]
	PlayCinematic,




	[Signal("play-indexed-social")]
	PlayIndexedSocial,

	[Signal("play-social")]
	PlaySocial,

	[Signal("play-surround-social")]
	PlaySurroundSocial,

	[Signal("reset-npc-all-hate")]
	ResetNpcAllHate,

	[Signal("reset-npc-hate")]
	ResetNpcHate,

	[Signal("reset-stage")]
	ResetStage,

	[Signal("set-public-raid-event")]
	SetPublicRaidEvent,

	[Signal("set-npc-act")]
	SetNpcAct,

	[Signal("set-npc-attackable")]
	SetNpcAttackable,

	[Signal("set-npc-brain")]
	SetNpcBrain,

	[Signal("set-npc-combat-mode")]
	SetNpcCombatMode,

	[Signal("set-npc-follow")]
	SetNpcFollow,

	[Signal("set-npc-hate-on")]
	SetNpcHateOn,

	[Signal("set-npc-indexed-act")]
	SetNpcIndexedAct,

	[Signal("set-npc-interactive")]
	SetNpcInteractive,

	[Signal("set-npc-number")]
	SetNpcNumber,


	[Signal("set-party-number")]
	SetPartyNumber,

	[Signal("set-party-object")]
	SetPartyObject,

	[Signal("set-undying")]
	SetUndying,


	[Signal("spawn-npc")]
	SpawnNpc,

	[Signal("spawn-npc-groups")]
	SpawnNpcGroups,

	[Signal("spawn-npc-index")]
	SpawnNpcIndex,

	[Signal("spawn-random-npc")]
	SpawnRandomNpc,

	[Signal("spawn-random-npc-group")]
	SpawnRandomNpcGroup,



	#region Teleport
	[Signal("activate-teleport")]
	ActivateTeleport,

	[Signal("deactivate-teleport")]
	DeactivateTeleport,
	#endregion



	[Signal("transit-npc-combat")]
	TransitNpcCombat,

	/// <summary>
	/// 转移战斗序列
	/// </summary>
	[Signal("transit-npc-combat-index")]
	TransitNpcCombatIndex,


	#region Warp
	[Signal("warp")]
	Warp,

	[Signal("warp-party")]
	WarpParty,

	[Signal("warp-to-reentrance")]
	WarpToReentrance,
	#endregion

	#region ZoneObject
	[Signal("reset-zone-object")]
	ResetZoneObject,

	[Signal("set-zone-object")]
	SetZoneObject,
	#endregion

	[Signal("add-zone-score")]
	AddZoneScore,



	// ******
	[Signal("reset-zone-timer")]
	ResetZoneTimer,

	[Signal("set-zone-timer")]
	SetZoneTimer,

	[Signal("diff-faction-reputation")]
	DiffFactionReputation,

	[Signal("reference-npc-hate-start")]
	ReferenceNpcHateStart,

	[Signal("reference-npc-hate-end")]
	ReferenceNpcHateEnd,

	[Signal("request-clan-help")]
	RequestClanHelp,

	[Signal("set-npc-script")]
	SetNpcScript,

	[Signal("transit-npc-berserk")]
	TransitNpcBerserk,
}