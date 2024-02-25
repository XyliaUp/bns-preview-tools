using System.ComponentModel;
using System.Globalization;

namespace CUE4Parse.UE4.Objects.Core.i18N;
public class FTextTypeConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s) return new FText(s);

		return base.ConvertFrom(context, culture, value);
	}
}