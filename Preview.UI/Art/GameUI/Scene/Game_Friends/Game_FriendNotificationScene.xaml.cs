namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Friends;
public partial class Game_FriendNotificationScene : Window
{
	public Game_FriendNotificationScene()
	{
        DataContext = new Game_FriendNotificationSceneViewModel();
		InitializeComponent();
	}
}