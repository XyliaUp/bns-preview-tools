namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GameTip;
public partial class Game_GuideScene : Window
{
	public Game_GuideScene()
	{
        DataContext = new Game_GuideSceneViewModel();
		InitializeComponent();
	}
}