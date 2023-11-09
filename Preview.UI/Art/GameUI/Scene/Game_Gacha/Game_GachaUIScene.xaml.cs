namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Gacha;
public partial class Game_GachaUIScene : Window
{
	public Game_GachaUIScene()
	{
        DataContext = new Game_GachaUISceneViewModel();
		InitializeComponent();
	}
}