namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Loading;
/// <summary>
/// 过图（含过图时的排行信息）
/// </summary>
public partial class Loading_ConfirmScene : Window
{
	public Loading_ConfirmScene()
	{
        DataContext = new Loading_ConfirmSceneViewModel();
		InitializeComponent();
	}
}