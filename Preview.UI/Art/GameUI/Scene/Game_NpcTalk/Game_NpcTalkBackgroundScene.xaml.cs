namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NpcTalk;
public partial class Game_NpcTalkBackgroundScene : Window
{
	public Game_NpcTalkBackgroundScene()
	{
        DataContext = new Game_NpcTalkBackgroundSceneViewModel();
		InitializeComponent();
	}
}