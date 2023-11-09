namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_MapWindow;
public partial class Game_MapWindowScene : Window
{
	public Game_MapWindowScene()
	{
        DataContext = new Game_MapWindowSceneViewModel();
		InitializeComponent();
	}
}