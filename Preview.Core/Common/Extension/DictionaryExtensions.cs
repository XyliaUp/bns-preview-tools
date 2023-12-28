namespace Xylia.Preview.Common.Extension;
internal static class DictionaryExtensions
{
	public static T GetOrDefault<K, T>(this IDictionary<K, T> dict, K key, T defaultValue = default)
	{
		if (dict.TryGetValue(key, out T result))
		{
			return result;
		}

		return defaultValue;
	}

	public static Dictionary<TKey, TSource> ToDistinctDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
	{
		return source
			.ToLookup(x => keySelector(x), x => x, comparer)
			.Where(x => x.Key != null)
			.ToDictionary(o => o.Key, o => o.First(), comparer);
	}
}