namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Clock;
public partial class Game_ClockScene : Window
{
	public Game_ClockScene()
	{
        DataContext = new Game_ClockSceneViewModel();
		InitializeComponent();
	}
}