namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_LevelDownload;
public partial class Game_LevelDownloadScene : Window
{
	public Game_LevelDownloadScene()
	{
        DataContext = new Game_LevelDownloadSceneViewModel();
		InitializeComponent();
	}
}