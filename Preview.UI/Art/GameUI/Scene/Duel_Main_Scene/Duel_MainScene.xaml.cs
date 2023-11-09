namespace Xylia.Preview.UI.Art.GameUI.Scene.Duel_Main_Scene;
public partial class Duel_MainScene : Window
{
	public Duel_MainScene()
	{
        DataContext = new Duel_MainSceneViewModel();
		InitializeComponent();
	}
}