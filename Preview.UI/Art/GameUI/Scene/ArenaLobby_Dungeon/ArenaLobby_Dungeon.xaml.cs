namespace Xylia.Preview.UI.Art.GameUI.Scene.ArenaLobby_Dungeon;
public partial class ArenaLobby_Dungeon : Window
{
	public ArenaLobby_Dungeon()
	{
        DataContext = new ArenaLobby_DungeonViewModel();
		InitializeComponent();
	}
}