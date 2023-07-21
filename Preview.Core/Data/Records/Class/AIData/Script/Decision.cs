using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
public class Decision : BaseRecord
{
    [Signal("filter-set")]
    public List<FilterSet> FilterSet;

    [Signal("reaction-set")]
    public List<ReactionSet> ReactionSet;


	#region Sub
	public class Default : Decision
	{
		public EventType Event;
		public enum EventType
		{
			/// <summary>
			/// 活动结束
			/// </summary>
			[Signal("act-finished")]
			ActFinished,

			/// <summary>
			/// 活动暂停
			/// </summary>
			[Signal("act-paused")]
			ActPaused,


			/// <summary>
			/// 攻击Event
			/// </summary>
			Attack,

			/// <summary>
			/// 被攻击Event
			/// </summary>
			Attacked,

			/// <summary>
			/// 护送失败Event
			/// </summary>
			[Signal("convoy-failed")]
			ConvoyFailed,

			/// <summary>
			/// 造成伤害Event
			/// </summary>
			Damage,

			/// <summary>
			/// 被伤害Event
			/// </summary>
			Damaged,

			[Signal("detect-creature")]
			DetectCreature,


			[Signal("gp-section-change")]
			GpSectionChange,


			/// <summary>
			/// 获得效果后Event
			/// </summary>
			[Signal("invoked-effect")]
			InvokedEffect,

			/// <summary>
			/// 获得效果时Event
			/// </summary>
			[Signal("invoke-effect")]
			InvokeEffect,


			/// <summary>
			/// 击杀Event
			/// </summary>
			Kill,

			/// <summary>
			/// 被击杀Event
			/// </summary>
			Killed,

			/// <summary>
			/// 连线结束Event
			/// </summary>
			[Signal("link-laser-target-terminate")]
			LinkLaserTargetTerminate,



			#region BOSS 类型 
			[Signal("boss-bleeding")]
			BossBleeding,

			/// <summary>
			/// 仇恨列表清空Event
			/// </summary>
			[Signal("boss-hate-list-emptied")]
			BossHateListEmptied,
			#endregion

			#region NPC 类型
			[Signal("npc-bleeding")]
			NpcBleeding,

			/// <summary>
			/// 战斗开始Event
			/// </summary>
			[Signal("npc-combat-start")]
			NpcCombatStart,

			/// <summary>
			/// NPC战斗结束Event（包含死亡）
			/// </summary>
			[Signal("npc-combat-end")]
			NpcCombatEnd,

			/// <summary>
			/// NPC操作开始
			/// </summary>
			[Signal("npc-manipulate-start")]
			NpcManipulateStart,

			/// <summary>
			/// NPC操作结束
			/// </summary>
			[Signal("npc-manipulate-end")]
			NpcManipulateEnd,

			/// <summary>
			/// NPC刷新Event
			/// </summary>
			[Signal("npc-spawn")]
			NpcSpawn,

			/// <summary>
			/// NPC消除Event
			/// </summary>
			[Signal("npc-despawn")]
			NpcDespawn,




			/// <summary>
			/// NPC特殊Event<br/>通过Combat中的 event-step 触发
			/// </summary>
			[Signal("npc-special")]
			NpcSpecial,
			#endregion


			/// <summary>
			/// 挑战模式阶段结束
			/// </summary>
			[Signal("pattern-finish")]
			PatternFinish,

			/// <summary>
			/// 停止动画Event
			/// </summary>
			[Signal("stop-cinematic")]
			StopCinematic,


			#region Env
			[Signal("env-enable-change")]
			EnvEnableChange,

			[Signal("env-entered")]
			EnvEntered,

			[Signal("env-leaved")]
			EnvLeaved,

			[Signal("env-manipulated")]
			EnvManipulated,

			[Signal("env-manipulate-start")]
			EnvManipulateStart,

			[Signal("env-manipulate-end")]
			EnvManipulateEnd,

			[Signal("env-state-change")]
			EnvStateChange,
			#endregion

			#region 区域类型
			[Signal("enter-zone")]
			EnterZone,


			[Signal("leave-zone")]
			LeaveZone,

			[Signal("zone-mode-changed")]
			ZoneModeChanged,

			[Signal("zone-open")]
			ZoneOpen,

			[Signal("zone-in-creature-detect")]
			ZoneInCreatureDetect,

			[Signal("zone-out-creature-detect")]
			ZoneOutCreatureDetect,

			[Signal("stop-zone-creature-detect")]
			StopZoneCreatureDetect,
			#endregion
		}


		public bool Forward;
	}

	[Signal("quest-decision")]
	public sealed class QuestDecision : Decision
	{
		public string Alias;

		//recation  mission-step-completed 	mission-step-skipped  mission-step-failed
	}
	#endregion
}