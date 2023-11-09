namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_CustomizeCharacter;
public partial class Lobby_SelectWorldScene : Window
{
	public Lobby_SelectWorldScene()
	{
        DataContext = new Lobby_SelectWorldSceneViewModel();
		InitializeComponent();
	}
}