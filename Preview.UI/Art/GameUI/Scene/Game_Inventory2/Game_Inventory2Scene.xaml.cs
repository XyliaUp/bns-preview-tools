namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Inventory2;
public partial class Game_Inventory2Scene : Window
{
	public Game_Inventory2Scene()
	{
        DataContext = new Game_Inventory2SceneViewModel();
		InitializeComponent();
	}
}