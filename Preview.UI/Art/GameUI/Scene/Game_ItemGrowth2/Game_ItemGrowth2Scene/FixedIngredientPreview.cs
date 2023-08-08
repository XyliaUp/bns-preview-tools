using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Extension;


namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.Game_ItemGrowth2;

[DesignTimeVisible(false)]
public partial class FixedIngredientPreview : Panel
{
    #region Functions
    private void CreateNew(Item Item, int StackCount, ref int LocX)
    {
        var ItemIcon = new ItemIconCell()
        {
            ObjectRef = Item,
            Image = Item.Icon(),
            ShowStackCount = true,
            StackCount = StackCount,

            Location = new Point(LocX, 0),
        };

        Controls.Add(ItemIcon);
        LocX = ItemIcon.Right + 3;
    }

    private void HandleSize(int LocX)
    {
        Width = LocX;
        Height = 50;

        DataLoaded?.Invoke();
    }
    #endregion


    #region SetData
    public event EmptyHandler DataLoaded;

    public void SetData(BaseRecord record, string itemAttr, string countAttr, byte num = 8)
    {
        if (record is null)
            throw new ArgumentNullException(nameof(record));


        Controls.Remove<ItemIconCell>();

        int LocX = 0;
        for (byte i = 1; i <= num; i++)
        {
            var Item = FileCache.Data.Item[record.Attributes[itemAttr, i]];
            if (Item is null) continue;

            var ItemCount = record.Attributes[countAttr, i].ToInt16();
            CreateNew(Item, ItemCount, ref LocX);
        }

        HandleSize(LocX);
    }
    #endregion
}