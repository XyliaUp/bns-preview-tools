using System.Globalization;
using System.Windows;
using System.Windows.Data;

using HandyControl.Controls;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.UI.Controls;
internal class NumberAttributeEditor : PropertyEditorBase
{
	public NumberAttributeEditor(AttributeDefinition attribute)
	{
		Attribute = attribute;
		Minimum = attribute.Min;
		Maximum = attribute.Max;
	}

	private AttributeDefinition Attribute { get; set; }

	public double Minimum { get; set; }

	public double Maximum { get; set; }

	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new NumericUpDown
	{
		IsReadOnly = propertyItem.IsReadOnly,
		Minimum = Minimum,
		Maximum = Maximum
	};

	public override DependencyProperty GetDependencyProperty() => NumericUpDown.ValueProperty;

	protected override IValueConverter GetConverter(PropertyItem propertyItem) => new NumberValueConverter(Attribute.Type);
}

internal class NumberValueConverter : IValueConverter
{
	private readonly AttributeType Type;

	public NumberValueConverter(AttributeType type)	=> Type = type;

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
		// For DynamicObject, targetType is always Object  
		// Therefore, we can only use AttributeType
		if (value is double Value)
		{
			if (Type == AttributeType.TInt8) return (sbyte)Value;
			if (Type == AttributeType.TInt16) return (short)Value;
			if (Type == AttributeType.TInt32) return (int)Value;
			if (Type == AttributeType.TInt64) return (long)Value;
			if (Type == AttributeType.TFloat32) return (float)Value;
		}

		throw new NotImplementedException();
	}
}