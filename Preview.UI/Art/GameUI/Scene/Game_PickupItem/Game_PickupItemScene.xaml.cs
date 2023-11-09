namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PickupItem;
public partial class Game_PickupItemScene : Window
{
	public Game_PickupItemScene()
	{
        DataContext = new Game_PickupItemSceneViewModel();
		InitializeComponent();
	}
}