namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Battle;
public partial class Game_HUDHigh : Window
{
	public Game_HUDHigh()
	{
        DataContext = new Game_HUDHighViewModel();
		InitializeComponent();
	}
}