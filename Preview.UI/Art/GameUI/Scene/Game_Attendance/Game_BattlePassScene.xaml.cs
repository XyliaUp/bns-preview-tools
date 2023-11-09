namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Attendance;
public partial class Game_BattlePassScene : Window
{
	public Game_BattlePassScene()
	{
        DataContext = new Game_BattlePassSceneViewModel();
		InitializeComponent();
	}
}