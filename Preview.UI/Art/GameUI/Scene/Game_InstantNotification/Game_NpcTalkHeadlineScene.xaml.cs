namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_InstantNotification;
public partial class Game_NpcTalkHeadlineScene : Window
{
	public Game_NpcTalkHeadlineScene()
	{
        DataContext = new Game_NpcTalkHeadlineSceneViewModel();
		InitializeComponent();
	}
}