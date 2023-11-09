namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GlyphInventory;
public partial class Game_GlyphInventoryScene : Window
{
	public Game_GlyphInventoryScene()
	{
        DataContext = new Game_GlyphInventorySceneViewModel();
		InitializeComponent();
	}
}