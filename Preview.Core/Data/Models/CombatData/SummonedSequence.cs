using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class SummonedSequence : Record
{
	public string Alias;


	public List<Melee> melee { get; set; }
	public List<Range> range { get; set; }
	public List<RangeSim> rangeSim { get; set; }


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
		public List<Action> Action { get; set; }

		public Distance Margin;
	}

	public class Range : Record
	{
		public List<Action> Action { get; set; }

		public Distance Margin;
	}

	public class RangeSim : Range
	{
	}
}