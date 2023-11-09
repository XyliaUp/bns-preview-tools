namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Fishing;
public partial class Game_NPCFishingInfoScene : Window
{
	public Game_NPCFishingInfoScene()
	{
        DataContext = new Game_NPCFishingInfoSceneViewModel();
		InitializeComponent();
	}
}