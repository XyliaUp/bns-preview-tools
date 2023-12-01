using System.Text.RegularExpressions;

namespace Xylia.Preview.Common.Extension;
public static class CollectionExtensions
{
	public static bool IsList(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);


	public static List<T> Randomize<T>(this IEnumerable<T> list)
	{
		var originalList = new List<T>(list); // Create a new list, so no operation performed here affects the original list object.
		var randomList = new List<T>();

		var r = new Random();

		int randomIndex;

		while (originalList.Count > 0)
		{
			randomIndex = r.Next(0, originalList.Count);  // Choose a random object in the list
			randomList.Add(originalList[randomIndex]); // Add it to the new, random list
			originalList.RemoveAt(randomIndex); // Remove to avoid duplicates
		}

		return randomList;
	}

	public static IEnumerable<string> SortNaturally(this IEnumerable<string> strings)
	{
		return strings.OrderBy(x => Regex.Replace(x, @"\d+", match => match.Value.PadLeft(4, '0')));
	}
}