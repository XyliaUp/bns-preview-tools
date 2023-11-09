namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Global_ConfirmScene : Window
{
	public Global_ConfirmScene()
	{
        DataContext = new Global_ConfirmSceneViewModel();
		InitializeComponent();
	}
}