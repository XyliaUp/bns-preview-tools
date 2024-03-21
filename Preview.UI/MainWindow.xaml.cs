using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.GameUI.Scene.Game_QuestJournal;
using Xylia.Preview.UI.Resources.Themes;
using Xylia.Preview.UI.Services;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Pages;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Xylia.Preview.UI;
public partial class MainWindow
{
	#region Constructor
	public MainWindow()
	{
		InitializeComponent();

		this.MinWidth = this.Width;
		this.MinHeight = this.Height;

		#region page
		SideMenu.ItemsSource = new List<object>()
		{
			new PageController<ItemPage>(),
			new PageController<TextView>("TextView_Name"),
			new PageController<Legacy_QuestJournalPanel>("Page_QuestJournalPanel"),
			new PageController<GameResourcePage>(),
			new PageController<AbilityPage>(),
		};
		SideMenu.SelectedIndex = 0;
		SideMenu_Switch(SideMenu, new RoutedEventArgs());
		#endregion

		new ServiceManager() { new UpdateService(), new RegisterService() }.RegisterAll();
	}
	#endregion


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
		else if (content is BnsCustomWindowWidget window2)
		{
			window2.Closed += (s, e) => page.Content = null;
			window2.Show();
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

	private void OpenPopupSkin(object sender, RoutedEventArgs e) => PopupSkin.IsOpen = true;

	private void ButtonSkins_OnClick(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is Button { Tag: SkinType skinType })
		{
			PopupSkin.IsOpen = false;
			UserSettings.Default.SkinType = skinType;
		}
	}

	private void SwitchNight_OnClick(object sender, RoutedEventArgs e)
	{
		var current = UserSettings.Default.NightMode;
		if (current == null && MessageBox.Show(StringHelper.Get("Settings_NightMode_Ask"), StringHelper.Get("Message_Tip"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

		UserSettings.Default.NightMode = !UserSettings.Default.NightMode ?? true;
	}


	protected override void OnClosing(CancelEventArgs e)
	{
		var result = MessageBox.Show(StringHelper.Get("Application_ExitMessage"), StringHelper.Get("Message_Tip"), MessageBoxButton.YesNo);
		if (result != MessageBoxResult.Yes)
		{
			e.Cancel = true;
			return;
		}

		Application.Current.Shutdown();
	}
	#endregion
}