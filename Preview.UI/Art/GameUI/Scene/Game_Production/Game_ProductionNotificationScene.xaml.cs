namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Production;
public partial class Game_ProductionNotificationScene : Window
{
	public Game_ProductionNotificationScene()
	{
        DataContext = new Game_ProductionNotificationSceneViewModel();
		InitializeComponent();
	}
}