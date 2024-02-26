using System.ComponentModel;
using System.Windows;

using Xylia.Preview.UI.GameUI.Scene.Game_QuestJournal;
using Xylia.Preview.UI.Services;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Pages;

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
			new PageController<ItemPage>(),
			new PageController<TextView>(),
			new PageController<Game_QuestJournalScene>(),
			new PageController<GameResourcePage>(),
			new PageController<AbilityPage>(),
		};
		SideMenu.SelectedIndex = 0;
		SideMenu_Switch(SideMenu, null);
		#endregion

		new UpdateService().Register();
		RegisterService.Create();
	}


	#region Methods
	private void SideMenu_Switch(object sender, RoutedEventArgs e)
	{
		SideMenuContainer.IsOpen = false;
		var page = (IPageController)SideMenu.SelectedItem;

		var content = page!.Content;
		if (content is Window window)
		{
			window.Closed += (s, e) => page.Content = null;
			window.Show();
		}
		else if (content is FrameworkElement element)
		{
			Presenter.Content = element;
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