using System.Collections;
using System.Runtime.CompilerServices;

namespace Xylia.Preview.UI.Common.Documents.Args;
public sealed class ContentParams
{
	#region Fields
	private const int DefaultCapacity = 4;

	internal object[] _items = new object[DefaultCapacity];
	internal int _size;

	public ContentParams(params object[] args)
	{
		this.AddRange(args);
	}
	#endregion

	#region Properties
	// Gets and sets the capacity of this list.  The capacity is the size of
	// the internal array used to hold items.  When set, the internal
	// array of the list is reallocated to the given capacity.
	//
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

	/// <summary>
	/// Gets or sets the element at the specified index.
	/// </summary>
	/// <param name="ArgIndex">The one-based index of the element to get or set.</param>
	/// <returns></returns>
	public object this[int ArgIndex]
	{
		get => ArgIndex > this.Count ? default : _items[ArgIndex - 1];
		set
		{
			if (ArgIndex > this.Count)
			{
				int count = ArgIndex - this.Count;
				for (int x = 0; x < count; x++)
					this.Add(default);
			}

			_items[ArgIndex - 1] = value;
		}
	}
	#endregion

	#region Methods
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(object item)
	{
		object[] array = _items;
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

	public IEnumerator GetEnumerator() => _items.GetEnumerator();
	#endregion
}