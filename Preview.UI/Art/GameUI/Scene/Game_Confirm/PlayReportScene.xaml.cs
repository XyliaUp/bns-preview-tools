namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Confirm;
public partial class PlayReportScene : Window
{
	public PlayReportScene()
	{
        DataContext = new PlayReportSceneViewModel();
		InitializeComponent();
	}
}