namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_PhotoAlbum;
public partial class Photo_Album_Scene : Window
{
	public Photo_Album_Scene()
	{
        DataContext = new Photo_Album_SceneViewModel();
		InitializeComponent();
	}
}