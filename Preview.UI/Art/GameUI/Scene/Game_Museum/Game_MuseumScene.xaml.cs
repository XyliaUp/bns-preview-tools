namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Museum;
public partial class Game_MuseumScene : Window
{
	public Game_MuseumScene()
	{
        DataContext = new Game_MuseumSceneViewModel();
		InitializeComponent();
	}
}