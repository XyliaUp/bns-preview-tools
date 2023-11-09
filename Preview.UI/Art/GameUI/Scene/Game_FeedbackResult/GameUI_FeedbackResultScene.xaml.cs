namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_FeedbackResult;
public partial class GameUI_FeedbackResultScene : Window
{
	public GameUI_FeedbackResultScene()
	{
        DataContext = new GameUI_FeedbackResultSceneViewModel();
		InitializeComponent();
	}
}