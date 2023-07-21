using System.ComponentModel;

using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill;
public sealed class SkillTooltipPanel : Panel
{
	public override void Refresh()
	{
		int y = 0;												  
		foreach (ContentPanel o in this.Controls)
		{
			o.Location = new Point(0, y);
			o.Refresh();
			y = o.Bottom + 5;
		}

		this.Height = y;
	}
}


[DesignTimeVisible(false)]
public class IconContentPanel : ContentPanel
{
	public Bitmap icon;
}