namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_EventGuide;
public partial class Game_EventGuideScene : Window
{
	public Game_EventGuideScene()
	{
        DataContext = new Game_EventGuideSceneViewModel();
		InitializeComponent();
	}
}