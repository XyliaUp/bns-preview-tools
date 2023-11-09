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

	[Name("equip-level")]
	public sbyte EquipLevel;

	[Name("equip-mastery-level")]
	public sbyte EquipMasteryLevel;

	[Name("equip-job-check") , Repeat(4)]
	public JobSeq[] EquipJobCheck;

	[Name("equip-sex")]
	public SexSeq2 EquipSex;

	[Name("equip-race")]
	public RaceSeq2 EquipRace;

	[Name("equip-solo-duel-grade")]
	public sbyte EquipSoloDuelGrade;

	[Name("equip-team-duel-grade")]
	public sbyte EquipTeamDuelGrade;

	public string icon;

	[Name("tag-icon")]
	public string TagIcon;

	[Name("tag-icon-grade")]
	public string TagIconGrade;

	[Name("main-info")]
	public Ref<Text> MainInfo;

	[Name("sub-info")]
	public Ref<Text> SubInfo;

	public Ref<Text> Description2;

	[Name("description4-title")]
	public Ref<Text> Description4Title;

	[Name("description5-title")]
	public Ref<Text> Description5Title;

	[Name("description6-title")]
	public Ref<Text> Description6Title;

	public Ref<Text> Description4;

	public Ref<Text> Description5;

	public Ref<Text> Description6;

	[Name("store-description")]
	public Ref<Text> StoreDescription;

	[Name("title-item")]
	public Ref<Item> TitleItem;
	#endregion

	#region Properties
	public SKBitmap FrontIcon => icon.GetIcon();

	public SKBitmap Icon => ItemGrade.GetBackground().Compose(FrontIcon);
	#endregion
}