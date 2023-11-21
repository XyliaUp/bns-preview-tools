using System.Collections;
using System.Diagnostics;
using System.Reflection;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Cast;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Database;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data;
/// <summary>
/// entity model table
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ModelTable<T> : Table, IEnumerable<T>, IEnumerable where T : Record
{
	#region Load Methods
	public override Task LoadAsync() => Task.Run(() =>
	{
		var subs = ModelTypeHelper.Get(typeof(T));
		foreach (var record in _records)
		{
			record.Model = new(() =>
			{
				var Object = subs.CreateInstance(record.SubclassType) as T;
				return ModelTypeHelper.As(record, Object);
			});
		}

		Trace.WriteLine($"[{DateTime.Now}] load table `{Name}` successful ({_records.Count})");
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

}

public class ModelTypeHelper
{
	#region Load Methods
	readonly Dictionary<short, Type> Types = new();
	readonly Dictionary<string, short> Types_Name = new(StringComparer.OrdinalIgnoreCase);

	//TODO: from def
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
	/// Convert original record to RecordModel
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="record"></param>
	/// <returns></returns>
	public static T As<T>(Record source, T record) where T : Record
	{
		record.Owner = source.Owner;
		record.Attributes = source.Attributes;
		record.Children = source.Children;
		record.Data = source.Data;
		record.StringLookup = source.StringLookup;

		#region	instance
		foreach (var field in record.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
		{
			var name = field.GetName().TitleLowerCase();
			var type = field.GetMemberType();
			if (type.IsList())
			{
				var recordType = type.GetGenericArguments()[0];
				if (!typeof(Record).IsAssignableFrom(recordType)) continue;
				if (!record.Children.TryGetValue(name, out var children)) continue;

				var records = Activator.CreateInstance(type);
				var add = records.GetType().GetMethod("Add", ClassExtension.Flags);
				var subs = ModelTypeHelper.Get(recordType);

				foreach (var child in children)
				{
					var _record = (Record)subs.CreateInstance(child.Attributes["type"], out _);
					add.Invoke(records, new object[] { ModelTypeHelper.As(child, _record) });
				}

				field.SetValue(record, records);
				continue;
			}


			var repeat = field.GetAttribute<Repeat>()?.Value ?? 1;
			if (repeat == 1)
			{
				field.SetValue(record, AttributeConverter.ConvertTo(record.Attributes[name], type, record.Owner.Owner));
			}
			else
			{
				if (!type.IsArray)
					throw new Exception($"Repeatable object must to use array type: {record.GetType()} -> {name}");

				type = type.GetElementType();
				var value = Array.CreateInstance(type, repeat);

				for (int i = 0; i < repeat; i++)
					value.SetValue(AttributeConverter.ConvertTo(record.Attributes[name, i + 1], type, record.Owner.Owner), i);

				field.SetValue(record, value);
			}
		}
		#endregion

		return record;
	}

	public static ModelTable<T> As<T>(Table source) where T : Record
	{
		var table = new ModelTable<T>();
		table.Name = source.Name;
		table.Owner = source.Owner;
		table.Type = source.Type;
		table.MajorVersion = source.MajorVersion;
		table.MinorVersion = source.MajorVersion;
		table.Size = source.Size;
		table.Definition = source.Definition;
		table.Records = source.Records;
		table.ByRef = source.ByRef;
		table.ByRequired = source.ByRequired;
		table.LoadAsync().Wait();

		return table;
	}
}