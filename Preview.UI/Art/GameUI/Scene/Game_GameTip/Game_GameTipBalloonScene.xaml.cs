namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GameTip;
public partial class Game_GameTipBalloonScene : Window
{
	public Game_GameTipBalloonScene()
	{
        DataContext = new Game_GameTipBalloonSceneViewModel();
		InitializeComponent();
	}
}