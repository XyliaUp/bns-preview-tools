namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Game_ArenaLobby_ConfirmScene : Window
{
	public Game_ArenaLobby_ConfirmScene()
	{
        DataContext = new Game_ArenaLobby_ConfirmSceneViewModel();
		InitializeComponent();
	}
}