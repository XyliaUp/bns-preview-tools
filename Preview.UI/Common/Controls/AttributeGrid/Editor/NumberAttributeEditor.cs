using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HandyControl.Controls;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.UI.Controls;
internal class NumberAttributeEditor(AttributeDefinition attribute) : PropertyEditorBase, IValueConverter
{
	#region Properties
	public double Minimum { get; set; } = attribute.Min;

	public double Maximum { get; set; } = attribute.Max;
	#endregion

	#region Methods
	public override FrameworkElement CreateElement(PropertyItem propertyItem) => new NumericUpDown
	{
		IsReadOnly = propertyItem.IsReadOnly,
		Minimum = Minimum,
		Maximum = Maximum
	};

	public override DependencyProperty GetDependencyProperty() => NumericUpDown.ValueProperty;

	protected override IValueConverter GetConverter(PropertyItem propertyItem) => this;

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
			switch (attribute.Type)
			{
				case AttributeType.TInt8: return (sbyte)Value;
				case AttributeType.TInt16: return (short)Value;
				case AttributeType.TInt32: return (int)Value;
				case AttributeType.TInt64: return (long)Value;
				case AttributeType.TFloat32: return (float)Value;
			}
		}

		throw new NotImplementedException();
	}
	#endregion
}