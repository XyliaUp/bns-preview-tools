using System.IO;

using Ookii.Dialogs.Wpf;

using Xylia.Extension;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views;
public partial class SettingsView : Window
{
	public UserSettings settings;

	public SettingsView()
	{
		DataContext = settings = new UserSettings();
		InitializeComponent();
	}



	private void OnClosing(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void OnBrowseDirectories(object sender, RoutedEventArgs e)
	{
		if (!TryBrowseFolder(out var path))
			return;

		this.Activate();

		settings.GameFolder = path;
		var Locale = new Locale(new DirectoryInfo(path));
		if (Locale._language != null)
		{
			var region = Locale.Language == ELanguage.None ? Locale._language : Locale.Language.GetDescription();
		}
	}

	private void OnBrowseDirectories2(object sender, RoutedEventArgs e)
	{
		if (TryBrowseFolder(out var path)) settings.OutputFolder = path;

		this.Activate();
	}

	private void OnBrowseDirectories3(object sender, RoutedEventArgs e)
	{
		if (TryBrowseFolder(out var path)) settings.OutputFolderResource = path;

		this.Activate();
	}






	public static bool TryBrowseFolder(out string path)
	{
		var dialog = new VistaFolderBrowserDialog()
		{

		};

		if (dialog.ShowDialog() == true)
		{
			path = dialog.SelectedPath;
			return true;
		}

		path = string.Empty;
		return false;
	}

	public static bool TryBrowse(out string path, string filter = null)
	{
		var dialog = new VistaOpenFileDialog()
		{
			Filter = filter,
			RestoreDirectory = false
		};

		if (dialog.ShowDialog() == true)
		{
			path = dialog.FileName;
			return true;
		}

		path = string.Empty;
		return false;
	}
}