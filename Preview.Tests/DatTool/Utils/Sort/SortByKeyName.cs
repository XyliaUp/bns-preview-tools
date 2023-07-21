using Xylia.Extension;

namespace Xylia.Preview.Data.Models.Util.Sort.Common;

/// <summary>
/// 通过主键的大小进行排序的公共泛型方法
/// </summary>
/// <typeparam name="T"></typeparam>
public class SortByKeyName<T> : IComparer<KeyValuePair<string, T>>
{
    public int Compare(KeyValuePair<string, T> x, KeyValuePair<string, T> y) => Method.StrCompare(x.Value, y.Value);
}
