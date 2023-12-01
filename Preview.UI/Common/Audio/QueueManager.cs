using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.UI.Audio;

internal class QueueManager<T> : Collection<T>, INotifyCollectionChanged, INotifyPropertyChanged
{
	#region Collection
	protected override void ClearItems()
	{
		base.ClearItems();
		this.Current = default;
		this._order = null;

		OnCountPropertyChanged();
		OnIndexerPropertyChanged();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Reset));
	}

	protected override void RemoveItem(int index)
	{
		T removedItem = this[index];

		base.RemoveItem(index);
		_order.Remove(index);

		OnCountPropertyChanged();
		OnIndexerPropertyChanged();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Remove, removedItem, index));
	}

	protected override void InsertItem(int index, T item)
	{
		base.InsertItem(index, item);
		_order.Add(index);

		OnCountPropertyChanged();
		OnIndexerPropertyChanged();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Add, item, index));
	}

	protected override void SetItem(int index, T item)
	{
		T originalItem = this[index];
		base.SetItem(index, item);

		OnIndexerPropertyChanged();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Replace, originalItem, item, index));
	}

	protected virtual void MoveItem(int oldIndex, int newIndex)
	{
		T removedItem = this[oldIndex];

		base.RemoveItem(oldIndex);
		base.InsertItem(newIndex, removedItem);

		OnIndexerPropertyChanged();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Move, removedItem, newIndex, oldIndex));
	}

	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
	{
		PropertyChanged?.Invoke(this, e);
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
	{
		CollectionChanged?.Invoke(this, e);
	}

	private void OnCountPropertyChanged() => OnPropertyChanged(new PropertyChangedEventArgs("Count"));

	private void OnIndexerPropertyChanged() => OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
	#endregion


	#region Order
	private List<int> _order = new List<int>();
	private bool _shuffle;

	public bool Shuffle
	{
		get => _shuffle;
		set
		{
			_shuffle = value;
			CreateOrder();
		}
	}

	/// <summary>
	///  Create a new queue order
	/// </summary>
	/// <param name="Shuffle"></param>
	private void CreateOrder()
	{
		lock(this)
		{
			// Create indices
			var Indices = Enumerable.Range(0, this.Count).ToList();
			if (_shuffle) Indices = Indices.Randomize();

			// We're playing a track from the queue: shuffle, but make sure the playing track comes first.
			if (Current != null && this.Contains(Current))
			{
				Indices.Insert(0, this.IndexOf(Current));
			}

			_order = Indices;
		}
	}

	/// <summary>
	/// Searches for the specified object and returns the queue order
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	private int OrderOf(T value)
	{
		int index = this.IndexOf(value);
		return _order.IndexOf(index);
	}
	#endregion

	#region Queue
	public T Current { get; set; }

	public T First()
	{
		if (_order != null && _order.Count > 0) return this[_order.First()];
		else return this.FirstOrDefault();
	}

	public T Previous(LoopMode loopMode)
	{
		lock (this)
		{
			int currentIndex = this.OrderOf(Current);
			if (loopMode == LoopMode.One)
			{
				// Return the current
				return this.Current;
			}
			else if (currentIndex > 0)
			{
				// If we didn't reach the start of the queue, return the previous track.
				return this[_order[currentIndex - 1]];
			}
			else if (loopMode == LoopMode.All)
			{
				// When LoopMode.All is enabled, when we reach the start of the queue, return the last track.
				return this[_order.Last()];
			}
			else return default;
		}
	}

	public T Next(LoopMode loopMode, bool returnToStart)
	{
		lock (this)
		{
			int currentIndex = this.OrderOf(Current);
			if (loopMode.Equals(LoopMode.One))
			{
				// Return the current
				return Current;
			}
			else if (currentIndex < _order.Count - 1)
			{
				// If we didn't reach the end of the queue, return the next track.
				int increment = 1;

				var nextTrack = this[_order[currentIndex + increment]];

				// HACK: voids getting stuck on the same track when the playlist contains the same track multiple times
				while (Equals(nextTrack, Current))
				{
					increment++;
					nextTrack = this[_order[currentIndex + increment]];
				}

				return nextTrack;
			}
			else if (loopMode.Equals(LoopMode.All) | returnToStart)
			{
				// When LoopMode.All is enabled, when we reach the end of the queue, return the first track.
				return this[_order.First()];
			}
			else return default;
		}
	}
	#endregion
}