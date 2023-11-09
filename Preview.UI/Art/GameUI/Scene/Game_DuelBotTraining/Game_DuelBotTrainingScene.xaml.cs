namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_DuelBotTraining;
public partial class Game_DuelBotTrainingScene : Window
{
	public Game_DuelBotTrainingScene()
	{
        DataContext = new Game_DuelBotTrainingSceneViewModel();
		InitializeComponent();
	}
}