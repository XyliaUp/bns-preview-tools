namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ContentsJournal;
public partial class Game_ContentsJournalScene : Window
{
	public Game_ContentsJournalScene()
	{
        DataContext = new Game_ContentsJournalSceneViewModel();
		InitializeComponent();
	}
}