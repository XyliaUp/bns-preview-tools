using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.Data.Models.Sequence;
using CinematicModel = Xylia.Preview.Data.Models.Cinematic;
using FactionModel = Xylia.Preview.Data.Models.Faction;
using FieldItemModel = Xylia.Preview.Data.Models.FieldItem;
using Skill3Model = Xylia.Preview.Data.Models.Skill3;
using SkillModel = Xylia.Preview.Data.Models.Skill;

namespace Xylia.Preview.Data.Models;
public abstract class Filter : ModelElement
{
	public string Alias;

	public Script_obj Subject;
	public Script_obj Target;
	public Script_obj Subject2;
	public Script_obj Target2;

	#region Sub
	public sealed class Race : Filter
	{
		[Repeat(4)]
		public RaceSeq[] Value;

		public bool Either;
	}

	public sealed class Sex : Filter
	{
		[Repeat(4)]
		public SexSeq[] Value;

		public bool Either;
	}

	public sealed class Job : Filter
	{
		[Repeat(4)]
		public JobSeq[] Value;

		public bool Either;
	}

	public sealed class JobStyle : Filter
	{
		public sbyte Count;

		[Repeat(4)]
		public JobSeq[] job;

		[Repeat(4)]
		public JobStyleSeq[] jobStyle;

		public bool Either;
	}

	public sealed class Stance : Filter
	{
		[Repeat(4)]
		public StanceSeq[] Value;

		public bool Either;
	}

	public sealed class Prop : Filter
	{
		public CreatureField Field;

		public Op Op;

		public long Value;
	}

	public sealed class PropPercent : Filter
	{
		public CreatureField Field; 

		public Op Op;

		public sbyte Value;
	}

	public sealed class PropFlag : Filter
	{
		public CreatureField Field; 

		public Op Op;

		public bool Flag;
	}

	public sealed class EffectFlag : Filter
	{
		public Flag FlagType;

		public bool Flag;
	}

	public sealed class Faction : Filter
	{
		public Ref<FactionModel> Value;
	}

	public sealed class ActiveFaction : Filter
	{
		public Ref<FactionModel> Value;
	}

	public sealed class FactionReputation : Filter
	{
		public Op Op;

		public short Value;
	}

	public sealed class FactionLevel : Filter
	{
		public Op Op;

		public short Value;
	}

	public sealed class EffectAttribute : Filter
	{
		[Repeat(4)]
		public EffectAttributeSeq[] Value;

		public bool Either;
	}

	public sealed class WeaponType : Filter
	{
		[Repeat(4)]
		public Item.WeaponTypeSeq[] weaponType;

		public bool Either;
	}

	/// <summary>
	/// 背包中存在物品
	/// </summary>
	public sealed class Inventory : Filter
	{
		public Ref<Item> Item;

		public sbyte Amount;
	}

	public sealed class FieldItem : Filter
	{
		public Ref<FieldItemModel> fieldItem;
	}

	public sealed class NpcId : Filter
	{
		public Ref<Npc> Value;
	}

	public sealed class NpcConvoy : Filter
	{
		public bool Convoy;
	}

	public sealed class EnvId : Filter
	{
		public Ref<ZoneEnv2Spawn> Env2spawn;
	}

	public sealed class EnvState : Filter
	{
		public EnvState envState;
	}

	public sealed class EnvPrestate : Filter
	{
		public EnvState envState;
	}

	public sealed class EnvHpPercent : Filter
	{
		public Op Op;

		public sbyte Value;
	}

	public sealed class Skill : Filter
	{
		public Ref<SkillModel> Value;
	}

	public sealed class SkillId : Filter
	{
		public Ref<SkillModel> Value;
	}

	public sealed class Skill3 : Filter
	{
		public Ref<Skill3Model> Value;
	}

	public sealed class Skill3Id : Filter
	{
		public Ref<Skill3Model> Value;
	}

	public sealed class EffectId : Filter
	{
		public Ref<Effect> Value;
	}

	public sealed class EffectStackCount : Filter
	{
		public EffectSlotSeq EffectSlot;
		public enum EffectSlotSeq
		{
			All,
			Buff,
			Debuff,
		}

		public TermOpSeq TermOp;
		public enum TermOpSeq
		{
			None,
			and,
			or,
		}

		[Repeat(2)]
		public Op[] Op;

		[Repeat(2)]
		public long[] Value;
	}

	public sealed class QuestComplete : Filter
	{
		public Ref<Quest> Quest;

		public sbyte MissionStep;

		public short Count;

		public Op CountOp = Op.ge;
	}

	public sealed class QuestNotComplete : Filter
	{
		public Ref<Quest> Quest;
	}

	public sealed class ContentQuotaCharge : Filter
	{
		public Ref<ContentQuota> ContentQuota;

		public Op Op;

		public long ChargeValue;
	}

	public sealed class Cinematic : Filter
	{
		public Ref<CinematicModel> Value;
	}

	public sealed class NpcSpawn : Filter
	{
		[Side(ReleaseSide.Server)]
		public string Spawn;
	}

	public sealed class NpcParty : Filter
	{
		[Side(ReleaseSide.Server)]
		public bool Leader;

		[Side(ReleaseSide.Server)]
		public Script_obj Party;
	}
	#endregion
}

public sealed class FilterSet : ModelElement
{
	[Side(ReleaseSide.Client)]
	public string Name;

	public List<Filter> Filter { get; set; }
}