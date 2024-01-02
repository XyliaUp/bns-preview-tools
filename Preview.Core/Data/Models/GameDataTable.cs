using System.Collections;
using System.Diagnostics;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class GameDataTable<T> : IEnumerable<T>, IEnumerable, IDisposable where T : ModelElement
{
	#region Interface
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var element in this.Elements)
			yield return element;

		yield break;
	}

	public void Dispose()
	{
		Source.Dispose();
		Elements.Clear();

		GC.SuppressFinalize(this);
	}
	#endregion

	#region Load Methods
	ModelTypeHelper Helper;

	public Table Source { get; }

	public List<T> Elements { get; set; } = [];

	public GameDataTable(Table source)
	{
		Source = source;
		Helper = ModelTypeHelper.Get(typeof(T));

		foreach (var record in source.Records)
		{
			Elements.Add(LoadElement(record));
		}

		Trace.WriteLine($"[{DateTime.Now}] load table `{source.Name}` successful ({source.Records.Count})");
	}
	#endregion


	public T this[string alias]
	{
		get => LoadElement(Source[alias]);
	}

	protected T LoadElement(Record record)
	{
		if (record is null) return null;

		var type = record.SubclassType == -1 ? null : record.Definition.Name;
		var element = ModelElement.As(record, Helper.CreateInstance<T>(type));
		element.LoadHiddenField();

		return element;
	}
}