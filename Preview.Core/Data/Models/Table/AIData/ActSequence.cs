using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Server)]
public class ActSequence : ModelElement
{
	public string Alias;
	public Detect Detect;
	public sbyte IndexedDetect;


	public List<Action> action { get; set; }

	#region Sub
	public sealed class Act : ActSequence
	{

	}

	public sealed class Peace : ActSequence
	{

	}
	#endregion

	#region Element
	public abstract class Action : ModelElement
	{
		/// <summary>
		/// 仅当上级节点是 Select 时才有意义
		/// </summary>
		public sbyte Prob;

		#region Sub
		public sealed class Despawn : Action
		{
			public bool Respawn;
		}

		public sealed class Hide : Action
		{
			public bool hide;

			public sbyte social;
		}

		public sealed class IndexedMovearound : MovearoundBase
		{
			public sbyte Area;
		}

		public sealed class IndexedPathway : Action
		{
			public sbyte pathway;
		}

		public sealed class IndexedSocial : Action
		{
			public Msec Duration;

			public sbyte social;
		}

		public sealed class Loop : Action
		{
			public int MaxCount;

			public int MinCount;
		}


		public abstract class MovearoundBase : Action
		{
			public int MaxIdleSec;
			public int MinIdleSec;

			public int MaxMoveCount;
			public int MinMoveCount;

			public Script_obj Target;
		}

		public sealed class Movearound : MovearoundBase
		{
			public Script_obj Area;
		}

		public sealed class MovearoundForFieldBossSpawn : MovearoundBase
		{

		}

		public sealed class MovearoundForRandomSpawn : MovearoundBase
		{

		}

		public sealed class Pathway : Action
		{
			public Ref<ZonePathWay> pathway;
		}

		public sealed class Pause : Action
		{
			public sbyte Step;
		}

		public sealed class Select : Action
		{
			public sbyte EnterProb;
		}

		public sealed class Social : Action
		{
			public Ref<Social> social;

			public Detect detect;

			public Script_obj Target;
		}

		public sealed class Stay : Action
		{
			public Msec Duration;

			public Detect detect;

			public sbyte Repeat;
		}
		#endregion
	}
	#endregion
}