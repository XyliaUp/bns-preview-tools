namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_TradeShelf;
public partial class Game_TradeShelfScene : Window
{
	public Game_TradeShelfScene()
	{
        DataContext = new Game_TradeShelfSceneViewModel();
		InitializeComponent();
	}
}