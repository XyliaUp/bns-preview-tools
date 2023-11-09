namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GameMenu;
public partial class Game_GameMenuScene : Window
{
	public Game_GameMenuScene()
	{
        DataContext = new Game_GameMenuSceneViewModel();
		InitializeComponent();
	}
}