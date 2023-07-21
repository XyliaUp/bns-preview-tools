using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public abstract class Filter : BaseRecord
{
	public Script_obj Subject;
	public Script_obj Target;
	public Script_obj Subject2;
	public Script_obj Target2;

	#region Sub
	public sealed class Race : Filter
	{
		[Signal("value-1")]
		public RaceSeq Value1;

		[Signal("value-2")]
		public RaceSeq Value2;

		[Signal("value-3")]
		public RaceSeq Value3;

		[Signal("value-4")]
		public RaceSeq Value4;

		public bool Either;
	}

	public sealed class Sex : Filter
	{
		[Signal("value-1")]
		public SexSeq Value1;

		[Signal("value-2")]
		public SexSeq Value2;

		[Signal("value-3")]
		public SexSeq Value3;

		[Signal("value-4")]
		public SexSeq Value4;

		public bool Either;
	}

	public sealed class Job : Filter
	{
		[Signal("value-1")]
		public JobSeq Value1;

		[Signal("value-2")]
		public JobSeq Value2;

		[Signal("value-3")]
		public JobSeq Value3;

		[Signal("value-4")]
		public JobSeq Value4;

		public bool Either;
	}

	public sealed class JobStyle : Filter
	{
		public byte Count;

		[Signal("job-1")]
		public JobSeq Job1;

		[Signal("job-2")]
		public JobSeq Job2;

		[Signal("job-3")]
		public JobSeq Job3;

		[Signal("job-4")]
		public JobSeq Job4;

		[Signal("job-style-1")]
		public JobStyleSeq JobStyle1;

		[Signal("job-style-2")]
		public JobStyleSeq JobStyle2;

		[Signal("job-style-3")]
		public JobStyleSeq JobStyle3;

		[Signal("job-style-4")]
		public JobStyleSeq JobStyle4;

		public bool Either;
	}

	public sealed class Stance : Filter
	{
		[Signal("value-1")]
		public StanceSeq Value1;

		[Signal("value-2")]
		public StanceSeq Value2;

		[Signal("value-3")]
		public StanceSeq Value3;

		[Signal("value-4")]
		public StanceSeq Value4;

		public bool Either;
	}

	public sealed class Prop : Filter
	{
		public string Field;  //creature_field

		public Op Op;

		public long Value;
	}

	public sealed class PropPercent : Filter
	{
		public string Field;

		public Op Op;

		public byte Value;
	}

	public sealed class PropFlag : Filter
	{
		public string Field;

		public Op Op;

		public bool Flag;
	}

	public sealed class EffectFlag : Filter
	{
		//public FlagtypeSeq Flagtype;

		public bool Flag;
	}

	public sealed class Faction : Filter
	{
		public Faction Value;
	}

	public sealed class ActiveFaction : Filter
	{
		public Faction Value;
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
		[Signal("value-1")]
		public EffectAttributeSeq Value1;

		[Signal("value-2")]
		public EffectAttributeSeq Value2;

		[Signal("value-3")]
		public EffectAttributeSeq Value3;

		[Signal("value-4")]
		public EffectAttributeSeq Value4;

		public bool Either;
	}

	public sealed class WeaponType : Filter
	{
		[Signal("weapon-type-1")]
		public Item.WeaponTypeSeq WeaponType1;

		[Signal("weapon-type-2")]
		public Item.WeaponTypeSeq WeaponType2;

		[Signal("weapon-type-3")]
		public Item.WeaponTypeSeq WeaponType3;

		[Signal("weapon-type-4")]
		public Item.WeaponTypeSeq WeaponType4;

		public bool Either;
	}

	public sealed class Inventory : Filter
	{
		public Item Item;

		public byte Amount;
	}

	public sealed class fieldItem : Filter
	{
		[Signal("field-item")]
		public FieldItem FieldItem;
	}

	public sealed class NpcId : Filter
	{
		public Npc Value;
	}

	public sealed class NpcConvoy : Filter
	{
		public bool Convoy;
	}

	public sealed class EnvId : Filter
	{
		public ZoneEnv2Spawn Env2spawn;
	}

	public sealed class envState : Filter
	{
		[Signal("env-state")]
		public EnvState EnvState;
	}

	public sealed class EnvPrestate : Filter
	{
		[Signal("env-state")]
		public EnvState EnvState;
	}

	public sealed class EnvHpPercent : Filter
	{
		public Op Op;

		public byte Value;
	}

	public sealed class Skill : Filter
	{
		public Skill Value;
	}

	public sealed class SkillId : Filter
	{
		public Skill Value;
	}

	public sealed class Skill3 : Filter
	{
		public Skill Value;
	}

	public sealed class Skill3Id : Filter
	{
		public Skill Value;
	}

	public sealed class EffectId : Filter
	{
		public Effect Value;
	}

	public sealed class EffectStackCount : Filter
	{
		//[Signal("effect-type")]
		//public EffectTypeSeq EffectType;

		//[Signal("effect-slot")]
		//public EffectSlot EffectSlot;

		//[Signal("term-op")]
		//public TermOp TermOp;

		[Signal("op-1")]
		public Op Op1;

		[Signal("op-2")]
		public Op Op2;

		[Signal("value-1")]
		public long Value1;

		[Signal("value-2")]
		public long Value2;
	}

	public sealed class QuestComplete : Filter
	{
		public Quest Quest;

		[Signal("mission-step")]
		public byte MissionStep;

		public short Count;

		[Signal("count-op")]
		public Op CountOp = Op.ge;
	}

	public sealed class QuestNotComplete : Filter
	{
		public Quest Quest;
	}

	public sealed class ContentQuotaCharge : Filter
	{
		[Signal("content-quota")]
		public ContentQuota ContentQuota;

		public Op Op;

		[Signal("charge-value")]
		public long ChargeValue;
	}

	public sealed class Cinematic : Filter
	{
		public Cinematic Value;

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