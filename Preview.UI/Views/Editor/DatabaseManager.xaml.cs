using System.Windows;
using HandyControl.Controls;
using HandyControl.Data;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.Views.Editor;
public partial class DatabaseManager
{
	#region Data
	public IEngine? Engine { get; private set; }

	internal bool IsGlobalData = false;
	#endregion

	#region Constructors
	public DatabaseManager()
	{
		InitializeComponent();
	}
	#endregion


	#region Methods
	private void ProviderSearch_SearchStarted(object sender, FunctionEventArgs<string> e)
	{
		if (!SettingsView.TryBrowseFolder(out var path))
			return;

		ProviderSearch.Text = path;
	}

	private void Connect_Click(object sender, RoutedEventArgs e)
	{
		if (Provider_GlobalMode.IsChecked == true)
		{
			IsGlobalData = true;
			Engine = FileCache.Data;
		}
		else
		{
			IDataProvider? provider;

			// check
			var path = ProviderSearch.Text;
			if (string.IsNullOrWhiteSpace(path))
			{
				Growl.Error(new GrowlInfo()
				{
					Token = DatabaseStudio.TOKEN,
					Message = "invalid path",
					StaysOpen = true,
				});
				return;
			}
			else if (Provider_GameMode.IsChecked == true) provider = DefaultProvider.Load(path, new DatSelectDialog());
			else if (Provider_FolderMode.IsChecked == true) provider = new FolderProvider(path);
			else return;

			Engine = new BnsDatabase(provider);
		}

		DialogResult = true;
		Close();
	}
	#endregion
}