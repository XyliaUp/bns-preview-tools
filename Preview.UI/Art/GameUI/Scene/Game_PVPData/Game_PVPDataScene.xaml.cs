namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PVPData;
public partial class Game_PVPDataScene : Window
{
	public Game_PVPDataScene()
	{
        DataContext = new Game_PVPDataSceneViewModel();
		InitializeComponent();
	}
}