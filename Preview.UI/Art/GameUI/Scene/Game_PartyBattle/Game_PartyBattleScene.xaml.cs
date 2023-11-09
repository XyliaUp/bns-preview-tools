namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PartyBattle;
public partial class Game_PartyBattleScene : Window
{
	public Game_PartyBattleScene()
	{
        DataContext = new Game_PartyBattleSceneViewModel();
		InitializeComponent();
	}
}