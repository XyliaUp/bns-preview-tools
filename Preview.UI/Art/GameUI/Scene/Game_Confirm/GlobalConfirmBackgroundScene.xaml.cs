namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class GlobalConfirmBackgroundScene : Window
{
	public GlobalConfirmBackgroundScene()
	{
        DataContext = new GlobalConfirmBackgroundSceneViewModel();
		InitializeComponent();
	}
}