namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Attendance;
public partial class Game_AttendanceScene : Window
{
	public Game_AttendanceScene()
	{
        DataContext = new Game_AttendanceSceneViewModel();
		InitializeComponent();
	}
}