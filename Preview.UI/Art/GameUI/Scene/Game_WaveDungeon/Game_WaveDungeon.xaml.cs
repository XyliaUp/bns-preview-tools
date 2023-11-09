namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_WaveDungeon;
public partial class Game_WaveDungeon : Window
{
	public Game_WaveDungeon()
	{
        DataContext = new Game_WaveDungeonViewModel();
		InitializeComponent();
	}
}