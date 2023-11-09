namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_JumpingLevel;
public partial class Game_JumpingLevel : Window
{
	public Game_JumpingLevel()
	{
        DataContext = new Game_JumpingLevelViewModel();
		InitializeComponent();
	}
}