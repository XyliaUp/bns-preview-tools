namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Loading;
public partial class Loding : Window
{
	public Loding()
	{
		InitializeComponent();
		DataContext = new LodingViewModel();

		//FileCache.Data.LoadingImage
	}
}