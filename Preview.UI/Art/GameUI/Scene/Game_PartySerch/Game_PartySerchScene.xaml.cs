namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PartySerch;
public partial class Game_PartySerchScene : Window
{
	public Game_PartySerchScene()
	{
        DataContext = new Game_PartySerchSceneViewModel();
		InitializeComponent();
	}
}