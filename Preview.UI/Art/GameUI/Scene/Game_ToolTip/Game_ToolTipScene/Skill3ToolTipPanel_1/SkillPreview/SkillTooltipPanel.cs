using System.ComponentModel;

using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTipScene.Skill3ToolTipPanel_1.SkillPreview;
public sealed class SkillTooltipPanel : Panel
{
    public override void Refresh()
    {
        int y = 0;
        foreach (ContentPanel o in Controls)
        {
            o.Location = new Point(0, y);
            o.Refresh();
            y = o.Bottom + 5;
        }

        Height = y;
    }
}


[DesignTimeVisible(false)]
public class IconContentPanel : ContentPanel
{
    public Bitmap icon;
}