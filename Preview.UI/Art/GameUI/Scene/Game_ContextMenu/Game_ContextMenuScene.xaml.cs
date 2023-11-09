namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ContextMenu;
public partial class Game_ContextMenuScene : Window
{
	public Game_ContextMenuScene()
	{
        DataContext = new Game_ContextMenuSceneViewModel();
		InitializeComponent();
	}
}