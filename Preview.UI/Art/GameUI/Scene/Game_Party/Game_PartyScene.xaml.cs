namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Party;
public partial class Game_PartyScene : Window
{
	public Game_PartyScene()
	{
        DataContext = new Game_PartySceneViewModel();
		InitializeComponent();
	}
}