namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_HugeBoss;
public partial class Game_HugeBossScene : Window
{
	public Game_HugeBossScene()
	{
        DataContext = new Game_HugeBossSceneViewModel();
		InitializeComponent();
	}
}