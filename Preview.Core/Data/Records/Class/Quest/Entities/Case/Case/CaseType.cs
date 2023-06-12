using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData
{
	public enum CaseType
	{
		[Signal("talk")]
		Talk,

		[Signal("talk-to-item")]
		TalkToItem,

		[Signal("talk-to-self")]
		TalkToSelf,

		[Signal("manipulate")]
		Manipulate,

		/// <summary>
		/// 操作 NPC
		/// </summary>
		[Signal("npc-manipulate")]
		NpcManipulate,

		[Signal("approach")]
		Approach,

		Skill,

		/// <summary>
		/// 拾取
		/// </summary>
		Loot,

		[Signal("killed")]
		Killed,

		[Signal("finish-blow")]
		FinishBlow,

		[Signal("env-entered")]
		EnvEntered,

		[Signal("enter-zone")]
		EnterZone,

		[Signal("convoy-arrived")]
		ConvoyArrived,

		[Signal("convoy-failed")]
		ConvoyFailed,

		[Signal("npc-bleeding-occured")]
		NpcBleedingOccured,

		/// <summary>
		/// 进入传送门
		/// </summary>
		[Signal("enter-portal")]
		EnterPortal,

		/// <summary>
		/// 获得召唤兽（召唤师职业专用）
		/// </summary>
		[Signal("acquire-summoned")]
		AcquireSummoned,

		[Signal("pc-social")]
		PcSocial,

		[Signal("join-faction")]
		JoinFaction,

		[Signal("duel-finish")]
		DuelFinish,

		[Signal("party-battle")]
		PartyBattle,

		[Signal("party-battle-action")]
		PartyBattleAction,

		/// <summary>
		/// 完成指定任务
		/// </summary>
		[Signal("complete-quest")]
		CompleteQuest,

		[Signal("pick-up-fielditem")]
		PickUpFielditem,

		[Signal("battle-royal")]
		BattleRoyal,

		[Signal("attraction-popup")]
		AttractionPopup,


		[Signal("public-raid")]
		PublicRaid
	}
}