namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_HomeBrowser;
public partial class Lobby_HomeBrowser_Scene : Window
{
	public Lobby_HomeBrowser_Scene()
	{
        DataContext = new Lobby_HomeBrowser_SceneViewModel();
		InitializeComponent();
	}
}