namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Chatting;
public partial class Game_ChattingScene : Window
{
	public Game_ChattingScene()
	{
        DataContext = new Game_ChattingSceneViewModel();
		InitializeComponent();
	}
}