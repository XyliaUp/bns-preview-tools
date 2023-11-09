namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_DigitalRiver;
public partial class Game_DigitalRiverScene : Window
{
	public Game_DigitalRiverScene()
	{
        DataContext = new Game_DigitalRiverSceneViewModel();
		InitializeComponent();
	}
}