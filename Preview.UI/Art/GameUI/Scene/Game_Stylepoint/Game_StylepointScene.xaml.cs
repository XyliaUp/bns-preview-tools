namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Stylepoint;
public partial class Game_StylepointScene : Window
{
	public Game_StylepointScene()
	{
        DataContext = new Game_StylepointSceneViewModel();
		InitializeComponent();
	}
}