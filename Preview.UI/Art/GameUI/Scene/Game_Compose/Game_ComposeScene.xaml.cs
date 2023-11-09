namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Compose;
public partial class Game_ComposeScene : Window
{
	public Game_ComposeScene()
	{
        DataContext = new Game_ComposeSceneViewModel();
		InitializeComponent();
	}
}