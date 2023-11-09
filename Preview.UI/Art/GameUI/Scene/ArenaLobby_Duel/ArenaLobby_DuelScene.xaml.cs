namespace Xylia.Preview.UI.Art.GameUI.Scene.ArenaLobby_Duel;
public partial class ArenaLobby_DuelScene : Window
{
	public ArenaLobby_DuelScene()
	{
        DataContext = new ArenaLobby_DuelSceneViewModel();
		InitializeComponent();
	}
}