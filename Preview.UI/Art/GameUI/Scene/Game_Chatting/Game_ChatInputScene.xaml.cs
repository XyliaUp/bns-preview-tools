namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Chatting;
public partial class Game_ChatInputScene : Window
{
	public Game_ChatInputScene()
	{
        DataContext = new Game_ChatInputSceneViewModel();
		InitializeComponent();
	}
}