namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Survey;
public partial class Game_SurveyScene : Window
{
	public Game_SurveyScene()
	{
        DataContext = new Game_SurveySceneViewModel();
		InitializeComponent();
	}
}