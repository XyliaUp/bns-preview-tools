namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SNS;
public partial class Game_SNSScene : Window
{
	public Game_SNSScene()
	{
        DataContext = new Game_SNSSceneViewModel();
		InitializeComponent();
	}
}