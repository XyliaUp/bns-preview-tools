using System.ComponentModel;
using System.Windows.Controls;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ToolTip;
[DesignTimeVisible(false)]
public partial class ItemTooltipPanel : UserControl
{
	public ItemTooltipPanel()
	{
		InitializeComponent();
	}


	#region Properties
	public Item Model { get; set; }

	#endregion
}