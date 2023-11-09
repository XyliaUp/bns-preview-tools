namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_AttendanceTimeTable;
public partial class Game_AttendanceTimeTableScene : Window
{
	public Game_AttendanceTimeTableScene()
	{
        DataContext = new Game_AttendanceTimeTableSceneViewModel();
		InitializeComponent();
	}
}