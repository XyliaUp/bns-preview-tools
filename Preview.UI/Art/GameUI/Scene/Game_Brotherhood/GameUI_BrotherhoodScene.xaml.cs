namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Brotherhood;
public partial class GameUI_BrotherhoodScene : Window
{
	public GameUI_BrotherhoodScene()
	{
        DataContext = new GameUI_BrotherhoodSceneViewModel();
		InitializeComponent();
	}
}