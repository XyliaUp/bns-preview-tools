namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GameTip;
public partial class Game_GameTipScene : Window
{
	public Game_GameTipScene()
	{
        DataContext = new Game_GameTipSceneViewModel();
		InitializeComponent();
	}
}