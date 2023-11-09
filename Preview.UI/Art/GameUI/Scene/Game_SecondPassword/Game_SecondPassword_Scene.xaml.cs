namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SecondPassword;
public partial class Game_SecondPassword_Scene : Window
{
	public Game_SecondPassword_Scene()
	{
        DataContext = new Game_SecondPassword_SceneViewModel();
		InitializeComponent();
	}
}