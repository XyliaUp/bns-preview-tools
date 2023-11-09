namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_QQ;
public partial class Game_QQScene : Window
{
	public Game_QQScene()
	{
        DataContext = new Game_QQSceneViewModel();
		InitializeComponent();
	}
}