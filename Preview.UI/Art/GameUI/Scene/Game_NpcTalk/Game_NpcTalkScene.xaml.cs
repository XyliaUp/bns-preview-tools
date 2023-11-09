namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NpcTalk;
public partial class Game_NpcTalkScene : Window
{
	public Game_NpcTalkScene()
	{
        DataContext = new Game_NpcTalkSceneViewModel();
		InitializeComponent();
	}
}