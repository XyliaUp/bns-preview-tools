using System.Reflection;
using System.Runtime.Serialization;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public abstract class ModelElement
{
	[IgnoreDataMember]
	public Record Source { get; private set; }

	public AttributeCollection Attributes =>  Source.Attributes;

	public override string ToString() => Source.ToString();


	public void Serialize()
	{
		//Attributes.Synchronize();

		//// check definition
		//ArgumentNullException.ThrowIfNull(ElDefinition);

		//// create record
		//builder.InitializeRecord();

		//Data = new byte[ElDefinition.Size];
		//XmlNodeType = 1;
		//SubclassType = ElDefinition.SubclassType;
		//DataSize = ElDefinition.Size;
		//StringLookup = builder.StringLookup;

		//// Go through each attribute
		////AttributeDefaultValues.SetRecordDefaults(record, this);
		//foreach (var attr in ElDefinition.ExpandedAttributes)
		//{
		//	try
		//	{
		//		builder.SetAttribute(this, attr, Attributes[attr.Name]);
		//	}
		//	catch (Exception ex)
		//	{
		//		Debug.WriteLine(ex.Message);
		//	}
		//}

		//builder.FinalizeRecord();
	}




	/// <summary>
	/// Convert original record to ModelRecord
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="record"></param>
	/// <returns></returns>
	public static T As<T>(Record source, T element) where T : ModelElement
	{
		element.Source = source;

		#region	instance
		foreach (var prop in element.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
		{
			if (!prop.CanWrite || prop.ContainAttribute<IgnoreDataMemberAttribute>()) continue;

			// props
			var type = prop.PropertyType;
			var name = (prop.GetAttribute<NameAttribute>()?.Name ?? prop.Name).TitleLowerCase();
			var repeat = prop.GetAttribute<Repeat>()?.Value ?? 1;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(LazyList<>))
			{
				var toType = typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]);
				var value = Activator.CreateInstance(type,
					new Func<object>(() => Convert(element, name, toType, repeat)));

				prop.SetValue(element, value);
			}
			else
			{
				prop.SetValue(element, Convert(element, name, type, repeat));
			}
		}
		#endregion

		return element;
	}

	/// <summary>
	/// Convert Attribute to Field
	/// </summary>
	/// <param name="element"></param>
	/// <param name="name"></param>
	/// <param name="toType"></param>
	/// <param name="repeat"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	private static object Convert(ModelElement element, string name, Type toType, ushort repeat)
	{
		var record = element.Source;

		if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(List<>))
		{
			// valid
			var recordType = toType.GetGenericArguments()[0];
			if (!record.Children.TryGetValue(name, out var children)) throw new InvalidDataException($"No `{name}` child element in definition");
			if (!typeof(ModelElement).IsAssignableFrom(recordType)) throw new InvalidCastException($"{recordType} unable cast to {typeof(ModelElement)}");

			// create instance
			var records = Activator.CreateInstance(toType);
			var add = records.GetType().GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			var subs = ModelTypeHelper.Get(recordType);

			foreach (var child in children)
			{
				var type = child.Attributes[AttributeCollection.s_type];
				var _record = (ModelElement)subs.CreateInstance(type, out _);
				add.Invoke(records, new object[] { ModelElement.As(child, _record) });
			}

			return records;
		}

		if (repeat == 1)
		{
			return AttributeConverter.Convert(record.Attributes[name], toType, record.Owner?.Owner);
		}
		else
		{
			if (!toType.IsArray)
				throw new Exception($"Repeatable object must to use array type: {record.GetType()} -> {name}");

			toType = toType.GetElementType();
			var value = Array.CreateInstance(toType, repeat);

			for (int i = 0; i < repeat; i++)
				value.SetValue(AttributeConverter.Convert(record.Attributes[name, i + 1], toType, record.Owner.Owner), i);

			return value;
		}
	}
}


public struct Ref<TElement> where TElement : ModelElement
{
	public Ref(string value, BnsDatabase database = null)
	{
		if (value is null)
		{
			throw new Exception();
		}
		else if (value.Contains(':'))
		{
			Table = value.Split(':')[0];
			Alias = value.Split(':')[1]?.Trim();
		}
		else if (typeof(TElement) != typeof(Record))
		{
			Table = typeof(TElement).Name;
			Alias = value;
		}

		Database = database ?? FileCache.Data;
	}

	#region Field
	private readonly BnsDatabase Database;

	public readonly string Table;
	public string Alias;
	//public int Id;
	//public int Variant;

	public bool IsNull => Alias is null || Instance is null;

	public override string ToString() => IsNull ? null : $"{Table}:{Alias}";
	#endregion

	#region	Instance
	private TElement _instance;

	public TElement Instance => _instance ??= CastObject<TElement>(Table, Alias, Database);

	public static T CastObject<T>(string table, string alias, BnsDatabase data) where T : ModelElement
	{
		if (alias is null) return null;

		// tref: need register on the database
		// TODO: change to auto create ?
		if (typeof(T) == typeof(ModelElement))
		{
			var prop = data.GetProperty(table);
			return (T) (prop?.GetValue(data) as Table)?[alias]?.Model.Value;
		}
			

		// ref: create model table
		return data.Get<T>(table)?[alias];
	}
	#endregion


	#region Operator
	public static bool operator ==(Ref<TElement> a, Ref<TElement> b)
	{
		// If one is null, but not both, return false.
		if (a.GetType() != b.GetType()) return false;

		// Return true if the fields match:
		if (a.Alias is null && b.Alias is null) return false;
		else if (a.Alias != null && a.Alias.Equals(b.Alias, StringComparison.OrdinalIgnoreCase)) return true;


		return false;
	}
	public static bool operator !=(Ref<TElement> a, Ref<TElement> b) => !(a == b);

	public static bool operator ==(Ref<TElement> a, TElement b)
	{
		return a.Instance == b;
	}
	public static bool operator !=(Ref<TElement> a, TElement b) => !(a == b);

	public override readonly bool Equals(object other) => other is Ref<TElement> record && this == record;
	public override readonly int GetHashCode() => base.GetHashCode();
	#endregion
}