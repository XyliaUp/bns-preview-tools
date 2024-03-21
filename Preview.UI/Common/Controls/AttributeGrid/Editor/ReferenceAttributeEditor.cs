using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HandyControl.Controls;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.UI.Controls;
internal class ReferenceAttributeEditor : PropertyEditorBase, IValueConverter
{
	#region Constructorss
	protected TableCollection Tables { get; }

	protected Table? ReferedTable { get; }

	public ReferenceAttributeEditor(string reference)
	{
		Tables = FileCache.Data.Provider.Tables;
		ReferedTable = Tables[reference];
	}
	#endregion

	#region Methods
	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new System.Windows.Controls.TextBox
	{
		IsReadOnly = propertyItem.IsReadOnly
	};

	//public override FrameworkElement CreateElement(PropertyItem propertyItem)
	//{
	//	var element = new AutoCompleteTextBox
	//	{
	//		IsEnabled = !propertyItem.IsReadOnly,
	//	};

	//	// set source
	//	BindingOperations.SetBinding(element, ItemsControl.ItemsSourceProperty,
	//		new Binding("Records")
	//		{
	//			Source = ReferedTable,
	//			Mode = BindingMode.OneWay,
	//			UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
	//			IsAsync = true,
	//			Delay = 100,
	//		});

	//	// set tooltip
	//	BindingOperations.SetBinding(element, Selector.ToolTipProperty,
	//		new Binding("SelectedItem")
	//		{
	//			Source = element,
	//			Mode = BindingMode.OneWay, 
	//			Converter = new RecordNameConverter()
	//		});

	//	return element;
	//}

	public override DependencyProperty GetDependencyProperty() => System.Windows.Controls.TextBox.TextProperty;

	public override UpdateSourceTrigger GetUpdateSourceTrigger(PropertyItem propertyItem) => UpdateSourceTrigger.LostFocus;
	#endregion

	#region IValueConverter
	protected override IValueConverter GetConverter(PropertyItem propertyItem) => this;

	public virtual object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value?.ToString();
	}

	public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is string text)
		{
			if (text.Contains(':')) return Tables.GetRecord(text);
			else return ReferedTable?[text];
		}

		throw new NotImplementedException();
	}
	#endregion
}