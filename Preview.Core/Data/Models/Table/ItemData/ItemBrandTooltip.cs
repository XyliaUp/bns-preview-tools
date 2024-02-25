using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.Data.Models;
public sealed class ItemBrandTooltip : ModelElement
{
	#region Fields
	public int BrandId { get; set; }

	public ConditionType ItemConditionType { get; set; }

	public Ref<Text> Name2 { get; set; }

	public GameCategory3Seq GameCategory3 { get; set; }

	public sbyte ItemGrade { get; set; }

	public JobSeq[] EquipJobCheck { get; set; }

	[Name("equip-sex")]
	public SexSeq2 EquipSex { get; set; }

	[Name("equip-race")]
	public RaceSeq2 EquipRace { get; set; }
	#endregion
}