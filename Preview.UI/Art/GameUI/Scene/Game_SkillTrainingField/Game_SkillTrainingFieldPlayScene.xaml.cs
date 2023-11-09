namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SkillTrainingField;
public partial class Game_SkillTrainingFieldPlayScene : Window
{
	public Game_SkillTrainingFieldPlayScene()
	{
        DataContext = new Game_SkillTrainingFieldPlaySceneViewModel();
		InitializeComponent();
	}
}