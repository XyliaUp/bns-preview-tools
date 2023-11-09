namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NpcTalk;
public partial class Game_TalkStateGuildScene : Window
{
	public Game_TalkStateGuildScene()
	{
        DataContext = new Game_TalkStateGuildSceneViewModel();
		InitializeComponent();
	}
}