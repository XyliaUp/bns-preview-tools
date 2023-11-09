namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_InstantNotification;
public partial class Game_Global_HeadlineScene : Window
{
	public Game_Global_HeadlineScene()
	{
        DataContext = new Game_Global_HeadlineSceneViewModel();
		InitializeComponent();
	}
}