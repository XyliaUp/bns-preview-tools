namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
public partial class Game_ItemMapScene : GameScene
{
	public Game_ItemMapScene()
	{
		InitializeComponent();
		ItemMapPanel.DataContext = this.DataContext;
	}
}