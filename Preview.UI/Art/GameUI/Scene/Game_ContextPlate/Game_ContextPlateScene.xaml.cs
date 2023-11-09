namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ContextPlate;
public partial class Game_ContextPlateScene : Window
{
	public Game_ContextPlateScene()
	{
        DataContext = new Game_ContextPlateSceneViewModel();
		InitializeComponent();
	}
}