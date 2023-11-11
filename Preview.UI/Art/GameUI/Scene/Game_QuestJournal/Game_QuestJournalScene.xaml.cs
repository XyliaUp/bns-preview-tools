using System.ComponentModel;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.Input;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Helpers.Output.Quests;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Editor;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalScene : GameScene
{
	#region Ctr
	public Game_QuestJournalScene()
	{
		InitializeComponent();
	}

	protected override void OnLoading(EventArgs e)
	{
		// Quest
		TreeView1.ItemsSource = FileCache.Data.Quest.OrderBy(q => q.id);

		// Completed
		List<Quest> CompletedQuest = new();
		QuestEpic.GetEpic(CompletedQuest.Add);
		TreeView2.ItemsSource = CompletedQuest.GroupBy(o => o.Group2).Select(o => new TreeViewItem
		{
			Header = o.Key.GetText(),
			ItemsSource = o.ToList(),
		});
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		this.Hide();
		e.Cancel = true;
	}
	#endregion


	#region ProgressTab
	private void ProgressTab_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		QuestJournalPanel.DataContext = e.NewValue as Quest;
	}

	[RelayCommand]
	public static void ViewRaw(Quest record)
	{
		// Warning: is not original text
		var editor = new TextEditor { Text = record.Owner.WriteXml(record) };
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