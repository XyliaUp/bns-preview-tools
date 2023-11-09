namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PersonalCustomize;
public partial class Game_PersonalCustomize : Window
{
	public Game_PersonalCustomize()
	{
        DataContext = new Game_PersonalCustomizeViewModel();
		InitializeComponent();
	}
}