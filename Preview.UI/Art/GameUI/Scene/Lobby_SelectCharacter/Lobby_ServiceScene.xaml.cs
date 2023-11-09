namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_SelectCharacter;
public partial class Lobby_ServiceScene : Window
{
	public Lobby_ServiceScene()
	{
        DataContext = new Lobby_ServiceSceneViewModel();
		InitializeComponent();
	}
}