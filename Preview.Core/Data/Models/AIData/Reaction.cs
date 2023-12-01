using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public abstract class Reaction : Record
{
	public sbyte Probability;


	#region Sub
	public sealed class ActResume : Reaction
	{
		public Script_obj Target;
	}

	public sealed class AcquireFieldItem : Reaction
	{
		public Script_obj Target;
		public Ref<FieldItem> FieldItem;
	}
	public sealed class RemoveFieldItem : Reaction
	{
		public Script_obj Target;

		[Repeat(4)]
		public string Spawn;
	}


	public sealed class ActivateTeleport : Reaction
	{
		public Script_obj Target;

		public Ref<Teleport> Teleport;
	}
	public sealed class DeactivateTeleport : Reaction
	{
		public Script_obj Target;

		public Ref<Teleport> Teleport;
	}



	public sealed class AddZoneScore : Reaction
	{
		public int Score;
	}

	public sealed class CopyNpcHate : Reaction
	{
		public Script_obj Opponent;

		public Script_obj Target;
	}


	public sealed class Damage : Reaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;


		public long Amount;

		public sbyte Percent;
	}


	public sealed class DebugPrint : Reaction
	{
		public string text;

		[Repeat(8)]
		public Script_obj[] Param;
	}
	public sealed class DebugTrace : Reaction
	{
		public string Text;
	}


	public sealed class DespawnNpc : Reaction
	{
		[Repeat(10)] public Script_obj Target;
		[Repeat(10)] public Ref<ZonePcSpawn> Spawn;


		public Msec DespawnDelay;
		public bool DespawnForce;
		public bool RespawnAfterDespawn;

		[Repeat(10)]
		public Ref<Social> DespawnSocial;
	}
	public sealed class DespawnNpcGroups : Reaction
	{
		[Repeat(10)] public Script_obj[] Target;
		[Repeat(10)] public Script_obj[] Group;

		public Msec DespawnDelay;
		public bool DespawnForce;
		public bool RespawnAfterDespawn;

		[Repeat(10)]
		public Ref<Social> DespawnSocial;
	}
	public sealed class DespawnNpcIndex : Reaction
	{
		public Script_obj Group;
		[Repeat(15)] public sbyte[] Index;


		public Msec DespawnDelay;
		public bool DespawnForce;
		public bool RespawnAfterDespawn;

		[Repeat(10)]
		public Ref<Social> DespawnSocial;
	}

	public sealed class DiffNpcHate : Reaction
	{
		public Script_obj Opponent;

		public Script_obj Target;

		public int Amount;
	}
	public sealed class DiffNpcNumber : SetNpcNumber
	{
		
	}
	public sealed class DiffPartyNumber : SetNpcNumber
	{

	}

	public sealed class DispelBuff : Reaction
	{
		public Script_obj Target;
	}
	public sealed class DispelByAttr : Reaction
	{
		public Script_obj Target;

		public EffectAttributeSeq Attr;
	}
	public sealed class DispelByType : Reaction
	{
		public Script_obj Target;
		public Script_obj From;
		public Sub<Effect> EffectType;

		public bool DispelForce;
	}
	public sealed class DispelDebuff : Reaction
	{
		public Script_obj Target;
	}

	public sealed class Heal : Reaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;


		public sbyte Percent;
	}
	public sealed class HealMax : Reaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;
	}

	public sealed class InOutDetectStart : Reaction
	{
		public Script_obj RefObject;
		public Ref<ZoneArea> RefArea;

		[Repeat(4)]
		public Ref<Filter> Filter;

		public Msec Duration;
		public Distance Radius;

		public RefTypeSeq RefType;
		public enum RefTypeSeq
		{
			Area,

			Object,
		}


		public Script_obj Subscriber;

		public sbyte GatherCount;

		public sbyte Index;
	}

	public sealed class InOutDetectStop : Reaction
	{
		public sbyte Index;
	}


	public sealed class InvokeEffect : Reaction
	{
		public Script_obj Target;
		public Script_obj From;

		/// <summary>
		/// SoulMask 类型效果必须设置
		/// </summary>
		public Script_obj Caster;
		public Script_obj Invoker;

		public Ref<Effect> Effect;

		public bool Immediately;

	}

	public sealed class Kill : Reaction
	{
		[Repeat(4)]
		public Script_obj Target;
	}

	/// <summary>
	/// 使用指定的特殊战斗序列
	/// </summary>
	public sealed class NpcFireSpecial : Reaction
	{
		public Script_obj Target;
		public Script_obj Requester;

		public sbyte SpecialId;
	}

	public sealed class NpcTalkFinish : Reaction
	{
		public Script_obj Npc;
	}

	/// <summary>
	/// 挑战模式阶段开始
	/// </summary>
	public sealed class PatternStart : Reaction
	{
		public sbyte Index;
	}

	/// <summary>
	/// 挑战模式阶段成功
	/// </summary>
	public sealed class PatternSuccess : Reaction
	{
		public sbyte Index;
	}

	public sealed class PlayCinematic : Reaction
	{
		public Ref<Cinematic> Cinematic;

		public Sight sight;

		public Script_obj To;

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
		public Script_obj From;
		public Script_obj To;

		public sbyte Social;
		public Msec PlaySocialDelay;
	}
	public sealed class PlaySocial : Reaction
	{
		public Script_obj From;
		public Script_obj To;

		public Ref<Social> Social;
		public Msec PlaySocialDelay;
	}
	public sealed class PlaySurroundSocial : Reaction
	{
		public Script_obj From;
		public Script_obj To;

		public Ref<Social> Social;
		public Msec PlaySocialDelay;
	}

	public sealed class ResetNpcAllHate : Reaction
	{
		public Script_obj Target;

		[Repeat(4)]
		public Script_obj Group;
	}
	public sealed class ResetNpcHate : Reaction
	{
		public Script_obj Opponent;

		public Script_obj Target;
	}
	public sealed class ResetStage : Reaction
	{
		public Script_obj Target;

		public Ref<NpcResetStage> resetStage;
	}
	public sealed class ResetZoneObject : Reaction
	{
		public sbyte zreg;
	}
	public sealed class SetEnvEnable : Reaction
	{
		[Obsolete]
		public Script_obj Target;

		[Obsolete]
		public Script_obj Target1;


		public Script_obj Target2;


		public bool Enable;

		public bool State;
	}
	public sealed class SetEnvInitEnable : Reaction
	{
		[Obsolete]
		public Script_obj Target1;

		public Script_obj Target2;

		public bool InitEnable;
	}
	public sealed class SetEnvState : Reaction
	{
		[Obsolete]
		public Script_obj Target;

		public Script_obj Target2;

		[Obsolete]
		public EnvState State;

		public EnvState State2;
	}
	public sealed class SetNpcAct : Reaction
	{
		public Script_obj Target;

		public Ref<ActSequence> Seq;
	}
	public sealed class SetNpcAttackable : Reaction
	{
		public Script_obj Target;

		public bool Flag;
	}
	public sealed class SetNpcBrain : Reaction
	{
		public Script_obj Npc;
		public Script_obj Target;


		public bool Attackable;
		public bool HateOn;
		public NpcBrain Brain;
	}
	public sealed class SetNpcCombatMode : Reaction
	{
		public Script_obj Target;
		public bool CombatMode;
		public Script_obj AttackTarget;

		public bool Attackable;
		public bool HateOn;
		public NpcBrain Brain;
	}
	public sealed class SetNpcFollow : Reaction
	{
		public Script_obj Master;

		public Script_obj Npc;

		public Detect Detect;
	}
	public sealed class SetNpcHateOn : Reaction
	{
		public Script_obj Target;

		public bool Flag;
	}
	public sealed class SetNpcIndexedAct : Reaction
	{
		public sbyte SeqIdx;

		public Script_obj Target;
	}
	public sealed class SetNpcInteractive : Reaction
	{
		public Script_obj Target;

		public bool Flag;
	}
	public class SetNpcNumber : Reaction
	{
		public Script_obj Target;

		public sbyte Reg;

		public int Amount;
	}
	public sealed class SetPartyNumber : Reaction
	{
		public Script_obj Target;

		public sbyte Reg;

		public int Amount;
	}
	public sealed class SetPartyObject : Reaction
	{
		public sbyte Reg;

		/// <summary>
		/// NPC队伍
		/// </summary>
		public Script_obj Target;

		/// <summary>
		/// 设置指向对象
		/// </summary>
		public Script_obj Object;
	}
	public sealed class SetPublicRaidEvent : Reaction
	{
		public Ref<PublicRaidEvent> PublicRaidEvent;
	}
	public sealed class SetUndying : Reaction
	{
		public Script_obj Target;

		public bool Undying;

		public bool Flag;
	}

	public sealed class SetZoneObject : Reaction
	{
		public Script_obj Object;

		public sbyte Zreg;
	}

	public sealed class SpawnFieldItem : Reaction
	{
		public Ref<ZoneArea> Area;

		public Ref<FieldItem> FieldItem;
	}


	public abstract class SpawnNpcBase : Reaction
	{
		public Script_obj AttackTarget;

		public Msec SpawnDelay;
		public bool SpawnForce;

		public Ref<ZoneArea> SpawnArea;
		public short SpawnAngleTheta;
		public bool UseSpawnTargetYaw;


		public Script_obj SpawnTarget;

		public Distance SpawnRadius;
		public Distance SpawnRadiusDiff;

		public Ref<Social> SpawnSocial;


		public bool Attackable;
		public bool HateOn;
		public NpcBrain Brain;
	}
	public sealed class SpawnNpc : SpawnNpcBase
	{
		[Repeat(10)] public Script_obj[] Target;
		[Repeat(10)] public string[] Spawn;
	}
	public sealed class SpawnNpcGroups : SpawnNpcBase
	{
		[Repeat(10)]
		public Script_obj[] Group;
	}
	public sealed class SpawnNpcIndex : SpawnNpcBase
	{
		public Script_obj Target;
		public Script_obj Party;
		public Script_obj Group;

		[Repeat(15)]
		public sbyte[] Index;
	}
	public sealed class SpawnRandomNpc : SpawnNpcBase
	{
		public Script_obj Group;

		public sbyte Min;
		public sbyte Max;
		public sbyte Start;
		public sbyte End;
	}
	public sealed class SpawnRandomNpcGroup : SpawnNpcBase
	{
		[Repeat(10)] public Script_obj[] Group;
		[Repeat(10)] public sbyte[] Prob;
	}


	/// <summary>
	/// 对于 manual 条件序列, 指示转移到下一个战斗序列
	/// </summary>
	public class TransitNpcCombat : Reaction
	{
		public Script_obj Target;

		[Name("transit-cond-idx")]
		public sbyte TransitCondIdx;

		public bool Immediately;
	}

	public sealed class TransitNpcCombatIndex : TransitNpcCombat
	{
		/// <summary>
		/// 转移战斗序列索引 (智力参数索引)   索引从1开始
		/// </summary>
		public sbyte Index;
	}

	public class Warp : Reaction
	{
		public Script_obj Target;

		public Ref<Zone> Zone;

		/// <summary>
		/// 转移区域 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		public Ref<ZoneArea> Area;

		/// <summary>
		/// 转移玩家刷新点 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		public sbyte PcSpawn;


		#region 可缺省字段
		public Ref<Cinematic> EnterCinematic;
		public Ref<Cinematic> LeaveCinematic;

		[Repeat(15)] public sbyte[] PhaseZonePcSpawn;
		[Repeat(15)] public Ref<Cinematic>[] PhaseZoneEnterCinematic;
		[Repeat(15)] public Ref<Cinematic>[] PhaseZoneLeaveCinematic;
		#endregion
	}

	public sealed class WarpParty : Warp
	{

	}

	public sealed class WarpToReentrance : Reaction
	{
		public Script_obj Target;
	}
	#endregion
}