namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_IngameNotice;
public partial class Game_IngameNoticeScene : Window
{
	public Game_IngameNoticeScene()
	{
        DataContext = new Game_IngameNoticeSceneViewModel();
		InitializeComponent();
	}
}