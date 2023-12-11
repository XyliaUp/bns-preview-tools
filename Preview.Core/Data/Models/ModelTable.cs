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
public sealed class ModelTable<T> : Table, IEnumerable<T>, IEnumerable where T : ModelElement
{
	#region Load Methods
	bool _loaded;

	public override Task LoadAsync() => Task.Run(() =>
	{
		if (_loaded) return;

		var subs = ModelTypeHelper.Get(typeof(T));
		foreach (var record in _records)
		{
			record.Model = new(() =>
			{
				var Object = subs.CreateInstance(record.SubclassType) as T;
				return ModelElement.As(record, Object);
			});
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



	//public byte[] ToArray(bool is64Bit)
	//{
	//	//var builder = new RecordBuilder();
	//	//builder.InitializeTable(IsCompressed);

	//	//foreach (var record in Records)
	//	//{
	//	//	record.Serialize(builder);
	//	//}

	//	//builder.FinalizeTable();


	//	using var memoryStream = new MemoryStream();
	//	using var writer = new BinaryWriter(memoryStream);

	//	var bnsTableWriter = new TableWriter();
	//	bnsTableWriter.WriteTo(writer, this, is64Bit);

	//	return memoryStream.ToArray();
	//}
}

/// <summary>
/// entity model helper
/// </summary>
public class ModelTypeHelper
{
	#region Load Methods
	readonly Dictionary<short, Type> Types = new();
	readonly Dictionary<string, short> Types_Name = new(StringComparer.OrdinalIgnoreCase);

	private void GetSubType(Type baseType)
	{
		short typeIndex = -1;
		AddType(typeIndex, baseType);

		foreach (var instance in Assembly.GetExecutingAssembly().GetTypes())
		{
			if (!instance.IsAbstract && baseType.IsAssignableFrom(instance) && instance != baseType)
				AddType(++typeIndex, instance);
		}
	}

	private void AddType(short type, Type instance)
	{
		Types[type] = instance;
		Types_Name[instance.Name.TitleLowerCase()] = type;
	}
	#endregion

	#region Instance 
	public object CreateInstance(short type) => Activator.CreateInstance(Types.GetValueOrDefault(type, Types[-1]));

	public object CreateInstance(string typeString, out short type)
	{
		type = -1;
		if (!string.IsNullOrWhiteSpace(typeString) &&
			!Types_Name.TryGetValue(typeString, out type))
		{
			type = -1;
			Trace.WriteLine($"cast object subclass failed: {Types[-1]} -> {typeString}");
		}

		return CreateInstance(type);
	}
	#endregion


	#region Helper
	private static readonly Dictionary<Type, ModelTypeHelper> helpers = new();

	public static ModelTypeHelper Get(Type baseType)
	{
		Debug.Assert(baseType != typeof(Record));

		// result 
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
	#endregion




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
}