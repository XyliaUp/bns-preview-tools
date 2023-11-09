namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_AccountContents;
public partial class Game_AccountContentsScene : Window
{
	public Game_AccountContentsScene()
	{
        DataContext = new Game_AccountContentsSceneViewModel();
		InitializeComponent();
	}
}