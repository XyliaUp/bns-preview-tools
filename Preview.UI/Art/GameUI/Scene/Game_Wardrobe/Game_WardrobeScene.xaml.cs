namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Wardrobe;
public partial class Game_WardrobeScene : Window
{
	public Game_WardrobeScene()
	{
        DataContext = new Game_WardrobeSceneViewModel();
		InitializeComponent();
	}
}