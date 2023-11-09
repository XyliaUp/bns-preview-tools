namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_TencentCharge;
public partial class Game_TencentCharge_Scene : Window
{
	public Game_TencentCharge_Scene()
	{
        DataContext = new Game_TencentCharge_SceneViewModel();
		InitializeComponent();
	}
}