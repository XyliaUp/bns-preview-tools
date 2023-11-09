namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_TreasureBoard;
public partial class GameUI_TreasureBoardScene : Window
{
	public GameUI_TreasureBoardScene()
	{
        DataContext = new GameUI_TreasureBoardSceneViewModel();
		InitializeComponent();
	}
}