using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class SummonedSequence : Record
{
	public string Alias;


	public List<Melee> melee;
	public List<Range> range;
	public List<RangeSim> rangeSim;


	public class Action : Record
	{
		public int Line;
		public sbyte Prob;
		public sealed class UseIndexedSkill : Action
		{
			public sbyte Skill;
		}

		public sealed class Stay : Action
		{
			public Msec Duration;
		}

		public sealed class Select : Action
		{
		}
	}

	public class Melee : Record
	{
		public List<Action> Action;

		public Distance Margin;
	}

	public class Range : Record
	{
		public List<Action> Action;

		public Distance Margin;
	}

	public class RangeSim : Range
	{
	}
}