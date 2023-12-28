using System.Reflection;
using System.Runtime.Serialization;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public abstract class ModelElement
{
	[IgnoreDataMember]
	public Record Source { get; private set; }

	public AttributeCollection Attributes => Source.Attributes;

	public override string ToString() => Source.ToString();

	public override bool Equals(object obj)
	{
		return obj is ModelElement other && this.Source == other.Source;
	}

	public override int GetHashCode() => Source.GetHashCode();


	/// <summary>
	/// Convert original record to ModelRecord
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source"></param>
	/// <param name="element"></param>
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
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(LazyList<>))
			{
				var toType = typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]);
				var value = Activator.CreateInstance(type, new Func<object>(() => Convert(element, name, toType)));

				prop.SetValue(element, value);
			}
			else
			{
				prop.SetValue(element, Convert(element, name, type));
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
	private static object Convert(ModelElement element, string name, Type toType)
	{
		var record = element.Source;
		var attribute = record.Definition.GetAttribute(name);

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
				var type = child.Attributes.Get<string>(AttributeCollection.s_type);
				add.Invoke(records, new object[] { As(child, subs.CreateInstance(type)) });
			}

			return records;
		}

		// load attribute
		if (attribute is null || attribute.Repeat == 1)
		{
			return AttributeConverter.Convert(record.Attributes[name], toType);
		}
		else
		{
			if (!toType.IsArray)
				throw new Exception($"Repeatable object must to use array type: {record.GetType()} -> {name}");

			toType = toType.GetElementType();
			var value = Array.CreateInstance(toType, attribute.Repeat);

			for (int i = 0; i < attribute.Repeat; i++)
				value.SetValue(AttributeConverter.Convert(record.Attributes[name, i + 1], toType), i);

			return value;
		}
	}
}


public struct Ref<TElement> where TElement : ModelElement
{
	public Ref(Record value)
	{
		source = value;
	}

	public Ref(string value, BnsDatabase database = null)
	{
		var provider = (database ?? FileCache.Data).Provider;

		if (value.Contains(':')) source = provider.Tables.GetRecord(value);
		else source = provider.Tables.GetRecord(typeof(TElement).Name, value);
	}


	#region	Instance
	private readonly Record source;
	private TElement _instance;

	public TElement Instance => _instance ??= CastObject<TElement>();

	private readonly T CastObject<T>() where T : ModelElement
	{
		if (source is null) return null;

		// NOTE: create new object
		var subs = ModelTypeHelper.Get(typeof(T));
		return ModelElement.As(source, (T)subs.CreateInstance(source.Attributes["type"]?.ToString()));
	}
	#endregion
}