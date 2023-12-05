using CUE4Parse.BNS.Conversion;

using SkiaSharp;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.Data.Models;
public sealed class ItemBrandTooltip : Record
{
	#region Fields
	public int BrandId;
	public ConditionType ItemConditionType;

	public Ref<Text> Name2;

	[Name("game-category-3")]
	public GameCategory3Seq GameCategory3;

	[Name("item-grade")]
	public sbyte ItemGrade;

	[Name("equip-job-check") , Repeat(4)]
	public JobSeq[] EquipJobCheck;

	[Name("equip-sex")]
	public SexSeq2 EquipSex;

	[Name("equip-race")]
	public RaceSeq2 EquipRace;


	public string icon;

	[Name("tag-icon")]
	public string TagIcon;

	[Name("tag-icon-grade")]
	public string TagIconGrade;
	#endregion

	#region Properties
	public SKBitmap FrontIcon => icon.GetIcon();

	public SKBitmap Icon => ItemGrade.GetBackground().Compose(FrontIcon);
	#endregion
}