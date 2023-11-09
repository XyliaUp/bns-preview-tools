namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ContentsGuide;
public partial class Game_ContentsGuideScene : Window
{
	public Game_ContentsGuideScene()
	{
        DataContext = new Game_ContentsGuideSceneViewModel();
		InitializeComponent();
	}
}