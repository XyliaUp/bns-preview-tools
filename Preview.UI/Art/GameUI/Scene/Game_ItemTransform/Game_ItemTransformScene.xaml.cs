namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemTransform;
public partial class Game_ItemTransformScene : Window
{
	public Game_ItemTransformScene()
	{
        DataContext = new Game_ItemTransformSceneViewModel();
		InitializeComponent();
	}
}