namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Slate;
public partial class Game_SlateUIScene : Window
{
	public Game_SlateUIScene()
	{
        DataContext = new Game_SlateUISceneViewModel();
		InitializeComponent();
	}
}