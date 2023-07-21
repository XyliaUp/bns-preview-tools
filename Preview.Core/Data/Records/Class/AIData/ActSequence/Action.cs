using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action;
public abstract class Action : BaseRecord
{
	/// <summary>
	/// 仅当上级节点是 Select 时才有意义
	/// </summary>
	public byte Prob;

	#region Sub
	public sealed class Despawn : Action
	{
		public bool Respawn;
	}

	public sealed class Hide : Action
	{
		public bool hide;

		public byte Social;
	}

	public sealed class IndexedMovearound : MovearoundBase
	{
		public byte Area;
	}

	public sealed class IndexedPathway : Action
	{
		public byte pathway;
	}

	public sealed class IndexedSocial : Action
	{
		public long Duration;

		public byte Social;
	}

	public sealed class Loop : Action
	{
		[Signal("max-count")]
		public int MaxCount;

		[Signal("min-count")]
		public int MinCount;
	}




	public abstract class MovearoundBase : Action
	{
		[Signal("max-idle-sec")]
		public int MaxIdleSec;

		[Signal("min-idle-sec")]
		public int MinIdleSec;

		[Signal("max-move-count")]
		public int MaxMoveCount;

		[Signal("min-move-count")]
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
		public ZonePathWay pathway;
	}

	public sealed class Pause : Action
	{
		public byte Step;
	}

	public sealed class Select : Action
	{
		[Signal("enter-prob")]
		public byte EnterProb;
	}

	public sealed class social : Action
	{
		public Social Social;

		public Detect detect;

		public Script_obj Target;
	}

	public sealed class Stay : Action
	{
		public long Duration;

		public Detect detect;

		public byte Repeat;
	}
	#endregion
}