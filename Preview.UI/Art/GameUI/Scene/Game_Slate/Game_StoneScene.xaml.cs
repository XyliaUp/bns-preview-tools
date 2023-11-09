namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Slate;
public partial class Game_StoneScene : Window
{
	public Game_StoneScene()
	{
        DataContext = new Game_StoneSceneViewModel();
		InitializeComponent();
	}
}