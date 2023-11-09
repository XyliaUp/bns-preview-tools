namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Chatting;
public partial class Game_ChatOptionScene : Window
{
	public Game_ChatOptionScene()
	{
        DataContext = new Game_ChatOptionSceneViewModel();
		InitializeComponent();
	}
}