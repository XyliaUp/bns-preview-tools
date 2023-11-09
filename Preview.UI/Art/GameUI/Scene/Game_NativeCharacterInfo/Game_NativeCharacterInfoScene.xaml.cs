namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_NativeCharacterInfo;
/// <summary>
/// 个人信息（旧版本，可看捏脸）
/// </summary>
public partial class Game_NativeCharacterInfoScene : Window
{
	public Game_NativeCharacterInfoScene()
	{
        DataContext = new Game_NativeCharacterInfoSceneViewModel();
		InitializeComponent();
	}
}