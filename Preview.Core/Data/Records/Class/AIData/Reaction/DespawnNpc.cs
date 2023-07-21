using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record.ReactionClass;
public abstract class DespawnNpcBase : Reaction
{
	/// <summary>
	/// 消除延迟
	/// </summary>
	[Signal("despawn-delay")]
	public long DespawnDelay;

	/// <summary>
	/// 强制消除
	/// </summary>
	[Signal("despawn-force")]
	public bool DespawnForce;

	/// <summary>
	/// 消除后允许再刷新
	/// </summary>
	[Signal("respawn-after-despawn")]
	public bool RespawnAfterDespawn;




	[Signal("despawn-social")]
	public Social DespawnSocial;

	[Signal("despawn-social-1")]
	public Social DespawnSocial1;

	[Signal("despawn-social-2")]
	public Social DespawnSocial2;

	[Signal("despawn-social-3")]
	public Social DespawnSocial3;

	[Signal("despawn-social-4")]
	public Social DespawnSocial4;

	[Signal("despawn-social-5")]
	public Social DespawnSocial5;

	[Signal("despawn-social-6")]
	public Social DespawnSocial6;

	[Signal("despawn-social-7")]
	public Social DespawnSocial7;

	[Signal("despawn-social-8")]
	public Social DespawnSocial8;

	[Signal("despawn-social-9")]
	public Social DespawnSocial9;

	[Signal("despawn-social-9")]
	public Social DespawnSocial10;
}


[Signal("despawn-npc")]
public sealed class DespawnNpc : DespawnNpcBase
{
	[Signal("target")] public Script_obj Target;
	[Signal("target-1")] public Script_obj Target1;
	[Signal("target-2")] public Script_obj Target2;
	[Signal("target-3")] public Script_obj Target3;
	[Signal("target-4")] public Script_obj Target4;
	[Signal("target-5")] public Script_obj Target5;
	[Signal("target-6")] public Script_obj Target6;
	[Signal("target-7")] public Script_obj Target7;
	[Signal("target-8")] public Script_obj Target8;
	[Signal("target-9")] public Script_obj Target9;
	[Signal("target-10")] public Script_obj Target10;

	[Signal("spawn-1")] public string Spawn1;
	[Signal("spawn-2")] public string Spawn2;
	[Signal("spawn-3")] public string Spawn3;
	[Signal("spawn-4")] public string Spawn4;
	[Signal("spawn-5")] public string Spawn5;
	[Signal("spawn-6")] public string Spawn6;
	[Signal("spawn-7")] public string Spawn7;
	[Signal("spawn-8")] public string Spawn8;
	[Signal("spawn-9")] public string Spawn9;
	[Signal("spawn-10")] public string Spawn10;
}