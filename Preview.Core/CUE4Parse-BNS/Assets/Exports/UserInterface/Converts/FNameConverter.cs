using System.ComponentModel;
using System.Globalization;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class FNameConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s) return new FName(s);

		return base.ConvertFrom(context, culture, value);
	}
}