using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class WorldAccountMuseum : Record
{
	public string Alias;

	public DateTime StartTime;
	public DateTime EndTime;
	public bool CanNotUsed;

	[Repeat(3)] public AttachAbility[] Ability;
	[Repeat(3)] public int[] AbilityValue;

	public Ref<Text> CollectionName;

	public CollectionCategorySeq CollectionCategory;
	public enum CollectionCategorySeq
	{
		[Name("level-1")]
		Level1,

		[Name("level-2")]
		Level2,

		[Name("level-3")]
		Level3,

		[Name("level-4")]
		Level4,

		Event,

		Favorite,
	}
}