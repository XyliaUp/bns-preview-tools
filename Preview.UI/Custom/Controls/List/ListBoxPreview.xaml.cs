using System.ComponentModel;
using Xylia.Preview.UI.Custom.Controls;

using UserControl = System.Windows.Controls.UserControl;

namespace Xylia.Preview.UI.Controls;
public partial class ListBoxPreview : UserControl
{
	#region Constructor
	public ListBoxPreview()
	{
		InitializeComponent();
	}
	#endregion


	private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
	{
		for (uint i = 0; i <= 10; i++)
		{
			var item = new ItemShow();

			Layout.Children.Add(item);
		}
			
    }
}