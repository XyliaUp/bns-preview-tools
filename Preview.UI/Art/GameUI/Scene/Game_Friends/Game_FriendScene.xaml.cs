namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Friends;
public partial class Game_FriendScene : Window
{
	public Game_FriendScene()
	{
        DataContext = new Game_FriendSceneViewModel();
		InitializeComponent();
	}
}