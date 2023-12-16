using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public abstract class Reaction : ModelElement
{
	public sbyte Probability { get; set; }


	#region Sub
	public sealed class ActResume : Reaction
	{
		public Script_obj Target { get; set; }
	}

	public sealed class AcquireFieldItem : Reaction
	{
		public Script_obj Target { get; set; }
		public Ref<FieldItem> FieldItem { get; set; }
	}
	public sealed class RemoveFieldItem : Reaction
	{
		public Script_obj Target { get; set; }

		[Repeat(4)]
		public string Spawn { get; set; }
	}


	public sealed class ActivateTeleport : Reaction
	{
		public Script_obj Target { get; set; }

		public Ref<Teleport> Teleport { get; set; }
	}
	public sealed class DeactivateTeleport : Reaction
	{
		public Script_obj Target { get; set; }

		public Ref<Teleport> Teleport { get; set; }
	}



	public sealed class AddZoneScore : Reaction
	{
		public int Score { get; set; }
	}

	public sealed class CopyNpcHate : Reaction
	{
		public Script_obj Opponent { get; set; }

		public Script_obj Target { get; set; }
	}


	public sealed class Damage : Reaction
	{
		[Obsolete]
		public Script_obj Target { get; set; }

		public Script_obj Target2 { get; set; }


		public long Amount { get; set; }

		public sbyte Percent { get; set; }
	}


	public sealed class DebugPrint : Reaction
	{
		public string text { get; set; }

		[Repeat(8)]
		public Script_obj[] Param { get; set; }
	}
	public sealed class DebugTrace : Reaction
	{
		public string Text { get; set; }
	}


	public sealed class DespawnNpc : Reaction
	{
		[Repeat(10)] public Script_obj Target { get; set; }
		[Repeat(10)] public Ref<ZonePcSpawn> Spawn { get; set; }


		public Msec DespawnDelay { get; set; }
		public bool DespawnForce { get; set; }
		public bool RespawnAfterDespawn { get; set; }

		[Repeat(10)]
		public Ref<Social> DespawnSocial { get; set; }
	}
	public sealed class DespawnNpcGroups : Reaction
	{
		[Repeat(10)] public Script_obj[] Target { get; set; }
		[Repeat(10)] public Script_obj[] Group { get; set; }

		public Msec DespawnDelay { get; set; }
		public bool DespawnForce { get; set; }
		public bool RespawnAfterDespawn { get; set; }

		[Repeat(10)]
		public Ref<Social> DespawnSocial { get; set; }
	}
	public sealed class DespawnNpcIndex : Reaction
	{
		public Script_obj Group { get; set; }
		[Repeat(15)] public sbyte[] Index { get; set; }


		public Msec DespawnDelay { get; set; }
		public bool DespawnForce { get; set; }
		public bool RespawnAfterDespawn { get; set; }

		[Repeat(10)]
		public Ref<Social> DespawnSocial { get; set; }
	}

	public sealed class DiffNpcHate : Reaction
	{
		public Script_obj Opponent { get; set; }

		public Script_obj Target { get; set; }

		public int Amount { get; set; }
	}
	public sealed class DiffNpcNumber : SetNpcNumber
	{
		
	}
	public sealed class DiffPartyNumber : SetNpcNumber
	{

	}

	public sealed class DispelBuff : Reaction
	{
		public Script_obj Target { get; set; }
	}
	public sealed class DispelByAttr : Reaction
	{
		public Script_obj Target { get; set; }

		public EffectAttributeSeq Attr { get; set; }
	}
	public sealed class DispelByType : Reaction
	{
		public Script_obj Target { get; set; }
		public Script_obj From { get; set; }

		public bool DispelForce { get; set; }
	}
	public sealed class DispelDebuff : Reaction
	{
		public Script_obj Target { get; set; }
	}

	public sealed class Heal : Reaction
	{
		[Obsolete]
		public Script_obj Target { get; set; }

		public Script_obj Target2 { get; set; }


		public sbyte Percent { get; set; }
	}
	public sealed class HealMax : Reaction
	{
		[Obsolete]
		public Script_obj Target { get; set; }

		public Script_obj Target2 { get; set; }
	}

	public sealed class InOutDetectStart : Reaction
	{
		public Script_obj RefObject { get; set; }
		public Ref<ZoneArea> RefArea { get; set; }

		[Repeat(4)]
		public Ref<Filter> Filter { get; set; }

		public Msec Duration { get; set; }
		public Distance Radius { get; set; }

		public RefTypeSeq RefType { get; set; }
		public enum RefTypeSeq
		{
			Area,

			Object,
		}


		public Script_obj Subscriber { get; set; }

		public sbyte GatherCount { get; set; }

		public sbyte Index { get; set; }
	}

	public sealed class InOutDetectStop : Reaction
	{
		public sbyte Index { get; set; }
	}


	public sealed class InvokeEffect : Reaction
	{
		public Script_obj Target { get; set; }
		public Script_obj From { get; set; }

		/// <summary>
		/// SoulMask 类型效果必须设置
		/// </summary>
		public Script_obj Caster { get; set; }
		public Script_obj Invoker { get; set; }

		public Ref<Effect> Effect { get; set; }

		public bool Immediately { get; set; }

	}

	public sealed class Kill : Reaction
	{
		[Repeat(4)]
		public Script_obj Target { get; set; }
	}

	/// <summary>
	/// 使用指定的特殊战斗序列
	/// </summary>
	public sealed class NpcFireSpecial : Reaction
	{
		public Script_obj Target { get; set; }
		public Script_obj Requester { get; set; }

		public sbyte SpecialId { get; set; }
	}

	public sealed class NpcTalkFinish : Reaction
	{
		public Script_obj Npc { get; set; }
	}

	/// <summary>
	/// 挑战模式阶段开始
	/// </summary>
	public sealed class PatternStart : Reaction
	{
		public sbyte Index { get; set; }
	}

	/// <summary>
	/// 挑战模式阶段成功
	/// </summary>
	public sealed class PatternSuccess : Reaction
	{
		public sbyte Index { get; set; }
	}

	public sealed class PlayCinematic : Reaction
	{
		public Ref<Cinematic> Cinematic { get; set; }

		public Sight sight { get; set; }

		public Script_obj To { get; set; }

		public enum Sight
		{
			None,

			One,

			Party,

			Team,

			Zone,
		}
	}
	public sealed class PlayIndexedSocial : Reaction
	{
		public Script_obj From { get; set; }
		public Script_obj To { get; set; }

		public sbyte Social { get; set; }
		public Msec PlaySocialDelay { get; set; }
	}
	public sealed class PlaySocial : Reaction
	{
		public Script_obj From { get; set; }
		public Script_obj To { get; set; }

		public Ref<Social> Social { get; set; }
		public Msec PlaySocialDelay { get; set; }
	}
	public sealed class PlaySurroundSocial : Reaction
	{
		public Script_obj From { get; set; }
		public Script_obj To { get; set; }

		public Ref<Social> Social { get; set; }
		public Msec PlaySocialDelay { get; set; }
	}

	public sealed class ResetNpcAllHate : Reaction
	{
		public Script_obj Target { get; set; }

		[Repeat(4)]
		public Script_obj Group { get; set; }
	}
	public sealed class ResetNpcHate : Reaction
	{
		public Script_obj Opponent { get; set; }

		public Script_obj Target { get; set; }
	}
	public sealed class ResetStage : Reaction
	{
		public Script_obj Target { get; set; }

		public Ref<NpcResetStage> resetStage { get; set; }
	}
	public sealed class ResetZoneObject : Reaction
	{
		public sbyte zreg { get; set; }
	}
	public sealed class SetEnvEnable : Reaction
	{
		[Obsolete]
		public Script_obj Target { get; set; }

		[Obsolete]
		public Script_obj Target1 { get; set; }


		public Script_obj Target2 { get; set; }


		public bool Enable { get; set; }

		public bool State { get; set; }
	}
	public sealed class SetEnvInitEnable : Reaction
	{
		[Obsolete]
		public Script_obj Target1 { get; set; }

		public Script_obj Target2 { get; set; }

		public bool InitEnable { get; set; }
	}
	public sealed class SetEnvState : Reaction
	{
		[Obsolete]
		public Script_obj Target { get; set; }

		public Script_obj Target2 { get; set; }

		[Obsolete]
		public EnvState State { get; set; }

		public EnvState State2 { get; set; }
	}
	public sealed class SetNpcAct : Reaction
	{
		public Script_obj Target { get; set; }

		public Ref<ActSequence> Seq { get; set; }
	}
	public sealed class SetNpcAttackable : Reaction
	{
		public Script_obj Target { get; set; }

		public bool Flag { get; set; }
	}
	public sealed class SetNpcBrain : Reaction
	{
		public Script_obj Npc { get; set; }
		public Script_obj Target { get; set; }


		public bool Attackable { get; set; }
		public bool HateOn { get; set; }
		public NpcBrain Brain { get; set; }
	}
	public sealed class SetNpcCombatMode : Reaction
	{
		public Script_obj Target { get; set; }
		public bool CombatMode { get; set; }
		public Script_obj AttackTarget { get; set; }

		public bool Attackable { get; set; }
		public bool HateOn { get; set; }
		public NpcBrain Brain { get; set; }
	}
	public sealed class SetNpcFollow : Reaction
	{
		public Script_obj Master { get; set; }

		public Script_obj Npc { get; set; }

		public Detect Detect { get; set; }
	}
	public sealed class SetNpcHateOn : Reaction
	{
		public Script_obj Target { get; set; }

		public bool Flag { get; set; }
	}
	public sealed class SetNpcIndexedAct : Reaction
	{
		public sbyte SeqIdx { get; set; }

		public Script_obj Target { get; set; }
	}
	public sealed class SetNpcInteractive : Reaction
	{
		public Script_obj Target { get; set; }

		public bool Flag { get; set; }
	}
	public class SetNpcNumber : Reaction
	{
		public Script_obj Target { get; set; }

		public sbyte Reg { get; set; }

		public int Amount { get; set; }
	}
	public sealed class SetPartyNumber : Reaction
	{
		public Script_obj Target { get; set; }

		public sbyte Reg { get; set; }

		public int Amount { get; set; }
	}
	public sealed class SetPartyObject : Reaction
	{
		public sbyte Reg { get; set; }

		/// <summary>
		/// NPC队伍
		/// </summary>
		public Script_obj Target { get; set; }

		/// <summary>
		/// 设置指向对象
		/// </summary>
		public Script_obj Object { get; set; }
	}
	public sealed class SetPublicRaidEvent : Reaction
	{
		public Ref<PublicRaidEvent> PublicRaidEvent { get; set; }
	}
	public sealed class SetUndying : Reaction
	{
		public Script_obj Target { get; set; }

		public bool Undying { get; set; }

		public bool Flag { get; set; }
	}

	public sealed class SetZoneObject : Reaction
	{
		public Script_obj Object { get; set; }

		public sbyte Zreg { get; set; }
	}

	public sealed class SpawnFieldItem : Reaction
	{
		public Ref<ZoneArea> Area { get; set; }

		public Ref<FieldItem> FieldItem { get; set; }
	}


	public abstract class SpawnNpcBase : Reaction
	{
		public Script_obj AttackTarget { get; set; }

		public Msec SpawnDelay { get; set; }
		public bool SpawnForce { get; set; }

		public Ref<ZoneArea> SpawnArea { get; set; }
		public short SpawnAngleTheta { get; set; }
		public bool UseSpawnTargetYaw { get; set; }


		public Script_obj SpawnTarget { get; set; }

		public Distance SpawnRadius { get; set; }
		public Distance SpawnRadiusDiff { get; set; }

		public Ref<Social> SpawnSocial { get; set; }


		public bool Attackable { get; set; }
		public bool HateOn { get; set; }
		public NpcBrain Brain { get; set; }
	}
	public sealed class SpawnNpc : SpawnNpcBase
	{
		[Repeat(10)] public Script_obj[] Target { get; set; }
		[Repeat(10)] public string[] Spawn { get; set; }
	}
	public sealed class SpawnNpcGroups : SpawnNpcBase
	{
		[Repeat(10)]
		public Script_obj[] Group { get; set; }
	}
	public sealed class SpawnNpcIndex : SpawnNpcBase
	{
		public Script_obj Target { get; set; }
		public Script_obj Party { get; set; }
		public Script_obj Group { get; set; }

		[Repeat(15)]
		public sbyte[] Index { get; set; }
	}
	public sealed class SpawnRandomNpc : SpawnNpcBase
	{
		public Script_obj Group { get; set; }

		public sbyte Min { get; set; }
		public sbyte Max { get; set; }
		public sbyte Start { get; set; }
		public sbyte End { get; set; }
	}
	public sealed class SpawnRandomNpcGroup : SpawnNpcBase
	{
		[Repeat(10)] public Script_obj[] Group { get; set; }
		[Repeat(10)] public sbyte[] Prob { get; set; }
	}


	/// <summary>
	/// 对于 manual 条件序列, 指示转移到下一个战斗序列
	/// </summary>
	public class TransitNpcCombat : Reaction
	{
		public Script_obj Target { get; set; }

		[Name("transit-cond-idx")]
		public sbyte TransitCondIdx { get; set; }

		public bool Immediately { get; set; }
	}

	public sealed class TransitNpcCombatIndex : TransitNpcCombat
	{
		/// <summary>
		/// 转移战斗序列索引 (智力参数索引)   索引从1开始
		/// </summary>
		public sbyte Index { get; set; }
	}

	public class Warp : Reaction
	{
		public Script_obj Target { get; set; }

		public Ref<Zone> Zone { get; set; }

		/// <summary>
		/// 转移区域 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		public Ref<ZoneArea> Area { get; set; }

		/// <summary>
		/// 转移玩家刷新点 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		public sbyte PcSpawn { get; set; }


		#region 可缺省字段
		public Ref<Cinematic> EnterCinematic { get; set; }
		public Ref<Cinematic> LeaveCinematic { get; set; }

		[Repeat(15)] public sbyte[] PhaseZonePcSpawn { get; set; }
		[Repeat(15)] public Ref<Cinematic>[] PhaseZoneEnterCinematic { get; set; }
		[Repeat(15)] public Ref<Cinematic>[] PhaseZoneLeaveCinematic { get; set; }
		#endregion
	}

	public sealed class WarpParty : Warp
	{

	}

	public sealed class WarpToReentrance : Reaction
	{
		public Script_obj Target { get; set; }
	}
	#endregion
}