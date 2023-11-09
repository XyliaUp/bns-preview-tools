namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GoodsStore;
public partial class Game_GoodsStore_Scene : Window
{
	public Game_GoodsStore_Scene()
	{
        DataContext = new Game_GoodsStore_SceneViewModel();
		InitializeComponent();
	}
}