namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Fishing;
public partial class Game_FishingScene : Window
{
	public Game_FishingScene()
	{
        DataContext = new Game_FishingSceneViewModel();
		InitializeComponent();
	}
}