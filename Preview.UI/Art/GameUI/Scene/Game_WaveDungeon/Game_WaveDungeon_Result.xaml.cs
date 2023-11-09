namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_WaveDungeon;
public partial class Game_WaveDungeon_Result : Window
{
	public Game_WaveDungeon_Result()
	{
        DataContext = new Game_WaveDungeon_ResultViewModel();
		InitializeComponent();
	}
}