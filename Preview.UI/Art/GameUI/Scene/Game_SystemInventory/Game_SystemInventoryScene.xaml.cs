namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SystemInventory;
/// <summary>
/// 保管箱 & 流失物品
/// </summary>
public partial class Game_SystemInventoryScene : Window
{
	public Game_SystemInventoryScene()
	{
        DataContext = new Game_SystemInventorySceneViewModel();
		InitializeComponent();
	}
}