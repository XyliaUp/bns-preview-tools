namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_NShop;
public partial class Lobby_NShop_Scene : Window
{
	public Lobby_NShop_Scene()
	{
        DataContext = new Lobby_NShop_SceneViewModel();
		InitializeComponent();
	}
}