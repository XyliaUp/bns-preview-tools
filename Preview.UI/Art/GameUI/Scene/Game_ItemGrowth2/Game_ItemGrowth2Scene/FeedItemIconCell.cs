using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2.Game_ItemGrowth2;
public sealed class FeedItemIconCell : ItemIconCell
{
    public override Bitmap FrameImage => Resource_Common.FeedItem;

    /// <summary>
    /// ResultWeaponPreview	绑定控件
    /// </summary>

    public ItemNamePanel BindName;
}