namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_DuelBotChallenge;
public partial class Game_DuelBotChallengeScene : Window
{
	public Game_DuelBotChallengeScene()
	{
        DataContext = new Game_DuelBotChallengeSceneViewModel();
		InitializeComponent();
	}
}