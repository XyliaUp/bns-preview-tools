using System.Collections;

namespace Xylia.Preview.Common.Extension;

internal static class TypeInfoExtensions
{
    public static bool IsEnumerable(this Type type)
    {
        return
            type != typeof(String) &&
            typeof(IEnumerable).IsAssignableFrom(type);
    }
}