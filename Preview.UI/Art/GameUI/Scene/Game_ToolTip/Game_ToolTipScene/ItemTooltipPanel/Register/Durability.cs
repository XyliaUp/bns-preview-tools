using Xylia.Extension;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;
public class Durability : ItemTooltip
{
	public override Control Load()
	{
		var MaxDurability = Data.Attributes["max-durability"].ToInt();
		if (MaxDurability > 0) return new ContentPanel("UI.ItemTooltip.Durability", null, MaxDurability, MaxDurability);

		return null;
	}
}
