namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Intension;
public partial class Game_IntensionScene : Window
{
	public Game_IntensionScene()
	{
        DataContext = new Game_IntensionSceneViewModel();
		InitializeComponent();
	}
}