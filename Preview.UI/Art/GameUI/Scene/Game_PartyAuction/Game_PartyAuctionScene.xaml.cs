namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PartyAuction;
public partial class Game_PartyAuctionScene : Window
{
	public Game_PartyAuctionScene()
	{
        DataContext = new Game_PartyAuctionSceneViewModel();
		InitializeComponent();
	}
}