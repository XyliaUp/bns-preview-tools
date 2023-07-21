using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;
public abstract class SpawnNpcBase : NpcBase
{
	[Signal("attack-target")]
	public Script_obj AttackTarget;


	[Signal("spawn-delay")]
	public long SpawnDelay = 0;

	[Signal("spawn-force")]
	public bool SpawnForce;


	/// <summary>
	/// 刷新到的目标位置
	/// </summary>
	[Signal("spawn-target")]
	public Script_obj SpawnTarget;

	[Signal("spawn-radius")]
	public short SpawnRadius;

	[Signal("spawn-radius-diff")]
	public short SpawnRadiusDiff;

	[Signal("spawn-area")]
	public short SpawnArea;

	[Signal("spawn-angle-theta")]
	public short SpawnAngleTheta;

	[Signal("use-spawn-target-yaw")]
	public bool UseSpawnTargetYaw;

	[Signal("spawn-social")]
	public string SpawnSocial;
}