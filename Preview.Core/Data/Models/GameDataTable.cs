using System.Collections;
using System.Diagnostics;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class GameDataTable<T> : IEnumerable<T>, IEnumerable, IDisposable where T : ModelElement
{
	#region Constructors
	internal GameDataTable(Table source)
	{
		Helper = ModelTypeHelper.Get(typeof(T));
		Source = source;

		Trace.WriteLine($"[{DateTime.Now}] load table `{source.Name}` successful ({source.Records.Count})");
	}
	#endregion

	#region Properties
	private readonly ModelTypeHelper Helper;
	private List<T> elements;

	public Table Source { get; }

	public List<T> Elements => elements ??= LoadElements();
	#endregion

	#region Methods
	public T this[Ref Ref]
	{
		get => LoadElement(Source[Ref]);
	}

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

	private List<T> LoadElements()
	{
		return Source.Records.Select(LoadElement).ToList();
	}
	#endregion


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
		Elements?.Clear();

		GC.SuppressFinalize(this);
	}
	#endregion
}