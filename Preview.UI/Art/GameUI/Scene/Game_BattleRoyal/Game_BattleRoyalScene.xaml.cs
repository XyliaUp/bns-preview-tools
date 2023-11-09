namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_BattleRoyal;
public partial class Game_BattleRoyalScene : Window
{
	public Game_BattleRoyalScene()
	{
        DataContext = new Game_BattleRoyalSceneViewModel();
		InitializeComponent();
	}
}