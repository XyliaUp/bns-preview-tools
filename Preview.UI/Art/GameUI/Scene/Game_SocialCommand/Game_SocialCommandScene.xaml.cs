namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SocialCommand;
public partial class Game_SocialCommandScene : Window
{
	public Game_SocialCommandScene()
	{
        DataContext = new Game_SocialCommandSceneViewModel();
		InitializeComponent();
	}
}