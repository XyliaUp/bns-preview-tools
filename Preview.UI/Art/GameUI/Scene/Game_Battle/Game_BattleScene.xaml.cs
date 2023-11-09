namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Battle;
public partial class Game_BattleScene : Window
{
	public Game_BattleScene()
	{
        DataContext = new Game_BattleSceneViewModel();
		InitializeComponent();
	}
}