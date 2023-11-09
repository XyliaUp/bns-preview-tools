namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Lobby_ConfirmScene : Window
{
	public Lobby_ConfirmScene()
	{
        DataContext = new Lobby_ConfirmSceneViewModel();
		InitializeComponent();
	}
}