namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PowerBook;
public partial class Game_PowerBook_Scene : Window
{
	public Game_PowerBook_Scene()
	{
        DataContext = new Game_PowerBook_SceneViewModel();
		InitializeComponent();
	}
}