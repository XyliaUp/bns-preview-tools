namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_Title;
public partial class Lobby_FocusScene : Window
{
	public Lobby_FocusScene()
	{
        DataContext = new Lobby_FocusSceneViewModel();
		InitializeComponent();
	}
}