namespace Xylia.Preview.UI.Art.GameUI.Scene.ArenaLobby_Duel;
public partial class ArenaLobby_SpectateDuelListScene : Window
{
	public ArenaLobby_SpectateDuelListScene()
	{
        DataContext = new ArenaLobby_SpectateDuelListSceneViewModel();
		InitializeComponent();
	}
}