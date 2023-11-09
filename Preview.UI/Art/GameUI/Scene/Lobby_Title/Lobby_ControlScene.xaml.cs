namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_Title;
public partial class Lobby_ControlScene : Window
{
	public Lobby_ControlScene()
	{
        DataContext = new Lobby_ControlSceneViewModel();
		InitializeComponent();
	}
}