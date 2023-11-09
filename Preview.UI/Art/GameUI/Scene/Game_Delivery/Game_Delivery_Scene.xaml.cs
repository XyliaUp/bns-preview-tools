namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Delivery;
public partial class Game_Delivery_Scene : Window
{
	public Game_Delivery_Scene()
	{
        DataContext = new Game_Delivery_SceneViewModel();
		InitializeComponent();
	}
}