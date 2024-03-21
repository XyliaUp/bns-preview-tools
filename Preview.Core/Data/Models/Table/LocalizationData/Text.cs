using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using CUE4Parse.BNS.Assets.Exports;
using HtmlAgilityPack;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.TextArguments;

namespace Xylia.Preview.Data.Models;

[Side(ReleaseSide.Client)]
public sealed class Text : ModelElement
{
	#region Attributes
	public string alias { get; set; }

	public string text { get; set; }
	#endregion

	#region Methods
	public override string ToString() => this.text;

	/// <summary>
	/// Convert XML text to record
	/// </summary>
	/// <param name="xml"></param>
	/// <returns></returns>
	public static Text Parse(string xml)
	{
		try
		{
			var element = XElement.Parse(xml, LoadOptions.PreserveWhitespace);
			var reader = element.CreateReader();
			reader.MoveToContent();

			var alias = element.Attribute("alias")?.Value;
			var text = reader.ReadInnerXml();

			return new Text() { alias = alias, text = text };
		}
		catch
		{
			return null;
		}
	}
	#endregion
}

/// <summary>
/// Represents a weakly typed list of objects that can be accessed by index.
/// </summary>
public sealed class TextArguments : IEnumerable, ICollection, IList
{
	#region Properties
	// Gets and sets the capacity of this list.  The capacity is the size of
	// the internal array used to hold items.  When set, the internal
	// array of the list is reallocated to the given capacity.
	public int Capacity
	{
		get => _items.Length;
		set
		{
			if (value < _size)
				throw new ArgumentOutOfRangeException();

			if (value != _items.Length)
			{
				if (value > 0)
				{
					var newItems = new object[value];
					if (_size > 0)
					{
						Array.Copy(_items, newItems, _size);
					}
					_items = newItems;
				}
				else
				{
					_items = Array.Empty<object>();
				}
			}
		}
	}

	// Read-only property describing how many elements are in the List.
	public int Count => _size;

	bool IList.IsFixedSize => false;

	bool IList.IsReadOnly => false;

	// Is this List synchronized (thread-safe)?
	bool ICollection.IsSynchronized => false;

	// Synchronization root for this object.
	object ICollection.SyncRoot => this;

	/// <summary>
	/// Gets or sets the element at the specified index.
	/// </summary>
	/// <param name="index">The one-based index of the element to get or set.</param>
	/// <returns></returns>
	public object this[int index]
	{
		get => index > _size ? default : _items[index - 1];
		set
		{
			if (index > _size)
			{
				if (index > _items.Length) Grow(index);

				_size = index;
			}

			_items[index - 1] = value;
			Changed?.Invoke(this, EventArgs.Empty);
		}
	}
	#endregion

	#region Event
	public event EventHandler? Changed;
	#endregion

	#region Methods
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(object item)
	{
		var array = _items;
		int size = _size;
		if ((uint)size < (uint)array.Length)
		{
			_size = size + 1;
			array[size] = item;
		}
		else
		{
			AddWithResize(item);
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void AddWithResize(object item)
	{
		Debug.Assert(_size == _items.Length);
		int size = _size;
		Grow(size + 1);
		_size = size + 1;
		_items[size] = item;
	}

	// Adds the elements of the given collection to the end of this list. If
	// required, the capacity of the list is increased to twice the previous
	// capacity or the new size, whichever is larger.
	//
	public void AddRange(IEnumerable<object> collection)
	{
		if (collection is ICollection<object> c)
		{
			int count = c.Count;
			if (count > 0)
			{
				if (_items.Length - _size < count)
				{
					Grow(checked(_size + count));
				}

				c.CopyTo(_items, _size);
				_size += count;
			}
		}
		else
		{
			using IEnumerator<object> en = collection.GetEnumerator();
			while (en.MoveNext())
			{
				Add(en.Current);
			}
		}
	}

	/// <summary>
	/// Increase the capacity of this list to at least the specified <paramref name="capacity"/>.
	/// </summary>
	/// <param name="capacity">The minimum capacity to ensure.</param>
	internal void Grow(int capacity)
	{
		Capacity = GetNewCapacity(capacity);
	}

	/// <summary>
	/// Enlarge this list so it may contain at least <paramref name="insertionCount"/> more elements
	/// And copy data to their after-insertion positions.
	/// This method is specifically for insertion, as it avoids 1 extra array copy.
	/// You should only call this method when Count + insertionCount > Capacity.
	/// </summary>
	/// <param name="indexToInsert">Index of the first insertion.</param>
	/// <param name="insertionCount">How many elements will be inserted.</param>
	private void GrowForInsertion(int indexToInsert, int insertionCount = 1)
	{
		Debug.Assert(insertionCount > 0);

		int requiredCapacity = checked(_size + insertionCount);
		int newCapacity = GetNewCapacity(requiredCapacity);

		// Inline and adapt logic from set_Capacity

		var newItems = new object[newCapacity];
		if (indexToInsert != 0)
		{
			Array.Copy(_items, newItems, length: indexToInsert);
		}

		if (_size != indexToInsert)
		{
			Array.Copy(_items, indexToInsert, newItems, indexToInsert + insertionCount, _size - indexToInsert);
		}

		_items = newItems;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int GetNewCapacity(int capacity)
	{
		Debug.Assert(_items.Length < capacity);

		int newCapacity = _items.Length == 0 ? DefaultCapacity : 2 * _items.Length;

		// Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
		// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
		if ((uint)newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;

		// If the computed capacity is still less than specified, set to the original argument.
		// Capacities exceeding Array.MaxLength will be surfaced as OutOfMemoryException by Array.Resize.
		if (newCapacity < capacity) newCapacity = capacity;

		return newCapacity;
	}
	#endregion

	#region Interface
	public IEnumerator GetEnumerator() => _items.GetEnumerator();

	public void CopyTo(Array array, int index)
	{
		// Delegate rest of error checking to Array.Copy.
		Array.Copy(_items, 0, array, index, _size);
	}

	int IList.Add(object item)
	{
		Add(item!);
		return Count - 1;
	}

	public void Clear()
	{
		int size = _size;
		_size = 0;
		if (size > 0)
		{
			Array.Clear(_items, 0, size); // Clear the elements so that the gc can reclaim the references.
		}
	}

	public bool Contains(object item)
	{
		return _size != 0 && IndexOf(item) >= 0;
	}

	public int IndexOf(object item)
	{
		return Array.IndexOf(_items, item, 0, _size);
	}

	public void Insert(int index, object item)
	{
		// Note that insertions at the end are legal.
		if ((uint)index > (uint)_size)
		{
			throw new ArgumentOutOfRangeException(nameof(index));
		}
		if (_size == _items.Length)
		{
			GrowForInsertion(index, 1);
		}
		else if (index < _size)
		{
			Array.Copy(_items, index, _items, index + 1, _size - index);
		}
		_items[index] = item;
		_size++;
	}

	public void Remove(object item)
	{
		int index = IndexOf(item);
		if (index >= 0) RemoveAt(index);
	}

	public void RemoveAt(int index)
	{
		if ((uint)index >= (uint)_size)
		{
			throw new ArgumentOutOfRangeException(nameof(index));
		}
		_size--;
		if (index < _size)
		{
			Array.Copy(_items, index + 1, _items, index, _size - index);
		}
	}
	#endregion

	#region Private Fields
	private const int DefaultCapacity = 4;

	internal object[] _items = new object[DefaultCapacity];
	internal int _size;
	#endregion


	#region Helpers
	public class Argument(string p, string id, string seq)
	{
		#region Constructors
		public Argument(HtmlNode node) : this(
			node.Attributes["p"]?.Value,
			node.Attributes["id"]?.Value,
			node.Attributes["seq"]?.Value)
		{

		}
		#endregion


		#region Methods
		public object GetObject(TextArguments arguments)
		{
			try
			{
				object obj;

				#region source
				var ps = p?.Split(':');
				var type = ps?[0];
				if (type is null) return null;
				else if (type == "id") obj = new Ref<ModelElement>(id).Instance;
				else if (type == "seq")
				{
					var seqs = seq?.Split(':');
					obj = seqs[1].CastSeq(seqs[0]);
				}
				else
				{
					if (!byte.TryParse(type, out var index))
						throw new InvalidCastException("bad param, must be byte value: " + type);

					obj = arguments?[index];
				}

				if (obj is null) return null;
				#endregion

				#region child
				foreach (var pl in ps.Skip(1))
				{
					var args = ArgItem.GetArgs(pl);
					for (int x = 0; x < args.Length; x++)
					{
						if (x == 0) args[0].ValidType(ref obj);
						else obj = args[x].GetObject(obj);

						if (obj is ImageProperty) break;
					}
				}
				#endregion

				return obj;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"handle arg failed: {p}\n\t{ex.Message}");
				return null;
			}
		}
		#endregion
	}

	class ArgItem(string target)
	{
		#region Props
		public string Target => target;

		public ArgItem Prev { get; private set; }

		public ArgItem Next { get; private set; }
		#endregion

		#region Methods		
		public static ArgItem[] GetArgs(string text)
		{
			var args = text.Split('.').Select(o => new ArgItem(o)).ToArray();
			for (int x = 0; x < args.Length; x++)
			{
				if (x != 0)
					args[x].Prev = args[x - 1];

				if (x != args.Length - 1)
					args[x].Next = args[x + 1];
			}

			return args;
		}

		public void ValidType(ref object value)
		{
			var target = Target?.ToLower();
			if (target is null) return;

			if (value is null) return;
			if (value is Enum)
			{
				// may be get null value, so keep origin value
				var ori = value;
				if (target == "job" && value is JobSeq job)
					value = job.Convert();

				value ??= ori;
				return;
			}


			var type = value.GetType();
			if (target == "string")
			{
				if (type != typeof(string)) value = value.ToString();
				return;
			}
			else if (target == "integer")
			{
				value = new Integer(Convert.ToDouble(value));
				return;
			}
			else if (target.Equals("skill") && value is Skill3) return;
			else if (target.Equals("item-name") && value is Item item)
			{
				value = item.ItemName;
				return;
			}
			else if (value is Integer integer && integer.TryGetArgument(target, out var temp))
			{
				value = temp;
				return;
			}
			else
			{
				target = target.Replace("-", null).Replace("_", null);
				if (target.Equals(type.Name, StringComparison.OrdinalIgnoreCase)) return;
				else if (target.Equals(type.BaseType?.Name, StringComparison.OrdinalIgnoreCase)) return;

				throw new InvalidCastException($"valid failed: {Target} > {type}");
			}
		}

		public object? GetObject(object value)
		{
			if (value is null) return null;
			else if (value is string) return value;
			else if (value is ImageProperty bitmap)
			{
				if (Next!.Target == "scale")
				{
					var scale = short.Parse(Next.Next.Target);
					//bitmap.Scale = scale * 0.01F;
				}

				return bitmap;
			}
			else if (value.GetType().IsClass && value.TryGetArgument(Target, out var param)) return param;

			Debug.WriteLine($"not supported class: {value} ({value.GetType().Name} > {Target})");
			return null;
		}
		#endregion
	}
	#endregion
}


public static class TextExtension
{
	public static string GetText(this object obj)
	{
		if (obj is null) return null;
		else if (obj is Enum sequence) return SequenceExtensions.GetText(sequence);
		else if (obj is Ref<Text> reference) return reference.Instance?.text;
		else if (obj is Record record && record.Owner.Name == "text") return record.Attributes.Get<string>("text");
		else if (obj is string alias) return FileCache.Data.Provider.GetTable<Text>()[alias]?.text;
		else throw new NotSupportedException();
	}

	public static string GetText(this object obj, TextArguments arguments) => GetText(obj).Replace(arguments);

	public static string Replace(this string text, TextArguments arguments)
	{
		if (text is null) return null;

		foreach (Match m in new Regex("<arg.*?/>").Matches(text).Cast<Match>())
		{
			var doc = new HtmlDocument();
			string html = m.Value;
			doc.LoadHtml(html);

			// element
			var arg = new Argument(doc.DocumentNode.FirstChild);
			var result = arg.GetObject(arguments);
			text = text.Replace(html, result?.ToString());
		}

		return text;
	}

	public static bool TryGetArgument<T>(this T instance, string name, out object value)
	{
		if (name == instance!.GetType().Name)
		{
			value = instance;
			return true;
		}

		// element
		if (instance is ModelElement element && element.Attributes.TryGetValue(name, out value))
		{
			if (value is Record record && record.Owner.Name == "text")
				value = record.Attributes["text"];

			return true;
		}

		// instance
		var member = instance.GetProperty(name);
		if (member != null)
		{
			var obj = member.GetValue(instance);
			value = obj is Ref<Text> text ? text.GetText() : obj;
			return true;
		}

		value = null;
		return false;
	}
}