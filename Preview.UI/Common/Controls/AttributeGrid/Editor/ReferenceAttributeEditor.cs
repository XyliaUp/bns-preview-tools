using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using HandyControl.Controls;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Common.Converters;

namespace Xylia.Preview.UI.Controls;
internal class ReferenceAttributeEditor : PropertyEditorBase, IValueConverter
{
	public TableCollection Tables { get; }
	public Table ReferedTable { get; }

	public ReferenceAttributeEditor(string reference)
	{
		Tables = FileCache.Data.Provider.Tables;
		ReferedTable = Tables[reference];
	}


	public override FrameworkElement CreateElement(PropertyItem propertyItem)
	{
		var element = new AutoCompleteTextBox
		{
			IsEnabled = !propertyItem.IsReadOnly,
		};

		// set source
		BindingOperations.SetBinding(element, ItemsControl.ItemsSourceProperty,
			new Binding("Records")
			{
				Source = ReferedTable,
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				IsAsync = true,
				Delay = 100,
			});

		// set tooltip
		BindingOperations.SetBinding(element, Selector.ToolTipProperty,
			new Binding("SelectedItem")
			{
				Source = element,
				Mode = BindingMode.OneWay, 
				Converter = new RecordNameConverter()
			});

		return element;
	}

	public override DependencyProperty GetDependencyProperty() => AutoCompleteTextBox.TextProperty;

	public override UpdateSourceTrigger GetUpdateSourceTrigger(PropertyItem propertyItem) => UpdateSourceTrigger.LostFocus;

	protected override IValueConverter GetConverter(PropertyItem propertyItem) => this;


	#region Convert
	public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value?.ToString();
	}

	public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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