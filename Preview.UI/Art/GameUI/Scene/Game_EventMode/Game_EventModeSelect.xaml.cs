namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_EventMode;
public partial class Game_EventModeSelect : Window
{
	public Game_EventModeSelect()
	{
        DataContext = new Game_EventModeSelectViewModel();
		InitializeComponent();
	}
}