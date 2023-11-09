namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Main_Scene;
public partial class Game_MainScene : Window
{
	public Game_MainScene()
	{
        DataContext = new Game_MainSceneViewModel();
		InitializeComponent();
	}
}