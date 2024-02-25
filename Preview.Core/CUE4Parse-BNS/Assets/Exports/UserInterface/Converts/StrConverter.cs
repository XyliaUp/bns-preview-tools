using System.ComponentModel;

namespace CUE4Parse.BNS.Assets.Exports;
internal class StrConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}
}