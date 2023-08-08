using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Workbook;

namespace Xylia.Preview.Helper.Output;
public sealed class CollectingOut : OutBase
{
    protected override string Name => "Collecting";

    protected override void CreateData()
    {
        #region Title
        ExcelInfo.SetColumn("别名", 30);
        ExcelInfo.SetColumn("", 25);
        ExcelInfo.SetColumn("", 10);

        ExcelInfo.SetColumn("", 15);
        ExcelInfo.SetColumn("", 15);
        ExcelInfo.SetColumn("", 15);

        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        ExcelInfo.SetColumn("", 30);
        #endregion

        #region 获取组对应的默认物品 
        Dictionary<int, Item> groups = new();
        foreach (var record in FileCache.Data.Item)
        {
            int ClosetGroupId = record.ClosetGroupId;
            if (ClosetGroupId == 0) continue;

            if (groups.ContainsKey(ClosetGroupId)) continue;
            groups[ClosetGroupId] = record;
        }

        string GetName(int closet)
        {
            if (closet == 0) return null;
            else if (!groups.TryGetValue(closet, out var item)) return closet.ToString();
            else return item.Name2;
        }
        #endregion


        foreach (var record in FileCache.Data.Collecting)
        {
            var row = ExcelInfo.CreateRow();
            row.AddCell(record.alias);
            row.AddCell(record.Name.GetText());
            row.AddCell(record.Category);
            Linq.For(3, (i) => row.AddCell(record.Ability[i].GetName(record.AbilityValue[i])));


            for (int i = 1; i <= 8; i++)
            {
                var closet = record.Attributes["collect-closet-" + i].ToInt32();
                var closetreplace = record.Attributes["collect-closet-replace-" + i].ToInt32();
                var closetsubreplace = record.Attributes["collect-closet-subreplace-" + i].ToInt32();

                row.AddCell(GetName(closet));
            }
        }
    }
}