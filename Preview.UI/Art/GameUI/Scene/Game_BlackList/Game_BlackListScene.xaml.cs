namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_BlackList;
public partial class Game_BlackListScene : Window
{
	public Game_BlackListScene()
	{
        DataContext = new Game_BlackListSceneViewModel();
		InitializeComponent();
	}
}