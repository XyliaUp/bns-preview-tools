using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.CombatSequenceData.Action;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.CombatSequenceData.Action
{
	public enum ActionType
	{
		[Signal("boss-gp-select-attack")]
		BossGpSelectAttack,

		[Signal("boss-sp-select-attack")]
		BossSpSelectAttack,

		[Signal("boss-link-laser-attack")]
		BossLinkLaserAttack,

		[Signal("boss-multiground-attack")]
		BossMultigroundAttack,

		[Signal("boss-repeater-attack")]
		BossRepeaterAttack,


		/// <summary>
		/// boss同时施展攻击
		/// </summary>
		[Signal("boss-simultaneous-caster-attack")]
		BossSimultaneousCasterAttack,

		/// <summary>
		/// boss同时场地攻击
		/// </summary>
		[Signal("boss-simultaneous-ground-attack")]
		BossSimultaneousGroundAttack,



		[Signal("change-set")]
		ChangeSet,

		[Signal("combat-move")]
		CombatMove,

		/// <summary>
		/// 前冲攻击
		/// </summary>
		[Signal("boss-rush-attack")]
		BossRushAttack = 9,


		[Signal("detect-creature")]
		DetectCreature,

		[Signal("do-social")]
		DoSocial,

		[Signal("do-indexed-social")]
		DoIndexedSocial,


		Select,

		Stay,

		/// <summary>
		/// 使用场地技能
		/// </summary>
		[Signal("use-ground-skill")]
		UseGroundSkill,

		[Signal("use-indexed-skill")]
		UseIndexedSkill,

		[Signal("gather-targets")]
		GatherTargets = 17,

		[Signal("use-skill")]
		UseSkill,

		[Signal("use-soul-npc-skill")]
		UseSoulNpcSkill,
	}

	public static class Factory
	{
		public static IAction ActionFactory(this XmlElement CaseNode) => CaseNode.TypeFactory<ActionType, IAction>(s => s switch
		{
			ActionType.BossGpSelectAttack => new BossGpSelectAttack(),
			ActionType.BossLinkLaserAttack => new BossLinkLaserAttack(),
			ActionType.BossMultigroundAttack => new BossMultigroundAttack(),
			ActionType.BossRepeaterAttack => new BossRepeaterAttack(),
			ActionType.BossRushAttack => new BossRushAttack(),
			ActionType.BossSimultaneousCasterAttack => new BossSimultaneousCasterAttack(),
			ActionType.BossSimultaneousGroundAttack => new BossSimultaneousGroundAttack(),
			ActionType.BossSpSelectAttack => new BossSpSelectAttack(),
			ActionType.ChangeSet => new ChangeSet(),
			ActionType.CombatMove => new CombatMove(),
			ActionType.DetectCreature => new DetectCreature(),
			ActionType.DoIndexedSocial => new DoIndexedSocial(),
			ActionType.DoSocial => new DoSocial(),
			ActionType.GatherTargets => new GatherTargets(),
			ActionType.Select => new Select(),
			ActionType.Stay => new Stay(),
			ActionType.UseGroundSkill => new UseGroundSkill(),
			ActionType.UseIndexedSkill => new UseIndexedSkill(),
			ActionType.UseSkill => new UseSkill(),
			ActionType.UseSoulNpcSkill => new UseSoulNpcSkill(),

			_ => null
		});
	}
}