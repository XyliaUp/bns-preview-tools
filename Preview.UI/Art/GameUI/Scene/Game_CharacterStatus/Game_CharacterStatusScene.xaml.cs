namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_CharacterStatus;
/// <summary>
/// ∏ˆ»À√Ê∞Â
/// </summary>
public partial class Game_CharacterStatusScene : Window
{
	public Game_CharacterStatusScene()
	{
        DataContext = new Game_CharacterStatusSceneViewModel();
		InitializeComponent();
	}
}