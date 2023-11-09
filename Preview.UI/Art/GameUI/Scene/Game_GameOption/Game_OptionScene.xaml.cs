namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_GameOption;
public partial class Game_OptionScene : Window
{
	public Game_OptionScene()
	{
        DataContext = new Game_OptionSceneViewModel();
		InitializeComponent();
	}
}