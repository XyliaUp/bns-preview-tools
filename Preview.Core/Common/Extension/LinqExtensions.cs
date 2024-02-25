using System.Collections;

namespace Xylia.Preview.Common.Extension;
public static class LinqExtensions
{
	#region IEnumerable
	public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
	{
		foreach (var item in collection)
			action(item);
	}

	public static void ForEach(this IEnumerable collection, Action<object> action)
	{
		foreach (var item in collection)
			action(item);
	}

	public static List<T> Randomize<T>(this IEnumerable<T> source)
	{
		var originalList = new List<T>(source); // Create a new list, so no operation performed here affects the original list object.
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

	public static IEnumerable<string> Split(this IEnumerable<string> strings, char separator)
	{
		ArgumentNullException.ThrowIfNull(strings);
		return strings.Where(o => !string.IsNullOrEmpty(o)).SelectMany(o => o.Split(separator));
	}

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


	public static bool IsEmpty<T>(this IEnumerable<T> source) => source == null || !source.Any();

	public static bool IsEmpty<T>(this ICollection<T> source) => source == null || source.Count == 0;
	#endregion


	#region Array
	public static void For<T>(ref T[] array, int size, Func<int, T> func)
	{
		array = For(size, func);
	}

	public static T[] For<T>(int Num, Func<int, T> func)
	{
		var array = new T[Num];
		for (int INDEX = 0; INDEX < Num; INDEX++)
			array[INDEX] = func(INDEX + 1);

		return array;
	}

	public static void ForEach<T, TSource>(this TSource[] array, Func<TSource, T> selector, Action<T, int> func)
	{
		for (int idx = 0; idx < array.Length; idx++)
		{
			var item = selector(array[idx]);
			if (item is null) continue;

			func(item, idx);
		}
	}

	public static Tuple<T1, T2>[] Combine<T1, T2>(T1[] array1, T2[] array2)
	{
		if (array1 is null) return null;

		var source = For(array1.Length, (x) => new Tuple<T1, T2>(
			array1[x - 1],
			array2[x - 1]));

		return source.Where(x => x.Item1 != null && x.Item2 != null).ToArray();
	}
	#endregion
}