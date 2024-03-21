using System.Windows;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Common.Interactivity;

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
		if (Source.HasChildren) new PreviewRaw().Execute(Source, true);
		else Clipboard.SetText(Source.Data.ToHex());
	}
	#endregion
}