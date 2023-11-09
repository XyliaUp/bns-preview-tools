namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GuildVoiceChat;
public partial class Game_VoiceChatHUDScene : Window
{
	public Game_VoiceChatHUDScene()
	{
        DataContext = new Game_VoiceChatHUDSceneViewModel();
		InitializeComponent();
	}
}