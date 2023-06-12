using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData
{
	public enum TutorialCaseType
	{
		[Signal("acquire-item")]
		AcquireItem,

		[Signal("equip-item")]
		EquipItem,

		[Signal("use-item")]
		UseItem,

		[Signal("grow-item")]
		GrowItem,

		[Signal("transform-item")]
		TransformItem,

		[Signal("pick-up-fielditem")]
		PickUpFielditem,

		[Signal("pick-down-fielditem")]
		PickDownFielditem,

		[Signal("targeting")]
		Targeting,

		[Signal("talk-start")]
		TalkStart,

		[Signal("window-open")]
		WindowOpen,

		[Signal("complete-self-revival")]
		CompleteSelfRevival,

		[Signal("npc-bleeding")]
		NpcBleeding,

		[Signal("pc-bleeding")]
		PcBleeding,

		[Signal("exhausted")]
		Exhausted,

		[Signal("attacked")]
		Attacked,

		[Signal("acquire-sp")]
		AcquireSp,

		[Signal("skill")]
		Skill,

		[Signal("skill-sequence")]
		SkillSequence,

		[Signal("skill-training")]
		SkillTraining,

		[Signal("quest-tracking-position")]
		QuestTrackingPosition,

		[Signal("repair-with-campfire")]
		RepairWithCampfire,

		[Signal("teleport")]
		Teleport,

		[Signal("expand-inventory")]
		ExpandInventory,

		[Signal("gem-compose")]
		GemCompose,

		[Signal("gem-decompose")]
		GemDecompose,

		[Signal("weapon-gem")]
		WeaponGem,

		[Signal("detach-weapon-gem")]
		DetachWeaponGem,

		Airdash,

		[Signal("enlarge-mini-map")]
		EnlargeMiniMap,

		[Signal("transparent-mini-map")]
		TransparentMiniMap,

		[Signal("resurrecting-summoned")]
		ResurrectingSummoned,

		[Signal("move-to-position")]
		MoveToPosition,

		[Signal("use-heart-count")]
		UseHeartCount,

		[Signal("charge-heart-count")]
		ChargeHeartCount,

		[Signal("teleport-zone")]
		TeleportZone,
	}
}