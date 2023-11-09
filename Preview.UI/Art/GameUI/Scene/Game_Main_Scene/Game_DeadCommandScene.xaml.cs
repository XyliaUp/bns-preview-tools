namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Main_Scene;
public partial class Game_DeadCommandScene : Window
{
	public Game_DeadCommandScene()
	{
        DataContext = new Game_DeadCommandSceneViewModel();
		InitializeComponent();
	}
}