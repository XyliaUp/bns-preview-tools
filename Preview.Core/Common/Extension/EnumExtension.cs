using System.Diagnostics;

namespace Xylia.Preview.Common.Extension;
public static partial class EnumExtension
{
    #region Cast
    public static T ToEnum<T>(this string EnumItem, bool Extension = true) where T : Enum => EnumItem.TryParseToEnum(out T Val, Extension) ? Val : default;

    public static bool TryParseToEnum<T>(this string EnumItem, out T Value, bool Extension = true, bool HideError = false) where T : Enum
    {
        Value = default;

        if (EnumItem.TryParseToEnum(typeof(T), out var TempVal, Extension))
        {
            Value = (T)TempVal;
            return true;
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(EnumItem) && !HideError) Debug.WriteLine($"cast enum failed: {EnumItem} => {typeof(T)}"); 
            return false;
        }
    }

    public static bool TryParseToEnum(this string EnumItem, Type type, out object value, bool Extension)
    {
        value = default;
        if (string.IsNullOrWhiteSpace(EnumItem)) return false;

        bool flag = byte.TryParse(EnumItem, out var number);

        #region extra
        if (Extension)
        {
            if (EnumItem.Contains('-'))
                return Enum.TryParse(type, EnumItem.Replace("-", null), true, out value);

            if (flag && Enum.TryParse(type, "N" + EnumItem, true, out value))
                return true;
        }
        #endregion

        return Enum.TryParse(type, EnumItem, true, out value);
    }
    #endregion
}