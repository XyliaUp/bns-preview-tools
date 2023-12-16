using Xylia.Preview.Common.Attributes;
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
	public Script_obj Subject { get; set; }
	public Script_obj Target { get; set; }
	public Script_obj Subject2 { get; set; }
	public Script_obj Target2 { get; set; }

	#region Sub
	public sealed class Race : Filter
	{
		[Repeat(4)]
		public RaceSeq[] Value { get; set; }

		public bool Either { get; set; }
	}

	public sealed class Sex : Filter
	{
		[Repeat(4)]
		public SexSeq[] Value { get; set; }

		public bool Either { get; set; }
	}

	public sealed class Job : Filter
	{
		[Repeat(4)]
		public JobSeq[] Value { get; set; }

		public bool Either { get; set; }
	}

	public sealed class JobStyle : Filter
	{
		public sbyte Count { get; set; }

		[Repeat(4)]
		public JobSeq[] job { get; set; }

		[Repeat(4)]
		public JobStyleSeq[] jobStyle { get; set; }

		public bool Either { get; set; }
	}

	public sealed class Stance : Filter
	{
		[Repeat(4)]
		public StanceSeq[] Value { get; set; }

		public bool Either { get; set; }
	}

	public sealed class Prop : Filter
	{
		public CreatureField Field { get; set; }

		public Op Op { get; set; }

		public long Value { get; set; }
	}

	public sealed class PropPercent : Filter
	{
		public CreatureField Field { get; set; }

		public Op Op { get; set; }

		public sbyte Value { get; set; }
	}

	public sealed class PropFlag : Filter
	{
		public CreatureField Field { get; set; }

		public Op Op { get; set; }

		public bool Flag { get; set; }
	}

	public sealed class EffectFlag : Filter
	{
		public Flag FlagType { get; set; }

		public bool Flag { get; set; }
	}

	public sealed class Faction : Filter
	{
		public Ref<FactionModel> Value { get; set; }
	}

	public sealed class ActiveFaction : Filter
	{
		public Ref<FactionModel> Value { get; set; }
	}

	public sealed class FactionReputation : Filter
	{
		public Op Op { get; set; }

		public short Value { get; set; }
	}

	public sealed class FactionLevel : Filter
	{
		public Op Op { get; set; }

		public short Value { get; set; }
	}

	public sealed class EffectAttribute : Filter
	{
		[Repeat(4)]
		public EffectAttributeSeq[] Value { get; set; }

		public bool Either { get; set; }
	}

	public sealed class WeaponType : Filter
	{
		[Repeat(4)]
		public Item.WeaponTypeSeq[] weaponType { get; set; }

		public bool Either { get; set; }
	}

	/// <summary>
	/// 背包中存在物品
	/// </summary>
	public sealed class Inventory : Filter
	{
		public Ref<Item> Item { get; set; }

		public sbyte Amount { get; set; }
	}

	public sealed class FieldItem : Filter
	{
		public Ref<FieldItemModel> fieldItem { get; set; }
	}

	public sealed class NpcId : Filter
	{
		public Ref<Npc> Value { get; set; }
	}

	public sealed class NpcConvoy : Filter
	{
		public bool Convoy { get; set; }
	}

	public sealed class EnvId : Filter
	{
		public Ref<ZoneEnv2Spawn> Env2spawn { get; set; }
	}

	public sealed class EnvState : Filter
	{
		public EnvState envState { get; set; }
	}

	public sealed class EnvPrestate : Filter
	{
		public EnvState envState { get; set; }
	}

	public sealed class EnvHpPercent : Filter
	{
		public Op Op { get; set; }

		public sbyte Value { get; set; }
	}

	public sealed class Skill : Filter
	{
		public Ref<SkillModel> Value { get; set; }
	}

	public sealed class SkillId : Filter
	{
		public Ref<SkillModel> Value { get; set; }
	}

	public sealed class Skill3 : Filter
	{
		public Ref<Skill3Model> Value { get; set; }
	}

	public sealed class Skill3Id : Filter
	{
		public Ref<Skill3Model> Value { get; set; }
	}

	public sealed class EffectId : Filter
	{
		public Ref<Effect> Value { get; set; }
	}

	public sealed class EffectStackCount : Filter
	{
		public EffectSlotSeq EffectSlot { get; set; }
		public enum EffectSlotSeq
		{
			All,
			Buff,
			Debuff,
		}

		public TermOpSeq TermOp { get; set; }
		public enum TermOpSeq
		{
			None,
			and,
			or,
		}

		[Repeat(2)]
		public Op[] Op { get; set; }

		[Repeat(2)]
		public long[] Value { get; set; }
	}

	public sealed class QuestComplete : Filter
	{
		public Ref<Quest> Quest { get; set; }

		public sbyte MissionStep { get; set; }

		public short Count { get; set; }

		public Op CountOp { get; set; } = Op.ge;
	}

	public sealed class QuestNotComplete : Filter
	{
		public Ref<Quest> Quest { get; set; }
	}

	public sealed class ContentQuotaCharge : Filter
	{
		public Ref<ContentQuota> ContentQuota { get; set; }

		public Op Op { get; set; }

		public long ChargeValue { get; set; }
	}

	public sealed class Cinematic : Filter
	{
		public Ref<CinematicModel> Value { get; set; }
	}

	public sealed class NpcSpawn : Filter
	{
		[Side(ReleaseSide.Server)]
		public string Spawn { get; set; }
	}

	public sealed class NpcParty : Filter
	{
		[Side(ReleaseSide.Server)]
		public bool Leader { get; set; }

		[Side(ReleaseSide.Server)]
		public Script_obj Party { get; set; }
	}
	#endregion
}

public sealed class FilterSet : ModelElement
{
	[Side(ReleaseSide.Client)]
	public string Name { get; set; }

	public List<Filter> Filter { get; set; }
}