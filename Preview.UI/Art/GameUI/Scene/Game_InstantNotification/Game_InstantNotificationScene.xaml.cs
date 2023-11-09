namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_InstantNotification;
/// <summary>
/// 立即提示工具
/// </summary>
public partial class Game_InstantNotificationScene : Window
{
	public Game_InstantNotificationScene()
	{
        DataContext = new Game_InstantNotificationSceneViewModel();
		InitializeComponent();
	}
}