namespace Xylia.Preview.UI.Art.GameUI.Scene.Lobby_CreateCharacter;
/// <summary>
/// 创建角色时 选择职业
/// </summary>
public partial class Lobby_CreateCharacterScene : Window
{
	public Lobby_CreateCharacterScene()
	{
        DataContext = new Lobby_CreateCharacterSceneViewModel();
		InitializeComponent();
	}
}