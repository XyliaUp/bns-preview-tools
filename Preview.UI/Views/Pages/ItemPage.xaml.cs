#define DEV
using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.Common;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.ViewModels;
using Xylia.Preview.UI.Views.Editor;

namespace Xylia.Preview.UI.Views.Pages;
public partial class ItemPage : Page
{
	public ItemPage()
	{
		DataContext = new ItemPageViewModel();
		InitializeComponent();

#if DEVELOP
		DEBUG.Visibility = Visibility.Visible;
		DEBUG.IsSelected = true;
#endif
	}

	#region Methods
	private void DatabaseGui_Click(object sender, RoutedEventArgs e) => new DatabaseStudio().Show();

	private void ClearCacheData_Click(object sender, RoutedEventArgs e)
	{
		FileCache.Clear();
		ProcessHelper.ClearMemory();
	}
	#endregion
}