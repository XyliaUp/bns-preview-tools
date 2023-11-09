namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_BattlePass;
public partial class Game_BattlePass : Window
{
	public Game_BattlePass()
	{
        DataContext = new Game_BattlePassViewModel();
		InitializeComponent();
	}
}