using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers.Output.Quests;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalScene
{
	#region Window
	protected override void OnLoading()
	{
		InitializeComponent();

		// Progress
		source = CollectionViewSource.GetDefaultView(FileCache.Data.Provider.GetTable<Quest>().OrderBy(x => x.Source.PrimaryKey));
		source.Filter = QuestFilter;
		QuestJournal_ProgressQuestList.ItemsSource = source;


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


	#region Methods
	// ------------------------------------------------------------------
	// 
	//  ProgressTab
	// 
	// ------------------------------------------------------------------
	private void SearcherRule_TextChanged(object sender, TextChangedEventArgs e)
	{
		source.Refresh();
	}

	private bool QuestFilter(object obj)
	{
		if (obj is not Quest quest) return false;

		// rule valid
		var rule = SearcherRule.Text;

		var IsEmpty = string.IsNullOrEmpty(rule);
		if (IsEmpty) return true;

		// filter 
		if (int.TryParse(rule, out int id)) return quest.Source.PrimaryKey.Id == id;

		if (quest.Text?.IndexOf(rule, StringComparison.OrdinalIgnoreCase) > 0) return true;
		if (quest.Title?.IndexOf(rule, StringComparison.OrdinalIgnoreCase) > 0) return true;

		return false;
	}



	// ------------------------------------------------------------------
	// 
	//  CompletedTab
	// 
	// ------------------------------------------------------------------
	private void CompletedTab_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not Quest quest) return;

		TextBlock2.Text = quest.Attributes["completed-desc"].GetText();
	}

	// ------------------------------------------------------------------
	// 
	//  Extract
	// 
	// ------------------------------------------------------------------
	private void Extract_QuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestOut>();

	private void Extract_EpicQuestList_Click(object sender, RoutedEventArgs e) => ItemPageViewModel.StartOutput<QuestEpic>();
	#endregion

	#region Private Fields
	private ICollectionView source;
	#endregion
}