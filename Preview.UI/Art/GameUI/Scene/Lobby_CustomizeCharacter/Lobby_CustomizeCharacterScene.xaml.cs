namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_CustomizeCharacter;
/// <summary>
/// ÄóÁ³
/// </summary>
public partial class Lobby_CustomizeCharacterScene : Window
{
	public Lobby_CustomizeCharacterScene()
	{
        DataContext = new Lobby_CustomizeCharacterSceneViewModel();
		InitializeComponent();
	}
}