namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_Title;
public partial class Lobby_TitleScene : Window
{
	public Lobby_TitleScene()
	{
        DataContext = new Lobby_TitleSceneViewModel();
		InitializeComponent();
	}
}