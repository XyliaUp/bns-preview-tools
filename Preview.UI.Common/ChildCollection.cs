using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Xylia.Preview.UI;
public class ChildCollection : Collection<Visual>, ICollection, IEnumerable, IList, IItemProperties	, INotifyCollectionChanged
{
	internal ChildCollection(FrameworkElement parent)
	{

	}

	public ReadOnlyCollection<ItemPropertyInfo> ItemProperties => throw new NotImplementedException();


	#region INotifyCollectionChanged
	protected override void ClearItems()
	{
		base.ClearItems();
		OnCollectionChanged(new(NotifyCollectionChangedAction.Reset));
	}

	protected override void RemoveItem(int index)
	{
		Visual removedItem = this[index];

		base.RemoveItem(index);
		OnCollectionChanged(new(NotifyCollectionChangedAction.Remove, removedItem, index));
	}

	protected override void InsertItem(int index, Visual item)
	{
		base.InsertItem(index, item);
		OnCollectionChanged(new(NotifyCollectionChangedAction.Add, item, index));
	}

	protected override void SetItem(int index, Visual item)
	{
		Visual originalItem = this[index];
		base.SetItem(index, item);

		OnCollectionChanged(new(NotifyCollectionChangedAction.Replace, originalItem, item, index));
	}


	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
	{
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}