namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ContentsGuide;
public partial class Game_DungeonGuideScene : Window
{
	public Game_DungeonGuideScene()
	{
        DataContext = new Game_DungeonGuideSceneViewModel();
		InitializeComponent();
	}
}