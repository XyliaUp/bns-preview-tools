namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_RandomStore;
public partial class Game_TencentRandomStoreScene : Window
{
	public Game_TencentRandomStoreScene()
	{
        DataContext = new Game_TencentRandomStoreSceneViewModel();
		InitializeComponent();
	}
}