namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Inspector;
/// <summary>
/// 物品信息提示工具
/// </summary>
public partial class Game_InspectorScene : Window
{
	public Game_InspectorScene()
	{
        DataContext = new Game_InspectorSceneViewModel();
		InitializeComponent();
	}
}