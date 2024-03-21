using System.Windows;

namespace Xylia.Preview.UI.GameUI.Scene.Game_Tooltip2;
public partial class ItemGraphReceipeTooltipPanel
{
	public ItemGraphReceipeTooltipPanel()
	{
		InitializeComponent();

		ItemGraphReceipeTooltipPanel_2.Visibility =
		ItemGraphReceipeTooltipPanel_3.Visibility =
		ItemGraphReceipeTooltipPanel_4.Visibility = Visibility.Collapsed;
	}
}