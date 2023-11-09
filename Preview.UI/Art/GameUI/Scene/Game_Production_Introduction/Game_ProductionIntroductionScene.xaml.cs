namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Production_Introduction;
public partial class Game_ProductionIntroductionScene : Window
{
	public Game_ProductionIntroductionScene()
	{
        DataContext = new Game_ProductionIntroductionSceneViewModel();
		InitializeComponent();
	}
}