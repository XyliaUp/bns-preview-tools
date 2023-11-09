namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ReadingCandidate;
public partial class Game_ReadingCandidateScene : Window
{
	public Game_ReadingCandidateScene()
	{
        DataContext = new Game_ReadingCandidateSceneViewModel();
		InitializeComponent();
	}
}