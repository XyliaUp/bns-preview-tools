namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ClassChange;
public partial class Game_ClassChangeScene : Window
{
	public Game_ClassChangeScene()
	{
        DataContext = new Game_ClassChangeSceneViewModel();
		InitializeComponent();
	}
}