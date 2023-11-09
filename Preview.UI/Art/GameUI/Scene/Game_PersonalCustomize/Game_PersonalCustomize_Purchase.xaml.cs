namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PersonalCustomize;
public partial class Game_PersonalCustomize_Purchase : Window
{
	public Game_PersonalCustomize_Purchase()
	{
        DataContext = new Game_PersonalCustomize_PurchaseViewModel();
		InitializeComponent();
	}
}