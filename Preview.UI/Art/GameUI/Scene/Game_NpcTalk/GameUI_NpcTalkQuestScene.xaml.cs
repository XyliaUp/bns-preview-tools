namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NpcTalk;
public partial class GameUI_NpcTalkQuestScene : Window
{
	public GameUI_NpcTalkQuestScene()
	{
        DataContext = new GameUI_NpcTalkQuestSceneViewModel();
		InitializeComponent();
	}
}