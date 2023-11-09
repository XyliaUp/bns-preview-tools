namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_RandomStore;
public partial class Game_RandomStoreScene : Window
{
	public Game_RandomStoreScene()
	{
        DataContext = new Game_RandomStoreSceneViewModel();
		InitializeComponent();
	}
}