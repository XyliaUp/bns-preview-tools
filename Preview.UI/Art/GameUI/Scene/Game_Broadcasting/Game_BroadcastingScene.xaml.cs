namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Broadcasting;
/// <summary>
/// Ω£¡ÈTv
/// </summary>
public partial class Game_BroadcastingScene : Window
{
	public Game_BroadcastingScene()
	{
        DataContext = new Game_BroadcastingSceneViewModel();
		InitializeComponent();
	}
}