using System.Globalization;
using System.Windows;
using System.Windows.Data;

using HandyControl.Controls;

using Xylia.Preview.Data.Helpers;

using Models = Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.UI.Controls;
public class ReferenceAttributeEditor : PropertyEditorBase, IValueConverter
{
	public ReferenceAttributeEditor(string reference)
	{
		ReferedTable = FileCache.Data.Provider.Tables[reference];
	}

	public Models.Table ReferedTable { get; set; }

	public override FrameworkElement CreateElement(PropertyItem propertyItem)
	{
		// element
		var element = new AutoCompleteTextBox
		{
			IsEnabled = !propertyItem.IsReadOnly,
		};

		// set source
		BindingOperations.SetBinding(element, ItemsControl.ItemsSourceProperty,
			new Binding($"Records")
			{
				Source = ReferedTable,
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				IsAsync = true,
				Delay = 100,
			});

		return element;
	}

	public override DependencyProperty GetDependencyProperty() => System.Windows.Controls.ComboBox.TextProperty;

	public override UpdateSourceTrigger GetUpdateSourceTrigger(PropertyItem propertyItem) => UpdateSourceTrigger.LostFocus;

	protected override IValueConverter GetConverter(PropertyItem propertyItem) => this;


	#region Convert
	public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value?.ToString();
	}

	public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		// var split = value.Split(':', 2);
		if (value is string text)
		{
			return ReferedTable?[text];
		}

		throw new NotImplementedException();
	}
	#endregion
}