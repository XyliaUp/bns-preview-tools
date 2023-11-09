namespace Xylia.Preview.UI.Art.GameUI.Scene.GuildCustomize_Customize;
public partial class GuildCustomize_Purchase : Window
{
	public GuildCustomize_Purchase()
	{
        DataContext = new GuildCustomize_PurchaseViewModel();
		InitializeComponent();
	}
}