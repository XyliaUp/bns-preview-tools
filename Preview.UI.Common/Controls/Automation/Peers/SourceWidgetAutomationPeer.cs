using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using Xylia.Preview.UI.Controls.Primitives;
using ItemContainerGenerator = Xylia.Preview.UI.Controls.Primitives.ItemContainerGenerator;

namespace Xylia.Preview.UI.Controls.Automation.Peers;
public abstract class SourceWidgetAutomationPeer : FrameworkElementAutomationPeer, IItemContainerProvider
{
    ///
    protected SourceWidgetAutomationPeer(BnsCustomSourceBaseWidget owner) : base(owner)
    { }

    ///
    override public object GetPattern(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.Scroll)
        {
            //var owner = (BnsCustomSourceBaseWidget)Owner;
            //if (owner.ScrollHost != null)
            //{
            //	AutomationPeer scrollPeer = UIElementAutomationPeer.CreatePeerForElement(owner.ScrollHost);
            //	if (scrollPeer != null && scrollPeer is IScrollProvider)
            //	{
            //		scrollPeer.EventsSource = this;
            //		return (IScrollProvider)scrollPeer;
            //	}
            //}
        }
        else if (patternInterface == PatternInterface.ItemContainer)
        {
            if (Owner as BnsCustomSourceBaseWidget != null)
                return this;
            return null;
        }

        return base.GetPattern(patternInterface);
    }


    ///<summary>
    /// If grouping is enabled then return peers corresponding to all the items in container
    /// otherwise sees VirtualizingStackPanel(itemsHost) and return peers corresponding to
    /// items which are de-virtualized.
    ///</summary>
    protected override List<AutomationPeer> GetChildrenCore()
    {
        //List<AutomationPeer> children = null;
        //ItemPeersStorage<ItemAutomationPeer> oldChildren = _dataChildren; //cache the old ones for possible reuse
        //_dataChildren = new ItemPeersStorage<ItemAutomationPeer>();
        //BnsCustomSourceBaseWidget owner = (BnsCustomSourceBaseWidget)Owner;
        //var items = owner.Items;
        //Panel itemHost = owner.ItemsHost;
        //IList childItems = null;
        //bool useNetFx472CompatibleAccessibilityFeatures = AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures;

        //if (owner.IsGrouping)
        //{
        //	if (itemHost == null)
        //		return null;

        //	if (!useNetFx472CompatibleAccessibilityFeatures)
        //	{
        //		_reusablePeers = oldChildren;
        //	}

        //	childItems = itemHost.Children;
        //	children = new List<AutomationPeer>(childItems.Count);

        //	foreach (UIElement child in childItems)
        //	{
        //		UIElementAutomationPeer peer = child.CreateAutomationPeer() as UIElementAutomationPeer;
        //		if (peer != null)
        //		{
        //			children.Add(peer);

        //			if (useNetFx472CompatibleAccessibilityFeatures)
        //			{
        //				//
        //				// The AncestorsInvalid check is meant so that we do this call to invalidate the
        //				// GroupItemPeers containing the realized item peers only when we arrive here from an
        //				// UpdateSubtree call because that call does not otherwise descend into parts of the tree
        //				// that have their children invalid as an optimization.
        //				//
        //				if (_recentlyRealizedPeers != null && _recentlyRealizedPeers.Count > 0 && this.AncestorsInvalid)
        //				{
        //					GroupItemAutomationPeer groupItemPeer = peer as GroupItemAutomationPeer;
        //					if (groupItemPeer != null)
        //					{
        //						groupItemPeer.InvalidateGroupItemPeersContainingRecentlyRealizedPeers(_recentlyRealizedPeers);
        //					}
        //				}
        //			}
        //			else
        //			{
        //				//
        //				// The AncestorsInvalid check is meant so that we do this call to invalidate the
        //				// GroupItemPeers only when we arrive here from an
        //				// UpdateSubtree call because that call does not otherwise descend into parts of the tree
        //				// that have their children invalid as an optimization.
        //				//
        //				if (this.AncestorsInvalid)
        //				{
        //					GroupItemAutomationPeer groupItemPeer = peer as GroupItemAutomationPeer;
        //					if (groupItemPeer != null)
        //					{
        //						// invalidate all GroupItemAP children, so
        //						// that the top-level BnsCustomSourceBaseWidgetAP's
        //						// ItemPeers collection is repopulated.
        //						groupItemPeer.AncestorsInvalid = true;
        //						groupItemPeer.ChildrenValid = true;
        //					}
        //				}
        //			}
        //		}
        //	}

        //	return children;
        //}
        //else if (items.Count > 0)
        //{
        //	// To avoid the situation on legacy systems which may not have new unmanaged core. this check with old unmanaged core
        //	// ensures the older behavior as ItemContainer pattern won't be available.
        //	if (IsVirtualized)
        //	{
        //		if (itemHost == null)
        //			return null;

        //		childItems = itemHost.Children;
        //	}
        //	else
        //	{
        //		childItems = items;
        //	}
        //	children = new List<AutomationPeer>(childItems.Count);

        //	foreach (object item in childItems)
        //	{
        //		object dataItem;
        //		if (IsVirtualized)
        //		{
        //			// 'item' is a container - get the corresponding data item
        //			DependencyObject d = item as DependencyObject;
        //			dataItem = (d != null) ? owner.ItemContainerGenerator.ItemFromContainer(d) : null;

        //			// ItemFromContainer can return {UnsetValue} if we're in a re-entrant
        //			// call while the generator is in the midst of unhooking the container.
        //			// Ignore such children. 
        //			if (dataItem == DependencyProperty.UnsetValue)
        //			{
        //				continue;
        //			}
        //		}
        //		else
        //		{
        //			// 'item' is a data item
        //			dataItem = item;
        //		}

        //		// try to reuse old peer if it exists either in Current AT or in WeakRefStorage of Peers being sent to Client
        //		ItemAutomationPeer peer = oldChildren[dataItem];
        //		peer = ReusePeerForItem(peer, dataItem);

        //		if (peer == null)
        //		{
        //			peer = CreateItemAutomationPeer(dataItem);
        //		}

        //		// perform hookup so the events sourced from wrapper peer are fired as if from the data item
        //		if (peer != null)
        //		{
        //			AutomationPeer wrapperPeer = peer.GetWrapperPeer();
        //			if (wrapperPeer != null)
        //			{
        //				wrapperPeer.EventsSource = peer;
        //			}
        //		}

        //		// protection from indistinguishable items - for example, 2 strings with same value
        //		// this scenario does not work in BnsCustomSourceBaseWidget however is not checked for.
        //		if (peer != null && _dataChildren[dataItem] == null)
        //		{
        //			children.Add(peer);
        //			_dataChildren[dataItem] = peer;
        //		}
        //	}

        //	return children;
        //}

        return null;
    }

    internal ItemAutomationPeer ReusePeerForItem(ItemAutomationPeer peer, object item)
    {
        if (peer == null)
        {
            peer = GetPeerFromWeakRefStorage(item);
            //if (peer != null)
            //{
            //	// As cached peer is getting used it must be invalidated.
            //	peer.AncestorsInvalid = false;
            //	peer.ChildrenValid = false;
            //}
        }

        if (peer != null)
        {
            peer.ReuseForItem(item);
        }

        return peer;
    }


    internal void AddProxyToWeakRefStorage(WeakReference wr, ItemAutomationPeer itemPeer)
    {
        BnsCustomSourceBaseWidget owner = Owner as BnsCustomSourceBaseWidget;
        var items = owner.Items;
        if (items != null)
        {
            if (GetPeerFromWeakRefStorage(itemPeer.Item) == null)
                WeakRefElementProxyStorage[itemPeer.Item] = wr;
        }
    }

    ///
    IRawElementProviderSimple IItemContainerProvider.FindItemByProperty(IRawElementProviderSimple startAfter, int propertyId, object value)
    {
        ResetChildrenCache();
        // Checks if propertyId is valid else throws ArgumentException to notify it as invalid argument is being passed
        if (propertyId != 0)
        {
            if (!IsPropertySupportedByControlForFindItem(propertyId))
            {
                throw new ArgumentException("PropertyNotSupported");
            }
        }

        if (Owner is BnsCustomSourceBaseWidget owner)
        {
            var items = owner.Items;
            if (items != null && items.Count > 0)
            {
                ItemAutomationPeer startAfterItem = null;
                if (startAfter != null)
                {
                    // get the peer corresponding to this provider
                    startAfterItem = PeerFromProvider(startAfter) as ItemAutomationPeer;
                    if (startAfterItem == null)
                        return null;
                }

                // startIndex refers to the index of the item just after startAfterItem
                int startIndex = 0;
                if (startAfterItem != null)
                {
                    if (startAfterItem.Item == null)
                    {
                        throw new InvalidOperationException("InavalidStartItem");
                    }

                    // To find the index of the item in items collection which occurs
                    // immidiately after startAfterItem.Item
                    startIndex = items.IndexOf(startAfterItem.Item) + 1;
                    if (startIndex == 0 || startIndex == items.Count)
                        return null;
                }

                if (propertyId == 0)
                {
                    for (int i = startIndex; i < items.Count; i++)
                    {
                        // This is to handle the case of when dataItems are just plain strings and have duplicates,
                        // only the first occurence of duplicate Items will be returned. It has also been used couple more times below.
                        if (items.IndexOf(items[i]) != i)
                            continue;
                        return ProviderFromPeer(FindOrCreateItemAutomationPeer(items[i]));
                    }
                }

                ItemAutomationPeer currentItemPeer;
                object currentValue = null;
                for (int i = startIndex; i < items.Count; i++)
                {
                    currentItemPeer = FindOrCreateItemAutomationPeer(items[i]);
                    if (currentItemPeer == null)
                        continue;
                    //try
                    //{
                    //	currentValue = GetSupportedPropertyValue(currentItemPeer, propertyId);
                    //}
                    //catch (Exception ex)
                    //{
                    //	if (ex is ElementNotAvailableException)
                    //		continue;
                    //}

                    //if (value == null || currentValue == null)
                    //{
                    //	// Accept null as value corresponding to the property if it finds an item with null as the value of corresponding property else ignore.
                    //	if (currentValue == null && value == null && items.IndexOf(items[i]) == i)
                    //		return (ProviderFromPeer(currentItemPeer));
                    //	else
                    //		continue;
                    //}

                    // Match is found within the specified criterion of search
                    if (value.Equals(currentValue) && items.IndexOf(items[i]) == i)
                        return ProviderFromPeer(currentItemPeer);
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Verifies whether the propertyId is supported by find criterion. It can be overriden by derived classes
    /// if they wish to support more properties.
    /// </summary>
    /// <param name="id">Property Id to be verified</param>
    /// <returns>true if property id is supported else false</returns>
    virtual internal bool IsPropertySupportedByControlForFindItem(int id)
    {
        return IsPropertySupportedByControlForFindItemInternal(id);
    }

    internal static bool IsPropertySupportedByControlForFindItemInternal(int id)
    {
        if (AutomationElementIdentifiers.NameProperty.Id == id)
            return true;
        else if (AutomationElementIdentifiers.AutomationIdProperty.Id == id)
            return true;
        else if (AutomationElementIdentifiers.ControlTypeProperty.Id == id)
            return true;
        else
            return false;
    }

    ///// <summary>
    ///// This method is responsible for providing the value corresponding to the propertyId for itemPeer
    ///// This method can be overriden by derived classes if they support more properties for search.
    ///// </summary>
    ///// <param name="itemPeer"></param>
    ///// <param name="propertyId"></param>
    ///// <returns>returns the property value</returns>
    //virtual internal object GetSupportedPropertyValue(ItemAutomationPeer itemPeer, int propertyId)
    //{
    //	return SourceWidgetAutomationPeer.GetSupportedPropertyValueInternal(itemPeer, propertyId);
    //}

    //internal static object GetSupportedPropertyValueInternal(AutomationPeer itemPeer, int propertyId)
    //{
    //	return itemPeer.GetPropertyValue(propertyId);
    //}

    /// <summary>
    /// It returns the ItemAutomationPeer if it exist corresponding to the item otherwise it creates
    /// one and does add the Handle and parent info by calling TrySetParentInfo.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected virtual internal ItemAutomationPeer FindOrCreateItemAutomationPeer(object item)
    {
        ItemAutomationPeer peer = ItemPeers[item];
        if (peer == null)
            peer = GetPeerFromWeakRefStorage(item);

        if (peer == null)
        {
            peer = CreateItemAutomationPeer(item);

            //if (peer != null)
            //{
            //	peer.TrySetParentInfo(this);
            //}
        }

        if (peer != null)
        {
            //perform hookup so the events sourced from wrapper peer are fired as if from the data item
            AutomationPeer wrapperPeer = peer.GetWrapperPeer();
            if (wrapperPeer != null)
            {
                wrapperPeer.EventsSource = peer;
            }
        }

        return peer;
    }

    // Called by GroupItemAutomationPeer
    internal ItemAutomationPeer CreateItemAutomationPeerInternal(object item)
    {
        return CreateItemAutomationPeer(item);
    }

    ///
    abstract protected ItemAutomationPeer CreateItemAutomationPeer(object item);

    internal RecyclableWrapper GetRecyclableWrapperPeer(object item)
    {
        var widget = (BnsCustomSourceBaseWidget)Owner;

        if (_recyclableWrapperCache == null)
        {
            _recyclableWrapperCache = new RecyclableWrapper(widget, item);
        }
        else
        {
            _recyclableWrapperCache.LinkItem(item);
        }

        return _recyclableWrapperCache;
    }

    // UpdateChildrenIntenal is called with ItemsInvalidateLimit to ensure we don’t fire unnecessary structure change events when items are just scrolled in/out of view in case of
    // virtualized controls.
    //override internal IDisposable UpdateChildren()
    //{
    //	UpdateChildrenInternal(AutomationInteropProvider.ItemsInvalidateLimit);
    //	WeakRefElementProxyStorage.PurgeWeakRefCollection();
    //	return AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures
    //			? null
    //			: new UpdateChildrenHelper(this);
    //}

    // Provides Peer if exist in Weak Reference Storage
    internal ItemAutomationPeer GetPeerFromWeakRefStorage(object item)
    {
        ItemAutomationPeer returnPeer = null;
        WeakReference weakRefEP = WeakRefElementProxyStorage[item];
        if (weakRefEP != null)
        {
            //ElementProxy provider = weakRefEP.Target as ElementProxy;
            //if (provider != null)
            //{
            //	returnPeer = PeerFromProvider(provider as IRawElementProviderSimple) as ItemAutomationPeer;
            //	if (returnPeer == null)
            //		WeakRefElementProxyStorage.Remove(item);
            //}
            //else
            //	WeakRefElementProxyStorage.Remove(item);
        }

        return returnPeer;
    }

    //
    internal AutomationPeer GetExistingPeerByItem(object item, bool checkInWeakRefStorage)
    {
        AutomationPeer returnPeer = null;
        if (checkInWeakRefStorage)
        {
            returnPeer = GetPeerFromWeakRefStorage(item);
        }
        if (returnPeer == null)
        {
            returnPeer = ItemPeers[item];
        }

        return returnPeer;
    }

    internal ItemAutomationPeer ReusablePeerFor(object item)
    {
        if (_reusablePeers != null)
        {
            return _reusablePeers[item];
        }
        else
        {
            return ItemPeers[item];
        }
    }

    private void ClearReusablePeers(ItemPeersStorage<ItemAutomationPeer> oldChildren)
    {
        if (_reusablePeers == oldChildren)
        {
            _reusablePeers = null;
        }
    }

    protected virtual bool IsVirtualized
    {
        get { return ItemContainerPatternIdentifiers.Pattern != null; }
    }

    // Derived classes should be able to access peers cache
    internal ItemPeersStorage<ItemAutomationPeer> ItemPeers
    {
        get { return _dataChildren; }

        set { _dataChildren = value; }
    }

    internal ItemPeersStorage<WeakReference> WeakRefElementProxyStorage
    {
        get { return _WeakRefElementProxyStorage; }

        set { _WeakRefElementProxyStorage = value; }
    }

    // *** DEAD CODE   Only call is from dead code when UseNetFx472CompatibleAccessibilityFeatures==true ***
    internal List<ItemAutomationPeer> RecentlyRealizedPeers
    {
        get
        {
            if (_recentlyRealizedPeers == null)
            {
                _recentlyRealizedPeers = new List<ItemAutomationPeer>();
            }

            return _recentlyRealizedPeers;
        }
    }

    private ItemPeersStorage<ItemAutomationPeer> _dataChildren = new ItemPeersStorage<ItemAutomationPeer>();
    private ItemPeersStorage<ItemAutomationPeer> _reusablePeers;
    private ItemPeersStorage<WeakReference> _WeakRefElementProxyStorage = new ItemPeersStorage<WeakReference>();
    private List<ItemAutomationPeer> _recentlyRealizedPeers;    // *** DEAD CODE   Only use is from dead code when UseNetFx472CompatibleAccessibilityFeatures==true ***
    private RecyclableWrapper _recyclableWrapperCache;

    // In a grouped BnsCustomSourceBaseWidget, the item peers are held by the BnsCustomSourceBaseWidgetAP.
    // Yet during UpdateSubtree the peers should be available for re-use by
    // the GroupItemAPs at the leaf level.  Otherwise, the cost of creating new
    // peers and raising events is a huge perf hit 
    // To achieve this, the BnsCustomSourceBaseWidgetAP retains its old children during the
    // recursive UpdateSubtree work, in its "ReusablePeers" store.  When UpdateSubtree
    // is done, it calls Dispose on this helper to release the temporary store.
    private class UpdateChildrenHelper : IDisposable
    {
        internal UpdateChildrenHelper(SourceWidgetAutomationPeer peer)
        {
            _peer = peer;
            _oldChildren = peer.ItemPeers;
        }

        void IDisposable.Dispose()
        {
            if (_peer != null)
            {
                _peer.ClearReusablePeers(_oldChildren);
                _peer = null;
            }
        }

        SourceWidgetAutomationPeer _peer;
        ItemPeersStorage<ItemAutomationPeer> _oldChildren;
    }
}

internal class ItemPeersStorage<T> where T : class
{
    public ItemPeersStorage()
    {
    }

    public void Clear()
    {
        _usesHashCode = false;
        _count = 0;

        if (_hashtable != null)
            _hashtable.Clear();

        if (_list != null)
            _list.Clear();
    }

    public T this[object item]
    {
        get
        {
            if (_count == 0 || item == null)
                return default;

            if (_usesHashCode)
            {
                if (_hashtable == null || !_hashtable.ContainsKey(item))
                    return default;

                return _hashtable[item] as T;
            }
            else
            {
                if (_list == null)
                    return default;

                for (int i = 0; i < _list.Count; i++)
                {
                    KeyValuePair<object, T> pair = _list[i];
                    if (Equals(item, pair.Key))
                        return pair.Value;
                }

                return default;
            }
        }
        set
        {
            // Does not cache null items
            if (item == null)
                return;

            // When we add the first item we need to determine whether to use hashtable or list
            //if (_count == 0)
            //{
            //	_usesHashCode = item != null && HashHelper.HasReliableHashCode(item);
            //}

            //if (_usesHashCode)
            //{
            //	if (_hashtable == null)
            //		_hashtable = new WeakDictionary<object, T>();

            //	if (!_hashtable.ContainsKey(item) && value is T)
            //		_hashtable[item] = value;
            //	else
            //		Debug.Assert(false, "it must not add already present Item");
            //}
            //else
            //{
            //	if (_list == null)
            //		_list = new List<KeyValuePair<object, T>>();
            //	if (value is T)
            //		_list.Add(new KeyValuePair<object, T>(item, value));
            //}

            _count++;
        }
    }

    public void Remove(object item)
    {
        if (_usesHashCode)
        {
            if (item != null && _hashtable.ContainsKey(item))
            {
                _hashtable.Remove(item);
                if (!_hashtable.ContainsKey(item))
                    _count--;
            }
        }
        else
        {
            if (_list != null)
            {
                int i = 0;
                for (i = 0; i < _list.Count; i++)
                {
                    KeyValuePair<object, T> pair = _list[i];
                    if (Equals(item, pair.Key))
                        break;
                }
                if (i < _list.Count)
                {
                    _list.RemoveAt(i);
                    _count--;
                }
            }
        }
    }

    // To purge the collection corresponding to WeakReference for dead references
    // write a generic Iterator and move the purging code to BnsCustomSourceBaseWidgetAutomationPeer using the Iterator of this collection class
    public void PurgeWeakRefCollection()
    {
        if (!typeof(T).IsAssignableFrom(typeof(WeakReference)))
            return;
        List<object> cleanUpItemsCollection = new List<object>();

        if (_usesHashCode)
        {
            //if (_hashtable == null)
            //	return;
            //foreach (KeyValuePair<object, T> dictionaryEntry in _hashtable)
            //{
            //	WeakReference weakRef = dictionaryEntry.Value as WeakReference;
            //	if (weakRef == null)
            //	{
            //		cleanUpItemsCollection.Add(dictionaryEntry.Key);
            //		continue;
            //	}
            //	ElementProxy proxy = weakRef.Target as ElementProxy;
            //	if (proxy == null)
            //	{
            //		cleanUpItemsCollection.Add(dictionaryEntry.Key);
            //		continue;
            //	}
            //	ItemAutomationPeer peer = proxy.Peer as ItemAutomationPeer;
            //	if (peer == null)
            //		cleanUpItemsCollection.Add(dictionaryEntry.Key);
            //}
        }

        else
        {
            if (_list == null)
                return;
            foreach (KeyValuePair<object, T> keyValuePair in _list)
            {
                //WeakReference weakRef = keyValuePair.Value as WeakReference;
                //if (weakRef == null)
                //{
                //	cleanUpItemsCollection.Add(keyValuePair.Key);
                //	continue;
                //}
                //ElementProxy proxy = weakRef.Target as ElementProxy;
                //if (proxy == null)
                //{
                //	cleanUpItemsCollection.Add(keyValuePair.Key);
                //	continue;
                //}
                //ItemAutomationPeer peer = proxy.Peer as ItemAutomationPeer;
                //if (peer == null)
                //	cleanUpItemsCollection.Add(keyValuePair.Key);
            }
        }

        foreach (object item in cleanUpItemsCollection)
        {
            Remove(item);
        }
    }

    public int Count
    {
        get { return _count; }
    }

    private Dictionary<object, T> _hashtable = null;
    private List<KeyValuePair<object, T>> _list = null;
    private int _count = 0;
    private bool _usesHashCode = false;
}

internal class RecyclableWrapper : IDisposable
{
    public RecyclableWrapper(BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget, object item)
    {
        _BnsCustomSourceBaseWidget = BnsCustomSourceBaseWidget;
        _container = ((IGeneratorHost)BnsCustomSourceBaseWidget).GetContainerForItem(item);

        LinkItem(item);
    }

    public void LinkItem(object item)
    {
        _item = item;

        ItemContainerGenerator.LinkContainerToItem(_container, _item);
        _BnsCustomSourceBaseWidget.ItemContainerGenerator.PrepareItemContainer(_container);
    }

    private void UnlinkItem()
    {
        if (_item != null)
        {
            ItemContainerGenerator.UnlinkContainerFromItem(_container, _item, _BnsCustomSourceBaseWidget);
            _item = null;
        }
    }

    void IDisposable.Dispose()
    {
        UnlinkItem();
    }

    public AutomationPeer Peer
    {
        get
        {
            return UIElementAutomationPeer.CreatePeerForElement((UIElement)_container);
        }
    }

    BnsCustomSourceBaseWidget _BnsCustomSourceBaseWidget;
    DependencyObject _container;
    object _item;
}


internal class SourceWidgetWrapperAutomationPeer(BnsCustomSourceBaseWidget owner) : SourceWidgetAutomationPeer(owner)
{
	override protected ItemAutomationPeer CreateItemAutomationPeer(object item)
    {
        return new SourceWidgetItemAutomationPeer(item, this);
    }

    override protected string GetClassNameCore()
    {
        return "BnsCustomSourceBaseWidget";
    }

    override protected AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.List;
    }
}