using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.Services.Utils;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Pages;

using GameUI = Xylia.Preview.UI.Art.GameUI.Scene;

namespace Xylia.Preview.UI;
public partial class MainWindow : HandyControl.Controls.Window
{
	public MainWindow()
	{
		InitializeComponent();

		#region page
		PageControl.ItemsSource = new List<object>()
		{
			new ControlPage<ItemPage>(),
			new ControlPage<TextView>(),
			new ControlPage<GameUI.Game_QuestJournal.Game_QuestJournalScene>(),
			new ControlPage<GameResourcePage>(),
			new ControlPage<AbilityPage>(),
		};
		PageControl.SelectedIndex = 0;
		#endregion

		new Update().CheckForUpdates();
		Register.Create();
	}


	private void PageControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var page = PageControl.SelectedItem as ControlPage;
		var content = page.Content;

		if (content is Window window)
		{
			window.Closed += (s, e) => page.Content = null;
			window.Show();
		}
		else if (content is FrameworkElement element) Presenter.Content = element;
	}

	private void OpenSettings(object sender, RoutedEventArgs e)
	{
		new SettingsView().ShowDialog();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		var result = HandyControl.Controls.MessageBox.Show("您正在关闭应用程序, 是否确认这么做吗？", "提示信息", MessageBoxButton.YesNo);
		if (result != MessageBoxResult.Yes)
		{
			e.Cancel = true;
			return;
		}

		Application.Current.Shutdown();
	}
}