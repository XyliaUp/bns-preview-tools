namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_Main_Scene;
public partial class Lobby_MainScene : Window
{
	public Lobby_MainScene()
	{
        DataContext = new Lobby_MainSceneViewModel();
		InitializeComponent();
	}
}