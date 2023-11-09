namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Game_ConfirmScene : Window
{
	public Game_ConfirmScene()
	{
        DataContext = new Game_ConfirmSceneViewModel();
		InitializeComponent();
	}
}