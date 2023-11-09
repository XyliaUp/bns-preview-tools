namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SoulBoost;
public partial class Game_SoulBoostScene : Window
{
	public Game_SoulBoostScene()
	{
        DataContext = new Game_SoulBoostSceneViewModel();
		InitializeComponent();
	}
}