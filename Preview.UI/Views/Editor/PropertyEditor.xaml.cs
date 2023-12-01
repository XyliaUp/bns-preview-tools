using System.Windows;

using Xylia.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Interactivity;

namespace Xylia.Preview.UI.Views.Editor;
public partial class PropertyEditor 
{
	public PropertyEditor()
	{
		InitializeComponent();
	}

	#region Methods
	public Record Source
	{
		get => attributeGrid.SelectedObject;
		set => attributeGrid.SelectedObject = value;
	}

	private void ViewSource_Click(object sender, RoutedEventArgs e)
	{
		if (Source.Data.Length > 16) Clipboard.SetText(Source.Data.ToHex());
		else new PreviewRaw().Execute(Source, true);
	}
	#endregion
}