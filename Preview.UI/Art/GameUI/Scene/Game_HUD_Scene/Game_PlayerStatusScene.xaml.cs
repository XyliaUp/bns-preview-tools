namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_HUD_Scene;
public partial class Game_PlayerStatusScene : Window
{
	public Game_PlayerStatusScene()
	{
        DataContext = new Game_PlayerStatusSceneViewModel();
		InitializeComponent();
	}
}