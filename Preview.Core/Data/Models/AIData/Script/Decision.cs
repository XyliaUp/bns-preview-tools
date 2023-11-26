using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class Decision : Record
{
    public List<FilterSet> FilterSet { get; set; }
    public List<ReactionSet> ReactionSet { get; set; }

	#region Sub
	public class Default : Decision
	{
		public EventType Event;
		public enum EventType
		{
			/// <summary>
			/// 活动结束
			/// </summary>
			[Name("act-finished")]
			ActFinished,

			/// <summary>
			/// 活动暂停
			/// </summary>
			[Name("act-paused")]
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
			[Name("convoy-failed")]
			ConvoyFailed,

			/// <summary>
			/// 造成伤害Event
			/// </summary>
			Damage,

			/// <summary>
			/// 被伤害Event
			/// </summary>
			Damaged,

			[Name("detect-creature")]
			DetectCreature,


			[Name("gp-section-change")]
			GpSectionChange,


			/// <summary>
			/// 获得效果后Event
			/// </summary>
			[Name("invoked-effect")]
			InvokedEffect,

			/// <summary>
			/// 获得效果时Event
			/// </summary>
			[Name("invoke-effect")]
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
			[Name("link-laser-target-terminate")]
			LinkLaserTargetTerminate,



			#region BOSS 类型 
			[Name("boss-bleeding")]
			BossBleeding,

			/// <summary>
			/// 仇恨列表清空Event
			/// </summary>
			[Name("boss-hate-list-emptied")]
			BossHateListEmptied,
			#endregion

			#region NPC 类型
			[Name("npc-bleeding")]
			NpcBleeding,

			/// <summary>
			/// 战斗开始Event
			/// </summary>
			[Name("npc-combat-start")]
			NpcCombatStart,

			/// <summary>
			/// NPC战斗结束Event（包含死亡）
			/// </summary>
			[Name("npc-combat-end")]
			NpcCombatEnd,

			/// <summary>
			/// NPC操作开始
			/// </summary>
			[Name("npc-manipulate-start")]
			NpcManipulateStart,

			/// <summary>
			/// NPC操作结束
			/// </summary>
			[Name("npc-manipulate-end")]
			NpcManipulateEnd,

			/// <summary>
			/// NPC刷新Event
			/// </summary>
			[Name("npc-spawn")]
			NpcSpawn,

			/// <summary>
			/// NPC消除Event
			/// </summary>
			[Name("npc-despawn")]
			NpcDespawn,




			/// <summary>
			/// NPC特殊Event<br/>通过Combat中的 event-step 触发
			/// </summary>
			[Name("npc-special")]
			NpcSpecial,
			#endregion


			/// <summary>
			/// 挑战模式阶段结束
			/// </summary>
			[Name("pattern-finish")]
			PatternFinish,

			/// <summary>
			/// 停止动画Event
			/// </summary>
			[Name("stop-cinematic")]
			StopCinematic,


			#region Env
			[Name("env-enable-change")]
			EnvEnableChange,

			[Name("env-entered")]
			EnvEntered,

			[Name("env-leaved")]
			EnvLeaved,

			[Name("env-manipulated")]
			EnvManipulated,

			[Name("env-manipulate-start")]
			EnvManipulateStart,

			[Name("env-manipulate-end")]
			EnvManipulateEnd,

			[Name("env-state-change")]
			EnvStateChange,
			#endregion

			#region 区域类型
			[Name("enter-zone")]
			EnterZone,


			[Name("leave-zone")]
			LeaveZone,

			[Name("zone-mode-changed")]
			ZoneModeChanged,

			[Name("zone-open")]
			ZoneOpen,

			[Name("zone-in-creature-detect")]
			ZoneInCreatureDetect,

			[Name("zone-out-creature-detect")]
			ZoneOutCreatureDetect,

			[Name("stop-zone-creature-detect")]
			StopZoneCreatureDetect,
			#endregion
		}


		public bool Forward;
	}

	public sealed class QuestDecision : Decision
	{
		public string Alias;

		//recation  mission-step-completed 	mission-step-skipped  mission-step-failed
	}
	#endregion
}