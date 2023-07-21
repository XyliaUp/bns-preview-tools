using Xylia.Preview.Data.Record; 

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.Game_ToolTipScene.ItemTooltipPanel;
public abstract class ItemTooltip
{
	public Item Data;

	public abstract Control Load();
}
