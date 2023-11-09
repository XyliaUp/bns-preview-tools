namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Debug_Scene;
public partial class Game_Debug_HUD_Scene : Window
{
	public Game_Debug_HUD_Scene()
	{
        DataContext = new Game_Debug_HUD_SceneViewModel();
		InitializeComponent();
	}
}