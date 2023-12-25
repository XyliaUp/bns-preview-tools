using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
/// <summary>
/// entity model table
/// </summary>
/// <typeparam name="T"></typeparam>
public class ModelTable<T> : Table, IEnumerable<T>, IEnumerable where T : ModelElement
{
	#region Load Methods
	bool _loaded;

	public override Task LoadAsync() => Task.Run(() =>
	{
		if (_loaded) return;

		var subs = ModelTypeHelper.Get(typeof(T));
		foreach (var record in _records)
		{
			var type = record.SubclassType == -1 ? null : record.ElDefinition.Name;
			record.Model = new(() => ModelElement.As(record, subs.CreateInstance(type)));
		}

		Trace.WriteLine($"[{DateTime.Now}] load table `{Name}` successful ({_records.Count})");
		_loaded = true;
	});
	#endregion

	#region Get Methods
	public new T this[string alias] => base[alias]?.Model.Value as T;

	public T this[Ref Ref] => base[Ref]?.Model.Value as T;

	public T this[int Id, int Variant = 0] => this[new Ref(Id, Variant)];
	#endregion


	#region Interface
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<T> GetEnumerator()
	{
		foreach (var record in this._records)
			yield return record.Model.Value as T;

		yield break;
	}
	#endregion



	public virtual void LoadHiddenField()
	{

	}
}


/// <summary>
/// entity model helper
/// </summary>
public class ModelTypeHelper
{
	#region Helper
	private static readonly Dictionary<Type, ModelTypeHelper> helpers = [];

	public static ModelTypeHelper Get(Type baseType)
	{
		lock (helpers)
		{
			if (!helpers.TryGetValue(baseType, out var subs))
			{
				subs = helpers[baseType] = new ModelTypeHelper();
				subs.GetSubType(baseType);
			}

			return subs;
		}
	}

	/// <summary>
	/// Convert original table to ModelTable
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <returns></returns>
	public static ModelTable<T> As<T>(Table source) where T : ModelElement
	{
		var table = new ModelTable<T>();
		table.Name = source.Name;
		table.Owner = source.Owner;
		table.Type = source.Type;
		table.MajorVersion = source.MajorVersion;
		table.MinorVersion = source.MinorVersion;
		table.Size = source.Size;
		table.Definition = source.Definition;
		table.Records = source.Records;
		table.ByRef = source.ByRef;
		table.ByRequired = source.ByRequired;
		table.LoadAsync().Wait();

		return table;
	}
	#endregion


	#region Methods
	private Type BaseType;
	private Dictionary<string, Type> _subs = new(StringComparer.OrdinalIgnoreCase);

	private void GetSubType(Type baseType)
	{
		this.BaseType = baseType;

		foreach (var instance in Assembly.GetExecutingAssembly().GetTypes())
		{
			if (!instance.IsAbstract && baseType.IsAssignableFrom(instance) && instance != baseType)
				_subs[instance.Name.TitleLowerCase()] = instance;
		}
	}

	public ModelElement CreateInstance(string type)
	{
		Type _type = null;

		if (!string.IsNullOrWhiteSpace(type) && !_subs.TryGetValue(type, out _type))
		{
			Trace.WriteLine($"cast object subclass failed: {BaseType} -> {type}");
		}

		return (ModelElement)Activator.CreateInstance(_type ?? BaseType);
	}
	#endregion
}