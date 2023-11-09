namespace Xylia.Preview.UI.Art.GameUI.Scene.Intro_Main_Scene;
public partial class Intro_MainScene : Window
{
	public Intro_MainScene()
	{
        DataContext = new Intro_MainSceneViewModel();
		InitializeComponent();
	}
}