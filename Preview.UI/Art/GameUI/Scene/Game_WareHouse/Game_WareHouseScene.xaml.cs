namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_WareHouse;
public partial class Game_WareHouseScene : Window
{
	public Game_WareHouseScene()
	{
        DataContext = new Game_WareHouseSceneViewModel();
		InitializeComponent();
	}
}