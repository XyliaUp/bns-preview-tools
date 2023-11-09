namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_SelectCharacter;
public partial class Lobby_SelectCharacterScene : Window
{
	public Lobby_SelectCharacterScene()
	{
        DataContext = new Lobby_SelectCharacterSceneViewModel();
		InitializeComponent();
	}
}