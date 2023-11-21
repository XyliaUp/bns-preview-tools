using System.Globalization;
using System.Windows;
using System.Windows.Data;

using HandyControl.Controls;

namespace Xylia.Preview.UI.Controls;
public class NumberAttributeEditor : PropertyEditorBase
{
	public NumberAttributeEditor()
	{

	}

	public NumberAttributeEditor(double minimum, double maximum)
	{
		Minimum = minimum;
		Maximum = maximum;
	}

	public double Minimum { get; set; }

	public double Maximum { get; set; }

	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new NumericUpDown
	{
		IsReadOnly = propertyItem.IsReadOnly,
		Minimum = Minimum,
		Maximum = Maximum
	};

	public override DependencyProperty GetDependencyProperty() => NumericUpDown.ValueProperty;

	protected override IValueConverter GetConverter(PropertyItem propertyItem) => new NumberValueConverter();
}

internal class NumberValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is null) return 0.0;
		if (value is sbyte SByteNumber) return (double)SByteNumber;
		if (value is short Int16Number) return (double)Int16Number;
		if (value is int Int32Number) return (double)Int32Number;
		if (value is long Int64Number) return (double)Int64Number;
		if (value is float SingleNumber) return (double)SingleNumber;

		throw new NotImplementedException();
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is double Value)
		{
			if (targetType == typeof(sbyte)) return (sbyte)Value;
			if (targetType == typeof(short)) return (short)Value;
			if (targetType == typeof(int)) return (int)Value;
			if (targetType == typeof(long)) return (long)Value;
			if (targetType == typeof(float)) return (float)Value;
		}

		return null;
		throw new NotImplementedException();
	}
}