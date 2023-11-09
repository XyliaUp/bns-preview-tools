namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Product;
public partial class Game_ProductScene : Window
{
	public Game_ProductScene()
	{
        DataContext = new Game_ProductSceneViewModel();
		InitializeComponent();
	}
}