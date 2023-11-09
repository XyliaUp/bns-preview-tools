namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_CashInventory;
/// <summary>
/// 마롤관범
/// </summary>
public partial class Game_CashInventoryScene : Window
{
	public Game_CashInventoryScene()
	{
        DataContext = new Game_CashInventorySceneViewModel();
		InitializeComponent();
	}
}