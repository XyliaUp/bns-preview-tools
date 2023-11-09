namespace Xylia.Preview.Common.Extension;
public static class CollectionExtensions
{
	public static void AddItem<T>(this List<T> List, T item)
	{
		if (item != null)
			List.Add(item);
	}

	public static bool IsList(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
}