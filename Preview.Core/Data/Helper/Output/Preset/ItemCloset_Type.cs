using System.Linq;

using NPOI.SS.UserModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Data.Helper.Output.Preset
{
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
                (item.Type == ItemType.Costume ||
                item.Type == ItemType.Accessory && item.AccessoryType == AccessoryTypeSeq.CostumeAttach)
                && item.UsableDuration == 0))
            {
                var row = CreateRow(item, CostumeSheet);
                row.AddCell(item.EquipRace.GetName());
                row.AddCell(item.EquipSex.GetName());
                row.AddCell(item.CustomDressDesignState, DisplayType.HideValue);
            }
            #endregion

            #region 幻影石 
            var PetSheet = CreateSheet("幻影石");
            foreach (var item in ItemTable.Where(item =>
                item.Type == ItemType.Weapon &&
                item.WeaponType == WeaponTypeSeq.Pet1 &&
                item.UsableDuration == 0 &&
                (item.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.UsedOnlyAsApplyingWeapon || item.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.Both)))
                CreateRow(item, PetSheet);
            #endregion

            #region 幻影武器 
            var WeaponSheet = CreateSheet("武器");
            ExcelInfo.SetColumn("职业");

            foreach (var item in ItemTable.Where(item =>
                item.Type == ItemType.Weapon &&
                item.WeaponType != WeaponTypeSeq.Pet1 &&
                item.UsableDuration == 0 &&
                item.WeaponAppearanceChangeType == WeaponAppearanceChangeTypeSeq.UsedOnlyAsApplyingWeapon))
            {
                var CurRow = CreateRow(item, WeaponSheet);
                CurRow.AddCell(item.EquipJobCheck1.GetName());
            }
            #endregion

            var VehicleSheet = CreateSheet("坐骑");
            ItemTable.Where(item => item.Type == ItemType.Accessory && item.AccessoryType == AccessoryTypeSeq.Vehicle).ForEach(item => CreateRow(item, VehicleSheet));

            var AppearanceItem = CreateSheet("外观道具");
            ItemTable.Where(item => item.Type == ItemType.Accessory && item.ContainsAttribute("appearance", out _)).ForEach(item => CreateRow(item, AppearanceItem));
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
            row.AddCell(ItemInfo.Key());
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
}