namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Production;
public partial class Game_ProductionScene : Window
{
	public Game_ProductionScene()
	{
        DataContext = new Game_ProductionSceneViewModel();
		InitializeComponent();
	}
}