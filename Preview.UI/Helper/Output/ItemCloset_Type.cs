using NPOI.SS.UserModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

using static Xylia.Preview.Data.Record.Item;
using static Xylia.Preview.Data.Record.Item.Accessory;
using static Xylia.Preview.Data.Record.Item.Weapon;

namespace Xylia.Preview.Helper.Output;
public sealed class ItemCloset_Type : OutBase
{
    protected override string Name => "衣柜";

    protected override void CreateData()
    {
        //Remove default workbook
        ExcelInfo.Workbook.RemoveSheetAt(0);
        var ItemTable = FileCache.Data.Item;

        #region 时装  
        var CostumeSheet = CreateSheet("时装");
        ExcelInfo.SetColumn("种族", 10);
        ExcelInfo.SetColumn("性别", 10);
        ExcelInfo.SetColumn("定制类型", 20);

        foreach (var item in ItemTable.Where(item =>
            (item is Costume ||
            item is Accessory accessory && accessory.AccessoryType == AccessoryTypeSeq.CostumeAttach)
            && item.UsableDuration == 0))
        {
            var row = CreateRow(item, CostumeSheet);
            row.AddCell(item.EquipRace.GetName());
            row.AddCell(item.EquipSex.GetText());


			var state = CustomDressDesignStateSeq.None;
			if (item is Costume costume) state = costume.CustomDressDesignState;
			else if (item is Accessory accessory) state = accessory.CustomDressDesignState;

            row.AddCell(state, DisplayType.HideValue);
        }
        #endregion




        #region 幻影石 
        var PetSheet = CreateSheet("幻影石");
        foreach (var item in ItemTable.Where(item => item is Weapon weapon &&
			weapon.WeaponType == WeaponTypeSeq.Pet1 &&
			weapon.UsableDuration == 0 &&
            (weapon.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.UsedOnlyAsApplyingWeapon || weapon.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.Both)))
            CreateRow(item, PetSheet);
        #endregion

        #region 幻影武器 
        var WeaponSheet = CreateSheet("武器");
        ExcelInfo.SetColumn("职业");

        foreach (var item in ItemTable.Where(item => item is Weapon weapon &&
			weapon.WeaponType != WeaponTypeSeq.Pet1 &&
			weapon.UsableDuration == 0 &&
			weapon.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.UsedOnlyAsApplyingWeapon))
        {
            var CurRow = CreateRow(item, WeaponSheet);
            CurRow.AddCell(item.EquipJobCheck[0].GetText());
        }
        #endregion

        var VehicleSheet = CreateSheet("坐骑");
        ItemTable.Where(item => item is Accessory accessory && accessory.AccessoryType == AccessoryTypeSeq.Vehicle).ForEach(item => CreateRow(item, VehicleSheet));

        var AppearanceItem = CreateSheet("外观道具");
        ItemTable.Where(item => item is Accessory accessory && item.Contains("appearance", out _)).ForEach(item => CreateRow(item, AppearanceItem));
    }


    private ISheet CreateSheet(string SheetName)
    {
        var sheet = ExcelInfo.CreateSheet(SheetName);
        ExcelInfo.SetColumn("物品编号", 15);
        ExcelInfo.SetColumn("物品别名", 65);
        ExcelInfo.SetColumn("物品名称", 30);
        ExcelInfo.SetColumn("装备类型", 25);
        ExcelInfo.SetColumn("衣柜编号", 15);
        ExcelInfo.SetColumn("衣柜目录", 15);

        return sheet;
    }

    private IRow CreateRow(Item ItemInfo, ISheet Sheet)
    {
        var row = ExcelInfo.CreateRow(sheet: Sheet);
        row.AddCell(ItemInfo.Ref.Id);
        row.AddCell(ItemInfo.alias);
        row.AddCell(ItemInfo.Name2);
        row.AddCell(ItemInfo.EquipType.GetName());
        row.AddCell(ItemInfo.ClosetGroupId, DisplayType.HideValue);

        var ClosetGroup = FileCache.Data.ClosetGroup[ItemInfo.ClosetGroupId];
        var Category = ClosetGroup is null ? null : $"Name.closet-group.category.{ClosetGroup.Attributes["category"]}".GetText();
        row.AddCell(Category);

        return row;
    }
}