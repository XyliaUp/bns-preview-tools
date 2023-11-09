namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Party;
public partial class Game_PartyOptionScene : Window
{
	public Game_PartyOptionScene()
	{
        DataContext = new Game_PartyOptionSceneViewModel();
		InitializeComponent();
	}
}