namespace Xylia.Preview.Document;

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
}