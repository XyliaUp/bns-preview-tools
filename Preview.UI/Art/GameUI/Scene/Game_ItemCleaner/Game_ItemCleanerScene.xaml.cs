namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemCleaner;
public partial class Game_ItemCleanerScene : Window
{
	public Game_ItemCleanerScene()
	{
        DataContext = new Game_ItemCleanerSceneViewModel();
		InitializeComponent();
	}
}