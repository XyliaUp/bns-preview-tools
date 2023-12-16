using System.Windows;
using System.Windows.Controls;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers.Output.Quests;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalScene
{
	#region Window
	protected override void OnLoading()
	{
		InitializeComponent();

		// Progress
		QuestJournal_ProgressQuestList.ItemsSource = FileCache.Data.Get<Quest>().OrderBy(q => q.Source.RecordId);

		// Completed
		List<Quest> CompletedQuest = [];
		QuestEpic.GetEpic(CompletedQuest.Add);
		TreeView2.ItemsSource = CompletedQuest.GroupBy(o => o.Title).Select(o => new TreeViewItem
		{
			Header = o.Key,
			ItemsSource = o.ToList(),
		});
	}
	#endregion

	#region CompletedTab 
	private void CompletedTab_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not Quest quest) return;

		TextBlock2.Text = quest.Attributes["completed-desc"].GetText();
	}
	#endregion


	#region Extract
	private void Extract_QuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestOut>();

	private void Extract_EpicQuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestEpic>();
	#endregion
}