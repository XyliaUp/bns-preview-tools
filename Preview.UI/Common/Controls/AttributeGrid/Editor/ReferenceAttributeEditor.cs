using System.Windows;

using HandyControl.Controls;

using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.UI.Controls;
public class ReferenceAttributeEditor : PropertyEditorBase
{
	public ReferenceAttributeEditor(string reference)
	{
		ReferedTableName = reference;
	}

	public string ReferedTableName { get; set; }

	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new AutoCompleteTextBox
	{
		IsReadOnly = !propertyItem.IsReadOnly,
		ItemsSource = FileCache.Data.Provider.Tables[ReferedTableName]?.Records,
		//DisplayMemberPath = "Name",
		//FilterItem = FilterItem,
	};

	public override DependencyProperty GetDependencyProperty() => System.Windows.Controls.ComboBox.TextProperty;
}