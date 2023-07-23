using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Data.Record;
public sealed class ItemBrandTooltip : BaseRecord
{
	[Signal("item-condition-type")]
	public ConditionType ItemConditionType;

	public Text Name2;

	[Signal("game-category-3")]
	public Item.GameCategory3Seq GameCategory3;

	[Signal("item-grade")]
	public byte ItemGrade;

	[Signal("equip-level")]
	public byte EquipLevel;

	[Signal("equip-mastery-level")]
	public byte EquipMasteryLevel;

	[Signal("equip-job-check") , Repeat(4)]
	public JobSeq[] EquipJobCheck;

	[Signal("equip-sex")]
	public SexSeq2 EquipSex;

	[Signal("equip-race")]
	public RaceSeq2 EquipRace;

	[Signal("equip-solo-duel-grade")]
	public byte EquipSoloDuelGrade;

	[Signal("equip-team-duel-grade")]
	public byte EquipTeamDuelGrade;

	public string Icon;

	[Signal("tag-icon")]
	public string TagIcon;

	[Signal("tag-icon-grade")]
	public string TagIconGrade;

	[Signal("main-info")]
	public Text MainInfo;

	[Signal("sub-info")]
	public Text SubInfo;

	public Text Description2;

	[Signal("description4-title")]
	public Text Description4Title;

	[Signal("description5-title")]
	public Text Description5Title;

	[Signal("description6-title")]
	public Text Description6Title;

	public Text Description4;

	public Text Description5;

	public Text Description6;

	[Signal("store-description")]
	public Text StoreDescription;

	[Signal("title-item")]
	public Item TitleItem;
}