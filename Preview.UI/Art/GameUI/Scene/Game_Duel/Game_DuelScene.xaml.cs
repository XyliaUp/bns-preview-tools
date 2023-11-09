namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Duel;
public partial class Game_DuelScene : Window
{
	public Game_DuelScene()
	{
        DataContext = new Game_DuelSceneViewModel();
		InitializeComponent();
	}
}