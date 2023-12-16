using OfficeOpenXml;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;
using static Xylia.Preview.Data.Models.Item;
using static Xylia.Preview.Data.Models.Item.Accessory;

namespace Xylia.Preview.UI.Helpers.Output.Items;
public sealed class ItemCloset : OutSet
{
    protected override void CreateData(ExcelWorksheet sheet)
    {
        #region Title
        sheet.SetColumn(Column++, "物品编号", 15);
        sheet.SetColumn(Column++, "物品别名", 40);
        sheet.SetColumn(Column++, "物品名称", 25);
        sheet.SetColumn(Column++, "装备类型", 15);
        sheet.SetColumn(Column++, "性别", 15);
        sheet.SetColumn(Column++, "种族", 15);
        sheet.SetColumn(Column++, "衣柜归属", 20);
        sheet.SetColumn(Column++, "衣柜目录", 20);
        #endregion

        foreach (var item in FileCache.Data.Item)
        {
            #region Check
            bool Flag = false;

            if (item is Costume) Flag = true;
            else if (item is Weapon && item.ClosetGroupId != 0) Flag = true;
            else if (item is Accessory accessory &&
                accessory.AccessoryType is AccessoryTypeSeq.CostumeAttach or AccessoryTypeSeq.Vehicle) Flag = true;


            if (!Flag) continue;
            else if (item.UsableDuration != 0) continue;
            #endregion


            Row++;
            int column = 1;

            sheet.Cells[Row, column++].SetValue(item.Source.Ref.Id);
            sheet.Cells[Row, column++].SetValue(item.ToString());
			sheet.Cells[Row, column++].SetValue(item.Name2);
			sheet.Cells[Row, column++].SetValue(item.EquipType.GetText());
			sheet.Cells[Row, column++].SetValue(item.EquipSex.GetText());
			sheet.Cells[Row, column++].SetValue(item.EquipRace);
			sheet.Cells[Row, column++].SetValue(item.ClosetGroupId);

			if (item.ClosetGroupId != 0)
            {
                var ClosetGroup = FileCache.Data.Get<ClosetGroup>()[item.ClosetGroupId];
                if (ClosetGroup != null) sheet.Cells[Row, column++].SetValue($"Name.closet-group.category.{ClosetGroup.Attributes["category"]}".GetText());
            }
        }
    }
}