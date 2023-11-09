namespace Xylia.Preview.UI.Art.GameUI.Scene.GuildCustomize_Customize;
public partial class GuildCustomize_CustomizeScene : Window
{
	public GuildCustomize_CustomizeScene()
	{
        DataContext = new GuildCustomize_CustomizeSceneViewModel();
		InitializeComponent();
	}
}