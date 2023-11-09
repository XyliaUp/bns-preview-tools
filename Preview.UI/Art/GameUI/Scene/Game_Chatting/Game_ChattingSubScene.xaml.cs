namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Chatting;
public partial class Game_ChattingSubScene : Window
{
	public Game_ChattingSubScene()
	{
        DataContext = new Game_ChattingSubSceneViewModel();
		InitializeComponent();
	}
}