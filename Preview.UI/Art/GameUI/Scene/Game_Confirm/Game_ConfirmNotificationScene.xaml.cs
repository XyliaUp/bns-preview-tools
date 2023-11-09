namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Game_ConfirmNotificationScene : Window
{
	public Game_ConfirmNotificationScene()
	{
        DataContext = new Game_ConfirmNotificationSceneViewModel();
		InitializeComponent();
	}
}