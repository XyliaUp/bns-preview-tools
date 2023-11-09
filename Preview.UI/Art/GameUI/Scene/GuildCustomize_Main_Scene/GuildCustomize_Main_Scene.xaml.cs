namespace Xylia.Preview.UI.Art.GameUI.Scene.GuildCustomize_Main_Scene;
public partial class GuildCustomize_Main_Scene : Window
{
	public GuildCustomize_Main_Scene()
	{
        DataContext = new GuildCustomize_Main_SceneViewModel();
		InitializeComponent();
	}
}