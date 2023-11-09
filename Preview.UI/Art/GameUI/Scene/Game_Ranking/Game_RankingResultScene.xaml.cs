namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Ranking;
public partial class Game_RankingResultScene : Window
{
	public Game_RankingResultScene()
	{
        DataContext = new Game_RankingResultSceneViewModel();
		InitializeComponent();
	}
}