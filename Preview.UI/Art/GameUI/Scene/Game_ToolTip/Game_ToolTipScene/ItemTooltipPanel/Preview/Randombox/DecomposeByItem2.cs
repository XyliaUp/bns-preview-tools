using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Randombox;
public sealed class DecomposeByItem2
{
	public Item Item;

	public int StackCount = 0;
			  

	public DecomposeByItem2(string Item,int StackCount)
	{
		this.Item = new() { alias = Item };
		this.StackCount = StackCount;
	}
}