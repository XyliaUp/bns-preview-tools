namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SkillTrainingField;
public partial class Game_SkillTrainingFieldScene : Window
{
	public Game_SkillTrainingFieldScene()
	{
        DataContext = new Game_SkillTrainingFieldSceneViewModel();
		InitializeComponent();
	}
}