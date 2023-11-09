namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_Title;
public partial class Lobby_TooltipScene : Window
{
	public Lobby_TooltipScene()
	{
        DataContext = new Lobby_TooltipSceneViewModel();
		InitializeComponent();
	}
}