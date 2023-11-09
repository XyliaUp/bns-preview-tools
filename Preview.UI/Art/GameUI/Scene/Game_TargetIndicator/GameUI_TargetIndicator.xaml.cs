namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_TargetIndicator;
public partial class GameUI_TargetIndicator : Window
{
	public GameUI_TargetIndicator()
	{
        DataContext = new GameUI_TargetIndicatorViewModel();
		InitializeComponent();
	}
}