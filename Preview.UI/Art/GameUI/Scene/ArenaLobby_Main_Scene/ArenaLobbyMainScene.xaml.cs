namespace Xylia.Preview.UI.Art.GameUI.Scene.ArenaLobby_Main_Scene;
public partial class ArenaLobbyMainScene : Window
{
	public ArenaLobbyMainScene()
	{
        DataContext = new ArenaLobbyMainSceneViewModel();
		InitializeComponent();
	}
}