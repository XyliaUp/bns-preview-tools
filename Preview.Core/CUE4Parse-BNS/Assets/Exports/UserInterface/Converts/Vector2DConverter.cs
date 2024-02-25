using System.ComponentModel;
using System.Globalization;
using CUE4Parse.UE4.Objects.Core.Math;

namespace CUE4Parse.BNS.Assets.Exports;
public class Vector2DConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s)
		{
			var array = s.Split(' ', ',');
			if (array.Length >= 2) return new FVector2D(
				Convert.ToSingle(array[0]),
				Convert.ToSingle(array[1]));

			return FVector2D.ZeroVector;
		}

		return base.ConvertFrom(context, culture, value);
	}
}