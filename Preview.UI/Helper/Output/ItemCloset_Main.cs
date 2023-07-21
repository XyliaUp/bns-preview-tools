using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

using static Xylia.Preview.Data.Record.Item;
using static Xylia.Preview.Data.Record.Item.Accessory;

namespace Xylia.Preview.Helper.Output;
public sealed class ItemCloset_Main : OutBase
{
    protected override string Name => "Item_closet";

    protected override void CreateData()
    {
        #region Title
        ExcelInfo.SetColumn("物品编号", 15);
        ExcelInfo.SetColumn("物品别名", 40);
        ExcelInfo.SetColumn("物品名称", 25);
        ExcelInfo.SetColumn("装备类型", 15);
        ExcelInfo.SetColumn("性别", 15);
        ExcelInfo.SetColumn("种族", 15);
        ExcelInfo.SetColumn("衣柜归属", 20);
        ExcelInfo.SetColumn("衣柜目录", 20);
        #endregion

        foreach(var item in FileCache.Data.Item)
        {
			#region Check
			bool Flag = false;

            if (item is Costume) Flag = true;
            else if (item is Weapon && item.ClosetGroupId != 0) Flag = true;
            else if (item is Accessory accessory &&
                (accessory.AccessoryType is AccessoryTypeSeq.CostumeAttach or AccessoryTypeSeq.Vehicle)) Flag = true;


            if (!Flag) continue;
            else if (item.UsableDuration != 0) continue;
            #endregion


            var row = ExcelInfo.CreateRow();
            row.AddCell(item.Ref.Id);
            row.AddCell(item.alias);
            row.AddCell(item.Name2);
            row.AddCell(item.EquipType.GetText());
            row.AddCell(item.EquipSex.GetText());
            row.AddCell(item.EquipRace.GetText());
            row.AddCell(item.ClosetGroupId);

            if (item.ClosetGroupId != 0)
            {
                var ClosetGroup = FileCache.Data.ClosetGroup[item.ClosetGroupId];
                if (ClosetGroup != null) row.AddCell($"Name.closet-group.category.{ClosetGroup.Attributes["category"]}".GetText());
            }
        }
    }
}