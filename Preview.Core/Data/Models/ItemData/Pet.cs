using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Pet : Record
{
	public string Alias;



	[Name("mesh-name")]
	public ObjectPath MeshName;

	[Name("mesh-scale")]
	public float MeshScale;

	[Name("material-name"), Repeat(3)]
	public ObjectPath[] MaterialName;

	[Name("anim-set-name")]
	public ObjectPath AnimSetName;

	[Name("anim-tree-name")]
	public ObjectPath AnimTreeName;

	[Name("caster-spawn-show")]
	public ObjectPath CasterSpawnShow;

	[Name("caster-despawn-show")]
	public ObjectPath CasterDespawnShow;

	[Name("idle-anim")]
	public string IdleAnim;

	[Name("combat-idle-anim")]
	public string CombatIdleAnim;

	[Name("spawn-show")]
	public ObjectPath SpawnShow;

	[Name("effect-show")]
	public ObjectPath EffectShow;

	[Name("despawn-show")]
	public ObjectPath DespawnShow;

	[Name("food-show")]
	public ObjectPath FoodShow;
}