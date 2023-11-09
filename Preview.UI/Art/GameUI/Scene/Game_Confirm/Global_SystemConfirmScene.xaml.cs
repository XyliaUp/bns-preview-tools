namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class Global_SystemConfirmScene : Window
{
	public Global_SystemConfirmScene()
	{
        DataContext = new Global_SystemConfirmSceneViewModel();
		InitializeComponent();
	}
}