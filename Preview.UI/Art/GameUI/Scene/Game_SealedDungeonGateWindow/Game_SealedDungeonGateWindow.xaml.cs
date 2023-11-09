namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_SealedDungeonGateWindow;
public partial class Game_SealedDungeonGateWindow : Window
{
	public Game_SealedDungeonGateWindow()
	{
        DataContext = new Game_SealedDungeonGateWindowViewModel();
		InitializeComponent();
	}
}