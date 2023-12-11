using System.Text.RegularExpressions;

namespace Xylia.Preview.Common.Extension;
public static class Linq
{
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


	#region Array
	public static void For(int Num, Action<int> func)
	{
		for (int INDEX = 0; INDEX < Num; INDEX++)
			func(INDEX);
	}

	public static void For<T>(ref T[] array, Func<int, T> func)
	{
		ArgumentNullException.ThrowIfNull(array);
		array = For(array.Length, func);
	}

	public static T[] For<T>(int Num, Func<int, T> func)
	{
		var array = new T[Num];
		for (int INDEX = 0; INDEX < Num; INDEX++)
			array[INDEX] = func(INDEX + 1);

		return array;
	}
	#endregion

	#region Linq
	public static string Aggregate(this IEnumerable<string> source, string comma, Func<string, string> func = null)
	{
		ArgumentNullException.ThrowIfNull(source);

		using var e = source.GetEnumerator();
		if (!e.MoveNext()) return null;

		string result = null;
		while (true)
		{
			result += func is null ? e.Current : func(e.Current);

			bool HasNext = e.MoveNext();
			if (HasNext) result += comma;
			else break;
		}

		return result;
	}

	public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
	{
		foreach (var item in collection)
			action(item);
	}

	public static IEnumerable<string> Split(this IEnumerable<string> strings, char separator)
	{
		ArgumentNullException.ThrowIfNull(strings);
		return strings.Where(o => !string.IsNullOrEmpty(o)).SelectMany(o => o.Split(separator));
	}
	#endregion
}