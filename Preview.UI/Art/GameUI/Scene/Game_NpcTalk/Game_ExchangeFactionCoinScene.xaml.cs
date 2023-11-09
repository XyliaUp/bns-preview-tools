namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NpcTalk;
public partial class Game_ExchangeFactionCoinScene : Window
{
	public Game_ExchangeFactionCoinScene()
	{
        DataContext = new Game_ExchangeFactionCoinSceneViewModel();
		InitializeComponent();
	}
}