namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Achievement;
public partial class Game_AchievementTooltip_Scene : Window
{
	public Game_AchievementTooltip_Scene()
	{
        DataContext = new Game_AchievementTooltip_SceneViewModel();
		InitializeComponent();
	}
}