namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Team;
public partial class Game_TeamScene : Window
{
	public Game_TeamScene()
	{
        DataContext = new Game_TeamSceneViewModel();
		InitializeComponent();
	}
}