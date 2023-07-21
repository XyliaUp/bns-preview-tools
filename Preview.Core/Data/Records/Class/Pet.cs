using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class Pet : BaseRecord
{
	[Signal("mesh-name")]
	public FPath MeshName;

	[Signal("mesh-scale")]
	public float MeshScale;

	[Signal("material-name"), Repeat(3)]
	public FPath[] MaterialName;

	[Signal("anim-set-name")]
	public FPath AnimSetName;

	[Signal("anim-tree-name")]
	public FPath AnimTreeName;

	[Signal("caster-spawn-show")]
	public FPath CasterSpawnShow;

	[Signal("caster-despawn-show")]
	public FPath CasterDespawnShow;

	[Signal("idle-anim")]
	public string IdleAnim;

	[Signal("combat-idle-anim")]
	public string CombatIdleAnim;

	[Signal("spawn-show")]
	public FPath SpawnShow;

	[Signal("effect-show")]
	public FPath EffectShow;

	[Signal("despawn-show")]
	public FPath DespawnShow;

	[Signal("food-show")]
	public FPath FoodShow;
}