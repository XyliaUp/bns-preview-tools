namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NShop;
public partial class Game_NShop_Scene : Window
{
	public Game_NShop_Scene()
	{
        DataContext = new Game_NShop_SceneViewModel();
		InitializeComponent();
	}
}