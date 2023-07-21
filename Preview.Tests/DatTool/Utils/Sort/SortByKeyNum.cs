namespace Xylia.Preview.Data.Models.Util.Sort.Common;
public class SortByKeyNum<T> : IComparer<KeyValuePair<int, T>>
{
    public int Compare(KeyValuePair<int, T> x, KeyValuePair<int, T> y) => x.Key - y.Key;
}
