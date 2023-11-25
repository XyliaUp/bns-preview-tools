using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Editor;
[ObservableObject]
public partial class PropertyEditor : Window
{
	#region Ctor
	public PropertyEditor()
	{
		InitializeComponent();
	}
	#endregion

	#region Fields
	[ObservableProperty]
	private Record source;
	#endregion


	#region Methods
	private void ViewSource_Click(object sender, RoutedEventArgs e)
	{
		if (Source.Data.Length > 16) Clipboard.SetText(Source.Data.ToHex());
		else PreviewRaw.Command.Execute(Source, true);
	}
	#endregion
}