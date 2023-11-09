using System.ComponentModel;
using System.Windows.Controls;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers.Output.Quests;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Editor;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalScene : Window
{
	public Game_QuestJournalScene()
	{
		DataContext = new Game_QuestJournalSceneViewModel();
		InitializeComponent();

		QuestJournalPanel.DataContext = null;

		LoadData(0);
		LoadData(2);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		this.Hide();
		e.Cancel = true;
	}


	#region ProgressTab
	public void LoadData(int type = 0)
	{
		if (type == 0)
		{
			TreeView1.ItemsSource = FileCache.Data.Quest.OrderBy(q => q.id);
		}


		if (type == 2)
		{
			List<Quest> CompletedQuest = new();
			QuestEpic.GetEpic(CompletedQuest.Add);

			TreeView2.ItemsSource = CompletedQuest.GroupBy(o => o.Group2).Select(o => new TreeViewItem
			{
				Header = o.Key.GetText(),
				ItemsSource = o.ToList(),
			});
		}
	}

	private void ProgressTab_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		QuestJournalPanel.DataContext = e.NewValue as Quest;
	}

	private void ViewRawData_Click(object sender, RoutedEventArgs e)
	{
		var menu = ((MenuItem)sender).Parent as ContextMenu;
		var data = ((FrameworkElement)menu.PlacementTarget).DataContext;
		if (data is not Quest quest) return;

		// Warning: is not original text
		var editor = new TextEditor
		{
			Text = quest.Owner.WriteXml(quest)
		};
		editor.Show();
	}
	#endregion

	#region CompletedTab 
	private void CompletedTab_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not Quest quest) return;

		TextBlock2.Text = quest.CompletedDesc.GetText();
	}
	#endregion


	#region Extract
	private void Extract_QuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestOut>();

	private void Extract_EpicQuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestEpic>();
	#endregion
}