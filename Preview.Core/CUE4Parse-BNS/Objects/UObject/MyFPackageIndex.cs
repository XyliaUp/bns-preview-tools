using System.ComponentModel;
using System.Globalization;
using CUE4Parse.FileProvider;
using Xylia.Preview.Data.Helpers;

namespace CUE4Parse.UE4.Objects.UObject;
public class MyFPackageIndex : FPackageIndex
{
	public readonly IFileProvider Provider;
	public readonly string ObjectPath;

	public MyFPackageIndex(string path, IFileProvider provider = null)
	{
		Provider = provider ?? FileCache.Provider;
		ObjectPath = path;
	}
}

public class FPackageIndexTypeConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s) return new MyFPackageIndex(s);

		return base.ConvertFrom(context, culture, value);
	}
}

public static class FPackageIndexEx
{
	public static string GetPathName(this FPackageIndex index)
	{
		return index is MyFPackageIndex t ? t.ObjectPath : index.ResolvedObject?.GetPathName();
	}

	public static Assets.Exports.UObject LoadEx(this FPackageIndex index)
	{
		return index is MyFPackageIndex t ? 
			t.Provider.LoadObject(t.ObjectPath) :
			index.Load();
	}
}