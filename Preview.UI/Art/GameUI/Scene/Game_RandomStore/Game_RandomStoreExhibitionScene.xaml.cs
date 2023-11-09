namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_RandomStore;
public partial class Game_RandomStoreExhibitionScene : Window
{
	public Game_RandomStoreExhibitionScene()
	{
        DataContext = new Game_RandomStoreExhibitionSceneViewModel();
		InitializeComponent();
	}
}