namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Achievement;
public partial class Game_Achievement_Scene : Window
{
	public Game_Achievement_Scene()
	{
        DataContext = new Game_Achievement_SceneViewModel();
		InitializeComponent();
	}
}