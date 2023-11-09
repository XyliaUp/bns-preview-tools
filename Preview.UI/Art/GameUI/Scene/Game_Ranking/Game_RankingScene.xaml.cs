namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Ranking;
public partial class Game_RankingScene : Window
{
	public Game_RankingScene()
	{
        DataContext = new Game_RankingSceneViewModel();
		InitializeComponent();
	}
}