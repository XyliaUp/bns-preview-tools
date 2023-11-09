namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Chatting;
public partial class Game_ChattingEmoticonScene : Window
{
	public Game_ChattingEmoticonScene()
	{
        DataContext = new Game_ChattingEmoticonSceneViewModel();
		InitializeComponent();
	}
}