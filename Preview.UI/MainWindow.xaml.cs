using System.ComponentModel;
using System.Windows;

using Xylia.Preview.UI.Helpers;
using Xylia.Preview.UI.Services;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Pages;

using GameUI = Xylia.Preview.UI.Art.GameUI.Scene;

namespace Xylia.Preview.UI;
public partial class MainWindow
{
	public MainWindow()
	{
		InitializeComponent();

		this.MinWidth = this.Width;
		this.MinHeight = this.Height;

		#region page
		SideMenu.ItemsSource = new List<object>()
		{
			new ControlPage<ItemPage>(),
			new ControlPage<TextView>(),
			new ControlPage<GameUI.Game_QuestJournal.Game_QuestJournalScene>(),
			new ControlPage<GameResourcePage>(),
			new ControlPage<AbilityPage>(),
		};
		SideMenu.SelectedIndex = 0;
		SideMenu_Switch(SideMenu, null);
		#endregion

		new UpdateService().CheckForUpdates();
		Register.Create();
	}

	#region Methods
	private void SideMenu_Switch(object sender, RoutedEventArgs e)
	{
		var page = SideMenu.SelectedItem as ControlPage;

		var content = page.Content;
		if (content is Window window)
		{
			window.Closed += (s, e) => page.Content = null;
			window.Show();
		}
		else if (content is FrameworkElement element)
		{
			Presenter.Content = element;
			SideMenuContainer.IsOpen = false;
		}
	}

	private void OpenSettings(object sender, RoutedEventArgs e)
	{
		new SettingsView().ShowDialog();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		var result = HandyControl.Controls.MessageBox.Show(
			StringHelper.Get("Application_ExitMessage"),
			StringHelper.Get("Message_Tip"), MessageBoxButton.YesNo);
		if (result != MessageBoxResult.Yes)
		{
			e.Cancel = true;
			return;
		}

		Application.Current.Shutdown();
	}
	#endregion
}