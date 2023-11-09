namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Boss;
public partial class Game_BossScene : Window
{
	public Game_BossScene()
	{
        DataContext = new Game_BossSceneViewModel();
		InitializeComponent();
	}
}