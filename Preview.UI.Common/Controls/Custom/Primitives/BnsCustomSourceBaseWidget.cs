using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Xylia.Preview.UI.Controls.Automation.Peers;
using Xylia.Preview.UI.Controls.Helpers;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.Controls.Primitives;

/// <summary>
///     The base class for all controls that have multiple children.
/// </summary>
[DefaultEvent("OnItemsChanged"), DefaultProperty("Items")]
[ContentProperty("Items")]
[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(FrameworkElement))]
[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)] // cannot be read & localized as string
public class BnsCustomSourceBaseWidget : BnsCustomBaseWidget, IGeneratorHost, IContainItemStorage
{
	#region Constructors

	/// <summary>
	///     Default constructor
	/// </summary>
	/// <remarks>
	///     Automatic determination of current Dispatcher. Use alternative constructor
	///     that accepts a Dispatcher for best performance.
	/// </remarks>
	public BnsCustomSourceBaseWidget() : base()
	{
		//ShouldCoerceCacheSizeField.SetValue(this, true);
		this.CoerceValue(VirtualizingPanel.CacheLengthUnitProperty);
	}

	static BnsCustomSourceBaseWidget()
	{
		// Define default style in code instead of in theme files.
		DefaultStyleKeyProperty.OverrideMetadata(typeof(BnsCustomSourceBaseWidget), new FrameworkPropertyMetadata(typeof(BnsCustomSourceBaseWidget)));
		EventManager.RegisterClassHandler(typeof(BnsCustomSourceBaseWidget), Keyboard.GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnGotFocus));
		VirtualizingStackPanel.ScrollUnitProperty.OverrideMetadata(typeof(BnsCustomSourceBaseWidget), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnScrollingModeChanged), new CoerceValueCallback(CoerceScrollingMode)));
		VirtualizingPanel.CacheLengthProperty.OverrideMetadata(typeof(BnsCustomSourceBaseWidget), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCacheSizeChanged)));
		VirtualizingPanel.CacheLengthUnitProperty.OverrideMetadata(typeof(BnsCustomSourceBaseWidget), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCacheSizeChanged), new CoerceValueCallback(CoerceVirtualizationCacheLengthUnit)));
	}

	private static void OnScrollingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		//ShouldCoerceScrollUnitField.SetValue(d, true);
		//d.CoerceValue(VirtualizingStackPanel.ScrollUnitProperty);
	}

	private static object CoerceScrollingMode(DependencyObject d, object baseValue)
	{
		//if (ShouldCoerceScrollUnitField.GetValue(d))
		//{
		//	ShouldCoerceScrollUnitField.SetValue(d, false);
		//	BaseValueSource baseValueSource = DependencyPropertyHelper.GetValueSource(d, VirtualizingStackPanel.ScrollUnitProperty).BaseValueSource;
		//	if (((BnsCustomSourceBaseWidget)d).IsGrouping && baseValueSource == BaseValueSource.Default)
		//	{
		//		return ScrollUnit.Pixel;
		//	}
		//}

		return baseValue;
	}

	private static void OnCacheSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		//ShouldCoerceCacheSizeField.SetValue(d, true);
		d.CoerceValue(e.Property);
	}

	//default VCLU will be Item for the flat non-grouping case
	private static object CoerceVirtualizationCacheLengthUnit(DependencyObject d, object baseValue)
	{
		//if (ShouldCoerceCacheSizeField.GetValue(d))
		//{
		//	ShouldCoerceCacheSizeField.SetValue(d, false);
		//	BaseValueSource baseValueSource = DependencyPropertyHelper.GetValueSource(d, VirtualizingStackPanel.CacheLengthUnitProperty).BaseValueSource;
		//	if (!((BnsCustomSourceBaseWidget)d).IsGrouping && !(d is TreeView) && baseValueSource == BaseValueSource.Default)
		//	{
		//		return VirtualizationCacheLengthUnit.Item;
		//	}
		//}

		return baseValue;
	}

	private void CreateItemCollectionAndGenerator()
	{
		_items = new ItemCollection(this);

		// ItemInfos must get adjusted before the generator's change handler is called,
		// so that any new ItemInfos arising from the generator don't get adjusted by mistake
		// (see Win8 690623).
		((INotifyCollectionChanged)_items).CollectionChanged += new NotifyCollectionChangedEventHandler(OnItemCollectionChanged1);

		// the generator must attach its collection change handler before
		// the control itself, so that the generator is up-to-date by the
		// time the control tries to use it (bug 892806 et al.)
		//_itemContainerGenerator = new ItemContainerGenerator(this);

		//_itemContainerGenerator.ChangeAlternationCount();

		((INotifyCollectionChanged)_items).CollectionChanged += new NotifyCollectionChangedEventHandler(OnItemCollectionChanged2);

		if (IsInitPending)
		{
			_items.BeginInit();
		}
		else if (IsInitialized)
		{
			_items.BeginInit();
			_items.EndInit();
		}

		((INotifyCollectionChanged)_groupStyle).CollectionChanged += new NotifyCollectionChangedEventHandler(OnGroupStyleChanged);
	}

	#endregion

	#region Properties

	/// <summary>
	///     Items is the collection of data that is used to generate the content
	///     of this control.
	/// </summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ItemCollection Items
	{
		get
		{
			if (_items == null)
			{
				CreateItemCollectionAndGenerator();
			}

			return _items;
		}
	}

	/// <summary>
	/// This method is used by TypeDescriptor to determine if this property should
	/// be serialized.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ShouldSerializeItems()
	{
		return HasItems;
	}

	/// <summary>
	///     The DependencyProperty for the ItemsSource property.
	///     Flags:              None
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemsSourceProperty
		= DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(BnsCustomSourceBaseWidget),
			 new FrameworkPropertyMetadata(null,
				 new PropertyChangedCallback(OnItemsSourceChanged)));

	private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var ic = (BnsCustomSourceBaseWidget)d;
		IEnumerable oldValue = (IEnumerable)e.OldValue;
		IEnumerable newValue = (IEnumerable)e.NewValue;

		((IContainItemStorage)ic).Clear();

		BindingExpressionBase beb = BindingOperations.GetBindingExpressionBase(d, ItemsSourceProperty);
		if (beb != null)
		{
			// ItemsSource is data-bound.   Always go to ItemsSource mode.
			// Also, extract the source item, to supply as context to the
			// CollectionRegistering event
			//ic.Items.SetItemsSource(newValue, (object x) => beb.GetSourceItem(x));
		}
		else if (e.NewValue != null)
		{
			// ItemsSource is non-null, but not data-bound.  Go to ItemsSource mode
			ic.Items.SetItemsSource(newValue);
		}
		else
		{
			// ItemsSource is explicitly null.  Return to normal mode.
			ic.Items.ClearItemsSource();
		}

		ic.OnItemsSourceChanged(oldValue, newValue);
	}

	/// <summary>
	/// Called when the value of ItemsSource changes.
	/// </summary>
	protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
	{
	}

	/// <summary>
	///     ItemsSource specifies a collection used to generate the content of
	/// this control.  This provides a simple way to use exactly one collection
	/// as the source of content for this control.
	/// </summary>
	/// <remarks>
	///     Any existing contents of the Items collection is replaced when this
	/// property is set. The Items collection will be made ReadOnly and FixedSize.
	///     When ItemsSource is in use, setting this property to null will remove
	/// the collection and restore use to Items (which will be an empty ItemCollection).
	///     When ItemsSource is not in use, the value of this property is null, and
	/// setting it to null has no effect.
	/// </remarks>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IEnumerable ItemsSource
	{
		get { return Items.ItemsSource; }
		set
		{
			if (value == null)
			{
				ClearValue(ItemsSourceProperty);
			}
			else
			{
				SetValue(ItemsSourceProperty, value);
			}
		}
	}

	/// <summary>
	/// The ItemContainerGenerator associated with this control
	/// </summary>
	[Bindable(false), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
	public ItemContainerGenerator ItemContainerGenerator
	{
		get
		{
			if (_itemContainerGenerator == null)
			{
				CreateItemCollectionAndGenerator();
			}

			return _itemContainerGenerator;
		}
	}

	/// <summary>
	///     Returns enumerator to logical children
	/// </summary>
	protected override IEnumerator LogicalChildren
	{
		get
		{
			if (!HasItems)
			{
				return EmptyEnumerator.Instance;
			}

			// Items in direct-mode of ItemCollection are the only model children.
			// note: the enumerator walks the ItemCollection.InnerList as-is,
			// no flattening of any content on model children level!
			return this.Items.LogicalChildren;
		}
	}

	// this is called before the generator's change handler
	private void OnItemCollectionChanged1(object sender, NotifyCollectionChangedEventArgs e)
	{
		AdjustItemInfoOverride(e);
	}

	// this is called after the generator's change handler
	private void OnItemCollectionChanged2(object sender, NotifyCollectionChangedEventArgs e)
	{
		SetValue(HasItemsPropertyKey, (_items != null) && !_items.IsEmpty);

		// If the focused item is removed, drop our reference to it.
		if (_focusedInfo != null && _focusedInfo.Index < 0)
		{
			_focusedInfo = null;
		}

		// on Reset, discard item storage
		if (e.Action == NotifyCollectionChangedAction.Reset)
		{
			((IContainItemStorage)this).Clear();
		}

		OnItemsChanged(e);
	}

	/// <summary>
	///     This method is invoked when the Items property changes.
	/// </summary>
	protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
	{
	}

	/// <summary>
	///     Adjust ItemInfos when the Items property changes.
	/// </summary>
	internal virtual void AdjustItemInfoOverride(NotifyCollectionChangedEventArgs e)
	{
		AdjustItemInfo(e, _focusedInfo);
	}

	/// <summary>
	///     The key needed set a read-only property.
	/// </summary>
	internal static readonly DependencyPropertyKey HasItemsPropertyKey =
			DependencyProperty.RegisterReadOnly(
					"HasItems",
					typeof(bool),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, OnVisualStatePropertyChanged));

	/// <summary>
	///     The DependencyProperty for the HasItems property.
	///     Flags:              None
	///     Other:              Read-Only
	///     Default Value:      false
	/// </summary>
	public static readonly DependencyProperty HasItemsProperty =
			HasItemsPropertyKey.DependencyProperty;

	/// <summary>
	///     True if Items.Count > 0, false otherwise.
	/// </summary>
	[Bindable(false), Browsable(false)]
	public bool HasItems
	{
		get { return (bool)GetValue(HasItemsProperty); }
	}

	/// <summary>
	///     The DependencyProperty for the DisplayMemberPath property.
	///     Flags:              none
	///     Default Value:      string.Empty
	/// </summary>
	public static readonly DependencyProperty DisplayMemberPathProperty =
			DependencyProperty.Register(
					"DisplayMemberPath",
					typeof(string),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							string.Empty,
							new PropertyChangedCallback(OnDisplayMemberPathChanged)));

	/// <summary>
	///     DisplayMemberPath is a simple way to define a default template
	///     that describes how to convert Items into UI elements by using
	///     the specified path.
	/// </summary>
	public string DisplayMemberPath
	{
		get { return (string)GetValue(DisplayMemberPathProperty); }
		set { SetValue(DisplayMemberPathProperty, value); }
	}

	/// <summary>
	///     Called when DisplayMemberPathProperty is invalidated on "d."
	/// </summary>
	private static void OnDisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		BnsCustomSourceBaseWidget ctrl = (BnsCustomSourceBaseWidget)d;

		ctrl.OnDisplayMemberPathChanged((string)e.OldValue, (string)e.NewValue);
		ctrl.UpdateDisplayMemberTemplateSelector();
	}

	// DisplayMemberPath and ItemStringFormat use the ItemTemplateSelector property
	// to achieve the desired result.  When either of these properties change,
	// update the ItemTemplateSelector property here.
	private void UpdateDisplayMemberTemplateSelector()
	{
		string displayMemberPath = DisplayMemberPath;
		string itemStringFormat = ItemStringFormat;

		//if (!string.IsNullOrEmpty(displayMemberPath) || !string.IsNullOrEmpty(itemStringFormat))
		//{
		//	// One or both of DisplayMemberPath and ItemStringFormat are desired.
		//	// Set ItemTemplateSelector to an appropriate object, provided that
		//	// this doesn't conflict with the user's own setting.
		//	DataTemplateSelector itemTemplateSelector = ItemTemplateSelector;
		//	if (itemTemplateSelector != null && !(itemTemplateSelector is DisplayMemberTemplateSelector))
		//	{
		//		// if ITS was actually set to something besides a DisplayMember selector,
		//		// it's an error to overwrite it with a DisplayMember selector
		//		// unless ITS came from a style and DMP is local
		//		if (ReadLocalValue(ItemTemplateSelectorProperty) != DependencyProperty.UnsetValue ||
		//			ReadLocalValue(DisplayMemberPathProperty) == DependencyProperty.UnsetValue)
		//		{
		//			throw new InvalidOperationException(SR.DisplayMemberPathAndItemTemplateSelectorDefined);
		//		}
		//	}

		//	// now set the ItemTemplateSelector to use the new DisplayMemberPath and ItemStringFormat
		//	ItemTemplateSelector = new DisplayMemberTemplateSelector(DisplayMemberPath, ItemStringFormat);
		//}
		//else
		//{
		//	// Neither property is desired.  Clear the ItemTemplateSelector if
		//	// we had set it earlier.
		//	if (ItemTemplateSelector is DisplayMemberTemplateSelector)
		//	{
		//		ClearValue(ItemTemplateSelectorProperty);
		//	}
		//}
	}

	/// <summary>
	///     This method is invoked when the DisplayMemberPath property changes.
	/// </summary>
	/// <param name="oldDisplayMemberPath">The old value of the DisplayMemberPath property.</param>
	/// <param name="newDisplayMemberPath">The new value of the DisplayMemberPath property.</param>
	protected virtual void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
	{
	}

	/// <summary>
	///     The DependencyProperty for the ItemTemplate property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemTemplateProperty =
			DependencyProperty.Register(
					"ItemTemplate",
					typeof(DataTemplate),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(DataTemplate)null,
							new PropertyChangedCallback(OnItemTemplateChanged)));

	/// <summary>
	///     ItemTemplate is the template used to display each item.
	/// </summary>
	public DataTemplate ItemTemplate
	{
		get { return (DataTemplate)GetValue(ItemTemplateProperty); }
		set { SetValue(ItemTemplateProperty, value); }
	}

	/// <summary>
	///     Called when ItemTemplateProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnItemTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemTemplate property changes.
	/// </summary>
	/// <param name="oldItemTemplate">The old value of the ItemTemplate property.</param>
	/// <param name="newItemTemplate">The new value of the ItemTemplate property.</param>
	protected virtual void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
	{
		CheckTemplateSource();

		_itemContainerGenerator?.Refresh();
	}


	/// <summary>
	///     The DependencyProperty for the ItemTemplateSelector property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemTemplateSelectorProperty =
			DependencyProperty.Register(
					"ItemTemplateSelector",
					typeof(DataTemplateSelector),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(DataTemplateSelector)null,
							new PropertyChangedCallback(OnItemTemplateSelectorChanged)));

	/// <summary>
	///     ItemTemplateSelector allows the application writer to provide custom logic
	///     for choosing the template used to display each item.
	/// </summary>
	/// <remarks>
	///     This property is ignored if <seealso cref="ItemTemplate"/> is set.
	/// </remarks>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataTemplateSelector ItemTemplateSelector
	{
		get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
		set { SetValue(ItemTemplateSelectorProperty, value); }
	}

	/// <summary>
	///     Called when ItemTemplateSelectorProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnItemTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnItemTemplateSelectorChanged((DataTemplateSelector)e.OldValue, (DataTemplateSelector)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemTemplateSelector property changes.
	/// </summary>
	/// <param name="oldItemTemplateSelector">The old value of the ItemTemplateSelector property.</param>
	/// <param name="newItemTemplateSelector">The new value of the ItemTemplateSelector property.</param>
	protected virtual void OnItemTemplateSelectorChanged(DataTemplateSelector oldItemTemplateSelector, DataTemplateSelector newItemTemplateSelector)
	{
		CheckTemplateSource();

		//if ((_itemContainerGenerator != null) && (ItemTemplate == null))
		//{
		//	_itemContainerGenerator.Refresh();
		//}
	}

	/// <summary>
	///     The DependencyProperty for the ItemStringFormat property.
	///     Flags:              None
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemStringFormatProperty =
			DependencyProperty.Register(
					"ItemStringFormat",
					typeof(String),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(String)null,
						  new PropertyChangedCallback(OnItemStringFormatChanged)));


	/// <summary>
	///     ItemStringFormat is the format used to display an item (or a
	///     property of an item, as declared by DisplayMemberPath) as a string.
	///     This arises only when no template is available.
	/// </summary>
	public String ItemStringFormat
	{
		get { return (String)GetValue(ItemStringFormatProperty); }
		set { SetValue(ItemStringFormatProperty, value); }
	}

	/// <summary>
	///     Called when ItemStringFormatProperty is invalidated on "d."
	/// </summary>
	private static void OnItemStringFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		BnsCustomSourceBaseWidget ctrl = (BnsCustomSourceBaseWidget)d;

		ctrl.OnItemStringFormatChanged((String)e.OldValue, (String)e.NewValue);
		ctrl.UpdateDisplayMemberTemplateSelector();
	}

	/// <summary>
	///     This method is invoked when the ItemStringFormat property changes.
	/// </summary>
	/// <param name="oldItemStringFormat">The old value of the ItemStringFormat property.</param>
	/// <param name="newItemStringFormat">The new value of the ItemStringFormat property.</param>
	protected virtual void OnItemStringFormatChanged(String oldItemStringFormat, String newItemStringFormat)
	{
	}


	/// <summary>
	///     The DependencyProperty for the ItemBindingGroup property.
	///     Flags:              None
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemBindingGroupProperty =
			DependencyProperty.Register(
					"ItemBindingGroup",
					typeof(BindingGroup),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(BindingGroup)null,
						  new PropertyChangedCallback(OnItemBindingGroupChanged)));


	/// <summary>
	///     ItemBindingGroup declares a BindingGroup to be used as a "master"
	///     for the generated containers.  Each container's BindingGroup is set
	///     to a copy of the master, sharing the same set of validation rules,
	///     but managing its own collection of bindings.
	/// </summary>
	public BindingGroup ItemBindingGroup
	{
		get { return (BindingGroup)GetValue(ItemBindingGroupProperty); }
		set { SetValue(ItemBindingGroupProperty, value); }
	}

	/// <summary>
	///     Called when ItemBindingGroupProperty is invalidated on "d."
	/// </summary>
	private static void OnItemBindingGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		BnsCustomSourceBaseWidget ctrl = (BnsCustomSourceBaseWidget)d;

		ctrl.OnItemBindingGroupChanged((BindingGroup)e.OldValue, (BindingGroup)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemBindingGroup property changes.
	/// </summary>
	/// <param name="oldItemBindingGroup">The old value of the ItemBindingGroup property.</param>
	/// <param name="newItemBindingGroup">The new value of the ItemBindingGroup property.</param>
	protected virtual void OnItemBindingGroupChanged(BindingGroup oldItemBindingGroup, BindingGroup newItemBindingGroup)
	{
	}


	/// <summary>
	/// Throw if more than one of DisplayMemberPath, xxxTemplate and xxxTemplateSelector
	/// properties are set on the given element.
	/// </summary>
	private void CheckTemplateSource()
	{
		if (string.IsNullOrEmpty(DisplayMemberPath))
		{
			//Helper.CheckTemplateAndTemplateSelector("Item", ItemTemplateProperty, ItemTemplateSelectorProperty, this);
		}
		else
		{
			//if (!(this.ItemTemplateSelector is DisplayMemberTemplateSelector))
			//{
			//	throw new InvalidOperationException(SR.ItemTemplateSelectorBreaksDisplayMemberPath);
			//}
			//if (Helper.IsTemplateDefined(ItemTemplateProperty, this))
			//{
			//	throw new InvalidOperationException(SR.DisplayMemberPathAndItemTemplateDefined);
			//}
		}
	}

	/// <summary>
	///     The DependencyProperty for the ItemContainerStyle property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemContainerStyleProperty =
			DependencyProperty.Register(
					"ItemContainerStyle",
					typeof(Style),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(Style)null,
							new PropertyChangedCallback(OnItemContainerStyleChanged)));

	/// <summary>
	///     ItemContainerStyle is the style that is applied to the container element generated
	///     for each item.
	/// </summary>
	[Bindable(true), Category("Content")]
	public Style ItemContainerStyle
	{
		get { return (Style)GetValue(ItemContainerStyleProperty); }
		set { SetValue(ItemContainerStyleProperty, value); }
	}

	/// <summary>
	///     Called when ItemContainerStyleProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnItemContainerStyleChanged((Style)e.OldValue, (Style)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemContainerStyle property changes.
	/// </summary>
	/// <param name="oldItemContainerStyle">The old value of the ItemContainerStyle property.</param>
	/// <param name="newItemContainerStyle">The new value of the ItemContainerStyle property.</param>
	protected virtual void OnItemContainerStyleChanged(Style oldItemContainerStyle, Style newItemContainerStyle)
	{
		//Helper.CheckStyleAndStyleSelector("ItemContainer", ItemContainerStyleProperty, ItemContainerStyleSelectorProperty, this);

		//if (_itemContainerGenerator != null)
		//{
		//	_itemContainerGenerator.Refresh();
		//}
	}


	/// <summary>
	///     The DependencyProperty for the ItemContainerStyleSelector property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemContainerStyleSelectorProperty =
			DependencyProperty.Register(
					"ItemContainerStyleSelector",
					typeof(StyleSelector),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(StyleSelector)null,
							new PropertyChangedCallback(OnItemContainerStyleSelectorChanged)));

	/// <summary>
	///     ItemContainerStyleSelector allows the application writer to provide custom logic
	///     to choose the style to apply to each generated container element.
	/// </summary>
	/// <remarks>
	///     This property is ignored if <seealso cref="ItemContainerStyle"/> is set.
	/// </remarks>
	[Bindable(true), Category("Content")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public StyleSelector ItemContainerStyleSelector
	{
		get { return (StyleSelector)GetValue(ItemContainerStyleSelectorProperty); }
		set { SetValue(ItemContainerStyleSelectorProperty, value); }
	}

	/// <summary>
	///     Called when ItemContainerStyleSelectorProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnItemContainerStyleSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnItemContainerStyleSelectorChanged((StyleSelector)e.OldValue, (StyleSelector)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemContainerStyleSelector property changes.
	/// </summary>
	/// <param name="oldItemContainerStyleSelector">The old value of the ItemContainerStyleSelector property.</param>
	/// <param name="newItemContainerStyleSelector">The new value of the ItemContainerStyleSelector property.</param>
	protected virtual void OnItemContainerStyleSelectorChanged(StyleSelector oldItemContainerStyleSelector, StyleSelector newItemContainerStyleSelector)
	{
		//Helper.CheckStyleAndStyleSelector("ItemContainer", ItemContainerStyleProperty, ItemContainerStyleSelectorProperty, this);

		//if ((_itemContainerGenerator != null) && (ItemContainerStyle == null))
		//{
		//	_itemContainerGenerator.Refresh();
		//}
	}

	/// <summary>
	///     Returns the BnsCustomSourceBaseWidgetfor which element is an ItemsHost.
	///     More precisely, if element is marked by setting IsItemsHost="true"
	///     in the style for an BnsCustomSourceBaseWidget, or if element is a panel created
	///     by the ItemsPresenter for an BnsCustomSourceBaseWidget, return that BnsCustomSourceBaseWidget.
	///     Otherwise, return null.
	/// </summary>
	public static BnsCustomSourceBaseWidget GetItemsOwner(DependencyObject element)
	{
		BnsCustomSourceBaseWidget container = null;
		Panel panel = element as Panel;

		//if (panel != null && panel.IsItemsHost)
		//{
		//	// see if element was generated for an ItemsPresenter
		//	ItemsPresenter ip = ItemsPresenter.FromPanel(panel);

		//	if (ip != null)
		//	{
		//		// if so use the element whose style begat the ItemsPresenter
		//		container = ip.Owner;
		//	}
		//	else
		//	{
		//		// otherwise use element's templated parent
		//		container = panel.TemplatedParent as BnsCustomSourceBaseWidget;
		//	}
		//}

		return container;
	}

	internal static DependencyObject GetItemsOwnerInternal(DependencyObject element)
	{
		BnsCustomSourceBaseWidget temp;
		return GetItemsOwnerInternal(element, out temp);
	}

	/// <summary>
	/// Different from public GetItemsOwner
	/// Returns ip.TemplatedParent instead of ip.Owner
	/// More accurate when we want to distinguish if owner is a GroupItem or BnsCustomSourceBaseWidget
	/// </summary>
	/// <param name="element"></param>
	/// <returns></returns>
	internal static DependencyObject GetItemsOwnerInternal(DependencyObject element, out BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget)
	{
		DependencyObject container = null;
		Panel panel = element as Panel;
		BnsCustomSourceBaseWidget = null;

		//if (panel != null && panel.IsItemsHost)
		//{
		//	// see if element was generated for an ItemsPresenter
		//	ItemsPresenter ip = ItemsPresenter.FromPanel(panel);

		//	if (ip != null)
		//	{
		//		// if so use the element whose style begat the ItemsPresenter
		//		container = ip.TemplatedParent;
		//		BnsCustomSourceBaseWidget= ip.Owner;
		//	}
		//	else
		//	{
		//		// otherwise use element's templated parent
		//		container = panel.TemplatedParent;
		//		BnsCustomSourceBaseWidget= container as BnsCustomSourceBaseWidget;
		//	}
		//}

		return container;
	}

	/// <summary>
	///     The DependencyProperty for the ItemsPanel property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemsPanelProperty
		= DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(BnsCustomSourceBaseWidget),
			   new FrameworkPropertyMetadata(GetDefaultItemsPanelTemplate(),
				   new PropertyChangedCallback(OnItemsPanelChanged)));

	private static ItemsPanelTemplate GetDefaultItemsPanelTemplate()
	{
		ItemsPanelTemplate template = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(StackPanel)));
		template.Seal();
		return template;
	}

	/// <summary>
	///     ItemsPanel is the panel that controls the layout of items.
	///     (More precisely, the panel that controls layout is created
	///     from the template given by ItemsPanel.)
	/// </summary>
	[Bindable(false)]
	public ItemsPanelTemplate ItemsPanel
	{
		get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
		set { SetValue(ItemsPanelProperty, value); }
	}

	/// <summary>
	///     Called when ItemsPanelProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnItemsPanelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnItemsPanelChanged((ItemsPanelTemplate)e.OldValue, (ItemsPanelTemplate)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemsPanel property changes.
	/// </summary>
	/// <param name="oldItemsPanel">The old value of the ItemsPanel property.</param>
	/// <param name="newItemsPanel">The new value of the ItemsPanel property.</param>
	protected virtual void OnItemsPanelChanged(ItemsPanelTemplate oldItemsPanel, ItemsPanelTemplate newItemsPanel)
	{
		//ItemContainerGenerator.OnPanelChanged();
	}


	private static readonly DependencyPropertyKey IsGroupingPropertyKey =
		DependencyProperty.RegisterReadOnly("IsGrouping", typeof(bool), typeof(BnsCustomSourceBaseWidget), new FrameworkPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(OnIsGroupingChanged)));

	/// <summary>
	///     The DependencyProperty for the IsGrouping property.
	/// </summary>
	public static readonly DependencyProperty IsGroupingProperty = IsGroupingPropertyKey.DependencyProperty;

	/// <summary>
	///     Returns whether the control is using grouping.
	/// </summary>
	[Bindable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IsGrouping
	{
		get
		{
			return (bool)GetValue(IsGroupingProperty);
		}
	}

	private static void OnIsGroupingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnIsGroupingChanged(e);
	}

	internal virtual void OnIsGroupingChanged(DependencyPropertyChangedEventArgs e)
	{
		//ShouldCoerceScrollUnitField.SetValue(this, true);
		//CoerceValue(VirtualizingStackPanel.ScrollUnitProperty);

		//ShouldCoerceCacheSizeField.SetValue(this, true);
		//CoerceValue(VirtualizingPanel.CacheLengthUnitProperty);

		//((IContainItemStorage)this).Clear();
	}

	/// <summary>
	/// The collection of GroupStyle objects that describes the display of
	/// each level of grouping.  The entry at index 0 describes the top level
	/// groups, the entry at index 1 describes the next level, and so forth.
	/// If there are more levels of grouping than entries in the collection,
	/// the last entry is used for the extra levels.
	/// </summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ObservableCollection<GroupStyle> GroupStyle
	{
		get { return _groupStyle; }
	}

	/// <summary>
	/// This method is used by TypeDescriptor to determine if this property should
	/// be serialized.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ShouldSerializeGroupStyle()
	{
		return (GroupStyle.Count > 0);
	}

	private void OnGroupStyleChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (_itemContainerGenerator != null)
		{
			//_itemContainerGenerator.Refresh();
		}
	}


	/// <summary>
	///     The DependencyProperty for the GroupStyleSelector property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty GroupStyleSelectorProperty
		= DependencyProperty.Register("GroupStyleSelector", typeof(GroupStyleSelector), typeof(BnsCustomSourceBaseWidget),
									  new FrameworkPropertyMetadata((GroupStyleSelector)null,
																	new PropertyChangedCallback(OnGroupStyleSelectorChanged)));

	/// <summary>
	///     GroupStyleSelector allows the app writer to provide custom selection logic
	///     for a GroupStyle to apply to each group collection.
	/// </summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public GroupStyleSelector GroupStyleSelector
	{
		get { return (GroupStyleSelector)GetValue(GroupStyleSelectorProperty); }
		set { SetValue(GroupStyleSelectorProperty, value); }
	}

	/// <summary>
	///     Called when GroupStyleSelectorProperty is invalidated on "d."
	/// </summary>
	/// <param name="d">The object on which the property was invalidated.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnGroupStyleSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((BnsCustomSourceBaseWidget)d).OnGroupStyleSelectorChanged((GroupStyleSelector)e.OldValue, (GroupStyleSelector)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the GroupStyleSelector property changes.
	/// </summary>
	/// <param name="oldGroupStyleSelector">The old value of the GroupStyleSelector property.</param>
	/// <param name="newGroupStyleSelector">The new value of the GroupStyleSelector property.</param>
	protected virtual void OnGroupStyleSelectorChanged(GroupStyleSelector oldGroupStyleSelector, GroupStyleSelector newGroupStyleSelector)
	{
		if (_itemContainerGenerator != null)
		{
			//_itemContainerGenerator.Refresh();
		}
	}

	/// <summary>
	///     The DependencyProperty for the AlternationCount property.
	///     Flags:              none
	///     Default Value:      0
	/// </summary>
	public static readonly DependencyProperty AlternationCountProperty =
			DependencyProperty.Register(
					"AlternationCount",
					typeof(int),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(
							(int)0,
							new PropertyChangedCallback(OnAlternationCountChanged)));

	/// <summary>
	///     AlternationCount controls the range of values assigned to the
	///     AlternationIndex property attached to each generated container.  The
	///     default value 0 means "do not set AlternationIndex".  A positive
	///     value means "assign AlternationIndex in the range [0, AlternationCount)
	///     so that adjacent containers receive different values".
	/// </summary>
	/// <remarks>
	///     By referring to AlternationIndex in a trigger or binding (typically
	///     in the ItemContainerStyle), you can make the appearance of items
	///     depend on their position in the display.  For example, you can make
	///     the background color of the items in ListBox alternate between
	///     blue and white.
	/// </remarks>
	public int AlternationCount
	{
		get { return (int)GetValue(AlternationCountProperty); }
		set { SetValue(AlternationCountProperty, value); }
	}

	/// <summary>
	///     Called when AlternationCountProperty is invalidated on "d."
	/// </summary>
	private static void OnAlternationCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		BnsCustomSourceBaseWidget ctrl = (BnsCustomSourceBaseWidget)d;

		int oldAlternationCount = (int)e.OldValue;
		int newAlternationCount = (int)e.NewValue;

		ctrl.OnAlternationCountChanged(oldAlternationCount, newAlternationCount);
	}

	/// <summary>
	///     This method is invoked when the AlternationCount property changes.
	/// </summary>
	/// <param name="oldAlternationCount">The old value of the AlternationCount property.</param>
	/// <param name="newAlternationCount">The new value of the AlternationCount property.</param>
	protected virtual void OnAlternationCountChanged(int oldAlternationCount, int newAlternationCount)
	{
		//ItemContainerGenerator.ChangeAlternationCount();
	}

	private static readonly DependencyPropertyKey AlternationIndexPropertyKey =
				DependencyProperty.RegisterAttachedReadOnly(
							"AlternationIndex",
							typeof(int),
							typeof(BnsCustomSourceBaseWidget),
							new FrameworkPropertyMetadata((int)0));

	/// <summary>
	/// AlternationIndex is set on containers generated for an BnsCustomSourceBaseWidget, when
	/// the BnsCustomSourceBaseWidget's AlternationCount property is positive.  The AlternationIndex
	/// lies in the range [0, AlternationCount), and adjacent containers always get
	/// assigned different values.
	/// </summary>
	public static readonly DependencyProperty AlternationIndexProperty =
				AlternationIndexPropertyKey.DependencyProperty;

	/// <summary>
	/// Static getter for the AlternationIndex attached property.
	/// </summary>
	public static int GetAlternationIndex(DependencyObject element)
	{
		ArgumentNullException.ThrowIfNull(element);

		return (int)element.GetValue(AlternationIndexProperty);
	}

	// internal setter for AlternationIndex.  This property is not settable by
	// an app, only by internal code
	internal static void SetAlternationIndex(DependencyObject d, int value)
	{
		d.SetValue(AlternationIndexPropertyKey, value);
	}

	// internal clearer for AlternationIndex.  This property is not settable by
	// an app, only by internal code
	internal static void ClearAlternationIndex(DependencyObject d)
	{
		d.ClearValue(AlternationIndexPropertyKey);
	}

	/// <summary>
	///     The DependencyProperty for the IsTextSearchEnabled property.
	///     Default Value:      false
	/// </summary>
	public static readonly DependencyProperty IsTextSearchEnabledProperty =
			DependencyProperty.Register(
					"IsTextSearchEnabled",
					typeof(bool),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));

	/// <summary>
	///     Whether TextSearch is enabled or not on this BnsCustomSourceBaseWidget
	/// </summary>
	public bool IsTextSearchEnabled
	{
		get { return (bool)GetValue(IsTextSearchEnabledProperty); }
		set { SetValue(IsTextSearchEnabledProperty, BooleanBoxes.Box(value)); }
	}

	/// <summary>
	///     The DependencyProperty for the IsTextSearchCaseSensitive property.
	///     Default Value:      false
	/// </summary>
	public static readonly DependencyProperty IsTextSearchCaseSensitiveProperty =
			DependencyProperty.Register(
					"IsTextSearchCaseSensitive",
					typeof(bool),
					typeof(BnsCustomSourceBaseWidget),
					new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));

	/// <summary>
	///     Whether TextSearch is case sensitive or not on this BnsCustomSourceBaseWidget
	/// </summary>
	public bool IsTextSearchCaseSensitive
	{
		get { return (bool)GetValue(IsTextSearchCaseSensitiveProperty); }
		set { SetValue(IsTextSearchCaseSensitiveProperty, BooleanBoxes.Box(value)); }
	}

	#endregion

	#region Mapping methods

	///<summary>
	/// Return the BnsCustomSourceBaseWidgetthat owns the given container element
	///</summary>
	public static BnsCustomSourceBaseWidget BnsCustomSourceBaseWidgetFromItemContainer(DependencyObject container)
	{
		UIElement ui = container as UIElement;
		if (ui == null)
			return null;

		// ui appeared in items collection
		var ic = LogicalTreeHelper.GetParent(ui) as BnsCustomSourceBaseWidget;
		if (ic != null)
		{
			// this is the right BnsCustomSourceBaseWidgetas long as the item
			// is (or is eligible to be) its own container
			//IGeneratorHost host = ic as IGeneratorHost;
			if (ic.IsItemItsOwnContainer(ui))
				return ic;
			else
				return null;
		}

		ui = VisualTreeHelper.GetParent(ui) as UIElement;

		return BnsCustomSourceBaseWidget.GetItemsOwner(ui);
	}

	///<summary>
	/// Return the container that owns the given element.  If BnsCustomSourceBaseWidget
	/// is not null, return a container that belongs to the given BnsCustomSourceBaseWidget.
	/// If BnsCustomSourceBaseWidgetis null, return the closest container belonging to
	/// any BnsCustomSourceBaseWidget.  Return null if no such container exists.
	///</summary>
	public static DependencyObject ContainerFromElement(BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget, DependencyObject element)
	{
		ArgumentNullException.ThrowIfNull(element);

		//// if the element is itself the desired container, return it
		//if (IsContainerForBnsCustomSourceBaseWidget(element, BnsCustomSourceBaseWidget))
		//{
		//	return element;
		//}

		//// start the tree walk at the element's parent
		//FrameworkObject fo = new FrameworkObject(element);
		//fo.Reset(fo.GetPreferVisualParent(true).DO);

		//// walk up, stopping when we reach the desired container
		//while (fo.DO != null)
		//{
		//	if (IsContainerForBnsCustomSourceBaseWidget(fo.DO, BnsCustomSourceBaseWidget))
		//	{
		//		break;
		//	}

		//	fo.Reset(fo.PreferVisualParent.DO);
		//}

		//return fo.DO;


		return null;
	}

	///<summary>
	/// Return the container belonging to the current BnsCustomSourceBaseWidgetthat owns
	/// the given container element.  Return null if no such container exists.
	///</summary>
	public DependencyObject ContainerFromElement(DependencyObject element)
	{
		return ContainerFromElement(this, element);
	}

	// helper method used by ContainerFromElement
	private static bool IsContainerForBnsCustomSourceBaseWidget(DependencyObject element, BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget)
	{
		// is the element a container?
		//if (element.ContainsValue(ItemContainerGenerator.ItemForItemContainerProperty))
		//{
		//	// does the element belong to the BnsCustomSourceBaseWidget?
		//	if (BnsCustomSourceBaseWidget== null || BnsCustomSourceBaseWidget== BnsCustomSourceBaseWidgetFromItemContainer(element))
		//	{
		//		return true;
		//	}
		//}

		return false;
	}

	#endregion Mapping methods

	#region IGeneratorHost

	//------------------------------------------------------
	//
	//  Interface - IGeneratorHost
	//
	//------------------------------------------------------

	/// <summary>
	/// The view of the data
	/// </summary>
	ItemCollection IGeneratorHost.View
	{
		get { return Items; }
	}

	/// <summary>
	/// Return true if the item is (or is eligible to be) its own ItemContainer
	/// </summary>
	bool IGeneratorHost.IsItemItsOwnContainer(object item)
	{
		return IsItemItsOwnContainer(item);
	}

	/// <summary>
	/// Return the element used to display the given item
	/// </summary>
	DependencyObject IGeneratorHost.GetContainerForItem(object item)
	{
		DependencyObject container;

		// use the item directly, if possible (bug 870672)
		if (IsItemItsOwnContainerOverride(item))
			container = item as DependencyObject;
		else
			container = GetContainerForItemOverride();

		return container;
	}

	/// <summary>
	/// Prepare the element to act as the ItemContainer for the corresponding item.
	/// </summary>
	void IGeneratorHost.PrepareItemContainer(DependencyObject container, object item)
	{
		if (ShouldApplyItemContainerStyle(container, item))
		{
			// apply the ItemContainer style (if any)
			ApplyItemContainerStyle(container, item);
		}

		// forward ItemTemplate, et al.
		PrepareContainerForItemOverride(container, item);

		//// set up the binding group
		//if (!Helper.HasUnmodifiedDefaultValue(this, ItemBindingGroupProperty) &&
		//	Helper.HasUnmodifiedDefaultOrInheritedValue(container, FrameworkElement.BindingGroupProperty))
		//{
		//	BindingGroup itemBindingGroup = ItemBindingGroup;
		//	BindingGroup containerBindingGroup =
		//		(itemBindingGroup != null) ? new BindingGroup(itemBindingGroup)
		//									: null;
		//	container.SetValue(FrameworkElement.BindingGroupProperty, containerBindingGroup);
		//}

		//if (container == item && TraceData.IsEnabled)
		//{
		//	// issue a message if there's an ItemTemplate(Selector) for "direct" items
		//	// The ItemTemplate isn't used, which may confuse the user (bug 991101).
		//	if (ItemTemplate != null || ItemTemplateSelector != null)
		//	{
		//		TraceData.TraceAndNotify(TraceEventType.Error, TraceData.ItemTemplateForDirectItem, null,
		//			traceParameters: new object[] { AvTrace.TypeName(item) });
		//	}
		//}
	}

	/// <summary>
	/// Undo any initialization done on the element during GetContainerForItem and PrepareItemContainer
	/// </summary>
	void IGeneratorHost.ClearContainerForItem(DependencyObject container, object item)
	{
		// This method no longer does most of the work it used to (bug 1445288).
		// It is called when a container is removed from the tree;  such a
		// container will be GC'd soon, so there's no point in changing
		// its properties.
		//
		// We still call the override method, to give subclasses a chance
		// to clean up anything they may have done during Prepare (bug 1561206).

		//GroupItem groupItem = container as GroupItem;
		//if (groupItem == null)
		//{
		//	ClearContainerForItemOverride(container, item);

		//	TreeViewItem treeViewItem = container as TreeViewItem;
		//	if (treeViewItem != null)
		//	{
		//		treeViewItem.ClearItemContainer(item, this);
		//	}
		//}
		//else
		//{
		//	// GroupItems are special - their information comes from a different place
		//	// Recursively clear the sub-generators, so that ClearContainerForItemOverride
		//	// is called on the bottom-level containers.
		//	groupItem.ClearItemContainer(item, this);
		//}
	}


	/// <summary>
	/// Determine if the given element was generated for this host as an ItemContainer.
	/// </summary>
	bool IGeneratorHost.IsHostForItemContainer(DependencyObject container)
	{
		// If ItemsControlFromItemContainer can determine who owns the element,
		// use its decision.
		var ic = BnsCustomSourceBaseWidgetFromItemContainer(container);
		if (ic != null)
			return (ic == this);

		// If the element is in my items view, and if it can be its own ItemContainer,
		// it's mine.  Contains may be expensive, so we avoid calling it in cases
		// where we already know the answer - namely when the element has a
		// logical parent (ItemsControlFromItemContainer handles this case).  This
		// leaves only those cases where the element belongs to my items
		// without having a logical parent (e.g. via ItemsSource) and without
		// having been generated yet. HasItem indicates if anything has been generated.
		DependencyObject parent = LogicalTreeHelper.GetParent(container);
		if (parent == null)
		{
			return IsItemItsOwnContainerOverride(container) &&
				HasItems && Items.Contains(container);
		}

		// Otherwise it's not mine
		return false;
	}

	/// <summary>
	/// Return the GroupStyle (if any) to use for the given group at the given level.
	/// </summary>
	GroupStyle IGeneratorHost.GetGroupStyle(CollectionViewGroup group, int level)
	{
		GroupStyle result = null;

		// a. Use global selector
		if (GroupStyleSelector != null)
		{
			result = GroupStyleSelector(group, level);
		}

		// b. lookup in GroupStyle list
		if (result == null)
		{
			// use last entry for all higher levels
			if (level >= GroupStyle.Count)
			{
				level = GroupStyle.Count - 1;
			}

			if (level >= 0)
			{
				result = GroupStyle[level];
			}
		}

		return result;
	}

	/// <summary>
	/// Communicates to the host that the generator is using grouping.
	/// </summary>
	void IGeneratorHost.SetIsGrouping(bool isGrouping)
	{
		SetValue(IsGroupingPropertyKey, BooleanBoxes.Box(isGrouping));
	}

	/// <summary>
	/// The AlternationCount
	/// <summary>
	int IGeneratorHost.AlternationCount { get { return AlternationCount; } }

	#endregion IGeneratorHost

	#region ISupportInitialize

	/// <summary>
	///     Initialization of this element is about to begin
	/// </summary>
	public override void BeginInit()
	{
		base.BeginInit();

		if (_items != null)
		{
			_items.BeginInit();
		}
	}

	/// <summary>
	///     Initialization of this element has completed
	/// </summary>
	public override void EndInit()
	{
		if (IsInitPending)
		{
			if (_items != null)
			{
				_items.EndInit();
			}

			base.EndInit();
		}
	}

	private bool IsInitPending { get; set; }

	#endregion

	#region Protected Methods

	/// <summary>
	/// Return true if the item is (or should be) its own item container
	/// </summary>
	public bool IsItemItsOwnContainer(object item)
	{
		return IsItemItsOwnContainerOverride(item);
	}

	/// <summary>
	/// Return true if the item is (or should be) its own item container
	/// </summary>
	protected virtual bool IsItemItsOwnContainerOverride(object item)
	{
		return (item is UIElement);
	}

	/// <summary> Create or identify the element used to display the given item. </summary>
	protected virtual DependencyObject GetContainerForItemOverride()
	{
		return new ContentPresenter();
	}

	/// <summary>
	/// Prepare the element to display the item.  This may involve
	/// applying styles, setting bindings, etc.
	/// </summary>
	protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
	{
		// Each type of "ItemContainer" element may require its own initialization.
		// We use explicit polymorphism via internal methods for this.
		//
		// Another way would be to define an interface IGeneratedItemContainer with
		// corresponding virtual "core" methods.  Base classes (ContentControl,
		// BnsCustomSourceBaseWidget, ContentPresenter) would implement the interface
		// and forward the work to subclasses via the "core" methods.
		//
		// While this is better from an OO point of view, and extends to
		// 3rd-party elements used as containers, it exposes more public API.
		// Management considers this undesirable, hence the following rather
		// inelegant code.

		if (element is BnsCustomSourceBaseWidget sw)
		{
			if (sw != this)
			{
				sw.PrepareBnsCustomSourceBaseWidget(item, this);
			}
		}
	}

	/// <summary>
	/// Undo the effects of PrepareContainerForItemOverride.
	/// </summary>
	protected virtual void ClearContainerForItemOverride(DependencyObject element, object item)
	{
		if (element is BnsCustomSourceBaseWidget sw)
		{
			if (sw != this)
			{
				sw.ClearBnsCustomSourceBaseWidget(item);
			}
		}
	}

	/// <summary>
	///     Called when a TextInput event is received.
	/// </summary>
	/// <param name="e"></param>
	protected override void OnTextInput(TextCompositionEventArgs e)
	{
		base.OnTextInput(e);

		//// Only handle text from ourselves or an item container
		//if (!string.IsNullOrEmpty(e.Text) && IsTextSearchEnabled &&
		//	(e.OriginalSource == this || BnsCustomSourceBaseWidgetFromItemContainer(e.OriginalSource as DependencyObject) == this))
		//{
		//	TextSearch instance = TextSearch.EnsureInstance(this);

		//	if (instance != null)
		//	{
		//		instance.DoSearch(e.Text);
		//		// Note: we always want to handle the event to denote that we
		//		// actually did something.  We wouldn't want an AccessKey
		//		// to get invoked just because there wasn't a match here.
		//		e.Handled = true;
		//	}
		//}
	}

	/// <summary>
	///     Called when a KeyDown event is received.
	/// </summary>
	/// <param name="e"></param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		//if (IsTextSearchEnabled)
		//{
		//	// If the pressed the backspace key, delete the last character
		//	// in the TextSearch current prefix.
		//	if (e.Key == Key.Back)
		//	{
		//		TextSearch instance = TextSearch.EnsureInstance(this);

		//		if (instance != null)
		//		{
		//			instance.DeleteLastCharacter();
		//		}
		//	}
		//}
	}

	/// <summary>
	/// Determine whether the ItemContainerStyle/StyleSelector should apply to the container
	/// </summary>
	/// <returns>true if the ItemContainerStyle should apply to the item</returns>
	protected virtual bool ShouldApplyItemContainerStyle(DependencyObject container, object item)
	{
		return true;
	}

	protected override AutomationPeer OnCreateAutomationPeer() => new SourceWidgetWrapperAutomationPeer(this);

	#endregion

	#region Internal Methods

	/// <summary>
	/// Prepare to display the item.
	/// </summary>
	internal void PrepareBnsCustomSourceBaseWidget(object item, BnsCustomSourceBaseWidget parent)
	{
		if (item != this)
		{
			// copy templates and styles from parent BnsCustomSourceBaseWidget
			DataTemplate itemTemplate = parent.ItemTemplate;
			DataTemplateSelector itemTemplateSelector = parent.ItemTemplateSelector;
			string itemStringFormat = parent.ItemStringFormat;
			Style itemContainerStyle = parent.ItemContainerStyle;
			StyleSelector itemContainerStyleSelector = parent.ItemContainerStyleSelector;
			int alternationCount = parent.AlternationCount;
			BindingGroup itemBindingGroup = parent.ItemBindingGroup;

			if (itemTemplate != null)
			{
				SetValue(ItemTemplateProperty, itemTemplate);
			}
			if (itemTemplateSelector != null)
			{
				SetValue(ItemTemplateSelectorProperty, itemTemplateSelector);
			}
			//if (itemStringFormat != null &&
			//	Helper.HasDefaultValue(this, ItemStringFormatProperty))
			//{
			//	SetValue(ItemStringFormatProperty, itemStringFormat);
			//}
			//if (itemContainerStyle != null &&
			//	Helper.HasDefaultValue(this, ItemContainerStyleProperty))
			//{
			//	SetValue(ItemContainerStyleProperty, itemContainerStyle);
			//}
			//if (itemContainerStyleSelector != null &&
			//	Helper.HasDefaultValue(this, ItemContainerStyleSelectorProperty))
			//{
			//	SetValue(ItemContainerStyleSelectorProperty, itemContainerStyleSelector);
			//}
			//if (alternationCount != 0 &&
			//	Helper.HasDefaultValue(this, AlternationCountProperty))
			//{
			//	SetValue(AlternationCountProperty, alternationCount);
			//}
			//if (itemBindingGroup != null &&
			//	Helper.HasDefaultValue(this, ItemBindingGroupProperty))
			//{
			//	SetValue(ItemBindingGroupProperty, itemBindingGroup);
			//}
		}
	}

	/// <summary>
	/// Undo the effect of PrepareBnsCustomSourceBaseWidget.
	/// </summary>
	internal void ClearBnsCustomSourceBaseWidget(object item)
	{
		if (item != this)
		{
			// nothing to do
		}
	}

	/// <summary>
	/// Bringing the item passed as arg into view. If item is virtualized it will become realized.
	/// </summary>
	/// <param name="arg"></param>
	/// <returns></returns>
	internal object OnBringItemIntoView(object arg)
	{
		ItemInfo info = arg as ItemInfo;
		if (info == null)
		{
			info = NewItemInfo(arg);
		}

		return OnBringItemIntoView(info);
	}

	internal object OnBringItemIntoView(ItemInfo info)
	{
		//FrameworkElement element = info.Container as FrameworkElement;
		//if (element != null)
		//{
		//	element.BringIntoView();
		//}
		//else if ((info = LeaseItemInfo(info, true)).Index >= 0)
		//{
		//	// We might be virtualized, try to de-virtualize the item.
		//	// Note: There is opportunity here to make a public OM.
		//	//
		//	// Call UpdateLayout first, in case there is a pending Measure
		//	// that replaces the ItemsHost with a different panel.   We should
		//	// forward the request to the correct panel, of course.
		//	if (!FrameworkCompatibilityPreferences.GetVSP45Compat())
		//	{
		//		UpdateLayout();
		//	}

		//	VirtualizingPanel itemsHost = ItemsHost as VirtualizingPanel;
		//	if (itemsHost != null)
		//	{
		//		itemsHost.BringIndexIntoView(info.Index);
		//	}
		//}

		return null;
	}



	internal Panel ItemsHost
	{
		get
		{
			return _itemsHost;
		}
		set { _itemsHost = value; }
	}

	#endregion


	#region Keyboard Navigation

	internal bool NavigateByLine(FocusNavigationDirection direction, ItemNavigateArgs itemNavigateArgs)
	{
		DependencyObject startingElement = Keyboard.FocusedElement as DependencyObject;
		//if (!FrameworkAppContextSwitches.KeyboardNavigationFromHyperlinkInBnsCustomSourceBaseWidgetIsNotRelativeToFocusedElement)
		//{
		//	while (startingElement != null && !(startingElement is FrameworkElement))
		//	{
		//		// if focus is on a non-FE (e.g. Hyperlink), start the navigation
		//		// from its nearest FE ancestor
		//		startingElement = KeyboardNavigation.GetParent(startingElement) as DependencyObject;
		//	}
		//}
		return NavigateByLine(FocusedInfo, startingElement as FrameworkElement, direction, itemNavigateArgs);
	}

	internal void PrepareNavigateByLine(ItemInfo startingInfo,
		FrameworkElement startingElement,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs,
		out FrameworkElement container)
	{
		container = null;
		if (ItemsHost == null)
		{
			return;
		}

		// If the focused container/item has been scrolled out of view and they want to
		// start navigating again, scroll it back into view.
		if (startingElement != null)
		{
			MakeVisible(startingElement, direction, false);
		}
		else
		{
			MakeVisible(startingInfo, direction, out startingElement);
		}

		object startingItem = (startingInfo != null) ? startingInfo.Item : null;

		// When we get here if startingItem is non-null, it must be on the visible page.
		NavigateByLineInternal(startingItem,
			direction,
			startingElement,
			itemNavigateArgs,
			false /*shouldFocus*/,
			out container);
	}

	internal bool NavigateByLine(ItemInfo startingInfo,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs)
	{
		return NavigateByLine(startingInfo, null, direction, itemNavigateArgs);
	}

	internal bool NavigateByLine(ItemInfo startingInfo,
		FrameworkElement startingElement,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs)
	{
		if (ItemsHost == null)
		{
			return false;
		}

		// If the focused container/item has been scrolled out of view and they want to
		// start navigating again, scroll it back into view.
		if (startingElement != null)
		{
			MakeVisible(startingElement, direction, false);
		}
		else
		{
			MakeVisible(startingInfo, direction, out startingElement);
		}

		object startingItem = (startingInfo != null) ? startingInfo.Item : null;

		// When we get here if startingItem is non-null, it must be on the visible page.
		FrameworkElement container;
		return NavigateByLineInternal(startingItem,
			direction,
			startingElement,
			itemNavigateArgs,
			true /*shouldFocus*/,
			out container);
	}

	private bool NavigateByLineInternal(object startingItem,
		FocusNavigationDirection direction,
		FrameworkElement startingElement,
		ItemNavigateArgs itemNavigateArgs,
		bool shouldFocus,
		out FrameworkElement container)
	{
		container = null;

		//
		// If there is no starting item, just navigate to the first item.
		//
		if (startingItem == null &&
			(startingElement == null || startingElement == this))
		{
			return NavigateToStartInternal(itemNavigateArgs, shouldFocus, out container);
		}
		else
		{
			FrameworkElement nextElement = null;

			//
			// If the container isn't there, it might have been degenerated or
			// it might have been scrolled out of view.  Either way, we
			// should start navigation from the ItemsHost b/c we know it
			// is visible.
			// The generator could have given us an element which isn't
			// actually visually connected.  In this case we should use
			// the ItemsHost as well.
			//
			if (startingElement == null || !ItemsHost.IsAncestorOf(startingElement))
			{
				//
				// Bug 991220 makes it so that we have to start from the ScrollHost.
				// If we try to start from the ItemsHost it will always skip the first item.
				//
				startingElement = ScrollHost;
			}
			else
			{
				// if the starting element is with in an element with contained or cycle scope
				// then let the default keyboard navigation logic kick in.
				DependencyObject startingParent = VisualTreeHelper.GetParent(startingElement);
				while (startingParent != null &&
					startingParent != ItemsHost)
				{
					KeyboardNavigationMode mode = KeyboardNavigation.GetDirectionalNavigation(startingParent);
					if (mode == KeyboardNavigationMode.Contained ||
						mode == KeyboardNavigationMode.Cycle)
					{
						return false;
					}
					startingParent = VisualTreeHelper.GetParent(startingParent);
				}
			}

			bool isHorizontal = false; // (ItemsHost != null && ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);
			bool treeViewNavigation = (this is TreeView);
			//nextElement = KeyboardNavigation.Current.PredictFocusedElement(startingElement,
			//	direction,
			//	treeViewNavigation) as FrameworkElement;

			if (ScrollHost != null)
			{
				bool didScroll = false;
				FrameworkElement viewport = GetViewportElement();
				VirtualizingPanel virtualizingPanel = ItemsHost as VirtualizingPanel;
				bool isCycle = KeyboardNavigation.GetDirectionalNavigation(this) == KeyboardNavigationMode.Cycle;

				while (true)
				{
					if (nextElement != null)
					{
						if (virtualizingPanel != null &&
							ScrollHost.CanContentScroll &&
							VirtualizingPanel.GetIsVirtualizing(this))
						{
							Rect currentRect;
							ElementViewportPosition elementPosition = GetElementViewportPosition(viewport,
								TryGetTreeViewItemHeader(nextElement) as FrameworkElement,
								direction,
								false /*fullyVisible*/,
								out currentRect);
							if (elementPosition == ElementViewportPosition.CompletelyInViewport ||
								elementPosition == ElementViewportPosition.PartiallyInViewport)
							{
								if (!isCycle)
								{
									break;
								}
								else
								{
									Rect startingRect;
									GetElementViewportPosition(viewport,
										startingElement,
										direction,
										false /*fullyVisible*/,
										out startingRect);
									bool isInDirection = IsInDirectionForLineNavigation(startingRect, currentRect, direction, isHorizontal);
									if (isInDirection)
									{
										// If the next element in cycle mode is in direction
										// then this is a valid candidate, If not then try
										// scrolling.
										break;
									}
								}
							}
						}
						else
						{
							break;
						}

						//
						// We are disregarding the previously predicted element because
						// it is outside the viewport extents of a VirtualizingPanel
						//
						nextElement = null;
					}

					double oldHorizontalOffset = ScrollHost.HorizontalOffset;
					double oldVerticalOffset = ScrollHost.VerticalOffset;

					switch (direction)
					{
						case FocusNavigationDirection.Down:
						{
							didScroll = true;
							if (isHorizontal)
							{
								ScrollHost.LineRight();
							}
							else
							{
								ScrollHost.LineDown();
							}
						}
						break;
						case FocusNavigationDirection.Up:
						{
							didScroll = true;
							if (isHorizontal)
							{
								ScrollHost.LineLeft();
							}
							else
							{
								ScrollHost.LineUp();
							}
						}
						break;
					}

					ScrollHost.UpdateLayout();

					// If offset does not change, or if offset goes out of range - exit the loop.
					// The out-of-range check is to defend against buggy implementations of
					// IScrollInfo;  WPF's implementations always leave the
					// offset within range.
					if ((DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) &&
						DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset))
						|| (direction == FocusNavigationDirection.Down &&
								(ScrollHost.VerticalOffset > ScrollHost.ExtentHeight ||
								 ScrollHost.HorizontalOffset > ScrollHost.ExtentWidth))
						|| (direction == FocusNavigationDirection.Up &&
								(ScrollHost.VerticalOffset < 0.0 ||
								 ScrollHost.HorizontalOffset < 0.0)))
					{
						if (isCycle)
						{
							if (direction == FocusNavigationDirection.Up)
							{
								// If scrollviewer cannot be scrolled any further,
								// then cycle and navigate to end.
								return NavigateToEndInternal(itemNavigateArgs, true, out container);
							}
							else if (direction == FocusNavigationDirection.Down)
							{
								// If scrollviewer cannot be scrolled any further,
								// then cycle and navigate to start.
								return NavigateToStartInternal(itemNavigateArgs, true, out container);
							}
						}
						break;
					}

					//nextElement = KeyboardNavigation.Current.PredictFocusedElement(startingElement,
					//	direction,
					//	treeViewNavigation) as FrameworkElement;
				}

				if (didScroll && nextElement != null && ItemsHost.IsAncestorOf(nextElement))
				{
					// Adjust offset so that the nextElement is aligned to the edge
					AdjustOffsetToAlignWithEdge(nextElement, direction);
				}
			}

			// We can only navigate there if the target element is in the items host.
			if ((nextElement != null) && (ItemsHost.IsAncestorOf(nextElement)))
			{
				BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget = null;
				object nextItem = GetEncapsulatingItem(nextElement, out container, out BnsCustomSourceBaseWidget);
				container = nextElement;

				if (shouldFocus)
				{
					if (nextItem == DependencyProperty.UnsetValue /*|| nextItem is CollectionViewGroupInternal*/)
					{
						return nextElement.Focus();
					}
					else if (BnsCustomSourceBaseWidget != null)
					{
						return BnsCustomSourceBaseWidget.FocusItem(NewItemInfo(nextItem, container), itemNavigateArgs);
					}
				}
				else
				{
					return false;
				}
			}
		}
		return false;
	}

	internal void PrepareToNavigateByPage(ItemInfo startingInfo,
		FrameworkElement startingElement,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs,
		out FrameworkElement container)
	{
		container = null;

		if (ItemsHost == null)
		{
			return;
		}

		// If the focused container/item has been scrolled out of view and they want to
		// start navigating again, scroll it back into view.
		if (startingElement != null)
		{
			MakeVisible(startingElement, direction, /*alwaysAtTopOfViewport*/ false);
		}
		else
		{
			MakeVisible(startingInfo, direction, out startingElement);
		}

		object startingItem = (startingInfo != null) ? startingInfo.Item : null;

		// When we get here if startingItem is non-null, it must be on the visible page.
		NavigateByPageInternal(startingItem,
			direction,
			startingElement,
			itemNavigateArgs,
			false /*shouldFocus*/,
			out container);
	}

	internal bool NavigateByPage(FocusNavigationDirection direction, ItemNavigateArgs itemNavigateArgs)
	{
		return NavigateByPage(FocusedInfo, Keyboard.FocusedElement as FrameworkElement, direction, itemNavigateArgs);
	}

	internal bool NavigateByPage(
		ItemInfo startingInfo,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs)
	{
		return NavigateByPage(startingInfo, null, direction, itemNavigateArgs);
	}

	internal bool NavigateByPage(
		ItemInfo startingInfo,
		FrameworkElement startingElement,
		FocusNavigationDirection direction,
		ItemNavigateArgs itemNavigateArgs)
	{
		if (ItemsHost == null)
		{
			return false;
		}

		// If the focused container/item has been scrolled out of view and they want to
		// start navigating again, scroll it back into view.
		if (startingElement != null)
		{
			MakeVisible(startingElement, direction, /*alwaysAtTopOfViewport*/ false);
		}
		else
		{
			MakeVisible(startingInfo, direction, out startingElement);
		}

		object startingItem = (startingInfo != null) ? startingInfo.Item : null;

		// When we get here if startingItem is non-null, it must be on the visible page.
		FrameworkElement container;
		return NavigateByPageInternal(startingItem,
			direction,
			startingElement,
			itemNavigateArgs,
			true /*shouldFocus*/,
			out container);
	}

	private bool NavigateByPageInternal(object startingItem,
		FocusNavigationDirection direction,
		FrameworkElement startingElement,
		ItemNavigateArgs itemNavigateArgs,
		bool shouldFocus,
		out FrameworkElement container)
	{
		container = null;

		//
		// Move to the last guy on the page if we're not already there.
		//
		//if (startingItem == null &&
		//	(startingElement == null || startingElement == this))
		//{
		//	return NavigateToFirstItemOnCurrentPage(startingItem, direction, itemNavigateArgs, shouldFocus, out container);
		//}
		//else
		//{
		//	//
		//	// See if the currently focused guy is the first or last one one the page
		//	//
		//	FrameworkElement firstElement;
		//	object firstItem = GetFirstItemOnCurrentPage(startingElement, direction, out firstElement);

		//	if ((object.Equals(startingItem, firstItem) ||
		//		object.Equals(startingElement, firstElement)) &&
		//		ScrollHost != null)
		//	{
		//		bool isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);

		//		do
		//		{
		//			double oldHorizontalOffset = ScrollHost.HorizontalOffset;
		//			double oldVerticalOffset = ScrollHost.VerticalOffset;

		//			switch (direction)
		//			{
		//				case FocusNavigationDirection.Up:
		//				{
		//					if (isHorizontal)
		//					{
		//						ScrollHost.PageLeft();
		//					}
		//					else
		//					{
		//						ScrollHost.PageUp();
		//					}
		//				}
		//				break;

		//				case FocusNavigationDirection.Down:
		//				{
		//					if (isHorizontal)
		//					{
		//						ScrollHost.PageRight();
		//					}
		//					else
		//					{
		//						ScrollHost.PageDown();
		//					}
		//				}
		//				break;
		//			}

		//			ScrollHost.UpdateLayout();

		//			// If offset does not change - exit the loop
		//			if (DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) &&
		//				DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset))
		//				break;

		//			firstItem = GetFirstItemOnCurrentPage(startingElement, direction, out firstElement);
		//		}
		//		while (firstItem == DependencyProperty.UnsetValue);
		//	}

		//	container = firstElement;
		//	if (shouldFocus)
		//	{
		//		if (firstElement != null &&
		//			(firstItem == DependencyProperty.UnsetValue /*|| firstItem is CollectionViewGroupInternal*/))
		//		{
		//			return firstElement.Focus();
		//		}
		//		else
		//		{
		//			BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget= GetEncapsulatingBnsCustomSourceBaseWidget(firstElement);
		//			if (BnsCustomSourceBaseWidget!= null)
		//			{
		//				return BnsCustomSourceBaseWidget.FocusItem(NewItemInfo(firstItem, firstElement), itemNavigateArgs);
		//			}
		//		}
		//	}
		//}
		return false;
	}

	internal void NavigateToStart(ItemNavigateArgs itemNavigateArgs)
	{
		FrameworkElement container;
		NavigateToStartInternal(itemNavigateArgs, true /*shouldFocus*/, out container);
	}

	internal bool NavigateToStartInternal(ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
	{
		container = null;

		if (ItemsHost != null)
		{
			if (ScrollHost != null)
			{
				double oldHorizontalOffset = 0.0;
				double oldVerticalOffset = 0.0;
				bool isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);

				do
				{
					oldHorizontalOffset = ScrollHost.HorizontalOffset;
					oldVerticalOffset = ScrollHost.VerticalOffset;

					if (isHorizontal)
					{
						ScrollHost.ScrollToLeftEnd();
					}
					else
					{
						ScrollHost.ScrollToTop();
					}

					// Wait for layout
					ItemsHost.UpdateLayout();
				}
				// If offset does not change - exit the loop
				while (!DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) ||
					   !DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset));
			}

			FrameworkElement firstElement;
			FrameworkElement hopefulFirstElement = FindEndFocusableLeafContainer(ItemsHost, false /*last*/);
			object firstItem = GetFirstItemOnCurrentPage(hopefulFirstElement,
				FocusNavigationDirection.Up,
				out firstElement);
			container = firstElement;
			if (shouldFocus)
			{
				if (firstElement != null &&
					(firstItem == DependencyProperty.UnsetValue /*|| firstItem is CollectionViewGroupInternal*/))
				{
					return firstElement.Focus();
				}
				else
				{
					BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget = GetEncapsulatingBnsCustomSourceBaseWidget(firstElement);
					if (BnsCustomSourceBaseWidget != null)
					{
						return BnsCustomSourceBaseWidget.FocusItem(NewItemInfo(firstItem, firstElement), itemNavigateArgs);
					}
				}
			}
		}
		return false;
	}

	internal void NavigateToEnd(ItemNavigateArgs itemNavigateArgs)
	{
		FrameworkElement container;
		NavigateToEndInternal(itemNavigateArgs, true /*shouldFocus*/, out container);
	}

	internal bool NavigateToEndInternal(ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
	{
		container = null;

		if (ItemsHost != null)
		{
			if (ScrollHost != null)
			{
				double oldHorizontalOffset = 0.0;
				double oldVerticalOffset = 0.0;
				bool isHorizontal = false;  /*(ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal)*/;

				do
				{
					oldHorizontalOffset = ScrollHost.HorizontalOffset;
					oldVerticalOffset = ScrollHost.VerticalOffset;

					if (isHorizontal)
					{
						ScrollHost.ScrollToRightEnd();
					}
					else
					{
						ScrollHost.ScrollToBottom();
					}

					// Wait for layout
					ItemsHost.UpdateLayout();
				}
				// If offset does not change - exit the loop
				while (!DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) ||
					   !DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset));
			}

			FrameworkElement lastElement;
			FrameworkElement hopefulLastElement = FindEndFocusableLeafContainer(ItemsHost, true /*last*/);
			object lastItem = GetFirstItemOnCurrentPage(hopefulLastElement,
				FocusNavigationDirection.Down,
				out lastElement);
			container = lastElement;
			if (shouldFocus)
			{
				if (lastElement != null &&
					(lastItem == DependencyProperty.UnsetValue /*|| lastItem is CollectionViewGroupInternal*/))
				{
					return lastElement.Focus();
				}
				else
				{
					var BnsCustomSourceBaseWidget = GetEncapsulatingBnsCustomSourceBaseWidget(lastElement);
					if (BnsCustomSourceBaseWidget != null)
					{
						return BnsCustomSourceBaseWidget.FocusItem(NewItemInfo(lastItem, lastElement), itemNavigateArgs);
					}
				}
			}
		}
		return false;
	}

	private FrameworkElement FindEndFocusableLeafContainer(Panel itemsHost, bool last)
	{
		if (itemsHost == null)
		{
			return null;
		}
		UIElementCollection children = itemsHost.Children;
		if (children != null)
		{
			int count = children.Count;
			int i = (last ? count - 1 : 0);
			int incr = (last ? -1 : 1);
			//while (i >= 0 && i < count)
			//{
			//	FrameworkElement fe = children[i] as FrameworkElement;
			//	if (fe != null)
			//	{
			//		BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget= fe as BnsCustomSourceBaseWidget;
			//		FrameworkElement result = null;
			//		if (BnsCustomSourceBaseWidget!= null)
			//		{
			//			if (BnsCustomSourceBaseWidget.ItemsHost != null)
			//			{
			//				result = FindEndFocusableLeafContainer(BnsCustomSourceBaseWidget.ItemsHost, last);
			//			}
			//		}
			//		else
			//		{
			//			GroupItem groupItem = fe as GroupItem;
			//			if (groupItem != null && groupItem.ItemsHost != null)
			//			{
			//				result = FindEndFocusableLeafContainer(groupItem.ItemsHost, last);
			//			}
			//		}
			//		if (result != null)
			//		{
			//			return result;
			//		}
			//		else if (KeyboardNavigation.IsFocusableInternal(fe))
			//		{
			//			return fe;
			//		}
			//	}
			//	i += incr;
			//}
		}
		return null;
	}

	internal void NavigateToItem(ItemInfo info, ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport = false)
	{
		if (info != null)
		{
			NavigateToItem(info.Item, info.Index, itemNavigateArgs, alwaysAtTopOfViewport);
		}
	}

	internal void NavigateToItem(object item, ItemNavigateArgs itemNavigateArgs)
	{
		NavigateToItem(item, -1, itemNavigateArgs, false /* alwaysAtTopOfViewport */);
	}

	internal void NavigateToItem(object item, int itemIndex, ItemNavigateArgs itemNavigateArgs)
	{
		NavigateToItem(item, itemIndex, itemNavigateArgs, false /* alwaysAtTopOfViewport */);
	}

	internal void NavigateToItem(object item, ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport)
	{
		NavigateToItem(item, -1, itemNavigateArgs, alwaysAtTopOfViewport);
	}

	private void NavigateToItem(object item, int elementIndex, ItemNavigateArgs itemNavigateArgs, bool alwaysAtTopOfViewport)
	{
		// need to deal with more than 1-D no-wrapping virtualization

		// Perhaps the container isn't generated yet.  In this case we try to shift the view,
		// wait for measure, and then call it again.
		if (item == DependencyProperty.UnsetValue)
		{
			return;
		}

		if (elementIndex == -1)
		{
			elementIndex = Items.IndexOf(item);
			if (elementIndex == -1)
				return;
		}

		bool isHorizontal = false;
		if (ItemsHost != null)
		{
			isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);
		}

		FrameworkElement container;
		FocusNavigationDirection direction = isHorizontal ? FocusNavigationDirection.Right : FocusNavigationDirection.Down;
		MakeVisible(elementIndex, direction, alwaysAtTopOfViewport, out container);

		FocusItem(NewItemInfo(item, container), itemNavigateArgs);
	}

	private object FindFocusable(int startIndex, int direction, out int foundIndex, out FrameworkElement foundContainer)
	{
		// HasItems may be wrong when underlying collection does not notify, but this function
		// only cares about what's been generated and is consistent with BnsCustomSourceBaseWidgetstate.
		if (HasItems)
		{
			//int count = Items.Count;
			//for (; startIndex >= 0 && startIndex < count; startIndex += direction)
			//{
			//	FrameworkElement container = ItemContainerGenerator.ContainerFromIndex(startIndex) as FrameworkElement;

			//	// If the UI is non-null it must meet some minimum requirements to consider it for
			//	// navigation (focusable, enabled).  If it has no UI we can make no judgements about it
			//	// at this time, so it is navigable.
			//	if (container == null || Keyboard.IsFocusable(container))
			//	{
			//		foundIndex = startIndex;
			//		foundContainer = container;
			//		return Items[startIndex];
			//	}
			//}
		}

		foundIndex = -1;
		foundContainer = null;
		return null;
	}

	private void AdjustOffsetToAlignWithEdge(FrameworkElement element, FocusNavigationDirection direction)
	{
		Debug.Assert(ScrollHost != null, "This operation to adjust the offset along an edge is only possible when there is a ScrollHost available");

		if (VirtualizingPanel.GetScrollUnit(this) != ScrollUnit.Item)
		{
			ScrollViewer scrollHost = ScrollHost;
			FrameworkElement viewportElement = GetViewportElement();
			element = TryGetTreeViewItemHeader(element) as FrameworkElement;
			Rect elementBounds = new Rect(new Point(), element.RenderSize);
			elementBounds = element.TransformToAncestor(viewportElement).TransformBounds(elementBounds);
			bool isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);

			if (direction == FocusNavigationDirection.Down)
			{
				// Align with the bottom edge of viewport
				if (isHorizontal)
				{
					scrollHost.ScrollToHorizontalOffset(scrollHost.HorizontalOffset - scrollHost.ViewportWidth + elementBounds.Right);
				}
				else
				{
					scrollHost.ScrollToVerticalOffset(scrollHost.VerticalOffset - scrollHost.ViewportHeight + elementBounds.Bottom);
				}
			}
			else if (direction == FocusNavigationDirection.Up)
			{
				// Align with the top edge of viewport
				if (isHorizontal)
				{
					scrollHost.ScrollToHorizontalOffset(scrollHost.HorizontalOffset + elementBounds.Left);
				}
				else
				{
					scrollHost.ScrollToVerticalOffset(scrollHost.VerticalOffset + elementBounds.Top);
				}
			}
		}
	}

	//
	// Shifts the viewport to make the given index visible.
	//
	private void MakeVisible(int index, FocusNavigationDirection direction, bool alwaysAtTopOfViewport, out FrameworkElement container)
	{
		container = null;

		if (index >= 0)
		{
			container = ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
			if (container == null)
			{
				// In case of VirtualizingPanel, the container might not have been
				// generated yet. Hence try generating it.
				VirtualizingPanel virtualizingPanel = ItemsHost as VirtualizingPanel;
				if (virtualizingPanel != null)
				{
					//virtualizingPanel.BringIndexIntoView(index);
					//UpdateLayout();
					//container = ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
				}
			}
			MakeVisible(container, direction, alwaysAtTopOfViewport);
		}
	}

	//
	// Shifts the viewport to make the given item visible.
	//
	private void MakeVisible(ItemInfo info, FocusNavigationDirection direction, out FrameworkElement container)
	{
		if (info != null)
		{
			MakeVisible(info.Index, direction, false /*alwaysAtTopOfViewport*/, out container);
			info.Container = container;
		}
		else
		{
			MakeVisible(-1, direction, false /*alwaysAtTopOfViewport*/, out container);
		}
	}

	//
	// Shifts the viewport to make the given index visible.
	//
	internal void MakeVisible(FrameworkElement container, FocusNavigationDirection direction, bool alwaysAtTopOfViewport)
	{
		if (ScrollHost != null && ItemsHost != null)
		{
			double oldHorizontalOffset;
			double oldVerticalOffset;

			FrameworkElement viewportElement = GetViewportElement();

			while (container != null && !IsOnCurrentPage(viewportElement, container, direction, false /*fullyVisible*/))
			{
				oldHorizontalOffset = ScrollHost.HorizontalOffset;
				oldVerticalOffset = ScrollHost.VerticalOffset;

				container.BringIntoView();

				// Wait for layout
				ItemsHost.UpdateLayout();

				// If offset does not change - exit the loop
				if (DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) &&
					DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset))
					break;
			}

			if (container != null && alwaysAtTopOfViewport)
			{
				bool isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);

				FrameworkElement firstElement;
				GetFirstItemOnCurrentPage(container, FocusNavigationDirection.Up, out firstElement);
				while (firstElement != container)
				{
					oldHorizontalOffset = ScrollHost.HorizontalOffset;
					oldVerticalOffset = ScrollHost.VerticalOffset;

					if (isHorizontal)
					{
						ScrollHost.LineRight();
					}
					else
					{
						ScrollHost.LineDown();
					}

					ScrollHost.UpdateLayout();

					// If offset does not change - exit the loop
					if (DoubleUtil.AreClose(oldHorizontalOffset, ScrollHost.HorizontalOffset) &&
						DoubleUtil.AreClose(oldVerticalOffset, ScrollHost.VerticalOffset))
						break;

					GetFirstItemOnCurrentPage(container, FocusNavigationDirection.Up, out firstElement);
				}
			}
		}
	}

	private bool NavigateToFirstItemOnCurrentPage(object startingItem, FocusNavigationDirection direction, ItemNavigateArgs itemNavigateArgs, bool shouldFocus, out FrameworkElement container)
	{
		object firstItem = GetFirstItemOnCurrentPage(ItemContainerGenerator.ContainerFromItem(startingItem) as FrameworkElement,
			direction,
			out container);

		if (firstItem != DependencyProperty.UnsetValue)
		{
			if (shouldFocus)
			{
				return FocusItem(NewItemInfo(firstItem, container), itemNavigateArgs);
			}
		}
		return false;
	}

	private object GetFirstItemOnCurrentPage(FrameworkElement startingElement,
		FocusNavigationDirection direction,
		out FrameworkElement firstElement)
	{
		Debug.Assert(direction == FocusNavigationDirection.Up || direction == FocusNavigationDirection.Down, "Can only get the first item on a page using North or South");

		bool isHorizontal = false; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Horizontal);
		bool isVertical = true; // (ItemsHost.HasLogicalOrientation && ItemsHost.LogicalOrientation == Orientation.Vertical);

		if (ScrollHost != null &&
			ScrollHost.CanContentScroll &&
			VirtualizingPanel.GetScrollUnit(this) == ScrollUnit.Item &&
			!(this is TreeView) &&
			!IsGrouping)
		{
			int foundIndex = -1;
			if (isVertical)
			{
				if (direction == FocusNavigationDirection.Up)
				{
					return FindFocusable((int)ScrollHost.VerticalOffset, 1, out foundIndex, out firstElement);
				}
				else
				{
					return FindFocusable((int)(ScrollHost.VerticalOffset + Math.Max(ScrollHost.ViewportHeight - 1, 0)),
						-1,
						out foundIndex,
						out firstElement);
				}
			}
			else if (isHorizontal)
			{
				if (direction == FocusNavigationDirection.Up)
				{
					return FindFocusable((int)ScrollHost.HorizontalOffset, 1, out foundIndex, out firstElement);
				}
				else
				{
					return FindFocusable((int)(ScrollHost.HorizontalOffset + Math.Max(ScrollHost.ViewportWidth - 1, 0)),
						-1,
						out foundIndex,
						out firstElement);
				}
			}
		}

		//
		// We assume we're physically scrolling in both directions now.
		//
		if (startingElement != null)
		{
			FrameworkElement currentElement = startingElement;
			if (isHorizontal)
			{
				// In horizontal orientation left/right directions must used to
				// predict the focus.
				if (direction == FocusNavigationDirection.Up)
				{
					direction = FocusNavigationDirection.Left;
				}
				else if (direction == FocusNavigationDirection.Down)
				{
					direction = FocusNavigationDirection.Right;
				}
			}

			FrameworkElement viewportElement = GetViewportElement();
			bool treeViewNavigation = (this is TreeView);
			//currentElement = KeyboardNavigation.Current.PredictFocusedElementAtViewportEdge(startingElement,
			//	direction,
			//	treeViewNavigation,
			//	viewportElement,
			//	viewportElement) as FrameworkElement;

			object returnItem = null;
			firstElement = null;

			if (currentElement != null)
			{
				returnItem = GetEncapsulatingItem(currentElement, out firstElement);
			}

			if (currentElement == null || returnItem == DependencyProperty.UnsetValue)
			{
				// Try the startingElement as a candidate.
				ElementViewportPosition elementPosition = GetElementViewportPosition(viewportElement,
					startingElement,
					direction,
					false /*fullyVisible*/);
				if (elementPosition == ElementViewportPosition.CompletelyInViewport ||
					elementPosition == ElementViewportPosition.PartiallyInViewport)
				{
					currentElement = startingElement;
					returnItem = GetEncapsulatingItem(currentElement, out firstElement);
				}
			}

			if (returnItem != null /*&& returnItem is CollectionViewGroupInternal*/)
			{
				firstElement = currentElement;
			}
			return returnItem;
		}

		firstElement = null;
		return null;
	}

	internal FrameworkElement GetViewportElement()
	{
		// NOTE: When ScrollHost is non-null, we use ScrollHost instead of
		//       ItemsHost because ItemsHost in the physically scrolling
		//       case will just have its layout offset shifted, and all
		//       items will always be within the bounding box of the ItemsHost,
		//       and we want to know if you can actually see the element.
		FrameworkElement viewPort = ScrollHost;
		if (viewPort == null)
		{
			viewPort = ItemsHost;
		}
		else
		{
			//// Try use the ScrollContentPresenter as the viewport it is it available
			//// because that is more representative of the viewport in case of
			//// DataGrid when the ColumnHeaders need to be excluded from the
			//// dimensions of the viewport.
			//ScrollContentPresenter scp = viewPort.GetTemplateChild(ScrollViewer.ScrollContentPresenterTemplateName) as ScrollContentPresenter;
			//if (scp != null)
			//{
			//	viewPort = scp;
			//}
		}

		return viewPort;
	}

	/// <summary>
	/// Determines if the given item is on the current visible page.
	/// </summary>
	private bool IsOnCurrentPage(object item, FocusNavigationDirection axis)
	{
		FrameworkElement container = ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;

		if (container == null)
		{
			return false;
		}

		return (GetElementViewportPosition(GetViewportElement(), container, axis, false) == ElementViewportPosition.CompletelyInViewport);
	}

	private bool IsOnCurrentPage(FrameworkElement element, FocusNavigationDirection axis)
	{
		return (GetElementViewportPosition(GetViewportElement(), element, axis, false) == ElementViewportPosition.CompletelyInViewport);
	}

	/// <summary>
	/// Determines if the given element is on the current visible page.
	/// The element must be completely on the page on the given axis, but need
	/// not be completely contained on the page in the perpendicular axis.
	/// For example, if axis == North, then the element's Top and Bottom must
	/// be completely contained on the page.
	/// </summary>
	private bool IsOnCurrentPage(FrameworkElement viewPort, FrameworkElement element, FocusNavigationDirection axis, bool fullyVisible)
	{
		return (GetElementViewportPosition(viewPort, element, axis, fullyVisible) == ElementViewportPosition.CompletelyInViewport);
	}

	internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort,
		UIElement element,
		FocusNavigationDirection axis,
		bool fullyVisible)
	{
		Rect elementRect;
		return GetElementViewportPosition(viewPort, element, axis, fullyVisible, out elementRect);
	}

	internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort,
		UIElement element,
		FocusNavigationDirection axis,
		bool fullyVisible,
		out Rect elementRect)
	{
		return GetElementViewportPosition(
			viewPort,
			element,
			axis,
			fullyVisible,
			false,
			out elementRect);
	}

	/// <summary>
	/// Determines if the given element is
	///     1) Completely in the current visible page along the given axis.
	///     2) Partially in the current visible page.
	///     3) Before the current page along the given axis.
	///     4) After the current page along the given axis.
	/// fullyVisible parameter specifies if the element needs to be completely
	///     in the current visible page along the perpendicular axis (if it is
	///     completely in the page along the major axis).
	/// ignorePerpendicularAxis parameter specifies whether the position of
	///     given element along the secondary axis doesn't matter
	/// </summary>
	internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort,
		UIElement element,
		FocusNavigationDirection axis,
		bool fullyVisible,
		bool ignorePerpendicularAxis,
		out Rect elementRect)
	{
		elementRect = Rect.Empty;

		// If there's no ScrollHost or ItemsHost, the element is not on the page
		if (viewPort == null)
		{
			return ElementViewportPosition.None;
		}

		if (element == null || !viewPort.IsAncestorOf(element))
		{
			return ElementViewportPosition.None;
		}

		Rect viewPortBounds = new Rect(new Point(), viewPort.RenderSize);
		Rect elementBounds = new Rect(new Point(), element.RenderSize);
		elementBounds = CorrectCatastrophicCancellation(element.TransformToAncestor(viewPort)).TransformBounds(elementBounds);
		bool northSouth = (axis == FocusNavigationDirection.Up || axis == FocusNavigationDirection.Down);
		bool eastWest = (axis == FocusNavigationDirection.Left || axis == FocusNavigationDirection.Right);

		elementRect = elementBounds;

		if (ignorePerpendicularAxis)
		{
			// expand the viewport bounds to infinity along the secondary axis
			if (northSouth)
			{
				viewPortBounds = new Rect(Double.NegativeInfinity, viewPortBounds.Top,
											Double.PositiveInfinity, viewPortBounds.Height);
			}
			else if (eastWest)
			{
				viewPortBounds = new Rect(viewPortBounds.Left, Double.NegativeInfinity,
											viewPortBounds.Width, Double.PositiveInfinity);
			}
		}

		// Return true if the element is completely contained within the page along the given axis.

		if (fullyVisible)
		{
			if (viewPortBounds.Contains(elementBounds))
			{
				return ElementViewportPosition.CompletelyInViewport;
			}
		}
		else
		{
			if (northSouth)
			{
				if (DoubleUtil.LessThanOrClose(viewPortBounds.Top, elementBounds.Top)
					&& DoubleUtil.LessThanOrClose(elementBounds.Bottom, viewPortBounds.Bottom))
				{
					return ElementViewportPosition.CompletelyInViewport;
				}
			}
			else if (eastWest)
			{
				if (DoubleUtil.LessThanOrClose(viewPortBounds.Left, elementBounds.Left)
					&& DoubleUtil.LessThanOrClose(elementBounds.Right, viewPortBounds.Right))
				{
					return ElementViewportPosition.CompletelyInViewport;
				}
			}
		}

		if (ElementIntersectsViewport(viewPortBounds, elementBounds))
		{
			return ElementViewportPosition.PartiallyInViewport;
		}
		else if ((northSouth && DoubleUtil.LessThanOrClose(elementBounds.Bottom, viewPortBounds.Top)) ||
			(eastWest && DoubleUtil.LessThanOrClose(elementBounds.Right, viewPortBounds.Left)))
		{
			return ElementViewportPosition.BeforeViewport;
		}
		else if ((northSouth && DoubleUtil.LessThanOrClose(viewPortBounds.Bottom, elementBounds.Top)) ||
			(eastWest && DoubleUtil.LessThanOrClose(viewPortBounds.Right, elementBounds.Left)))
		{
			return ElementViewportPosition.AfterViewport;
		}
		return ElementViewportPosition.None;
	}

	// this version also returns the element's layout rectangle (in viewport's coordinates).
	// VirtualizingStackPanel needs this, to determine the element's scroll offset.
	internal static ElementViewportPosition GetElementViewportPosition(FrameworkElement viewPort,
		UIElement element,
		FocusNavigationDirection axis,
		bool fullyVisible,
		bool ignorePerpendicularAxis,
		out Rect elementRect,
		out Rect layoutRect)
	{
		ElementViewportPosition position = GetElementViewportPosition(
			viewPort,
			element,
			axis,
			fullyVisible,
			false,
			out elementRect);

		//if (position == ElementViewportPosition.None)
		//{
		//	layoutRect = Rect.Empty;
		//}
		//else
		//{
		//	Visual parent = VisualTreeHelper.GetParent(element) as Visual;
		//	Debug.Assert(element != viewPort && element.IsArrangeValid && parent != null, "GetElementViewportPosition called in unsupported situation");
		//	layoutRect = CorrectCatastrophicCancellation(parent.TransformToAncestor(viewPort)).TransformBounds(element.PreviousArrangeRect);
		//}

		return position;
	}

	// in large virtualized hierarchical lists (TreeView or grouping), the transform
	// returned by element.TransformToAncestor(viewport) is vulnerable to catastrophic
	// cancellation.  If element is at the top of the viewport, but embedded in
	// layers of the hierarchy, the contributions of the intermediate elements add
	// up to a large positive number which should exactly cancel out the large
	// negative offset of the viewport's direct child to produce net offset of 0.0.
	// But floating-point drift while accumulating the intermediate offsets and
	// catastrophic cancellation in the last step may produce a very small
	// non-zero number instead (e.g. -0.0000000000006548). This can lead to
	// infinite loops and incorrect decisions in layout.
	// To mitigate this problem, replace near-zero offsets with zero.
	private static GeneralTransform CorrectCatastrophicCancellation(GeneralTransform transform)
	{
		MatrixTransform matrixTransform = transform as MatrixTransform;
		if (matrixTransform != null)
		{
			bool needNewTransform = false;
			Matrix matrix = matrixTransform.Matrix;

			if (matrix.OffsetX != 0.0 && DoubleUtil.AreClose(matrix.OffsetX, 0.0))
			{
				matrix.OffsetX = 0.0;
				needNewTransform = true;
			}

			if (matrix.OffsetY != 0.0 && DoubleUtil.AreClose(matrix.OffsetY, 0.0))
			{
				matrix.OffsetY = 0.0;
				needNewTransform = true;
			}

			if (needNewTransform)
			{
				transform = new MatrixTransform(matrix);
			}
		}

		return transform;
	}

	private static bool ElementIntersectsViewport(Rect viewportRect, Rect elementRect)
	{
		if (viewportRect.IsEmpty || elementRect.IsEmpty)
		{
			return false;
		}

		if (DoubleUtil.LessThan(elementRect.Right, viewportRect.Left) || DoubleUtil.AreClose(elementRect.Right, viewportRect.Left) ||
			DoubleUtil.GreaterThan(elementRect.Left, viewportRect.Right) || DoubleUtil.AreClose(elementRect.Left, viewportRect.Right) ||
			DoubleUtil.LessThan(elementRect.Bottom, viewportRect.Top) || DoubleUtil.AreClose(elementRect.Bottom, viewportRect.Top) ||
			DoubleUtil.GreaterThan(elementRect.Top, viewportRect.Bottom) || DoubleUtil.AreClose(elementRect.Top, viewportRect.Bottom))
		{
			return false;
		}
		return true;
	}

	private bool IsInDirectionForLineNavigation(Rect fromRect, Rect toRect, FocusNavigationDirection direction, bool isHorizontal)
	{
		Debug.Assert(direction == FocusNavigationDirection.Up ||
			direction == FocusNavigationDirection.Down);

		if (direction == FocusNavigationDirection.Down)
		{
			if (isHorizontal)
			{
				// Right
				return DoubleUtil.GreaterThanOrClose(toRect.Left, fromRect.Left);
			}
			else
			{
				// Down
				return DoubleUtil.GreaterThanOrClose(toRect.Top, fromRect.Top);
			}
		}
		else if (direction == FocusNavigationDirection.Up)
		{
			if (isHorizontal)
			{
				// Left
				return DoubleUtil.LessThanOrClose(toRect.Right, fromRect.Right);
			}
			else
			{
				// UP
				return DoubleUtil.LessThanOrClose(toRect.Bottom, fromRect.Bottom);
			}
		}
		return false;
	}

	private static void OnGotFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget = (BnsCustomSourceBaseWidget)sender;
		UIElement focusedElement = e.OriginalSource as UIElement;
		if ((focusedElement != null) && (focusedElement != BnsCustomSourceBaseWidget))
		{
			object item = BnsCustomSourceBaseWidget.ItemContainerGenerator.ItemFromContainer(focusedElement);
			if (item != DependencyProperty.UnsetValue)
			{
				BnsCustomSourceBaseWidget._focusedInfo = BnsCustomSourceBaseWidget.NewItemInfo(item, focusedElement);
			}
			else if (BnsCustomSourceBaseWidget._focusedInfo != null)
			{
				//	UIElement itemContainer = BnsCustomSourceBaseWidget._focusedInfo.Container as UIElement;
				//	if (itemContainer == null ||
				//		!Helper.IsAnyAncestorOf(itemContainer, focusedElement))
				//	{
				//		BnsCustomSourceBaseWidget._focusedInfo = null;
				//	}
				//}
			}
		}
	}

	/// <summary>
	/// The item corresponding to the UI container which has focus.
	/// Virtualizing panels remove visual children you can't see.
	/// When you scroll the focused element out of view we throw
	/// focus back on to the items control and remember the item which
	/// was focused.  When it scrolls back into view (and focus is
	/// still on the BnsCustomSourceBaseWidget) we'll focus it.
	/// </summary>
	internal ItemInfo FocusedInfo
	{
		get { return _focusedInfo; }
	}

	private ItemInfo _focusedInfo;

	internal class ItemNavigateArgs
	{
		public ItemNavigateArgs(InputDevice deviceUsed, ModifierKeys modifierKeys)
		{
			_deviceUsed = deviceUsed;
			_modifierKeys = modifierKeys;
		}

		public InputDevice DeviceUsed { get { return _deviceUsed; } }

		private InputDevice _deviceUsed;
		private ModifierKeys _modifierKeys;

		public static ItemNavigateArgs Empty
		{
			get
			{
				if (_empty == null)
				{
					_empty = new ItemNavigateArgs(null, ModifierKeys.None); ;
				}
				return _empty;
			}
		}
		private static ItemNavigateArgs _empty;
	}

	// make this protected
	internal virtual bool FocusItem(ItemInfo info, ItemNavigateArgs itemNavigateArgs)
	{
		object item = info.Item;
		bool returnValue = false;

		if (item != null)
		{
			UIElement container = info.Container as UIElement;
			if (container != null)
			{
				returnValue = container.Focus();
			}
		}
		if (itemNavigateArgs.DeviceUsed is KeyboardDevice)
		{
			//KeyboardNavigation.ShowFocusVisual();
		}
		return returnValue;
	}

	// ISSUE: IsLogicalVertical and IsLogicalHorizontal are rough guesses as to whether
	//        the ItemsHost is virtualizing in a particular direction.  Ideally this
	//        would be exposed through the IScrollInfo.


	internal ScrollViewer ScrollHost
	{
		get
		{
			//if (!ReadControlFlag(ControlBoolFlags.ScrollHostValid))
			//{
			//	if (_itemsHost == null)
			//	{
			//		return null;
			//	}
			//	else
			//	{
			//		// We have an itemshost, so walk up the tree looking for the ScrollViewer
			//		for (DependencyObject current = _itemsHost; current != this && current != null; current = VisualTreeHelper.GetParent(current))
			//		{
			//			ScrollViewer scrollViewer = current as ScrollViewer;
			//			if (scrollViewer != null)
			//			{
			//				_scrollHost = scrollViewer;
			//				break;
			//			}
			//		}

			//		WriteControlFlag(ControlBoolFlags.ScrollHostValid, true);
			//	}
			//}

			return _scrollHost;
		}
	}


	internal void DoAutoScroll()
	{
		DoAutoScroll(FocusedInfo);
	}

	internal void DoAutoScroll(ItemInfo startingInfo)
	{
		// Attempt to compute positions based on the ScrollHost.
		// If that doesn't exist, use the ItemsHost.
		FrameworkElement relativeTo = ScrollHost != null ? (FrameworkElement)ScrollHost : ItemsHost;
		if (relativeTo != null)
		{
			// Figure out where the mouse is w.r.t. the BnsCustomSourceBaseWidget.

			Point mousePosition = Mouse.GetPosition(relativeTo);

			// Take the bounding box of the ListBox and scroll against that
			Rect bounds = new Rect(new Point(), relativeTo.RenderSize);
			bool focusChanged = false;

			if (mousePosition.Y < bounds.Top)
			{
				NavigateByLine(startingInfo, FocusNavigationDirection.Up, new ItemNavigateArgs(Mouse.PrimaryDevice, Keyboard.Modifiers));
				focusChanged = startingInfo != FocusedInfo;
			}
			else if (mousePosition.Y >= bounds.Bottom)
			{
				NavigateByLine(startingInfo, FocusNavigationDirection.Down, new ItemNavigateArgs(Mouse.PrimaryDevice, Keyboard.Modifiers));
				focusChanged = startingInfo != FocusedInfo;
			}

			// Try horizontal scroll if vertical scroll did not happen
			if (!focusChanged)
			{
				if (mousePosition.X < bounds.Left)
				{
					FocusNavigationDirection direction = FocusNavigationDirection.Left;
					if (IsRTL(relativeTo))
					{
						direction = FocusNavigationDirection.Right;
					}

					NavigateByLine(startingInfo, direction, new ItemNavigateArgs(Mouse.PrimaryDevice, Keyboard.Modifiers));
				}
				else if (mousePosition.X >= bounds.Right)
				{
					FocusNavigationDirection direction = FocusNavigationDirection.Right;
					if (IsRTL(relativeTo))
					{
						direction = FocusNavigationDirection.Left;
					}

					NavigateByLine(startingInfo, direction, new ItemNavigateArgs(Mouse.PrimaryDevice, Keyboard.Modifiers));
				}
			}
		}
	}

	private bool IsRTL(FrameworkElement element)
	{
		FlowDirection flowDirection = element.FlowDirection;
		return (flowDirection == FlowDirection.RightToLeft);
	}

	private static BnsCustomSourceBaseWidget GetEncapsulatingBnsCustomSourceBaseWidget(FrameworkElement element)
	{
		while (element != null)
		{
			BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget = BnsCustomSourceBaseWidget.BnsCustomSourceBaseWidgetFromItemContainer(element);
			if (BnsCustomSourceBaseWidget != null)
			{
				return BnsCustomSourceBaseWidget;
			}
			element = VisualTreeHelper.GetParent(element) as FrameworkElement;
		}
		return null;
	}

	private static object GetEncapsulatingItem(FrameworkElement element, out FrameworkElement container)
	{
		BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget = null;
		return GetEncapsulatingItem(element, out container, out BnsCustomSourceBaseWidget);
	}

	private static object GetEncapsulatingItem(FrameworkElement element, out FrameworkElement container, out BnsCustomSourceBaseWidget BnsCustomSourceBaseWidget)
	{
		object item = DependencyProperty.UnsetValue;
		BnsCustomSourceBaseWidget = null;

		while (element != null)
		{
			BnsCustomSourceBaseWidget = BnsCustomSourceBaseWidget.BnsCustomSourceBaseWidgetFromItemContainer(element);
			if (BnsCustomSourceBaseWidget != null)
			{
				item = BnsCustomSourceBaseWidget.ItemContainerGenerator.ItemFromContainer(element);

				if (item != DependencyProperty.UnsetValue)
				{
					break;
				}
			}

			element = VisualTreeHelper.GetParent(element) as FrameworkElement;
		}

		container = element;
		return item;
	}

	internal static DependencyObject TryGetTreeViewItemHeader(DependencyObject element)
	{
		//TreeViewItem treeViewItem = element as TreeViewItem;
		//if (treeViewItem != null)
		//{
		//	return treeViewItem.TryGetHeaderElement();
		//}
		return element;
	}

	#endregion

	#region Private Methods

	private void ApplyItemContainerStyle(DependencyObject container, object item)
	{
		//FrameworkObject foContainer = new FrameworkObject(container);

		//// don't overwrite a locally-defined style (bug 1018408)
		//if (!foContainer.IsStyleSetFromGenerator &&
		//	container.ReadLocalValue(FrameworkElement.StyleProperty) != DependencyProperty.UnsetValue)
		//{
		//	return;
		//}

		//// Control's ItemContainerStyle has first stab
		//Style style = ItemContainerStyle;

		//// no ItemContainerStyle set, try ItemContainerStyleSelector
		//if (style == null)
		//{
		//	if (ItemContainerStyleSelector != null)
		//	{
		//		style = ItemContainerStyleSelector.SelectStyle(item, container);
		//	}
		//}

		//// apply the style, if found
		//if (style != null)
		//{
		//	// verify style is appropriate before applying it
		//	if (!style.TargetType.IsInstanceOfType(container))
		//		throw new InvalidOperationException("StyleForWrongType");

		//	foContainer.Style = style;
		//	foContainer.IsStyleSetFromGenerator = true;
		//}
		//else if (foContainer.IsStyleSetFromGenerator)
		//{
		//	// if Style was formerly set from ItemContainerStyle, clear it
		//	foContainer.IsStyleSetFromGenerator = false;
		//	container.ClearValue(FrameworkElement.StyleProperty);
		//}
	}

	private void RemoveItemContainerStyle(DependencyObject container)
	{
		//FrameworkObject foContainer = new FrameworkObject(container);

		//if (foContainer.IsStyleSetFromGenerator)
		//{
		//	container.ClearValue(FrameworkElement.StyleProperty);
		//}
	}

	internal object GetItemOrContainerFromContainer(DependencyObject container)
	{
		object item = ItemContainerGenerator.ItemFromContainer(container);

		if (item == DependencyProperty.UnsetValue
			&& BnsCustomSourceBaseWidgetFromItemContainer(container) == this
			&& this.IsItemItsOwnContainer(container))
		{
			item = container;
		}

		return item;
	}

	#endregion

	#region ItemInfo

	// create an ItemInfo with as much information as can be deduced
	internal ItemInfo NewItemInfo(object item, DependencyObject container = null, int index = -1)
	{
		return new ItemInfo(item, container, index).Refresh(ItemContainerGenerator);
	}

	// create an ItemInfo for the given container
	internal ItemInfo ItemInfoFromContainer(DependencyObject container)
	{
		return NewItemInfo(ItemContainerGenerator.ItemFromContainer(container), container, ItemContainerGenerator.IndexFromContainer(container));
	}

	// create an ItemInfo for the given index
	internal ItemInfo ItemInfoFromIndex(int index)
	{
		return (index >= 0) ? NewItemInfo(Items[index], ItemContainerGenerator.ContainerFromIndex(index), index)
							: null;
	}

	// create an unresolved ItemInfo
	internal ItemInfo NewUnresolvedItemInfo(object item)
	{
		return new ItemInfo(item, ItemInfo.UnresolvedContainer, -1);
	}

	// return the container corresponding to an ItemInfo
	internal DependencyObject ContainerFromItemInfo(ItemInfo info)
	{
		DependencyObject container = info.Container;
		if (container == null)
		{
			if (info.Index >= 0)
			{
				container = ItemContainerGenerator.ContainerFromIndex(info.Index);
				info.Container = container;
			}
			else
			{
				container = ItemContainerGenerator.ContainerFromItem(info.Item);
				// don't change info.Container - info is potentially shared by different BnsCustomSourceBaseWidgets
			}
		}

		return container;
	}

	// adjust ItemInfos after a generator status change
	internal void AdjustItemInfoAfterGeneratorChange(ItemInfo info)
	{
		if (info != null)
		{
			ItemInfo[] a = new ItemInfo[] { info };
			AdjustItemInfosAfterGeneratorChange(a, claimUniqueContainer: false);
		}
	}

	// adjust ItemInfos after a generator status change
	internal void AdjustItemInfosAfterGeneratorChange(IEnumerable<ItemInfo> list, bool claimUniqueContainer)
	{
		// detect discarded containers and mark the ItemInfo accordingly
		// (also see if there are infos awaiting containers)
		bool resolvePendingContainers = false;
		foreach (ItemInfo info in list)
		{
			DependencyObject container = info.Container;
			if (container == null)
			{
				resolvePendingContainers = true;
			}
			//else if (info.IsRemoved || !BnsCustomSourceBaseWidget.EqualsEx(info.Item,
			//			container.ReadLocalValue(ItemContainerGenerator.ItemForItemContainerProperty)))
			//{
			//	info.Container = null;
			//	resolvePendingContainers = true;
			//}
		}

		// if any of the ItemInfos correspond to containers
		// that are now realized, record the container in the ItemInfo
		if (resolvePendingContainers)
		{
			// first find containers that are already claimed by the list
			List<DependencyObject> claimedContainers = new List<DependencyObject>();
			if (claimUniqueContainer)
			{
				foreach (ItemInfo info in list)
				{
					DependencyObject container = info.Container;
					if (container != null)
					{
						claimedContainers.Add(container);
					}
				}
			}

			// now try to match the pending items with an unclaimed container
			foreach (ItemInfo info in list)
			{
				DependencyObject container = info.Container;
				if (container == null)
				{
					int index = info.Index;
					if (index >= 0)
					{
						// if we know the index, see if the container exists
						container = ItemContainerGenerator.ContainerFromIndex(index);
					}
					else
					{
						// otherwise see if an unclaimed container matches the item
						//object item = info.Item;
						//ItemContainerGenerator.FindItem(
						//	static (state, o, d) => BnsCustomSourceBaseWidget.EqualsEx(o, state.item) && !state.claimedContainers.Contains(d),
						//	(item, claimedContainers),
						//	out container, out index);
					}

					if (container != null)
					{
						// update ItemInfo and claim the container
						info.Container = container;
						info.Index = index;
						if (claimUniqueContainer)
						{
							claimedContainers.Add(container);
						}
					}
				}
			}
		}
	}

	// correct the indices in the given ItemInfo, in response to a collection change event
	internal void AdjustItemInfo(NotifyCollectionChangedEventArgs e, ItemInfo info)
	{
		if (info != null)
		{
			ItemInfo[] a = new ItemInfo[] { info };
			AdjustItemInfos(e, a);
		}
	}

	// correct the indices in the given ItemInfos, in response to a collection change event
	internal void AdjustItemInfos(NotifyCollectionChangedEventArgs e, IEnumerable<ItemInfo> list)
	{
		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
				// items at NewStartingIndex and above have moved up 1
				foreach (ItemInfo info in list)
				{
					int index = info.Index;
					if (index >= e.NewStartingIndex)
					{
						info.Index = index + 1;
					}
				}
				break;

			case NotifyCollectionChangedAction.Remove:
				// items at OldStartingIndex and above have moved down 1
				foreach (ItemInfo info in list)
				{
					int index = info.Index;
					if (index > e.OldStartingIndex)
					{
						info.Index = index - 1;
					}
					else if (index == e.OldStartingIndex)
					{
						info.Index = -1;
					}
				}
				break;

			case NotifyCollectionChangedAction.Move:
				// items between New and Old have moved.  The direction and
				// exact endpoints depends on whether New comes before Old.
				int left, right, delta;
				if (e.OldStartingIndex < e.NewStartingIndex)
				{
					left = e.OldStartingIndex + 1;
					right = e.NewStartingIndex;
					delta = -1;
				}
				else
				{
					left = e.NewStartingIndex;
					right = e.OldStartingIndex - 1;
					delta = 1;
				}

				foreach (ItemInfo info in list)
				{
					int index = info.Index;
					if (index == e.OldStartingIndex)
					{
						info.Index = e.NewStartingIndex;
					}
					else if (left <= index && index <= right)
					{
						info.Index = index + delta;
					}
				}
				break;

			case NotifyCollectionChangedAction.Replace:
				// nothing to do
				break;

			case NotifyCollectionChangedAction.Reset:
				// the indices and containers are no longer valid
				foreach (ItemInfo info in list)
				{
					info.Index = -1;
					info.Container = null;
				}
				break;
		}
	}

	// return an ItemInfo like the input one, but owned by this BnsCustomSourceBaseWidget
	internal ItemInfo LeaseItemInfo(ItemInfo info, bool ensureIndex = false)
	{
		// if the original has index data, it's already good enough
		if (info.Index < 0)
		{
			// otherwise create a new info from the original's item
			info = NewItemInfo(info.Item);
			if (ensureIndex && info.Index < 0)
			{
				info.Index = Items.IndexOf(info.Item);
			}
		}

		return info;
	}

	// refresh an ItemInfo
	internal void RefreshItemInfo(ItemInfo info)
	{
		if (info != null)
		{
			info.Refresh(ItemContainerGenerator);
		}
	}

	[DebuggerDisplay("Index: {Index}  Item: {Item}")]
	internal class ItemInfo
	{
		internal object Item { get; private set; }
		internal DependencyObject Container { get; set; }
		internal int Index { get; set; }

		internal static readonly DependencyObject SentinelContainer = new DependencyObject();
		internal static readonly DependencyObject UnresolvedContainer = new DependencyObject();
		internal static readonly DependencyObject KeyContainer = new DependencyObject();
		internal static readonly DependencyObject RemovedContainer = new DependencyObject();

		static ItemInfo()
		{
			// mark the special DOs as sentinels.  This helps catch bugs involving
			// using them accidentally for anything besides equality comparison.
			//SentinelContainer.MakeSentinel();
			//UnresolvedContainer.MakeSentinel();
			//KeyContainer.MakeSentinel();
			//RemovedContainer.MakeSentinel();
		}

		public ItemInfo(object item, DependencyObject container = null, int index = -1)
		{
			Item = item;
			Container = container;
			Index = index;
		}

		internal bool IsResolved { get { return Container != UnresolvedContainer; } }
		internal bool IsKey { get { return Container == KeyContainer; } }
		internal bool IsRemoved { get { return Container == RemovedContainer; } }

		internal ItemInfo Clone()
		{
			return new ItemInfo(Item, Container, Index);
		}

		internal static ItemInfo Key(ItemInfo info)
		{
			return (info.Container == UnresolvedContainer)
				? new ItemInfo(info.Item, KeyContainer, -1)
				: info;
		}

		public override int GetHashCode()
		{
			return (Item != null) ? Item.GetHashCode() : 314159;
		}

		public override bool Equals(object o)
		{
			if (o == (object)this)
				return true;

			ItemInfo that = o as ItemInfo;
			if (that == null)
				return false;

			return Equals(that, matchUnresolved: false);
		}

		internal bool Equals(ItemInfo that, bool matchUnresolved)
		{
			// Removed matches nothing
			if (this.IsRemoved || that.IsRemoved)
				return false;

			// items must match
			if (!object.Equals(this.Item, that.Item))
				return false;

			// Key matches anything, except Unresolved when matchUnresovled is false
			if (this.Container == KeyContainer)
				return matchUnresolved || that.Container != UnresolvedContainer;
			else if (that.Container == KeyContainer)
				return matchUnresolved || this.Container != UnresolvedContainer;

			// Unresolved matches nothing
			if (this.Container == UnresolvedContainer || that.Container == UnresolvedContainer)
				return false;

			return
				(this.Container == that.Container)
				 ? (this.Container == SentinelContainer)
					 ? (this.Index == that.Index)      // Sentinel => negative indices are significant
					 : (this.Index < 0 || that.Index < 0 ||
							this.Index == that.Index)   // ~Sentinel => ignore negative indices
				 : (this.Container == SentinelContainer) ||    // sentinel matches non-sentinel
					(that.Container == SentinelContainer) ||
					((this.Container == null || that.Container == null) &&   // null matches non-null
						(this.Index < 0 || that.Index < 0 ||                    // provided that indices match
							this.Index == that.Index));
		}

		public static bool operator ==(ItemInfo info1, ItemInfo info2)
		{
			return Object.Equals(info1, info2);
		}

		public static bool operator !=(ItemInfo info1, ItemInfo info2)
		{
			return !Object.Equals(info1, info2);
		}

		// update container and index with current values
		internal ItemInfo Refresh(ItemContainerGenerator generator)
		{
			if (Container == null && Index < 0)
			{
				Container = generator.ContainerFromItem(Item);
			}

			if (Index < 0 && Container != null)
			{
				Index = generator.IndexFromContainer(Container);
			}

			if (Container == null && Index >= 0)
			{
				Container = generator.ContainerFromIndex(Index);
			}

			if (Container == SentinelContainer && Index >= 0)
			{
				Container = null;   // caller explicitly wants null container
			}

			return this;
		}

		// Don't call this on entries used in hashtables - it changes the hashcode
		internal void Reset(object item)
		{
			Item = item;
		}
	}

	#endregion ItemInfo

	#region ItemValueStorage

	object IContainItemStorage.ReadItemValue(object item, DependencyProperty dp)
	{
		return null;

		//return Helper.ReadItemValue(this, item, dp.GlobalIndex);
	}


	void IContainItemStorage.StoreItemValue(object item, DependencyProperty dp, object value)
	{
		//Helper.StoreItemValue(this, item, dp.GlobalIndex, value);
	}

	void IContainItemStorage.ClearItemValue(object item, DependencyProperty dp)
	{
		//Helper.ClearItemValue(this, item, dp.GlobalIndex);
	}

	void IContainItemStorage.ClearValue(DependencyProperty dp)
	{
		//Helper.ClearItemValueStorage(this, new int[] { dp.GlobalIndex });
	}

	void IContainItemStorage.Clear()
	{
		//Helper.ClearItemValueStorage(this);
	}

	#endregion

	#region Data
	private ItemCollection _items;                      // Cache for Items property
	private ItemContainerGenerator _itemContainerGenerator;
	private Panel _itemsHost;
	private ScrollViewer _scrollHost;
	private ObservableCollection<GroupStyle> _groupStyle = new ObservableCollection<GroupStyle>();
	#endregion



	public void TestMethod()
	{
		var panel = new VerticalBox();
		Children.Add(panel);

		foreach (var item in ItemsSource)
		{
			//((IGeneratorHost)this).PrepareItemContainer(this, x);

			var child = (FrameworkElement)this.ItemTemplate.LoadContent();
			child.DataContext = item;

			panel.Children.Add(child);
		}
	}
}

internal enum ElementViewportPosition
{
	None,
	BeforeViewport,
	PartiallyInViewport,
	CompletelyInViewport,
	AfterViewport
}

/// <summary>
/// Interface through which an ItemContainerGenerator communicates with its host.
/// </summary>
internal interface IGeneratorHost
{
	/// <summary>
	/// The view of the data
	/// </summary>
	ItemCollection View { get; }

	/// <summary>
	/// Return true if the item is (or should be) its own item container
	/// </summary>
	bool IsItemItsOwnContainer(object item);

	/// <summary>
	/// Return the element used to display the given item
	/// </summary>
	DependencyObject GetContainerForItem(object item);

	/// <summary>
	/// Prepare the element to act as the ItemUI for the corresponding item.
	/// </summary>
	void PrepareItemContainer(DependencyObject container, object item);

	/// <summary>
	/// Undo any initialization done on the element during GetContainerForItem and PrepareItemContainer
	/// </summary>
	void ClearContainerForItem(DependencyObject container, object item);

	/// <summary>
	/// Determine if the given element was generated for this host as an ItemUI.
	/// </summary>
	bool IsHostForItemContainer(DependencyObject container);

	/// <summary>
	/// Return the GroupStyle (if any) to use for the given group at the given level.
	/// </summary>
	GroupStyle GetGroupStyle(CollectionViewGroup group, int level);

	/// <summary>
	/// Communicates to the host that the generator is using grouping.
	/// </summary>
	void SetIsGrouping(bool isGrouping);

	/// <summary>
	/// The AlternationCount
	/// <summary>
	int AlternationCount { get; }
}


/// <summary>
/// ItemCollection will contain items shaped as strings, objects, xml nodes,
/// elements, as well as other collections.  (It will not promote elements from
/// contained collections; to "flatten" contained collections, assign a
/// <seealso cref="CompositeCollection"/> to
/// the ItemsSource property on the BnsCustomSourceBaseWidget.)
/// A <seealso cref="BnsCustomSourceBaseWidget"/> uses the data
/// in the ItemCollection to generate its content according to its ItemTemplate.
/// </summary>
/// <remarks>
/// When first created, ItemCollection is in an uninitialized state, neither
/// ItemsSource-mode nor direct-mode.  It will hold settings like SortDescriptions and Filter
/// until the mode is determined, then assign the settings to the active view.
/// When uninitialized, calls to the list-modifying members will put the
/// ItemCollection in direct mode, and setting the ItemsSource will put the
/// ItemCollection in ItemsSource mode.
/// </remarks>
[Localizability(LocalizationCategory.Ignore)]
public sealed class ItemCollection : CollectionView, IList, IEditableCollectionViewAddNewItem, ICollectionViewLiveShaping, IItemProperties
{
	//------------------------------------------------------
	//
	//  Constructors
	//
	//------------------------------------------------------

	#region Constructors
	// ItemCollection cannot be created standalone, it is created by BnsCustomSourceBaseWidget

	/// <summary>
	/// Initializes a new instance of ItemCollection that is empty and has default initial capacity.
	/// </summary>
	/// <param name="modelParent">model parent of this item collection</param>
	/// <remarks>
	/// </remarks>
	internal ItemCollection(DependencyObject modelParent)
		: base(EmptyEnumerable.Instance)
	{
		_modelParent = new WeakReference(modelParent);
	}

	/// <summary>
	/// Initializes a new instance of ItemCollection that is empty and has specified initial capacity.
	/// </summary>
	/// <param name="modelParent">model parent of this item collection</param>
	/// <param name="capacity">The number of items that the new list is initially capable of storing</param>
	/// <remarks>
	/// Some BnsCustomSourceBaseWidget implementations have better idea how many items to anticipate,
	/// capacity parameter lets them tailor the initial size.
	/// </remarks>
	internal ItemCollection(FrameworkElement modelParent, int capacity)
		: base(EmptyEnumerable.Instance)
	{
		_defaultCapacity = capacity;
		_modelParent = new WeakReference(modelParent);
	}
	#endregion Constructors


	//------------------------------------------------------
	//
	//  Public Methods
	//
	//------------------------------------------------------

	#region Public Methods

	//------------------------------------------------------
	#region ICurrentItem

	// These currency methods do not call OKToChangeCurrent() because
	// ItemCollection already picks up and forwards the CurrentChanging
	// event from the inner _collectionView.

	/// <summary>
	/// Move <seealso cref="CurrentItem"/> to the first item.
	/// </summary>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentToFirst()
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentToFirst();
	}

	/// <summary>
	/// Move <seealso cref="CurrentItem"/> to the next item.
	/// </summary>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentToNext()
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentToNext();
	}

	/// <summary>
	/// Move <seealso cref="CurrentItem"/> to the previous item.
	/// </summary>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentToPrevious()
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentToPrevious();
	}

	/// <summary>
	/// Move <seealso cref="CurrentItem"/> to the last item.
	/// </summary>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentToLast()
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentToLast();
	}

	/// <summary>
	/// Move <seealso cref="ICollectionView.CurrentItem"/> to the given item.
	/// </summary>
	/// <param name="item">Move CurrentItem to this item.</param>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentTo(object item)
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentTo(item);
	}

	/// <summary>
	/// Move <seealso cref="CurrentItem"/> to the item at the given index.
	/// </summary>
	/// <param name="position">Move CurrentItem to this index</param>
	/// <returns>true if <seealso cref="CurrentItem"/> points to an item within the view.</returns>
	public override bool MoveCurrentToPosition(int position)
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.MoveCurrentToPosition(position);
	}


	#endregion ICurrentItem

	#region IList

	/// <summary>
	///     Returns an enumerator object for this ItemCollection
	/// </summary>
	/// <returns>
	///     Enumerator object for this ItemCollection
	/// </returns>
	protected override IEnumerator GetEnumerator()
	{
		if (!EnsureCollectionView())
			return EmptyEnumerator.Instance;

		return ((IEnumerable)_collectionView).GetEnumerator();
	}

	/// <summary>
	///     Add an item to this collection.
	/// </summary>
	/// <param name="newItem">
	///     New item to be added to collection
	/// </param>
	/// <returns>
	///     Zero-based index where the new item is added.  -1 if the item could not be added.
	/// </returns>
	/// <remarks>
	///     To facilitate initialization of direct-mode BnsCustomSourceBaseWidgets with Sort and/or Filter,
	/// Add() is permitted when BnsCustomSourceBaseWidget is initializing, even if a Sort or Filter has been set.
	/// </remarks>
	/// <exception cref="InvalidOperationException">
	/// trying to add an item which already has a different model/logical parent
	/// - or -
	/// trying to add an item when the ItemCollection is in ItemsSource mode.
	/// </exception>
	public int Add(object newItem)
	{
		CheckIsUsingInnerView();
		//int index = _internalView.Add(newItem);
		//ModelParent.SetValue(BnsCustomSourceBaseWidget.HasItemsPropertyKey, BooleanBoxes.TrueBox);
		//return index;

		return 0;
	}

	/// <summary>
	///     Clears the collection.  Releases the references on all items
	/// currently in the collection.
	/// </summary>
	/// <exception cref="InvalidOperationException">
	/// the ItemCollection is read-only because it is in ItemsSource mode
	/// </exception>
	public void Clear()
	{
		// Not using CheckIsUsingInnerView() because we don't want to create internal list

		VerifyRefreshNotDeferred();

		if (IsUsingItemsSource)
		{
			throw new InvalidOperationException("ItemsSourceInUse");
		}

		//if (_internalView != null)
		//{
		//	_internalView.Clear();
		//}
		ModelParent.ClearValue(BnsCustomSourceBaseWidget.HasItemsPropertyKey);
	}

	/// <summary>
	///     Checks to see if a given item is in this collection and in the view
	/// </summary>
	/// <param name="containItem">
	///     The item whose membership in this collection is to be checked.
	/// </param>
	/// <returns>
	///     True if the collection contains the given item and the item passes the active filter
	/// </returns>
	public override bool Contains(object containItem)
	{
		if (!EnsureCollectionView())
			return false;

		VerifyRefreshNotDeferred();

		return _collectionView.Contains(containItem);
	}

	/// <summary>
	///     Makes a shallow copy of object references from this
	///     ItemCollection to the given target array
	/// </summary>
	/// <param name="array">
	///     Target of the copy operation
	/// </param>
	/// <param name="index">
	///     Zero-based index at which the copy begins
	/// </param>
	public void CopyTo(Array array, int index)
	{
		ArgumentNullException.ThrowIfNull(array);
		if (array.Rank > 1)
			throw new ArgumentException("BadTargetArray"); // array is multidimensional.
		ArgumentOutOfRangeException.ThrowIfNegative(index);

		// use the view instead of the collection, because it may have special sort/filter
		if (!EnsureCollectionView())
			return;  // there is no collection (bind returned no collection) and therefore nothing to copy

		VerifyRefreshNotDeferred();

		//IndexedEnumerable.CopyTo(_collectionView, array, index);
	}

	/// <summary>
	///     Finds the index in this collection/view where the given item is found.
	/// </summary>
	/// <param name="item">
	///     The item whose index in this collection/view is to be retrieved.
	/// </param>
	/// <returns>
	///     Zero-based index into the collection/view where the given item can be
	/// found.  Otherwise, -1
	/// </returns>
	public override int IndexOf(object item)
	{
		if (!EnsureCollectionView())
			return -1;

		VerifyRefreshNotDeferred();

		return _collectionView.IndexOf(item);
	}

	/// <summary>
	/// Retrieve item at the given zero-based index in this CollectionView.
	/// </summary>
	/// <remarks>
	/// <p>The index is evaluated with any SortDescriptions or Filter being set on this CollectionView.</p>
	/// </remarks>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if index is out of range
	/// </exception>
	public override object GetItemAt(int index)
	{
		// only check lower bound because Count could be expensive
		ArgumentOutOfRangeException.ThrowIfNegative(index);

		VerifyRefreshNotDeferred();

		if (!EnsureCollectionView())
			throw new InvalidOperationException("ItemCollectionHasNoCollection");

		//if (_collectionView == _internalView)
		//{
		//	// check upper bound here because we know it's not expensive
		//	ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _internalView.Count());
		//}

		return _collectionView.GetItemAt(index);
	}



	/// <summary>
	///     Insert an item in the collection at a given index.  All items
	/// after the given position are moved down by one.
	/// </summary>
	/// <param name="insertIndex">
	///     The index at which to inser the item
	/// </param>
	/// <param name="insertItem">
	///     The item reference to be added to the collection
	/// </param>
	/// <exception cref="InvalidOperationException">
	/// Thrown when trying to add an item which already has a different model/logical parent
	/// or when the ItemCollection is read-only because it is in ItemsSource mode
	/// </exception>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if index is out of range
	/// </exception>
	public void Insert(int insertIndex, object insertItem)
	{
		CheckIsUsingInnerView();
		//_internalView.Insert(insertIndex, insertItem);
		ModelParent.SetValue(BnsCustomSourceBaseWidget.HasItemsPropertyKey, BooleanBoxes.TrueBox);
	}

	/// <summary>
	///     Removes the given item reference from the collection or view.
	/// All remaining items move up by one.
	/// </summary>
	/// <exception cref="InvalidOperationException">
	/// the ItemCollection is read-only because it is in ItemsSource mode or there
	/// is a sort or filter in effect
	/// </exception>
	/// <param name="removeItem">
	///     The item to be removed.
	/// </param>
	public void Remove(object removeItem)
	{
		//CheckIsUsingInnerView();
		//_internalView.Remove(removeItem);
		//if (IsEmpty)
		//{
		//	ModelParent.ClearValue(BnsCustomSourceBaseWidget.HasItemsPropertyKey);
		//}
	}

	/// <summary>
	///     Removes an item from the collection or view at the given index.
	/// All remaining items move up by one.
	/// </summary>
	/// <param name="removeIndex">
	///     The index at which to remove an item.
	/// </param>
	/// <exception cref="InvalidOperationException">
	/// the ItemCollection is read-only because it is in ItemsSource mode
	/// </exception>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if index is out of range
	/// </exception>
	public void RemoveAt(int removeIndex)
	{
		//CheckIsUsingInnerView();
		//_internalView.RemoveAt(removeIndex);
		//if (IsEmpty)
		//{
		//	ModelParent.ClearValue(BnsCustomSourceBaseWidget.HasItemsPropertyKey);
		//}
	}

	#endregion IList

	/// <summary>
	/// Return true if the item is acceptable to the active filter, if any.
	/// It is commonly used during collection-changed notifications to
	/// determine if the added/removed item requires processing.
	/// </summary>
	/// <returns>
	/// true if the item passes the filter or if no filter is set on collection view.
	/// </returns>
	public override bool PassesFilter(object item)
	{
		if (!EnsureCollectionView())
			return true;
		return _collectionView.PassesFilter(item);
	}

	/// <summary>
	/// Re-create the view, using any <seealso cref="SortDescriptions"/> and/or <seealso cref="Filter"/>.
	/// </summary>
	protected override void RefreshOverride()
	{
		if (_collectionView != null)
		{
			if (_collectionView.NeedsRefresh)
			{
				_collectionView.Refresh();
			}
			else
			{
				// if the view is up to date, we only need to raise the Reset event
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}
	}

	#endregion Public Methods


	//------------------------------------------------------
	//
	//  Public Properties
	//
	//------------------------------------------------------

	#region Public Properties

	/// <summary>
	///     Read-only property for the number of items stored in this collection of objects
	/// </summary>
	/// <remarks>
	///     returns 0 if the ItemCollection is uninitialized or
	///     there is no collection in ItemsSource mode
	/// </remarks>
	public override int Count
	{
		get
		{
			if (!EnsureCollectionView())
				return 0;

			VerifyRefreshNotDeferred();

			return _collectionView.Count;
		}
	}

	/// <summary>
	/// Returns true if the resulting (filtered) view is emtpy.
	/// </summary>
	public override bool IsEmpty
	{
		get
		{
			if (!EnsureCollectionView())
				return true;

			VerifyRefreshNotDeferred();

			return _collectionView.IsEmpty;
		}
	}

	/// <summary>
	///     Indexer property to retrieve or replace the item at the given
	/// zero-based offset into the collection.
	/// </summary>
	/// <exception cref="InvalidOperationException">
	/// trying to set an item which already has a different model/logical parent; or,
	/// trying to set when in ItemsSource mode; or,
	/// the ItemCollection is uninitialized; or,
	/// in ItemsSource mode, the binding on ItemsSource does not provide a collection.
	/// </exception>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Thrown if index is out of range
	/// </exception>
	public object this[int index]
	{
		get
		{
			return GetItemAt(index);
		}
		set
		{
			CheckIsUsingInnerView();

			//ArgumentOutOfRangeException.ThrowIfNegative(index);
			//ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _internalView.Count);

			//_internalView[index] = value;
		}
	}

	/// <summary>
	/// The ItemCollection's underlying collection or the user provided ItemsSource collection
	/// </summary>
	public override IEnumerable SourceCollection
	{
		get
		{
			if (IsUsingItemsSource)
			{
				return ItemsSource;
			}
			else
			{
				EnsureInternalView();
				return this;
			}
		}
	}

	/// <summary>
	///     Returns true if this view needs to be refreshed
	/// (i.e. when the view is not consistent with the current sort or filter).
	/// </summary>
	/// <returns>
	/// true when SortDescriptions or Filter is changed while refresh is deferred,
	/// or in direct-mode, when an item have been added while SortDescriptions or Filter is in place.
	/// </returns>
	public override bool NeedsRefresh
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.NeedsRefresh : false;
		}
	}

	/// <summary>
	/// Collection of Sort criteria to sort items in ItemCollection.
	/// </summary>
	/// <remarks>
	/// <p>
	/// Sorting is supported for items in the BnsCustomSourceBaseWidget.Items collection;
	/// if a collection is assigned to BnsCustomSourceBaseWidget.ItemsSource, the capability to sort
	/// depends on the CollectionView for that inner collection.
	/// Simpler implementations of CollectionVIew do not support sorting and will return an empty
	/// and immutable / read-only SortDescription collection.
	/// Attempting to modify such a collection will cause NotSupportedException.
	/// Use <seealso cref="CanSort"/> property on CollectionView to test if sorting is supported
	/// before modifying the returned collection.
	/// </p>
	/// <p>
	/// One or more sort criteria in form of <seealso cref="SortDescription"/>
	/// can be added, each specifying a property and direction to sort by.
	/// </p>
	/// </remarks>
	public override SortDescriptionCollection SortDescriptions
	{
		get
		{
			// always hand out this ItemCollection's SortDescription collection;
			// in ItemsSource mode the inner collection view will be kept in synch with this collection
			if (MySortDescriptions == null)
			{
				MySortDescriptions = new SortDescriptionCollection();
				if (_collectionView != null)
				{
					// no need to do this under the monitor - we haven't hooked up events yet
					CloneList(MySortDescriptions, _collectionView.SortDescriptions);
				}

				((INotifyCollectionChanged)MySortDescriptions).CollectionChanged += new NotifyCollectionChangedEventHandler(SortDescriptionsChanged);
			}
			return MySortDescriptions;
		}
	}

	/// <summary>
	/// Test if this ICollectionView supports sorting before adding
	/// to <seealso cref="SortDescriptions"/>.
	/// </summary>
	public override bool CanSort
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.CanSort : true;
		}
	}


	/// <summary>
	/// Set/get a filter callback to filter out items in collection.
	/// This property will always accept a filter, but the collection view for the
	/// underlying ItemsSource may not actually support filtering.
	/// Please check <seealso cref="CanFilter"/>
	/// </summary>
	/// <exception cref="NotSupportedException">
	/// Collections assigned to ItemsSource may not support filtering and could throw a NotSupportedException.
	/// Use <seealso cref="CanFilter"/> property to test if filtering is supported before assigning
	/// a non-null Filter value.
	/// </exception>
	public override Predicate<object> Filter
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.Filter : MyFilter;
		}
		set
		{
			MyFilter = value;
			if (_collectionView != null)
				_collectionView.Filter = value;
		}
	}

	/// <summary>
	/// Test if this ICollectionView supports filtering before assigning
	/// a filter callback to <seealso cref="Filter"/>.
	/// </summary>
	public override bool CanFilter
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.CanFilter : true;
		}
	}

	/// <summary>
	/// Returns true if this view really supports grouping.
	/// When this returns false, the rest of the interface is ignored.
	/// </summary>
	public override bool CanGroup
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.CanGroup : false;
		}
	}

	/// <summary>
	/// The description of grouping, indexed by level.
	/// </summary>
	public override ObservableCollection<GroupDescription> GroupDescriptions
	{
		get
		{
			// always hand out this ItemCollection's GroupDescription collection;
			// in ItemsSource mode the inner collection view will be kept in synch with this collection
			if (MyGroupDescriptions == null)
			{
				MyGroupDescriptions = new ObservableCollection<GroupDescription>();
				if (_collectionView != null)
				{
					// no need to do this under the monitor - we haven't hooked up events yet
					CloneList(MyGroupDescriptions, _collectionView.GroupDescriptions);
				}

				((INotifyCollectionChanged)MyGroupDescriptions).CollectionChanged += new NotifyCollectionChangedEventHandler(GroupDescriptionsChanged);
			}
			return MyGroupDescriptions;
		}
	}

	/// <summary>
	/// The top-level groups, constructed according to the descriptions
	/// given in GroupDescriptions and/or GroupBySelector.
	/// </summary>
	public override ReadOnlyObservableCollection<object> Groups
	{
		get
		{
			return (EnsureCollectionView()) ? _collectionView.Groups : null;
		}
	}

	/// <summary>
	/// Enter a Defer Cycle.
	/// Defer cycles are used to coalesce changes to the ICollectionView.
	/// </summary>
	public override IDisposable DeferRefresh()
	{
		// if already deferred (level > 0) and there is a _collectionView, there should be a _deferInnerRefresh
		Debug.Assert(_deferLevel == 0 || _collectionView == null || _deferInnerRefresh != null);

		// if not already deferred, there should NOT be a _deferInnerRefresh
		Debug.Assert(_deferLevel != 0 || _deferInnerRefresh == null);

		if (_deferLevel == 0 && _collectionView != null)
		{
			_deferInnerRefresh = _collectionView.DeferRefresh();
		}

		++_deferLevel;  // do this after inner DeferRefresh, in case it throws

		return new DeferHelper(this);
	}

	/// <summary>
	///     Gets a value indicating whether access to the ItemCollection is synchronized (thread-safe).
	/// </summary>
	bool ICollection.IsSynchronized
	{
		get
		{
			return false;
		}
	}

#pragma warning disable 1634, 1691  // about to use PreSharp message numbers - unknown to C#
	/// <summary>
	///     Returns an object to be used in thread synchronization.
	/// </summary>
	/// <exception cref="NotSupportedException">
	/// ItemCollection cannot provide a sync root for synchronization while
	/// in ItemsSource mode.  Please use the ItemsSource directly to
	/// get its sync root.
	/// </exception>
	object ICollection.SyncRoot
	{
		get
		{
			//			if (IsUsingItemsSource)
			//			{
			//				// see discussion in XML comment above.
			//#pragma warning suppress 6503 // "Property get methods should not throw exceptions."
			//				throw new NotSupportedException("ItemCollectionShouldUseInnerSyncRoot");
			//			}

			//			return _internalView.SyncRoot;

			return false;
		}
	}
#pragma warning restore 1634, 1691

	/// <summary>
	///     Gets a value indicating whether the IList has a fixed size.
	///     An ItemCollection can usually grow dynamically,
	///     this call will commonly return FixedSize = False.
	///     In ItemsSource mode, this call will return IsFixedSize = True.
	/// </summary>
	bool IList.IsFixedSize
	{
		get
		{
			return IsUsingItemsSource;
		}
	}

	/// <summary>
	///     Gets a value indicating whether the IList is read-only.
	///     An ItemCollection is usually writable,
	///     this call will commonly return IsReadOnly = False.
	///     In ItemsSource mode, this call will return IsReadOnly = True.
	/// </summary>
	bool IList.IsReadOnly
	{
		get
		{
			return IsUsingItemsSource;
		}
	}

	//------------------------------------------------------
	#region ICurrentItem

	/// <summary>
	/// The ordinal position of the <seealso cref="CurrentItem"/> within the (optionally
	/// sorted and filtered) view.
	/// </summary>
	public override int CurrentPosition
	{
		get
		{
			if (!EnsureCollectionView())
				return -1;

			VerifyRefreshNotDeferred();

			return _collectionView.CurrentPosition;
		}
	}

	/// <summary>
	/// Return current item.
	/// </summary>
	public override object CurrentItem
	{
		get
		{
			if (!EnsureCollectionView())
				return null;

			VerifyRefreshNotDeferred();

			return _collectionView.CurrentItem;
		}
	}

	/// <summary>
	/// Return true if <seealso cref="ICollectionView.CurrentItem"/> is beyond the end (End-Of-File).
	/// </summary>
	public override bool IsCurrentAfterLast
	{
		get
		{
			if (!EnsureCollectionView())
				return false;

			VerifyRefreshNotDeferred();

			return _collectionView.IsCurrentAfterLast;
		}
	}

	/// <summary>
	/// Return true if <seealso cref="ICollectionView.CurrentItem"/> is before the beginning (Beginning-Of-File).
	/// </summary>
	public override bool IsCurrentBeforeFirst
	{
		get
		{
			if (!EnsureCollectionView())
				return false;

			VerifyRefreshNotDeferred();

			return _collectionView.IsCurrentBeforeFirst;
		}
	}

	#endregion ICurrentItem

	#endregion Public Properties

	#region IEditableCollectionView

	#region Adding new items

	/// <summary>
	/// Indicates whether to include a placeholder for a new item, and if so,
	/// where to put it.
	/// </summary>
	NewItemPlaceholderPosition IEditableCollectionView.NewItemPlaceholderPosition
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.NewItemPlaceholderPosition;
			}
			else
			{
				return NewItemPlaceholderPosition.None;
			}
		}
		set
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				ecv.NewItemPlaceholderPosition = value;
			}
			else
			{
				//throw new InvalidOperationException(SR.Format(SR.MemberNotAllowedForView, "NewItemPlaceholderPosition"));
			}
		}
	}

	/// <summary>
	/// Return true if the view supports <seealso cref="IEditableCollectionView.AddNew"/>.
	/// </summary>
	bool IEditableCollectionView.CanAddNew
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.CanAddNew;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Add a new item to the underlying collection.  Returns the new item.
	/// After calling AddNew and changing the new item as desired, either
	/// <seealso cref="IEditableCollectionView.CommitNew"/> or <seealso cref="IEditableCollectionView.CancelNew"/> should be
	/// called to complete the transaction.
	/// </summary>
	object IEditableCollectionView.AddNew()
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			return ecv.AddNew();
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}


	/// <summary>
	/// Complete the transaction started by <seealso cref="IEditableCollectionView.AddNew"/>.  The new
	/// item remains in the collection, and the view's sort, filter, and grouping
	/// specifications (if any) are applied to the new item.
	/// </summary>
	void IEditableCollectionView.CommitNew()
	{
		if (_collectionView is IEditableCollectionView ecv)
		{
			ecv.CommitNew();
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}

	/// <summary>
	/// Complete the transaction started by <seealso cref="IEditableCollectionView.AddNew"/>.  The new
	/// item is removed from the collection.
	/// </summary>
	void IEditableCollectionView.CancelNew()
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.CancelNew();
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}

	/// <summary>
	/// Returns true if an </seealso cref="IEditableCollectionView.AddNew"> transaction is in progress.
	/// </summary>
	bool IEditableCollectionView.IsAddingNew
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.IsAddingNew;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// When an </seealso cref="IEditableCollectionView.AddNew"> transaction is in progress, this property
	/// returns the new item.  Otherwise it returns null.
	/// </summary>
	object IEditableCollectionView.CurrentAddItem
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.CurrentAddItem;
			}
			else
			{
				return null;
			}
		}
	}

	#endregion Adding new items

	#region Removing items

	/// <summary>
	/// Return true if the view supports <seealso cref="IEditableCollectionView.Remove"/> and
	/// <seealso cref="RemoveAt"/>.
	/// </summary>
	bool IEditableCollectionView.CanRemove
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.CanRemove;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Remove the item at the given index from the underlying collection.
	/// The index is interpreted with respect to the view (not with respect to
	/// the underlying collection).
	/// </summary>
	void IEditableCollectionView.RemoveAt(int index)
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.RemoveAt(index);
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}

	/// <summary>
	/// Remove the given item from the underlying collection.
	/// </summary>
	void IEditableCollectionView.Remove(object item)
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.Remove(item);
		}
		else
		{
			//throw new InvalidOperationException(SR.Format(SR.MemberNotAllowedForView, "Remove"));
		}
	}

	#endregion Removing items

	#region Transactional editing of an item

	/// <summary>
	/// Begins an editing transaction on the given item.  The transaction is
	/// completed by calling either <seealso cref="IEditableCollectionView.CommitEdit"/> or
	/// <seealso cref="IEditableCollectionView.CancelEdit"/>.  Any changes made to the item during
	/// the transaction are considered "pending", provided that the view supports
	/// the notion of "pending changes" for the given item.
	/// </summary>
	void IEditableCollectionView.EditItem(object item)
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.EditItem(item);
		}
		else
		{
			//throw new InvalidOperationException(SR.Format(SR.MemberNotAllowedForView, "EditItem"));
		}
	}

	/// <summary>
	/// Complete the transaction started by <seealso cref="IEditableCollectionView.EditItem"/>.
	/// The pending changes (if any) to the item are committed.
	/// </summary>
	void IEditableCollectionView.CommitEdit()
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.CommitEdit();
		}
		else
		{
			//throw new InvalidOperationException(SR.Format(SR.MemberNotAllowedForView, "CommitEdit"));
		}
	}

	/// <summary>
	/// Complete the transaction started by <seealso cref="IEditableCollectionView.EditItem"/>.
	/// The pending changes (if any) to the item are discarded.
	/// </summary>
	void IEditableCollectionView.CancelEdit()
	{
		IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
		if (ecv != null)
		{
			ecv.CancelEdit();
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}

	/// <summary>
	/// Returns true if the view supports the notion of "pending changes" on the
	/// current edit item.  This may vary, depending on the view and the particular
	/// item.  For example, a view might return true if the current edit item
	/// implements <seealso cref="IEditableObject"/>, or if the view has special
	/// knowledge about the item that it can use to support rollback of pending
	/// changes.
	/// </summary>
	bool IEditableCollectionView.CanCancelEdit
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.CanCancelEdit;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Returns true if an </seealso cref="IEditableCollectionView.EditItem"> transaction is in progress.
	/// </summary>
	bool IEditableCollectionView.IsEditingItem
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.IsEditingItem;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// When an </seealso cref="IEditableCollectionView.EditItem"> transaction is in progress, this property
	/// returns the affected item.  Otherwise it returns null.
	/// </summary>
	object IEditableCollectionView.CurrentEditItem
	{
		get
		{
			IEditableCollectionView ecv = _collectionView as IEditableCollectionView;
			if (ecv != null)
			{
				return ecv.CurrentEditItem;
			}
			else
			{
				return null;
			}
		}
	}

	#endregion Transactional editing of an item

	#endregion IEditableCollectionView

	#region IEditableCollectionViewAddNewItem

	/// <summary>
	/// Return true if the view supports <seealso cref="IEditableCollectionViewAddNewItem.AddNewItem"/>.
	/// </summary>
	bool IEditableCollectionViewAddNewItem.CanAddNewItem
	{
		get
		{
			IEditableCollectionViewAddNewItem ani = _collectionView as IEditableCollectionViewAddNewItem;
			if (ani != null)
			{
				return ani.CanAddNewItem;
			}
			else
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Add a new item to the underlying collection.  Returns the new item.
	/// After calling AddNewItem and changing the new item as desired, either
	/// <seealso cref="IEditableCollectionView.CommitNew"/> or <seealso cref="IEditableCollectionView.CancelNew"/> should be
	/// called to complete the transaction.
	/// </summary>
	object IEditableCollectionViewAddNewItem.AddNewItem(object newItem)
	{
		IEditableCollectionViewAddNewItem ani = _collectionView as IEditableCollectionViewAddNewItem;
		if (ani != null)
		{
			return ani.AddNewItem(newItem);
		}
		else
		{
			throw new InvalidOperationException("MemberNotAllowedForView");
		}
	}

	#endregion IEditableCollectionViewAddNewItem

	#region ICollectionViewLiveShaping

	///<summary>
	/// Gets a value that indicates whether this view supports turning live sorting on or off.
	///</summary>
	public bool CanChangeLiveSorting
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.CanChangeLiveSorting : false;
		}
	}

	///<summary>
	/// Gets a value that indicates whether this view supports turning live filtering on or off.
	///</summary>
	public bool CanChangeLiveFiltering
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.CanChangeLiveFiltering : false;
		}
	}

	///<summary>
	/// Gets a value that indicates whether this view supports turning live grouping on or off.
	///</summary>
	public bool CanChangeLiveGrouping
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.CanChangeLiveGrouping : false;
		}
	}


	///<summary>
	/// Gets or sets a value that indicates whether live sorting is enabled.
	/// The value may be null if the view does not know whether live sorting is enabled.
	/// Calling the setter when CanChangeLiveSorting is false will throw an
	/// InvalidOperationException.
	///</summary
	public bool? IsLiveSorting
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.IsLiveSorting : null;
		}
		set
		{
			MyIsLiveSorting = value;
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			if (cvls != null && cvls.CanChangeLiveSorting)
				cvls.IsLiveSorting = value;
		}
	}

	///<summary>
	/// Gets or sets a value that indicates whether live filtering is enabled.
	/// The value may be null if the view does not know whether live filtering is enabled.
	/// Calling the setter when CanChangeLiveFiltering is false will throw an
	/// InvalidOperationException.
	///</summary>
	public bool? IsLiveFiltering
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.IsLiveFiltering : null;
		}
		set
		{
			MyIsLiveFiltering = value;
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			if (cvls != null && cvls.CanChangeLiveFiltering)
				cvls.IsLiveFiltering = value;
		}
	}

	///<summary>
	/// Gets or sets a value that indicates whether live grouping is enabled.
	/// The value may be null if the view does not know whether live grouping is enabled.
	/// Calling the setter when CanChangeLiveGrouping is false will throw an
	/// InvalidOperationException.
	///</summary>
	public bool? IsLiveGrouping
	{
		get
		{
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			return (cvls != null) ? cvls.IsLiveGrouping : null;
		}
		set
		{
			MyIsLiveGrouping = value;
			ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
			if (cvls != null && cvls.CanChangeLiveGrouping)
				cvls.IsLiveGrouping = value;
		}
	}


	///<summary>
	/// Gets a collection of strings describing the properties that
	/// trigger a live-sorting recalculation.
	/// The strings use the same format as SortDescription.PropertyName.
	///</summary>
	///<notes>
	/// When this collection is empty, the view will use the PropertyName strings
	/// from its SortDescriptions.
	///
	/// This collection is useful when sorting is described code supplied
	/// by the application  (e.g. ListCollectionView.CustomSort).
	/// In this case the view does not know which properties the code examines;
	/// the application should tell the view by adding the relevant properties
	/// to the LiveSortingProperties collection.
	///</notes>
	public ObservableCollection<string> LiveSortingProperties
	{
		get
		{
			// always hand out this ItemCollection's LiveSortingProperties collection;
			// in ItemsSource mode the inner collection view will be kept in synch with this collection
			if (MyLiveSortingProperties == null)
			{
				MyLiveSortingProperties = new ObservableCollection<string>();
				ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
				if (icvls != null)
				{
					// no need to do this under the monitor - we haven't hooked up events yet
					CloneList(MyLiveSortingProperties, icvls.LiveSortingProperties);
				}

				((INotifyCollectionChanged)MyLiveSortingProperties).CollectionChanged += new NotifyCollectionChangedEventHandler(LiveSortingChanged);
			}
			return MyLiveSortingProperties;
		}
	}

	///<summary>
	/// Gets a collection of strings describing the properties that
	/// trigger a live-filtering recalculation.
	/// The strings use the same format as SortDescription.PropertyName.
	///</summary>
	///<notes>
	/// Filtering is described by a Predicate.  The view does not
	/// know which properties the Predicate examines;  the application should
	/// tell the view by adding the relevant properties to the LiveFilteringProperties
	/// collection.
	///</notes>
	public ObservableCollection<string> LiveFilteringProperties
	{
		get
		{
			// always hand out this ItemCollection's LiveFilteringProperties collection;
			// in ItemsSource mode the inner collection view will be kept in synch with this collection
			if (MyLiveFilteringProperties == null)
			{
				MyLiveFilteringProperties = new ObservableCollection<string>();
				ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
				if (icvls != null)
				{
					// no need to do this under the monitor - we haven't hooked up events yet
					CloneList(MyLiveFilteringProperties, icvls.LiveFilteringProperties);
				}

				((INotifyCollectionChanged)MyLiveFilteringProperties).CollectionChanged += new NotifyCollectionChangedEventHandler(LiveFilteringChanged);
			}
			return MyLiveFilteringProperties;
		}
	}

	///<summary>
	/// Gets a collection of strings describing the properties that
	/// trigger a live-grouping recalculation.
	/// The strings use the same format as PropertyGroupDescription.PropertyName.
	///</summary>
	///<notes>
	/// When this collection is empty, the view will use the PropertyName strings
	/// from its GroupDescriptions.
	///
	/// This collection is useful when grouping is described code supplied
	/// by the application (e.g. PropertyGroupDescription.Converter).
	/// In this case the view does not know which properties the code examines;
	/// the application should tell the view by adding the relevant properties
	/// to the LiveGroupingProperties collection.
	///</notes>
	public ObservableCollection<string> LiveGroupingProperties
	{
		get
		{
			// always hand out this ItemCollection's LiveGroupingProperties collection;
			// in ItemsSource mode the inner collection view will be kept in synch with this collection
			if (MyLiveGroupingProperties == null)
			{
				MyLiveGroupingProperties = new ObservableCollection<string>();
				ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
				if (icvls != null)
				{
					// no need to do this under the monitor - we haven't hooked up events yet
					CloneList(MyLiveGroupingProperties, icvls.LiveGroupingProperties);
				}

				((INotifyCollectionChanged)MyLiveGroupingProperties).CollectionChanged += new NotifyCollectionChangedEventHandler(LiveGroupingChanged);
			}
			return MyLiveGroupingProperties;
		}
	}

	#endregion ICollectionViewLiveShaping

	#region IItemProperties

	/// <summary>
	/// Returns information about the properties available on items in the
	/// underlying collection.  This information may come from a schema, from
	/// a type descriptor, from a representative item, or from some other source
	/// known to the view.
	/// </summary>
	ReadOnlyCollection<ItemPropertyInfo> IItemProperties.ItemProperties
	{
		get
		{
			IItemProperties iip = _collectionView as IItemProperties;
			if (iip != null)
			{
				return iip.ItemProperties;
			}
			else
			{
				return null;
			}
		}
	}

	#endregion IItemProperties

	//------------------------------------------------------
	//
	//  Internal API
	//
	//------------------------------------------------------

	#region Internal API

	internal DependencyObject ModelParent
	{
		get { return (DependencyObject)_modelParent.Target; }
	}

	internal FrameworkElement ModelParentFE
	{
		get { return ModelParent as FrameworkElement; }
	}

	// This puts the ItemCollection into ItemsSource mode.
	internal void SetItemsSource(IEnumerable value, Func<object, object> GetSourceItem = null)
	{
		// Allow this while refresh is deferred.

		// If we're switching from Normal mode, first make sure it's legal.
		if (!IsUsingItemsSource && (_internalView != null) && (_internalView.Count > 0))
		{
			throw new InvalidOperationException("CannotUseItemsSource");
		}

		_itemsSource = value;
		_isUsingItemsSource = true;

		SetCollectionView((CollectionView)CollectionViewSource.GetDefaultView(_itemsSource));
	}

	// This returns ItemCollection to direct mode.
	internal void ClearItemsSource()
	{
		//if (IsUsingItemsSource)
		//{
		//	// return to normal mode
		//	_itemsSource = null;
		//	_isUsingItemsSource = false;

		//	SetCollectionView(_internalView);   // it's ok if _internalView is null; just like uninitialized
		//}
		//else
		//{
		//	// already in normal mode - no-op
		//}
	}

	// Read-only property used by BnsCustomSourceBaseWidget
	internal IEnumerable ItemsSource
	{
		get
		{
			return _itemsSource;
		}
	}

	internal bool IsUsingItemsSource
	{
		get
		{
			return _isUsingItemsSource;
		}
	}

	internal CollectionView CollectionView
	{
		get { return _collectionView; }
	}

	internal void BeginInit()
	{
		Debug.Assert(_isInitializing == false);
		_isInitializing = true;
		if (_collectionView != null)            // disconnect from collectionView to cut extraneous events
			UnhookCollectionView(_collectionView);
	}

	internal void EndInit()
	{
		Debug.Assert(_isInitializing == true);
		EnsureCollectionView();
		_isInitializing = false;                // now we allow collectionView to be hooked up again
		if (_collectionView != null)
		{
			HookCollectionView(_collectionView);
			Refresh();                          // apply any sort or filter for the first time
		}
	}

	internal IEnumerator LogicalChildren
	{
		get
		{
			EnsureInternalView();
			//return _internalView.LogicalChildren;
			return null;
		}
	}

	//internal override void GetCollectionChangedSources(int level, Action<int, object, bool?, List<string>> format, List<string> sources)
	//{
	//	format(level, this, false, sources);
	//	if (_collectionView != null)
	//	{
	//		_collectionView.GetCollectionChangedSources(level + 1, format, sources);
	//	}
	//}


	#endregion Internal API


	//------------------------------------------------------
	//
	//  Private Properties
	//
	//------------------------------------------------------

	#region Private Properties
	private new bool IsRefreshDeferred
	{
		get { return _deferLevel > 0; }
	}

	#endregion


	//------------------------------------------------------
	//
	//  Private Methods
	//
	//------------------------------------------------------

	#region Private Methods

	// ===== Lazy creation of InternalView =====
	// When ItemCollection is instantiated, it is uninitialized (_collectionView == null).
	// It remains so until SetItemsSource() puts it into ItemsSource mode
	// or a modifying method call such as Add() or Insert() puts it into direct mode.

	// Several ItemCollection methods check EnsureCollectionView, which returns false if
	// (_collectionView == null) and (InternalView == null), and it can mean two things:
	//   1) ItemCollection is uninitialized
	//   2) BnsCustomSourceBaseWidget is in ItemsSource mode, but the ItemsSource binding returned null
	// for either of these cases, a reasonable default return value or behavior is provided.

	// EnsureCollectionView() will set _collectionView to the InternalView if the mode is correct.
	bool EnsureCollectionView()
	{
		if (_collectionView == null && !IsUsingItemsSource && _internalView != null)
		{
			// If refresh is not necessary, fake initialization so that SetCollectionView
			// doesn't raise a refresh event.
			if (_internalView.IsEmpty)
			{
				bool wasInitializing = _isInitializing;
				_isInitializing = true;
				SetCollectionView(_internalView);
				_isInitializing = wasInitializing;
			}
			else
			{
				SetCollectionView(_internalView);
			}

			// If we're not in Begin/End Init, now's a good time to hook up listeners
			if (!_isInitializing)
				HookCollectionView(_collectionView);
		}
		return (_collectionView != null);
	}

	void EnsureInternalView()
	{
		if (_internalView == null)
		{
			// lazy creation of the InnerItemCollectionView
			//_internalView = new InnerItemCollectionView(_defaultCapacity, this);
		}
	}

	// Change the collection view in use, unhook/hook event handlers
	void SetCollectionView(CollectionView view)
	{
		if (_collectionView == view)
			return;

		if (_collectionView != null)
		{
			// Unhook events first, to avoid unnecessary refresh while it is still the active view.
			if (!_isInitializing)
				UnhookCollectionView(_collectionView);

			if (IsRefreshDeferred)  // we've been deferring refresh on the _collectionView
			{
				// end defer refresh on the _collectionView that we're letting go
				_deferInnerRefresh.Dispose();
				_deferInnerRefresh = null;
			}
		}

		bool raiseReset = false;
		_collectionView = view;
		//InvalidateEnumerableWrapper();

		if (_collectionView != null)
		{
			_deferInnerRefresh = _collectionView.DeferRefresh();

			ApplySortFilterAndGroup();

			// delay event hook-up when initializing.  see BeginInit() and EndInit().
			if (!_isInitializing)
				HookCollectionView(_collectionView);

			if (!IsRefreshDeferred)
			{
				// make sure we get at least one refresh
				raiseReset = !_collectionView.NeedsRefresh;

				_deferInnerRefresh.Dispose();    // This fires refresh event that should reach BnsCustomSourceBaseWidget listeners
				_deferInnerRefresh = null;
			}
			// when refresh is deferred, we hold on to the inner DeferRefresh until EndDefer()
		}
		else    // ItemsSource binding returned null
		{
			if (!IsRefreshDeferred)
			{
				raiseReset = true;
			}
		}

		if (raiseReset)
		{
			// notify listeners that the view is changed
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		// with a new view, we have new live shaping behavior
		OnPropertyChanged(new PropertyChangedEventArgs("IsLiveSorting"));
		OnPropertyChanged(new PropertyChangedEventArgs("IsLiveFiltering"));
		OnPropertyChanged(new PropertyChangedEventArgs("IsLiveGrouping"));
	}

	void ApplySortFilterAndGroup()
	{
		if (!IsShapingActive)
			return;

		// Only apply sort/filter/group if new view supports it and ItemCollection has real values
		if (_collectionView.CanSort)
		{
			// if user has added SortDescriptions to this.SortDescriptions, those settings get pushed to
			// the newly attached collection view
			// if no SortDescriptions are set on ItemCollection,
			// the inner collection view's .SortDescriptions gets copied to this.SortDescriptions
			// when switching back to direct mode and no user-set on this.SortDescriptions
			// then clear any .SortDescriptions set from previous inner collection view
			//SortDescriptionCollection source = (IsSortingSet) ? MySortDescriptions : _collectionView.SortDescriptions;
			//SortDescriptionCollection target = (IsSortingSet) ? _collectionView.SortDescriptions : MySortDescriptions;

			//using (SortDescriptionsMonitor.Enter())
			//{
			//	CloneList(target, source);
			//}
		}

		if (_collectionView.CanFilter && MyFilter != null)
			_collectionView.Filter = MyFilter;

		if (_collectionView.CanGroup)
		{
			// if user has added GroupDescriptions to this.GroupDescriptions, those settings get pushed to
			// the newly attached collection view
			// if no GroupDescriptions are set on ItemCollection,
			// the inner collection view's .GroupDescriptions gets copied to this.GroupDescriptions
			// when switching back to direct mode and no user-set on this.GroupDescriptions
			// then clear any .GroupDescriptions set from previous inner collection view
			//ObservableCollection<GroupDescription> source = (IsGroupingSet) ? MyGroupDescriptions : _collectionView.GroupDescriptions;
			//ObservableCollection<GroupDescription> target = (IsGroupingSet) ? _collectionView.GroupDescriptions : MyGroupDescriptions;

			//using (GroupDescriptionsMonitor.Enter())
			//{
			//	CloneList(target, source);
			//}
		}

		ICollectionViewLiveShaping cvls = _collectionView as ICollectionViewLiveShaping;
		if (cvls != null)
		{
			if (MyIsLiveSorting != null && cvls.CanChangeLiveSorting)
			{
				cvls.IsLiveSorting = MyIsLiveSorting;
			}
			if (MyIsLiveFiltering != null && cvls.CanChangeLiveFiltering)
			{
				cvls.IsLiveFiltering = MyIsLiveFiltering;
			}
			if (MyIsLiveGrouping != null && cvls.CanChangeLiveGrouping)
			{
				cvls.IsLiveGrouping = MyIsLiveGrouping;
			}
		}
	}

	void HookCollectionView(CollectionView view)
	{
		CollectionChangedEventManager.AddHandler(view, OnViewCollectionChanged);
		CurrentChangingEventManager.AddHandler(view, OnCurrentChanging);
		CurrentChangedEventManager.AddHandler(view, OnCurrentChanged);
		PropertyChangedEventManager.AddHandler(view, OnViewPropertyChanged, String.Empty);

		SortDescriptionCollection sort = view.SortDescriptions;
		if (sort != null && sort != SortDescriptionCollection.Empty)
		{
			CollectionChangedEventManager.AddHandler(sort, OnInnerSortDescriptionsChanged);
		}

		ObservableCollection<GroupDescription> group = view.GroupDescriptions;
		if (group != null)
		{
			CollectionChangedEventManager.AddHandler(group, OnInnerGroupDescriptionsChanged);
		}

		ICollectionViewLiveShaping iclvs = view as ICollectionViewLiveShaping;
		if (iclvs != null)
		{
			ObservableCollection<string> liveSortingProperties = iclvs.LiveSortingProperties;
			if (liveSortingProperties != null)
			{
				CollectionChangedEventManager.AddHandler(liveSortingProperties, OnInnerLiveSortingChanged);
			}

			ObservableCollection<string> liveFilteringProperties = iclvs.LiveFilteringProperties;
			if (liveFilteringProperties != null)
			{
				CollectionChangedEventManager.AddHandler(liveFilteringProperties, OnInnerLiveFilteringChanged);
			}

			ObservableCollection<string> liveGroupingProperties = iclvs.LiveGroupingProperties;
			if (liveGroupingProperties != null)
			{
				CollectionChangedEventManager.AddHandler(liveGroupingProperties, OnInnerLiveGroupingChanged);
			}
		}
	}

	void UnhookCollectionView(CollectionView view)
	{
		CollectionChangedEventManager.RemoveHandler(view, OnViewCollectionChanged);
		CurrentChangingEventManager.RemoveHandler(view, OnCurrentChanging);
		CurrentChangedEventManager.RemoveHandler(view, OnCurrentChanged);
		PropertyChangedEventManager.RemoveHandler(view, OnViewPropertyChanged, String.Empty);

		SortDescriptionCollection sort = view.SortDescriptions;
		if (sort != null && sort != SortDescriptionCollection.Empty)
		{
			CollectionChangedEventManager.RemoveHandler(sort, OnInnerSortDescriptionsChanged);
		}

		ObservableCollection<GroupDescription> group = view.GroupDescriptions;
		if (group != null)
		{
			CollectionChangedEventManager.RemoveHandler(group, OnInnerGroupDescriptionsChanged);
		}

		ICollectionViewLiveShaping iclvs = view as ICollectionViewLiveShaping;
		if (iclvs != null)
		{
			ObservableCollection<string> liveSortingProperties = iclvs.LiveSortingProperties;
			if (liveSortingProperties != null)
			{
				CollectionChangedEventManager.RemoveHandler(liveSortingProperties, OnInnerLiveSortingChanged);
			}

			ObservableCollection<string> liveFilteringProperties = iclvs.LiveFilteringProperties;
			if (liveFilteringProperties != null)
			{
				CollectionChangedEventManager.RemoveHandler(liveFilteringProperties, OnInnerLiveFilteringChanged);
			}

			ObservableCollection<string> liveGroupingProperties = iclvs.LiveGroupingProperties;
			if (liveGroupingProperties != null)
			{
				CollectionChangedEventManager.RemoveHandler(liveGroupingProperties, OnInnerLiveGroupingChanged);
			}
		}

		// cancel any pending AddNew or EditItem transactions
		IEditableCollectionView iev = _collectionView as IEditableCollectionView;
		if (iev != null)
		{
			if (iev.IsAddingNew)
			{
				iev.CancelNew();
			}

			if (iev.IsEditingItem)
			{
				if (iev.CanCancelEdit)
				{
					iev.CancelEdit();
				}
				else
				{
					iev.CommitEdit();
				}
			}
		}
	}

	void OnViewCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		// when the collection changes, the enumerator is no longer valid.
		// This should be detected by IndexedEnumerable, but isn't because
		// of bug in CollectionView (CollectionView's enumerators
		// do not invalidate after a collection change).
		// As a partial remedy discard the
		// enumerator here.
		//
		// Remove this line when the CollectionView bug is fixed.
		//InvalidateEnumerableWrapper();

		// notify listeners on BnsCustomSourceBaseWidget (like ItemContainerGenerator)
		OnCollectionChanged(e);
	}

	void OnCurrentChanged(object sender, EventArgs e)
	{
		Debug.Assert(sender == _collectionView);
		OnCurrentChanged();
	}

	void OnCurrentChanging(object sender, CurrentChangingEventArgs e)
	{
		Debug.Assert(sender == _collectionView);
		OnCurrentChanging(e);
	}

	void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		OnPropertyChanged(e);
	}

	// Before any modifying access, first call CheckIsUsingInnerView() because
	// a) InternalView is lazily created
	// b) modifying access is only allowed when the InnerView is being used
	// c) modifying access is only allowed when Refresh is not deferred
	void CheckIsUsingInnerView()
	{
		if (IsUsingItemsSource)
			throw new InvalidOperationException("ItemsSourceInUse");
		EnsureInternalView();
		EnsureCollectionView();
		Debug.Assert(_collectionView != null);
		VerifyRefreshNotDeferred();
	}

	void EndDefer()
	{
		--_deferLevel;

		if (_deferLevel == 0)
		{
			// if there is a _collectionView, there should be a _deferInnerRefresh
			Debug.Assert(_collectionView == null || _deferInnerRefresh != null);

			if (_deferInnerRefresh != null)
			{
				// set _deferInnerRefresh to null before calling Dispose,
				// in case Dispose throws an exception.
				IDisposable deferInnerRefresh = _deferInnerRefresh;
				_deferInnerRefresh = null;
				deferInnerRefresh.Dispose();
			}
			else
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}
	}

	// Helper to validate that we are not in the middle of a DeferRefresh
	// and throw if that is the case. The reason that this *new* version of VerifyRefreshNotDeferred
	// on ItemCollection is needed is that ItemCollection has its own *new* IsRefreshDeferred
	// which overrides IsRefreshDeferred on the base class (CollectionView), and we need to
	// be sure that we reference that member on the derived class.
	private new void VerifyRefreshNotDeferred()
	{
#pragma warning disable 1634, 1691 // about to use PreSharp message numbers - unknown to C#
#pragma warning disable 6503
		// If the Refresh is being deferred to change filtering or sorting of the
		// data by this CollectionView, then CollectionView will not reflect the correct
		// state of the underlying data.

		if (IsRefreshDeferred)
			throw new InvalidOperationException("NoCheckOrChangeWhenDeferred");

#pragma warning restore 6503
#pragma warning restore 1634, 1691
	}

	// SortDescription was added/removed to/from this ItemCollection.SortDescriptions, refresh CollView
	private void SortDescriptionsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (SortDescriptionsMonitor.Busy)
		//	return;

		//// if we have an inner collection view, keep its .SortDescriptions collection it up-to-date
		//if (_collectionView != null && _collectionView.CanSort)
		//{
		//	using (SortDescriptionsMonitor.Enter())
		//	{
		//		SynchronizeCollections<SortDescription>(e, MySortDescriptions, _collectionView.SortDescriptions);
		//	}
		//}

		//IsSortingSet = true;       // most recent change came from ItemCollection
	}

	// SortDescription was added/removed to/from inner collectionView
	private void OnInnerSortDescriptionsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (!IsShapingActive || SortDescriptionsMonitor.Busy)
		//	return;

		//// keep this ItemColl.SortDescriptions in synch with inner collection view's
		//using (SortDescriptionsMonitor.Enter())
		//{
		//	SynchronizeCollections<SortDescription>(e, _collectionView.SortDescriptions, MySortDescriptions);
		//}

		//IsSortingSet = false;      // most recent change came from inner collection view
	}

	// GroupDescription was added/removed to/from this ItemCollection.GroupDescriptions, refresh CollView
	private void GroupDescriptionsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (GroupDescriptionsMonitor.Busy)
		//	return;

		//// if we have an inner collection view, keep its .SortDescriptions collection it up-to-date
		//if (_collectionView != null && _collectionView.CanGroup)
		//{
		//	using (GroupDescriptionsMonitor.Enter())
		//	{
		//		SynchronizeCollections<GroupDescription>(e, MyGroupDescriptions, _collectionView.GroupDescriptions);
		//	}
		//}

		//IsGroupingSet = true;       // most recent change came from ItemCollection
	}

	// GroupDescription was added/removed to/from inner collectionView
	private void OnInnerGroupDescriptionsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (!IsShapingActive || GroupDescriptionsMonitor.Busy)
		//	return;

		//// keep this ItemColl.GroupDescriptions in synch with inner collection view's
		//using (GroupDescriptionsMonitor.Enter())
		//{
		//	SynchronizeCollections<GroupDescription>(e, _collectionView.GroupDescriptions, MyGroupDescriptions);
		//}

		//IsGroupingSet = false;      // most recent change came from inner collection view
	}


	// Property was added/removed to/from this ItemCollection.LiveSortingProperties, refresh CollView
	private void LiveSortingChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (LiveSortingMonitor.Busy)
		//	return;

		//// if we have an inner collection view, keep its LiveSortingProperties collection in sync
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveSortingMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, MyLiveSortingProperties, icvls.LiveSortingProperties);
		//	}
		//}

		//IsLiveSortingSet = true;       // most recent change came from ItemCollection
	}

	// Property was added/removed to/from inner collectionView's LiveSortingProperties
	private void OnInnerLiveSortingChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (!IsShapingActive || LiveSortingMonitor.Busy)
		//	return;

		//// keep this ItemColl.LiveSortingProperties in sync with inner collection view's
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveSortingMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, icvls.LiveSortingProperties, MyLiveSortingProperties);
		//	}
		//}

		//IsLiveSortingSet = false;      // most recent change came from inner collection view
	}


	// Property was added/removed to/from this ItemCollection.LiveFilteringProperties, refresh CollView
	private void LiveFilteringChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (LiveFilteringMonitor.Busy)
		//	return;

		//// if we have an inner collection view, keep its LiveFilteringProperties collection in sync
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveFilteringMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, MyLiveFilteringProperties, icvls.LiveFilteringProperties);
		//	}
		//}

		//IsLiveFilteringSet = true;       // most recent change came from ItemCollection
	}

	// Property was added/removed to/from inner collectionView's LiveFilteringProperties
	private void OnInnerLiveFilteringChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (!IsShapingActive || LiveFilteringMonitor.Busy)
		//	return;

		//// keep this ItemColl.LiveFilteringProperties in sync with inner collection view's
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveFilteringMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, icvls.LiveFilteringProperties, MyLiveFilteringProperties);
		//	}
		//}

		//IsLiveFilteringSet = false;      // most recent change came from inner collection view
	}


	// Property was added/removed to/from this ItemCollection.LiveGroupingProperties, refresh CollView
	private void LiveGroupingChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (LiveGroupingMonitor.Busy)
		//	return;

		//// if we have an inner collection view, keep its LiveGroupingProperties collection in sync
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveGroupingMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, MyLiveGroupingProperties, icvls.LiveGroupingProperties);
		//	}
		//}

		//IsLiveGroupingSet = true;       // most recent change came from ItemCollection
	}

	// Property was added/removed to/from inner collectionView's LiveGroupingProperties
	private void OnInnerLiveGroupingChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		//if (!IsShapingActive || LiveGroupingMonitor.Busy)
		//	return;

		//// keep this ItemColl.LiveGroupingProperties in sync with inner collection view's
		//ICollectionViewLiveShaping icvls = _collectionView as ICollectionViewLiveShaping;
		//if (icvls != null)
		//{
		//	using (LiveGroupingMonitor.Enter())
		//	{
		//		SynchronizeCollections<string>(e, icvls.LiveGroupingProperties, MyLiveGroupingProperties);
		//	}
		//}

		//IsLiveGroupingSet = false;      // most recent change came from inner collection view
	}


	// keep collections in sync
	private void SynchronizeCollections<T>(NotifyCollectionChangedEventArgs e, Collection<T> origin, Collection<T> clone)
	{
		if (clone == null)
			return;             // the clone might be lazily-created

		switch (e.Action)
		{
			case NotifyCollectionChangedAction.Add:
				Debug.Assert(e.NewStartingIndex >= 0);
				if (clone.Count + e.NewItems.Count != origin.Count)
					goto case NotifyCollectionChangedAction.Reset;
				for (int i = 0; i < e.NewItems.Count; i++)
				{
					clone.Insert(e.NewStartingIndex + i, (T)e.NewItems[i]);
				}
				break;
			case NotifyCollectionChangedAction.Remove:
				if (clone.Count - e.OldItems.Count != origin.Count)
					goto case NotifyCollectionChangedAction.Reset;
				Debug.Assert(e.OldStartingIndex >= 0);
				for (int i = 0; i < e.OldItems.Count; i++)
				{
					clone.RemoveAt(e.OldStartingIndex);
				}
				break;

			case NotifyCollectionChangedAction.Replace:
				Debug.Assert(e.OldStartingIndex >= 0);
				if (clone.Count != origin.Count)
					goto case NotifyCollectionChangedAction.Reset;
				for (int i = 0; i < e.OldItems.Count; i++)
				{
					clone[e.OldStartingIndex + i] = (T)e.NewItems[i];
				}
				break;

			case NotifyCollectionChangedAction.Move:
				Debug.Assert(e.OldStartingIndex >= 0);
				if (clone.Count != origin.Count)
					goto case NotifyCollectionChangedAction.Reset;
				if (e.NewItems.Count == 1)
				{
					clone.RemoveAt(e.OldStartingIndex);
					clone.Insert(e.NewStartingIndex, (T)e.NewItems[0]);
				}
				else
				{
					for (int i = 0; i < e.OldItems.Count; i++)
					{
						clone.RemoveAt(e.OldStartingIndex);
					}
					for (int i = 0; i < e.NewItems.Count; i++)
					{
						clone.Insert(e.NewStartingIndex + i, (T)e.NewItems[i]);
					}
				}
				break;

			// this arm also handles cases where the two collections have gotten
			// out of sync (typically because exceptions prevented a previous sync
			// from happening)
			case NotifyCollectionChangedAction.Reset:
				CloneList(clone, origin);
				break;

			default:
				throw new NotSupportedException("UnexpectedCollectionChangeAction");
		}
	}

	private void CloneList(IList clone, IList master)
	{
		// if either party is null, do nothing.  Allowing null lets the caller
		// avoid a lazy instantiation of the Sort/Group description collection.
		if (clone == null || master == null)
			return;

		if (clone.Count > 0)
		{
			clone.Clear();
		}

		for (int i = 0, n = master.Count; i < n; ++i)
		{
			clone.Add(master[i]);
		}
	}

	#endregion Private Methods

	#region Shaping storage

	private bool IsShapingActive
	{
		get { return _shapingStorage != null; }
	}

	private void EnsureShapingStorage()
	{
		if (!IsShapingActive)
		{
			_shapingStorage = new ShapingStorage();
		}
	}


	private SortDescriptionCollection MySortDescriptions
	{
		get { return IsShapingActive ? _shapingStorage._sort : null; }
		set { EnsureShapingStorage(); _shapingStorage._sort = value; }
	}

	private bool IsSortingSet
	{
		get { return IsShapingActive ? _shapingStorage._isSortingSet : false; }
		set
		{
			Debug.Assert(IsShapingActive, "Shaping storage not available");
			_shapingStorage._isSortingSet = value;
		}
	}

	//private MonitorWrapper SortDescriptionsMonitor
	//{
	//	get
	//	{
	//		if (_shapingStorage._sortDescriptionsMonitor == null)
	//			_shapingStorage._sortDescriptionsMonitor = new MonitorWrapper();
	//		return _shapingStorage._sortDescriptionsMonitor;
	//	}
	//}


	private Predicate<object> MyFilter
	{
		get { return IsShapingActive ? _shapingStorage._filter : null; }
		set { EnsureShapingStorage(); _shapingStorage._filter = value; }
	}


	private ObservableCollection<GroupDescription> MyGroupDescriptions
	{
		get { return IsShapingActive ? _shapingStorage._groupBy : null; }
		set { EnsureShapingStorage(); _shapingStorage._groupBy = value; }
	}

	private bool IsGroupingSet
	{
		get { return IsShapingActive ? _shapingStorage._isGroupingSet : false; }
		set
		{
			if (IsShapingActive)
				_shapingStorage._isGroupingSet = value;
			else
				Debug.Assert(!value, "Shaping storage not available");
		}
	}

	//private MonitorWrapper GroupDescriptionsMonitor
	//{
	//	get
	//	{
	//		if (_shapingStorage._groupDescriptionsMonitor == null)
	//			_shapingStorage._groupDescriptionsMonitor = new MonitorWrapper();
	//		return _shapingStorage._groupDescriptionsMonitor;
	//	}
	//}


	private bool? MyIsLiveSorting
	{
		get { return IsShapingActive ? _shapingStorage._isLiveSorting : null; }
		set { EnsureShapingStorage(); _shapingStorage._isLiveSorting = value; }
	}

	private ObservableCollection<string> MyLiveSortingProperties
	{
		get { return IsShapingActive ? _shapingStorage._liveSortingProperties : null; }
		set { EnsureShapingStorage(); _shapingStorage._liveSortingProperties = value; }
	}

	private bool IsLiveSortingSet
	{
		get { return IsShapingActive ? _shapingStorage._isLiveSortingSet : false; }
		set
		{
			Debug.Assert(IsShapingActive, "Shaping storage not available");
			_shapingStorage._isLiveSortingSet = value;
		}
	}

	//private MonitorWrapper LiveSortingMonitor
	//{
	//	get
	//	{
	//		if (_shapingStorage._liveSortingMonitor == null)
	//			_shapingStorage._liveSortingMonitor = new MonitorWrapper();
	//		return _shapingStorage._liveSortingMonitor;
	//	}
	//}


	private bool? MyIsLiveFiltering
	{
		get { return IsShapingActive ? _shapingStorage._isLiveFiltering : null; }
		set { EnsureShapingStorage(); _shapingStorage._isLiveFiltering = value; }
	}

	private ObservableCollection<string> MyLiveFilteringProperties
	{
		get { return IsShapingActive ? _shapingStorage._liveFilteringProperties : null; }
		set { EnsureShapingStorage(); _shapingStorage._liveFilteringProperties = value; }
	}

	private bool IsLiveFilteringSet
	{
		get { return IsShapingActive ? _shapingStorage._isLiveFilteringSet : false; }
		set
		{
			Debug.Assert(IsShapingActive, "Shaping storage not available");
			_shapingStorage._isLiveFilteringSet = value;
		}
	}

	//private MonitorWrapper LiveFilteringMonitor
	//{
	//	get
	//	{
	//		if (_shapingStorage._liveFilteringMonitor == null)
	//			_shapingStorage._liveFilteringMonitor = new MonitorWrapper();
	//		return _shapingStorage._liveFilteringMonitor;
	//	}
	//}


	private bool? MyIsLiveGrouping
	{
		get { return IsShapingActive ? _shapingStorage._isLiveGrouping : null; }
		set { EnsureShapingStorage(); _shapingStorage._isLiveGrouping = value; }
	}

	private ObservableCollection<string> MyLiveGroupingProperties
	{
		get { return IsShapingActive ? _shapingStorage._liveGroupingProperties : null; }
		set { EnsureShapingStorage(); _shapingStorage._liveGroupingProperties = value; }
	}

	private bool IsLiveGroupingSet
	{
		get { return IsShapingActive ? _shapingStorage._isLiveGroupingSet : false; }
		set
		{
			Debug.Assert(IsShapingActive, "Shaping storage not available");
			_shapingStorage._isLiveGroupingSet = value;
		}
	}

	//private MonitorWrapper LiveGroupingMonitor
	//{
	//	get
	//	{
	//		if (_shapingStorage._liveGroupingMonitor == null)
	//			_shapingStorage._liveGroupingMonitor = new MonitorWrapper();
	//		return _shapingStorage._liveGroupingMonitor;
	//	}
	//}

	#endregion Shaping storage

	//------------------------------------------------------
	//
	//  Private Fields
	//
	//------------------------------------------------------

	#region Private Fields

	private CollectionView _internalView;     // direct-mode list and view
	private IEnumerable _itemsSource;           // BnsCustomSourceBaseWidget.ItemsSource property
	private CollectionView _collectionView;        // delegate ICollectionView
	private int _defaultCapacity = 16;

	private bool _isUsingItemsSource;        // true when using ItemsSource
	private bool _isInitializing;            // when true, ItemCollection does not listen to events of _collectionView

	private int _deferLevel;
	private IDisposable _deferInnerRefresh;
	private ShapingStorage _shapingStorage;

	private WeakReference _modelParent;       // use WeakRef to avoid leaking the parent

	#endregion Private Fields


	//------------------------------------------------------
	//
	//  Private Types
	//
	//------------------------------------------------------

	#region Private Types

	// ItemCollection rarely uses shaping directly.   Make it pay-for-play
	private class ShapingStorage
	{
		public bool _isSortingSet;       // true when user has added to this.SortDescriptions
		public bool _isGroupingSet;      // true when user has added to this.GroupDescriptions
		public bool _isLiveSortingSet;   // true when user has added to this.LiveSortingProperties
		public bool _isLiveFilteringSet; // true when user has added to this.LiveFilteringProperties
		public bool _isLiveGroupingSet;  // true when user has added to this.LiveGroupingProperties

		public SortDescriptionCollection _sort;      // storage for SortDescriptions; will forward to _collectionView.SortDescriptions when available
		public Predicate<object> _filter;    // storage for Filter when _collectionView is not available
		public ObservableCollection<GroupDescription> _groupBy; // storage for GroupDescriptions; will forward to _collectionView.GroupDescriptions when available

		public bool? _isLiveSorting;     // true if live Sorting is requested
		public bool? _isLiveFiltering;   // true if live Filtering is requested
		public bool? _isLiveGrouping;    // true if live Grouping is requested

		public ObservableCollection<string> _liveSortingProperties; // storage for LiveSortingProperties; will forward to _collectionView.LiveSortingProperties when available
		public ObservableCollection<string> _liveFilteringProperties; // storage for LiveFilteringProperties; will forward to _collectionView.LiveFilteringProperties when available
		public ObservableCollection<string> _liveGroupingProperties; // storage for LiveGroupingProperties; will forward to _collectionView.LiveGroupingProperties when available

		//public MonitorWrapper _sortDescriptionsMonitor;
		//public MonitorWrapper _groupDescriptionsMonitor;
		//public MonitorWrapper _liveSortingMonitor;
		//public MonitorWrapper _liveFilteringMonitor;
		//public MonitorWrapper _liveGroupingMonitor;
	}

	private class DeferHelper : IDisposable
	{
		public DeferHelper(ItemCollection itemCollection)
		{
			_itemCollection = itemCollection;
		}

		public void Dispose()
		{
			if (_itemCollection != null)
			{
				_itemCollection.EndDefer();
				_itemCollection = null;
			}

			GC.SuppressFinalize(this);
		}

		private ItemCollection _itemCollection;
	}
	#endregion
}

/// <summary>
/// An ItemContainerGenerator is responsible for generating the UI on behalf of
/// its host (e.g. ItemsControl).  It maintains the association between the items in
/// the control's data view and the corresponding
/// UIElements.  The control's item-host can ask the ItemContainerGenerator for
/// a Generator, which does the actual generation of UI.
/// </summary>
public sealed class ItemContainerGenerator : /*IRecyclingItemContainerGenerator, */IWeakEventListener
{
	//------------------------------------------------------
	//
	//  Constructors
	//
	//------------------------------------------------------

	/// <summary> Constructor </summary>
	/// <parameter name="host"> the control that owns the items </parameter>
	internal ItemContainerGenerator(IGeneratorHost host)
		: this(null, host, host as DependencyObject, 0)
	{
		// The top-level generator always listens to changes from ItemsCollection.
		// It needs to get these events before anyone else, so that other listeners
		// can call the generator's mapping functions with correct results.
		CollectionChangedEventManager.AddHandler(host.View, OnCollectionChanged);
	}

	private ItemContainerGenerator(ItemContainerGenerator parent, GroupItem groupItem)
		: this(parent, parent.Host, groupItem, parent.Level + 1)
	{
	}

	private ItemContainerGenerator(ItemContainerGenerator parent, IGeneratorHost host, DependencyObject peer, int level)
	{
		_parent = parent;
		_host = host;
		_peer = peer;
		_level = level;
		OnRefresh();
	}

	//------------------------------------------------------
	//
	//  Public Properties
	//
	//------------------------------------------------------

	/// <summary> The status of the generator </summary>
	public GeneratorStatus Status
	{
		get { return _status; }
	}

	//[CodeAnalysis("AptcaMethodsShouldOnlyCallAptcaMethods")] //Tracking Bug: 29647
	private void SetStatus(GeneratorStatus value)
	{
		if (value != _status)
		{
			_status = value;

			//			switch (_status)
			//			{
			//				case GeneratorStatus.GeneratingContainers:
			//					if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Info))
			//					{
			//						EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientStringBegin, EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Info, "ItemsControl.Generator");
			//						_itemsGenerated = 0;
			//					}
			//					else
			//						_itemsGenerated = Int32.MinValue;
			//#if GENERATOR_TRACE
			//                        _creationTimer.Reset();
			//                        _timer.Begin();
			//#endif
			//					break;

			//				case GeneratorStatus.ContainersGenerated:
			//					string label = null;
			//					if (_itemsGenerated >= 0)   // this implies that tracing is enabled
			//					{
			//						DependencyObject d = Host as DependencyObject;
			//						if (d != null)
			//							label = (string)d.GetValue(FrameworkElement.NameProperty);
			//						if (label == null || label.Length == 0)
			//							label = Host.GetHashCode().ToString(CultureInfo.InvariantCulture);
			//						EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientStringEnd, EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Info,
			//															 string.Create(CultureInfo.InvariantCulture, $"ItemContainerGenerator for {Host.GetType().Name} {label} - {_itemsGenerated} items"));
			//					}
			//#if GENERATOR_TRACE
			//                        _timer.End();
			//                        if (_itemsGenerated > 0)
			//                        {
			//                            Console.WriteLine("Generator for {0} {1}  did {2} items in {3:f2} msec - {4:f2} msec/item",
			//                                Host.GetType().Name, label, _itemsGenerated, _timer.TimeOfLastPeriod, _timer.TimeOfLastPeriod/_itemsGenerated);
			//                            Console.WriteLine("  this excludes time for element creation: {0:f2} msec - {1:f2} msec/item",
			//                                _creationTimer.OverallTimeInMilliseconds, _creationTimer.OverallTimeInMilliseconds/_itemsGenerated);
			//                        }
			//#endif
			//					break;
			//			}

			if (StatusChanged != null)
				StatusChanged(this, EventArgs.Empty);
		}
	}

	/// <summary>
	/// Read-only access to the list of items.
	/// <summary>
	/// <notes>
	/// The returned collection is only valid until the next Refresh.  Users
	/// should not cache a reference to this collection.
	/// </notes>
	public ReadOnlyCollection<object> Items
	{
		get
		{
			//// lazy creation
			//if (_itemsReadOnly == null && _items != null)
			//{
			//	_itemsReadOnly = new ReadOnlyCollection<object>(new List<object>(_items));
			//}

			return _itemsReadOnly;
		}
	}


	//------------------------------------------------------
	//
	//  Public Methods
	//
	//------------------------------------------------------

	#region IItemContainerGenerator

	/// <summary>
	/// Return the ItemContainerGenerator appropriate for use by the given panel
	/// </summary>
	public ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel)
	{
		if (!panel.IsItemsHost)
			throw new ArgumentException("PanelIsNotItemsHost");

		//// if panel came from an ItemsPresenter, use its generator
		//ItemsPresenter ip = ItemsPresenter.FromPanel(panel);
		//if (ip != null)
		//	return ip.Generator;

		// if panel came from a style, use the main generator
		if (panel.TemplatedParent != null)
			return this;

		// otherwise the panel doesn't have a generator
		return null;
	}

	/// <summary> Begin generating at the given position and direction </summary>
	/// <remarks>
	/// This method must be called before calling GenerateNext.  It returns an
	/// IDisposable object that tracks the lifetime of the generation loop.
	/// This method sets the generator's status to GeneratingContent;  when
	/// the IDisposable is disposed, the status changes to ContentReady or
	/// Error, as appropriate.
	/// </remarks>
	public IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction)
	{
		return this.StartAt(position, direction, false);
	}

	/// <summary> Begin generating at the given position and direction </summary>
	/// <remarks>
	/// This method must be called before calling GenerateNext.  It returns an
	/// IDisposable object that tracks the lifetime of the generation loop.
	/// This method sets the generator's status to GeneratingContent;  when
	/// the IDisposable is disposed, the status changes to ContentReady or
	/// Error, as appropriate.
	/// </remarks>
	public IDisposable StartAt(GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem)
	{
		if (_generator != null)
			throw new InvalidOperationException("GenerationInProgress");

		_generator = new Generator(this, position, direction, allowStartAtRealizedItem);
		return _generator;
	}

	public IDisposable GenerateBatches()
	{
		if (_isGeneratingBatches)
			throw new InvalidOperationException("GenerationInProgress");

		return new BatchGenerator(this);
	}

	public DependencyObject GenerateNext()
	{
		bool isNewlyRealized;
		if (_generator == null)
			throw new InvalidOperationException("GenerationNotInProgress");

		return _generator.GenerateNext(true, out isNewlyRealized);
	}

	public DependencyObject GenerateNext(out bool isNewlyRealized)
	{
		if (_generator == null)
			throw new InvalidOperationException("GenerationNotInProgress");

		return _generator.GenerateNext(false, out isNewlyRealized);
	}

	/// <summary>
	/// Prepare the given element to act as the container for the
	/// corresponding item.  This includes applying the container style,
	/// forwarding information from the host control (ItemTemplate, etc.),
	/// and other small adjustments.
	/// </summary>
	/// <remarks>
	/// This method must be called after the element has been added to the
	/// visual tree, so that resource references and inherited properties
	/// work correctly.
	/// </remarks>
	/// <param name="container"> The container to prepare.
	/// Normally this is the result of the previous call to GenerateNext.
	/// </param>
	public void PrepareItemContainer(DependencyObject container)
	{
		object item = container.ReadLocalValue(ItemForItemContainerProperty);
		Host.PrepareItemContainer(container, item);
	}

	/// <summary>
	/// Remove generated elements.
	/// </summary>
	public void Remove(GeneratorPosition position, int count)
	{
		Remove(position, count, /*isRecycling = */ false);
	}

	/// <summary>
	/// Remove generated elements.
	/// </summary>
	private void Remove(GeneratorPosition position, int count, bool isRecycling)
	{
		if (position.Offset != 0)
			throw new ArgumentException("RemoveRequiresOffsetZero");
		if (count <= 0)
			throw new ArgumentException("RemoveRequiresPositiveCount");

		if (_itemMap == null)
		{
			// ignore reentrant call (during RemoveAllInternal)
			Debug.Assert(false, "Unexpected reentrant call to ICG.Remove");
			return;
		}

		int index = position.Index;
		ItemBlock block;

		// find the leftmost item to remove
		int offsetL = index;
		for (block = _itemMap.Next; block != _itemMap; block = block.Next)
		{
			if (offsetL < block.ContainerCount)
				break;

			offsetL -= block.ContainerCount;
		}
		RealizedItemBlock blockL = block as RealizedItemBlock;

		// find the rightmost item to remove
		int offsetR = offsetL + count - 1;
		for (; block != _itemMap; block = block.Next)
		{
			if (!(block is RealizedItemBlock))
				throw new InvalidOperationException("CannotRemoveUnrealizedItems");

			if (offsetR < block.ContainerCount)
				break;

			offsetR -= block.ContainerCount;
		}
		RealizedItemBlock blockR = block as RealizedItemBlock;

		// de-initialize the containers that are being removed
		RealizedItemBlock rblock = blockL;
		int offset = offsetL;
		while (rblock != blockR || offset <= offsetR)
		{
			DependencyObject container = rblock.ContainerAt(offset);

			UnlinkContainerFromItem(container, rblock.ItemAt(offset));
			// DataGrid generates non-GroupItem for NewItemPlaceHolder
			// Dont recycle in this case.
			bool isNewItemPlaceHolderWhenGrouping = _generatesGroupItems && !(container is GroupItem);

			if (isRecycling && !isNewItemPlaceHolderWhenGrouping)
			{
				Debug.Assert(!_recyclableContainers.Contains(container), "trying to add a container to the collection twice");

				if (_containerType == null)
				{
					_containerType = container.GetType();
				}
				else if (_containerType != container.GetType())
				{
					throw new InvalidOperationException("CannotRecyleHeterogeneousTypes");
				}

				_recyclableContainers.Enqueue(container);
			}

			if (++offset >= rblock.ContainerCount && rblock != blockR)
			{
				rblock = rblock.Next as RealizedItemBlock;
				offset = 0;
			}
		}

		// see whether the range hits the edge of a block on either side,
		// and whether the a`butting block is an unrealized gap
		bool edgeL = (offsetL == 0);
		bool edgeR = (offsetR == blockR.ItemCount - 1);
		bool abutL = edgeL && (blockL.Prev is UnrealizedItemBlock);
		bool abutR = edgeR && (blockR.Next is UnrealizedItemBlock);

		// determine the target (unrealized) block,
		// the offset within the target at which to insert items,
		// and the intial change in cumulative item count
		UnrealizedItemBlock blockT;
		ItemBlock predecessor = null;
		int offsetT;
		int deltaCount;

		if (abutL)
		{
			blockT = (UnrealizedItemBlock)blockL.Prev;
			offsetT = blockT.ItemCount;
			deltaCount = -blockT.ItemCount;
		}
		else if (abutR)
		{
			blockT = (UnrealizedItemBlock)blockR.Next;
			offsetT = 0;
			deltaCount = offsetL;
		}
		else
		{
			blockT = new UnrealizedItemBlock();
			offsetT = 0;
			deltaCount = offsetL;

			// remember where the new block goes, so we can insert it later
			predecessor = (edgeL) ? blockL.Prev : blockL;
		}

		// move items within the range to the target block
		for (block = blockL; block != blockR; block = block.Next)
		{
			int itemCount = block.ItemCount;
			MoveItems(block, offsetL, itemCount - offsetL,
						blockT, offsetT, deltaCount);
			offsetT += itemCount - offsetL;
			offsetL = 0;
			deltaCount -= itemCount;
			if (block.ItemCount == 0)
				block.Remove();
		}

		// the last block in the range is a little special...
		// Move the last unrealized piece.
		int remaining = block.ItemCount - 1 - offsetR;
		MoveItems(block, offsetL, offsetR - offsetL + 1,
					blockT, offsetT, deltaCount);

		// Move the remaining realized items
		RealizedItemBlock blockX = blockR;
		if (!edgeR)
		{
			if (blockL == blockR && !edgeL)
			{
				blockX = new RealizedItemBlock();
			}

			MoveItems(block, offsetR + 1, remaining,
						blockX, 0, offsetR + 1);
		}

		// if we created any new blocks, insert them in the list
		if (predecessor != null)
			blockT.InsertAfter(predecessor);
		if (blockX != blockR)
			blockX.InsertAfter(blockT);

		RemoveAndCoalesceBlocksIfNeeded(block);
	}

	/// <summary>
	/// Remove all generated elements.
	/// </summary>
	public void RemoveAll()
	{
		RemoveAllInternal(false /*saveRecycleQueue*/);
	}

	internal void RemoveAllInternal(bool saveRecycleQueue)
	{
		// Take _itemMap offline, to protect against reentrancy (bug 1285179)
		ItemBlock itemMap = _itemMap;
		_itemMap = null;

		try
		{
			// de-initialize the containers that are being removed
			if (itemMap != null)
			{
				for (ItemBlock block = itemMap.Next; block != itemMap; block = block.Next)
				{
					RealizedItemBlock rib = block as RealizedItemBlock;
					if (rib != null)
					{
						for (int offset = 0; offset < rib.ContainerCount; ++offset)
						{
							UnlinkContainerFromItem(rib.ContainerAt(offset), rib.ItemAt(offset));
						}
					}
				}
			}
		}
		finally
		{
			PrepareGrouping();

			// re-initialize the data structure
			_itemMap = new ItemBlock();
			_itemMap.Prev = _itemMap.Next = _itemMap;

			UnrealizedItemBlock uib = new UnrealizedItemBlock();
			uib.InsertAfter(_itemMap);
			uib.ItemCount = ItemsInternal.Count;

			if (!saveRecycleQueue)
			{
				ResetRecyclableContainers();
			}

			SetAlternationCount();

			// tell generators what happened
			if (MapChanged != null)
			{
				MapChanged(null, -1, 0, uib, 0, 0);
			}
		}
	}

	private void ResetRecyclableContainers()
	{
		_recyclableContainers = new Queue<DependencyObject>();
		_containerType = null;
		_generatesGroupItems = false;
	}

	public void Recycle(GeneratorPosition position, int count)
	{
		Remove(position, count, /*isRecyling = */ true);
	}

	/// <summary>
	/// Map an index into the items collection to a GeneratorPosition.
	/// </summary>
	public GeneratorPosition GeneratorPositionFromIndex(int itemIndex)
	{
		GeneratorPosition position;
		ItemBlock itemBlock;
		int offsetFromBlockStart;

		GetBlockAndPosition(itemIndex, out position, out itemBlock, out offsetFromBlockStart);

		if (itemBlock == _itemMap && position.Index == -1)
			++position.Offset;

		return position;
	}

	/// <summary>
	/// Map a GeneratorPosition to an index into the items collection.
	/// </summary>
	public int IndexFromGeneratorPosition(GeneratorPosition position)
	{
		int index = position.Index;

		if (index == -1)
		{
			// offset is relative to the fictitious boundary item
			if (position.Offset >= 0)
			{
				return position.Offset - 1;
			}
			else
			{
				return ItemsInternal.Count + position.Offset;
			}
		}

		if (_itemMap != null)
		{
			int itemIndex = 0;      // number of items we've skipped over

			// locate container at the given index
			for (ItemBlock block = _itemMap.Next; block != _itemMap; block = block.Next)
			{
				if (index < block.ContainerCount)
				{
					// container is within this block.  return the answer
					return itemIndex + index + position.Offset;
				}
				else
				{
					// skip over this block
					itemIndex += block.ItemCount;
					index -= block.ContainerCount;
				}
			}
		}

		return -1;
	}

	#endregion IItemContainerGenerator

	/// <summary>
	/// Return the item corresponding to the given UI element.
	/// If the element was not generated as a container for this generator's
	/// host, the method returns DependencyProperty.UnsetValue.
	/// </summary>
	public object ItemFromContainer(DependencyObject container)
	{
		ArgumentNullException.ThrowIfNull(container);

		object item = container.ReadLocalValue(ItemForItemContainerProperty);

		if (item != DependencyProperty.UnsetValue)
		{
			// verify that the element really belongs to the host
			if (!Host.IsHostForItemContainer(container))
				item = DependencyProperty.UnsetValue;
		}

		return item;
	}

	/// <summary>
	/// Return the UI element corresponding to the given item.
	/// Returns null if the item does not belong to the item collection,
	/// or if no UI has been generated for it.
	/// </summary>
	public DependencyObject ContainerFromItem(object item)
	{
		object dummy;
		DependencyObject container;
		int index;

		DoLinearSearch(
			static (state, o, d) => ControlHelpers.EqualsEx(o, state), item,
			out dummy, out container, out index, false);

		return container;
	}

	/// <summary>
	/// Given a generated UI element, return the index of the corresponding item
	/// within the ItemCollection.
	/// </summary>
	public int IndexFromContainer(DependencyObject container)
	{
		return IndexFromContainer(container, false);
	}

	/// <summary>
	/// Given a generated UI element, return the index of the corresponding item
	/// within the ItemCollection.
	/// </summary>
	public int IndexFromContainer(DependencyObject container, bool returnLocalIndex)
	{
		ArgumentNullException.ThrowIfNull(container);

		int index;
		object item;
		DependencyObject dummy;

		DoLinearSearch(
			static (state, o, d) => d == state, container,
			out item, out dummy, out index, returnLocalIndex);

		return index;
	}

	// expose DoLinearSearch to internal code
	internal bool FindItem<TState>(Func<TState, object, DependencyObject, bool> match, TState matchState,
			out DependencyObject container, out int itemIndex)
	{
		return DoLinearSearch(match, matchState, out _, out container, out itemIndex, false);
	}

	/// <summary>
	///     Performs a linear search for an (item, container) pair that
	///     matches a given predicate.
	/// </summary>
	/// <remarks>
	///     There's no avoiding a linear search, which leads to O(n^2) performance
	///     if someone calls ContainerFromItem or IndexFromContainer for every item.
	///     To mitigate this, we start each search at _startIndexForUIFromItem, and
	///     heuristically set this in various places to where we expect the next
	///     call to occur.
	///
	///     For example, after a successul search, we set it to the resulting
	///     index, hoping that the next call will query either the same item or
	///     the one after it.  And after inserting a new item, we expect a query
	///     about the new item.  Etc.
	///
	///     Saving this as an index instead of a (block, offset) pair, makes it
	///     more robust during insertions/deletions.  If the index ends up being
	///     wrong, the worst that happens is a full search (as opposed to following
	///     a reference to a block that's no longer in use).
	///
	///     To re-use the search code for two methods, please read the description
	///     of the parameters.
	/// </remarks>
	/// <param name="match">
	///     The predicate with which to test each (item, container).
	/// </param>
	/// <param name="returnLocalIndex">
	///     If true, only search at the current level and return an index
	///         in local coordinates (w.r.t. the current level).
	///     If false, search subgroups, and return an index in global coordinates.
	/// </param>
	/// <param name="item">
	///     The matching item, or null
	/// </param>
	/// <param name="container">
	///     The matching container, or null
	/// </param>
	/// <param name="itemIndex">
	///     The index of the matching pair, or -1
	/// </param>
	/// <returns>
	///     true if found, false otherwise.
	/// </returns>
	private bool DoLinearSearch<TState>(Func<TState, object, DependencyObject, bool> match, TState matchState,
			out object item, out DependencyObject container, out int itemIndex,
			bool returnLocalIndex)
	{
		item = null;
		container = null;
		itemIndex = 0;

		if (_itemMap == null)
		{
			// _itemMap can be null if we re-enter the generator.  Scenario:  user calls RemoveAll(), we Unlink every container, fire
			// ClearContainerForItem for each, and someone overriding ClearContainerForItem decides to look up the container.
			goto NotFound;
		}

		// Move to the starting point of the search
		ItemBlock startBlock = _itemMap.Next;
		int index = 0;      // index of first item in current block
		RealizedItemBlock rib;
		int startOffset;

		while (index <= _startIndexForUIFromItem && startBlock != _itemMap)
		{
			index += startBlock.ItemCount;
			startBlock = startBlock.Next;
		}
		startBlock = startBlock.Prev;
		index -= startBlock.ItemCount;
		rib = startBlock as RealizedItemBlock;

		if (rib != null)
		{
			startOffset = _startIndexForUIFromItem - index;
			if (startOffset >= rib.ItemCount)
			{
				// we can get here if items get removed since the last
				// time we saved _startIndexForUIFromItem - so the
				// saved offset is no longer meaningful.  To make the
				// search work, we need to make sure the first loop
				// does at least one iteration.  Setting startOffset to 0
				// does exactly that.
				startOffset = 0;
			}
		}
		else
		{
			startOffset = 0;
		}

		// search for the desired item, wrapping around the end
		ItemBlock block = startBlock;
		int offset = startOffset;
		int endOffset = startBlock.ItemCount;
		while (true)
		{
			// search the current block (only need to search realized blocks)
			if (rib != null)
			{
				//for (; offset < endOffset; ++offset)
				//{
				//	CollectionViewGroup group;
				//	bool found = match(matchState, rib.ItemAt(offset), rib.ContainerAt(offset));

				//	if (found)
				//	{
				//		item = rib.ItemAt(offset);
				//		container = rib.ContainerAt(offset);
				//	}
				//	else if (!returnLocalIndex && IsGrouping && ((group = rib.ItemAt(offset) as CollectionViewGroup) != null))
				//	{
				//		// found a group;  see if the group contains the item
				//		GroupItem groupItem = (GroupItem)rib.ContainerAt(offset);
				//		int indexInGroup;
				//		found = groupItem.Generator.DoLinearSearch(match, matchState, out item, out container, out indexInGroup, false);
				//		if (found)
				//		{
				//			itemIndex = indexInGroup;
				//		}
				//	}

				//	if (found)
				//	{
				//		// found the item;  update state and return
				//		_startIndexForUIFromItem = index + offset;
				//		itemIndex += GetRealizedItemBlockCount(rib, offset, returnLocalIndex) + GetCount(block, returnLocalIndex);
				//		return true;
				//	}
				//}

				// check for termination
				if (block == startBlock && offset == startOffset)
				{
					break;  // not found
				}
			}

			// advance to next block
			index += block.ItemCount;
			offset = 0;
			block = block.Next;

			// if we've reached the end, wrap around
			if (block == _itemMap)
			{
				block = block.Next;
				index = 0;
			}

			// prepare to search the block
			endOffset = block.ItemCount;
			rib = block as RealizedItemBlock;

			// check for termination
			if (block == startBlock)
			{
				if (rib != null)
				{
					endOffset = startOffset;    // search first part of block
				}
				else
				{
					break;  // not found
				}
			}
		}

		NotFound:
		itemIndex = -1;
		item = null;
		container = null;
		return false;
	}

	private int GetCount()
	{
		return GetCount(_itemMap);
	}

	private int GetCount(ItemBlock stop)
	{
		return GetCount(stop, false);
	}

	private int GetCount(ItemBlock stop, bool returnLocalIndex)
	{
		if (_itemMap == null)
		{
			// handle reentrant call
			return 0;
		}

		int count = 0;
		ItemBlock start = _itemMap;
		ItemBlock block = start.Next;

		while (block != stop)
		{
			count += block.ItemCount;
			block = block.Next;
		}

		if (!returnLocalIndex && IsGrouping)
		{
			int n = count;
			count = 0;

			for (int i = 0; i < n; ++i)
			{
				CollectionViewGroup group = Items[i] as CollectionViewGroup;
				count += (group == null) ? 1 : group.ItemCount;
			}
		}

		return count;
	}

	private int GetRealizedItemBlockCount(RealizedItemBlock rib, int end, bool returnLocalIndex)
	{
		if (!IsGrouping || returnLocalIndex)
		{
			// when the UI is not grouping, each item counts as 1, even
			// groups (bug 1761421)
			return end;
		}

		int count = 0;

		for (int offset = 0; offset < end; ++offset)
		{
			CollectionViewGroup group;
			if ((group = rib.ItemAt(offset) as CollectionViewGroup) != null)
			{
				// found a group, count the group
				count += group.ItemCount;
			}
			else
			{
				count++;
			}
		}

		return count;
	}

	/// <summary>
	/// Return the UI element corresponding to the item at the given index
	/// within the ItemCollection.
	/// </summary>
	public DependencyObject ContainerFromIndex(int index)
	{
		if (_itemMap == null)
		{
			// handle reentrant call
			return null;
		}

#if DEBUG
		object target = (Parent == null) && (0 <= index && index < Host.View.Count) ? Host.View[index] : null;
#endif
		int subIndex = 0;

		// if we're grouping, determine the appropriate child
		if (IsGrouping)
		{
			int n;
			subIndex = index;
			for (index = 0, n = ItemsInternal.Count; index < n; ++index)
			{
				CollectionViewGroup group = ItemsInternal[index] as CollectionViewGroup;
				int size = (group == null) ? 1 : group.ItemCount;

				if (subIndex < size)
					break;
				else
					subIndex -= size;
			}
		}

		// search the table for the item

		for (ItemBlock block = _itemMap.Next; block != _itemMap; block = block.Next)
		{
			if (index < block.ItemCount)
			{
				DependencyObject container = block.ContainerAt(index);
				GroupItem groupItem = container as GroupItem;

				if (groupItem != null)
				{
					//container = groupItem.Generator.ContainerFromIndex(subIndex);
				}
#if DEBUG
				object item = (Parent == null) && (container != null) ?
							container.ReadLocalValue(ItemForItemContainerProperty) : null;
				Debug.Assert(item == null || ControlHelpers.EqualsEx(item, target),
					"Generator's data structure is corrupt - ContainerFromIndex found wrong item");
#endif
				return container;
			}

			index -= block.ItemCount;
		}

		return null;  // *not* throw new IndexOutOfRangeException(); - bug 890195
	}


	//------------------------------------------------------
	//
	//  Public Events
	//
	//------------------------------------------------------

	/// <summary>
	/// The ItemsChanged event is raised by a ItemContainerGenerator to inform
	/// layouts that the items collection has changed.
	/// </summary>
	public event ItemsChangedEventHandler ItemsChanged;

	/// <summary>
	/// The StatusChanged event is raised by a ItemContainerGenerator to inform
	/// controls that its status has changed.
	/// </summary>
	public event EventHandler StatusChanged;


	//------------------------------------------------------
	//
	//  Internal methods
	//
	//------------------------------------------------------

	// ItemsControl sometimes needs access to the recyclable containers.
	// For eg. DataGrid needs to mark recyclable containers dirty for measure when DataGridColumn.Visibility changes.
	internal IEnumerable RecyclableContainers
	{
		get
		{
			return _recyclableContainers;
		}
	}

	// regenerate everything
	internal void Refresh()
	{
		OnRefresh();
	}

	// called when this generator is no longer needed
	internal void Release()
	{
		this.RemoveAll();
	}

	// called when GenerateNext returns null when the caller wasn't expecting null.
	// This is a clue that the underlying collection or collection-view may
	// have raised the wrong CollectionChange events.  If there's evidence
	// that this has happened, throw an exception.
	internal void Verify()
	{
		if (_itemMap == null)
			return;

		//List<string> errors = new List<string>();

		//// compute accumulated count = sum of block counts
		//int accumulatedCount = 0;
		//for (ItemBlock block = _itemMap.Next; block != _itemMap; block = block.Next)
		//{
		//	accumulatedCount += block.ItemCount;
		//}

		//// compare accumulated count to actual count
		//if (accumulatedCount != _items.Count)
		//{
		//	errors.Add(SR.Format(SR.Generator_CountIsWrong, accumulatedCount, _items.Count));
		//}

		//// compare items
		//int badItems = 0, reportedItems = 0;
		//int blockIndex = 0;
		//for (ItemBlock block = _itemMap.Next; block != _itemMap; block = block.Next)
		//{
		//	RealizedItemBlock rib = block as RealizedItemBlock;
		//	if (rib != null)
		//	{
		//		for (int offset = 0; offset < rib.ItemCount; ++offset)
		//		{
		//			int index = blockIndex + offset;
		//			object genItem = rib.ItemAt(offset);
		//			object actualItem = (index < _items.Count) ? _items[index] : null;
		//			if (!ItemsControl.EqualsEx(genItem, actualItem))
		//			{
		//				if (reportedItems < 3)
		//				{
		//					errors.Add(SR.Format(SR.Generator_ItemIsWrong, index, genItem, actualItem));
		//					++reportedItems;
		//				}
		//				++badItems;
		//			}
		//		}
		//	}
		//	blockIndex += block.ItemCount;
		//}

		//if (badItems > reportedItems)
		//{
		//	errors.Add(SR.Format(SR.Generator_MoreErrors, badItems - reportedItems));
		//}

		//// if we found errors, throw an exception
		//if (errors.Count > 0)
		//{
		//	CultureInfo enUS = System.Windows.Markup.TypeConverterHelper.InvariantEnglishUS;

		//	// get the identifying information for the ItemsControl
		//	DependencyObject peer = Peer;
		//	string name = (String)peer.GetValue(FrameworkElement.NameProperty);
		//	if (String.IsNullOrWhiteSpace(name))
		//	{
		//		name = SR.Generator_Unnamed;
		//	}

		//	// get the sources involved in CollectionChanged events
		//	List<string> sources = new List<string>();
		//	GetCollectionChangedSources(0, FormatCollectionChangedSource, sources);

		//	// describe the details of the problem
		//	StringBuilder sb = new StringBuilder();
		//	sb.AppendLine(SR.Generator_Readme0);                          // Developer info:
		//	sb.Append(SR.Format(SR.Generator_Readme1, peer, name));              // The exception is thrown because...
		//	sb.Append("  ");
		//	sb.AppendLine(SR.Generator_Readme2);                          // The following differences...
		//	foreach (string s in errors)
		//	{
		//		sb.Append(enUS, $"  {s}");
		//		sb.AppendLine();
		//	}
		//	sb.AppendLine();

		//	sb.AppendLine(SR.Generator_Readme3);                          // The following sources...
		//	foreach (string s in sources)
		//	{
		//		sb.Append(enUS, $"  {s}");
		//		sb.AppendLine();
		//	}
		//	sb.AppendLine(SR.Generator_Readme4);                          // Starred sources are considered more likely
		//	sb.AppendLine();

		//	sb.AppendLine(SR.Generator_Readme5);                          // The most common causes...
		//	sb.AppendLine();

		//	sb.Append(SR.Generator_Readme6); sb.Append("  ");         // Stack trace describes detection...
		//	sb.Append(SR.Format(SR.Generator_Readme7,                            // To get better detection...
		//					"PresentationTraceSources.TraceLevel", "High"));
		//	sb.Append("  ");
		//	sb.AppendLine(SR.Format(SR.Generator_Readme8,                            // One way to do this ...
		//					"System.Diagnostics.PresentationTraceSources.SetTraceLevel(myItemsControl.ItemContainerGenerator, System.Diagnostics.PresentationTraceLevel.High)"));
		//	sb.AppendLine(SR.Generator_Readme9);                          // This slows down the app.

		//	// use an inner exception to hold the details.  There's a lot of
		//	// information, but it's only interesting to a developer.
		//	Exception exception = new Exception(sb.ToString());

		//	// throw the exception
		//	throw new InvalidOperationException(SR.Generator_Inconsistent, exception);
		//}
	}

	void FormatCollectionChangedSource(int level, object source, bool? isLikely, List<string> sources)
	{
		Type sourceType = source.GetType();

		//if (!isLikely.HasValue)
		//{
		//	// if the type doesn't come from WPF or DevDiv (e.g. ObservableCollection<T>),
		//	// mark it as "more likely to be at fault".   I'm not saying we're always right,
		//	// just that 3rd parties are more likely to be wrong than we are.
		//	isLikely = true;

		//	const string PublicKeyToken = "PublicKeyToken=";
		//	string aqn = sourceType.AssemblyQualifiedName;
		//	int index = aqn.LastIndexOf(PublicKeyToken);
		//	if (index >= 0)
		//	{
		//		ReadOnlySpan<char> token = aqn.AsSpan(index + PublicKeyToken.Length);
		//		if (token.Equals(MS.Internal.PresentationFramework.BuildInfo.WCP_PUBLIC_KEY_TOKEN, StringComparison.OrdinalIgnoreCase) ||
		//			token.Equals(MS.Internal.PresentationFramework.BuildInfo.DEVDIV_PUBLIC_KEY_TOKEN, StringComparison.OrdinalIgnoreCase))
		//		{
		//			isLikely = false;
		//		}
		//	}
		//}

		//char c = (isLikely == true) ? '*' : ' ';
		//string indent = new String(' ', level);
		//sources.Add(String.Format(System.Windows.Markup.TypeConverterHelper.InvariantEnglishUS, "{0} {1} {2}",
		//							c, indent, sourceType.FullName));
	}

	void GetCollectionChangedSources(int level, Action<int, object, bool?, List<string>> format, List<string> sources)
	{
		format(level, this, false, sources);
		//Host.View.GetCollectionChangedSources(level + 1, format, sources);
	}

	// called when the host's AlternationCount changes
	internal void ChangeAlternationCount()
	{
		if (_itemMap == null)
		{
			// handle reentrant call
			return;
		}

		// update my AlternationCount and adjust my containers
		SetAlternationCount();

		// propagate to subgroups, if necessary
		if (IsGrouping && GroupStyle != null)
		{
			ItemBlock block = _itemMap.Next;
			while (block != _itemMap)
			{
				for (int offset = 0; offset < block.ContainerCount; ++offset)
				{
					GroupItem gi = ((RealizedItemBlock)block).ContainerAt(offset) as GroupItem;
					if (gi != null)
					{
						//gi.Generator.ChangeAlternationCount();
					}
				}

				block = block.Next;
			}
		}
	}

	// update AlternationIndex on each container to reflect the new AlternationCount
	void ChangeAlternationCount(int newAlternationCount)
	{
		if (_alternationCount == newAlternationCount)
			return;

		// find the first realized container (need this regardless of what happens)
		ItemBlock block = _itemMap.Next;
		int offset = 0;
		while (offset == block.ContainerCount)
		{
			block = block.Next;
		}

		// if there are no realized containers, there's nothing to do
		if (block != _itemMap)
		{
			// if user is requesting alternation, reset each container's AlternationIndex
			if (newAlternationCount > 0)
			{
				_alternationCount = newAlternationCount;
				SetAlternationIndex((RealizedItemBlock)block, offset, GeneratorDirection.Forward);
			}
			// otherwise, clear each container's AlternationIndex
			else if (_alternationCount > 0)
			{
				while (block != _itemMap)
				{
					for (offset = 0; offset < block.ContainerCount; ++offset)
					{
						BnsCustomSourceBaseWidget.ClearAlternationIndex(((RealizedItemBlock)block).ContainerAt(offset));
					}

					block = block.Next;
				}
			}
		}

		_alternationCount = newAlternationCount;
	}

	//------------------------------------------------------
	//
	//  Internal properties
	//
	//------------------------------------------------------

	internal ItemContainerGenerator Parent
	{
		get { return _parent; }
	}

	internal int Level
	{
		get { return _level; }
	}

	// The group style that governs the generation of UI for the items.
	internal GroupStyle GroupStyle
	{
		get { return _groupStyle; }
		set
		{
			if (_groupStyle != value)
			{
				if (_groupStyle is INotifyPropertyChanged)
				{
					PropertyChangedEventManager.RemoveHandler(_groupStyle, OnGroupStylePropertyChanged, String.Empty);
				}

				_groupStyle = value;

				if (_groupStyle is INotifyPropertyChanged)
				{
					PropertyChangedEventManager.AddHandler(_groupStyle, OnGroupStylePropertyChanged, String.Empty);
				}
			}
		}
	}

	// The collection of items, as IList
	internal IList ItemsInternal
	{
		get { return _items; }
		set
		{
			if (_items != value)
			{
				INotifyCollectionChanged incc = _items as INotifyCollectionChanged;
				if (_items != Host.View && incc != null)
				{
					CollectionChangedEventManager.RemoveHandler(incc, OnCollectionChanged);
				}

				_items = value;
				_itemsReadOnly = null;

				incc = _items as INotifyCollectionChanged;
				if (_items != Host.View && incc != null)
				{
					CollectionChangedEventManager.AddHandler(incc, OnCollectionChanged);
				}
			}
		}
	}

	/// <summary>
	///     ItemForItemContainer DependencyProperty
	/// </summary>
	// This is an attached property that the generator sets on each container
	// (generated or direct) to point back to the item.
	internal static readonly DependencyProperty ItemForItemContainerProperty =
			DependencyProperty.RegisterAttached("ItemForItemContainer", typeof(object), typeof(ItemContainerGenerator),
										new FrameworkPropertyMetadata((object)null));

	//------------------------------------------------------
	//
	//  Internal events
	//
	//------------------------------------------------------

	internal event EventHandler PanelChanged;

	internal void OnPanelChanged()
	{
		if (PanelChanged != null)
			PanelChanged(this, EventArgs.Empty);
	}

	//------------------------------------------------------
	//
	//  Private Nested Class -  ItemContainerGenerator.Generator
	//
	//------------------------------------------------------


	/// <summary>
	///     Generator is the object that generates UI on behalf of an ItemsControl,
	///     working under the supervision of an ItemContainerGenerator.
	/// </summary>
	private class Generator : IDisposable
	{
		//------------------------------------------------------
		//
		//  Constructors
		//
		//------------------------------------------------------

		internal Generator(ItemContainerGenerator factory, GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem)
		{
			_factory = factory;
			_direction = direction;

			_factory.MapChanged += new MapChangedHandler(OnMapChanged);

			_factory.MoveToPosition(position, direction, allowStartAtRealizedItem, ref _cachedState);
			_done = (_factory.ItemsInternal.Count == 0);

			_factory.SetStatus(GeneratorStatus.GeneratingContainers);
		}

		//------------------------------------------------------
		//
		//  Public Properties
		//
		//------------------------------------------------------

		/* This method was requested for virtualization.  It's not being used right now
		(bug 1079525) but it probably will be when UI virtualization comes back.
					/// <summary>
					/// returns false if a call to GenerateNext is known to return null (indicating
					/// that the generator is done).  Does not generate anything or change the
					/// generator's state;  cheaper than GenerateNext.  Returning true does not
					/// necessarily mean GenerateNext will produce anything.
					/// </summary>
					public bool IsActive
					{
						get { return !_done; }
					}
		*/

		//------------------------------------------------------
		//
		//  Public Methods
		//
		//------------------------------------------------------

		/// <summary> Generate UI for the next item or group</summary>
		public DependencyObject GenerateNext(bool stopAtRealized, out bool isNewlyRealized)
		{
			DependencyObject container = null;
			isNewlyRealized = false;

			while (container == null)
			{
				UnrealizedItemBlock uBlock = _cachedState.Block as UnrealizedItemBlock;
				IList items = _factory.ItemsInternal;
				int itemIndex = _cachedState.ItemIndex;
				int incr = (_direction == GeneratorDirection.Forward) ? +1 : -1;

				if (_cachedState.Block == _factory._itemMap)
					_done = true;            // we've reached the end of the list

				if (uBlock == null && stopAtRealized)
					_done = true;

				if (!(0 <= itemIndex && itemIndex < items.Count))
					_done = true;

				if (_done)
				{
					isNewlyRealized = false;
					return null;
				}

				object item = items[itemIndex];

				if (uBlock != null)
				{
					// We don't have a realized container for this item.  Try to use a recycled container
					// if possible, otherwise generate a new container.

					isNewlyRealized = true;
					CollectionViewGroup group = item as CollectionViewGroup;

					// DataGrid needs to generate DataGridRows for special items like NewItemPlaceHolder and when adding a new row.
					// Generate a new container for such cases.
					bool isNewItemPlaceHolderWhenGrouping = (_factory._generatesGroupItems && group == null);

					if (_factory._recyclableContainers.Count > 0 && !_factory.Host.IsItemItsOwnContainer(item) && !isNewItemPlaceHolderWhenGrouping)
					{
						container = _factory._recyclableContainers.Dequeue();
						isNewlyRealized = false;
					}
					else
					{
						if (group == null || !_factory.IsGrouping)
						{
							// generate container for an item
							container = _factory.Host.GetContainerForItem(item);
						}
						else
						{
							// generate container for a group
							container = _factory.ContainerForGroup(group);
						}
					}

					// add the (item, container) to the current block
					if (container != null)
					{
						ItemContainerGenerator.LinkContainerToItem(container, item);

						_factory.Realize(uBlock, _cachedState.Offset, item, container);

						// set AlternationIndex on the container (and possibly others)
						_factory.SetAlternationIndex(_cachedState.Block, _cachedState.Offset, _direction);
					}
				}
				else
				{
					// return existing realized container
					isNewlyRealized = false;
					RealizedItemBlock rib = (RealizedItemBlock)_cachedState.Block;
					container = rib.ContainerAt(_cachedState.Offset);
				}

				// advance to the next item
				_cachedState.ItemIndex = itemIndex;
				if (_direction == GeneratorDirection.Forward)
				{
					_cachedState.Block.MoveForward(ref _cachedState, true);
				}
				else
				{
					_cachedState.Block.MoveBackward(ref _cachedState, true);
				}
			}

			return container;
		}

		//------------------------------------------------------
		//
		//  Interfaces - IDisposable
		//
		//------------------------------------------------------

		/// <summary> Dispose this generator. </summary>
		void IDisposable.Dispose()
		{
			if (_factory != null)
			{
				_factory.MapChanged -= new MapChangedHandler(OnMapChanged);
				_done = true;
				if (!_factory._isGeneratingBatches)
				{
					_factory.SetStatus(GeneratorStatus.ContainersGenerated);
				}
				_factory._generator = null;
				_factory = null;
			}

			GC.SuppressFinalize(this);
		}

		//------------------------------------------------------
		//
		//  Private methods
		//
		//------------------------------------------------------

		// The map data structure has changed, so the state must change accordingly.
		// This is called in various different ways.
		//  A. Items were moved within the data structure, typically because
		//  items were realized or un-realized.  In this case, the args are:
		//      block - the block from where the items were moved
		//      offset - the offset within the block of the first item moved
		//      count - how many items moved
		//      newBlock - the block to which the items were moved
		//      newOffset - the offset within the new block of the first item moved
		//      deltaCount - the difference between the cumululative item counts
		//                  of newBlock and block
		//  B. An item was added or removed from the data structure.  In this
		//  case the args are:
		//      block - null  (to distinguish case B from case A)
		//      offset - the index of the changed item, w.r.t. the entire item list
		//      count - +1 for insertion, -1 for deletion
		//      newBlock - block where item was inserted (null for deletion)
		//  C. Refresh: all items are returned to a single unrealized block.
		//  In this case, the args are:
		//      block - null
		//      offset - -1 (to distinguish case C from case B)
		//      newBlock = the single unrealized block
		//      others - unused
		void OnMapChanged(ItemBlock block, int offset, int count,
						ItemBlock newBlock, int newOffset, int deltaCount)
		{
			// Case A.  Items were moved within the map data structure
			if (block != null)
			{
				// if the move affects this generator, update the cached state
				if (block == _cachedState.Block && offset <= _cachedState.Offset &&
					_cachedState.Offset < offset + count)
				{
					_cachedState.Block = newBlock;
					_cachedState.Offset += newOffset - offset;
					_cachedState.Count += deltaCount;
				}
			}
			// Case B.  An item was inserted or deleted
			else if (offset >= 0)
			{
				// if the item occurs before my block, update my item count
				if (offset < _cachedState.Count ||
					(offset == _cachedState.Count && newBlock != null && newBlock != _cachedState.Block))
				{
					_cachedState.Count += count;
					_cachedState.ItemIndex += count;
				}
				// if the item occurs within my block before my item, update my offset
				else if (offset < _cachedState.Count + _cachedState.Offset)
				{
					_cachedState.Offset += count;
					_cachedState.ItemIndex += count;
				}
				// if the item occurs at my position, ...
				else if (offset == _cachedState.Count + _cachedState.Offset)
				{
					if (count > 0)
					{
						// for insert, update my offset
						_cachedState.Offset += count;
						_cachedState.ItemIndex += count;
					}
					else if (_cachedState.Offset == _cachedState.Block.ItemCount)
					{
						// if deleting last item in the block, advance to the next block
						_cachedState.Block = _cachedState.Block.Next;
						_cachedState.Offset = 0;
					}
				}
			}
			// Case C.  Refresh
			else
			{
				_cachedState.Block = newBlock;
				_cachedState.Offset += _cachedState.Count;
				_cachedState.Count = 0;
			}
		}

		//------------------------------------------------------
		//
		//  Private Fields
		//
		//------------------------------------------------------

		ItemContainerGenerator _factory;
		GeneratorDirection _direction;
		bool _done;
		GeneratorState _cachedState;
	}

	private class BatchGenerator : IDisposable
	{
		public BatchGenerator(ItemContainerGenerator factory)
		{
			_factory = factory;
			_factory._isGeneratingBatches = true;
			_factory.SetStatus(GeneratorStatus.GeneratingContainers);
		}

		void IDisposable.Dispose()
		{
			if (_factory != null)
			{
				_factory._isGeneratingBatches = false;
				_factory.SetStatus(GeneratorStatus.ContainersGenerated);
				_factory = null;
			}
			GC.SuppressFinalize(this);
		}

		ItemContainerGenerator _factory;
	}

	//------------------------------------------------------
	//
	//  Private Properties
	//
	//------------------------------------------------------

	IGeneratorHost Host { get { return _host; } }

	// The DO for which this generator was created.  For normal generators,
	// this is the ItemsControl.  For subgroup generators, this is
	// the GroupItem.
	DependencyObject Peer
	{
		get { return _peer; }
	}

	bool IsGrouping
	{
		get { return (ItemsInternal != Host.View); }
	}

	//------------------------------------------------------
	//
	//  Private Methods
	//
	//------------------------------------------------------

	void MoveToPosition(GeneratorPosition position, GeneratorDirection direction, bool allowStartAtRealizedItem, ref GeneratorState state)
	{
		ItemBlock block = _itemMap;
		if (block == null)
			return;         // this can happen in event-leapfrogging situations

		int itemIndex = 0;

		// first move to the indexed (realized) item
		if (position.Index != -1)
		{
			// find the right block
			int itemCount = 0;
			int index = position.Index;
			block = block.Next;
			while (index >= block.ContainerCount)
			{
				itemCount += block.ItemCount;
				index -= block.ContainerCount;
				itemIndex += block.ItemCount;
				block = block.Next;
			}

			// set the position
			state.Block = block;
			state.Offset = index;
			state.Count = itemCount;
			state.ItemIndex = itemIndex + index;
		}
		else
		{
			state.Block = block;
			state.Offset = 0;
			state.Count = 0;
			state.ItemIndex = itemIndex - 1;
		}

		// adjust the offset - we always set the state so it points to the next
		// item to be generated.
		int offset = position.Offset;
		if (offset == 0 && (!allowStartAtRealizedItem || state.Block == _itemMap))
		{
			offset = (direction == GeneratorDirection.Forward) ? 1 : -1;
		}

		// advance the state according to the offset
		if (offset > 0)
		{
			state.Block.MoveForward(ref state, true);
			--offset;

			while (offset > 0)
			{
				offset -= state.Block.MoveForward(ref state, allowStartAtRealizedItem, offset);
			}
		}
		else if (offset < 0)
		{
			if (state.Block == _itemMap)
			{
				state.ItemIndex = state.Count = ItemsInternal.Count;
			}

			state.Block.MoveBackward(ref state, true);
			++offset;

			while (offset < 0)
			{
				offset += state.Block.MoveBackward(ref state, allowStartAtRealizedItem, -offset);
			}
		}
	}

	// "Realize" the item in a block at the given offset, to be
	// the given item with corresponding container.  This means updating
	// the item map data structure so that the item belongs to a Realized block.
	// It also requires updating the state of every generator to track the
	// changes we make here.
	void Realize(UnrealizedItemBlock block, int offset, object item, DependencyObject container)
	{
		RealizedItemBlock prevR, nextR;

		RealizedItemBlock newBlock; // new location of the target item
		int newOffset;              // its offset within the new block
		int deltaCount;             // diff between cumulative item count of block and newBlock

		// if we're realizing the leftmost item and there's room in the
		// previous block, move it there
		if (offset == 0 &&
			(prevR = block.Prev as RealizedItemBlock) != null &&
			prevR.ItemCount < ItemBlock.BlockSize)
		{
			newBlock = prevR;
			newOffset = prevR.ItemCount;
			MoveItems(block, offset, 1, newBlock, newOffset, -prevR.ItemCount);
			MoveItems(block, 1, block.ItemCount, block, 0, +1);
		}

		// if we're realizing the rightmost item and there's room in the
		// next block, move it there
		else if (offset == block.ItemCount - 1 &&
			(nextR = block.Next as RealizedItemBlock) != null &&
			nextR.ItemCount < ItemBlock.BlockSize)
		{
			newBlock = nextR;
			newOffset = 0;
			MoveItems(newBlock, 0, newBlock.ItemCount, newBlock, 1, -1);
			MoveItems(block, offset, 1, newBlock, newOffset, offset);
		}

		// otherwise we need a new block for the target item
		else
		{
			newBlock = new RealizedItemBlock();
			newOffset = 0;
			deltaCount = offset;

			// if target is leftmost item, insert it before remaining items
			if (offset == 0)
			{
				newBlock.InsertBefore(block);
				MoveItems(block, offset, 1, newBlock, newOffset, 0);
				MoveItems(block, 1, block.ItemCount, block, 0, +1);
			}

			// if target is rightmost item, insert it after remaining items
			else if (offset == block.ItemCount - 1)
			{
				newBlock.InsertAfter(block);
				MoveItems(block, offset, 1, newBlock, newOffset, offset);
			}

			// otherwise split the block into two, with the target in the middle
			else
			{
				UnrealizedItemBlock newUBlock = new UnrealizedItemBlock();
				newUBlock.InsertAfter(block);
				newBlock.InsertAfter(block);
				MoveItems(block, offset + 1, block.ItemCount - offset - 1, newUBlock, 0, offset + 1);
				MoveItems(block, offset, 1, newBlock, 0, offset);
			}
		}

		RemoveAndCoalesceBlocksIfNeeded(block);

		// add the new target to the map
		newBlock.RealizeItem(newOffset, item, container);
	}

	void RemoveAndCoalesceBlocksIfNeeded(ItemBlock block)
	{
		if (block != null && block != _itemMap && block.ItemCount == 0)
		{
			block.Remove();

			// coalesce adjacent unrealized blocks
			if (block.Prev is UnrealizedItemBlock && block.Next is UnrealizedItemBlock)
			{
				MoveItems(block.Next, 0, block.Next.ItemCount, block.Prev, block.Prev.ItemCount, -block.Prev.ItemCount - 1);
				block.Next.Remove();
			}
		}
	}

	// Move 'count' items starting at position 'offset' in block 'block'
	// to position 'newOffset' in block 'newBlock'.  The difference between
	// the cumulative item counts of newBlock and block is given by 'deltaCount'.
	void MoveItems(ItemBlock block, int offset, int count,
					ItemBlock newBlock, int newOffset, int deltaCount)
	{
		RealizedItemBlock ribSrc = block as RealizedItemBlock;
		RealizedItemBlock ribDst = newBlock as RealizedItemBlock;

		// when both blocks are Realized, entries must be physically copied
		if (ribSrc != null && ribDst != null)
		{
			ribDst.CopyEntries(ribSrc, offset, count, newOffset);
		}
		// when the source block is Realized, clear the vacated entries -
		// to avoid leaks.  (No need if it's now empty - the block will get GC'd).
		else if (ribSrc != null && ribSrc.ItemCount > count)
		{
			ribSrc.ClearEntries(offset, count);
		}

		// update block information
		block.ItemCount -= count;
		newBlock.ItemCount += count;

		// tell generators what happened
		if (MapChanged != null)
			MapChanged(block, offset, count, newBlock, newOffset, deltaCount);
	}

	// Set the AlternationIndex on a newly-realized container.  Also, reset
	// the AlternationIndex on other containers to maintain the adjacency
	// criterion.
	void SetAlternationIndex(ItemBlock block, int offset, GeneratorDirection direction)
	{
		// If user doesn't request alternation, don't do anything
		if (_alternationCount <= 0)
			return;

		int index;
		RealizedItemBlock rib;

		// Proceed in the direction of generation.  This tends to reach the
		// end sooner (often in one step).
		if (direction != GeneratorDirection.Backward)
		{
			// Forward.  Back up one container to determine the starting index
			--offset;
			while (offset < 0 || block is UnrealizedItemBlock)
			{
				block = block.Prev;
				offset = block.ContainerCount - 1;
			}

			rib = block as RealizedItemBlock;
			index = (block == _itemMap) ? -1 : ItemsControl.GetAlternationIndex(rib.ContainerAt(offset));

			// loop through the remaining containers, resetting each AlternationIndex
			for (; ; )
			{
				// advance to next realized container
				++offset;
				while (offset == block.ContainerCount)
				{
					block = block.Next;
					offset = 0;
				}

				// exit if we've reached the end
				if (block == _itemMap)
					break;

				// advance the AlternationIndex
				index = (index + 1) % _alternationCount;

				// assign it to the container
				rib = block as RealizedItemBlock;
				BnsCustomSourceBaseWidget.SetAlternationIndex(rib.ContainerAt(offset), index);
			}
		}
		else
		{
			// Backward.  Advance one container to determine the starting index
			++offset;
			while (offset >= block.ContainerCount || block is UnrealizedItemBlock)
			{
				block = block.Next;
				offset = 0;
			}

			rib = block as RealizedItemBlock;

			// Get the alternation index for the advanced container. Use value 1 if no container
			// is found, so that 0 gets used for actual container in question.
			index = (block == _itemMap) ? 1 : ItemsControl.GetAlternationIndex(rib.ContainerAt(offset));

			// loop through the remaining containers, resetting each AlternationIndex
			for (; ; )
			{
				// retreat to next realized container
				--offset;
				while (offset < 0)
				{
					block = block.Prev;
					offset = block.ContainerCount - 1;
				}

				// exit if we've reached the end
				if (block == _itemMap)
					break;

				// retreat the AlternationIndex
				index = (_alternationCount + index - 1) % _alternationCount;

				// assign it to the container
				rib = block as RealizedItemBlock;
				BnsCustomSourceBaseWidget.SetAlternationIndex(rib.ContainerAt(offset), index);
			}
		}
	}

	// create a group item for the given group
	DependencyObject ContainerForGroup(CollectionViewGroup group)
	{
		_generatesGroupItems = true;
		if (!ShouldHide(group))
		{
			// normal group - link a new GroupItem
			GroupItem groupItem = new GroupItem();

			LinkContainerToItem(groupItem, group);

			// create the generator
			//groupItem.Generator = new ItemContainerGenerator(this, groupItem);

			return groupItem;
		}
		else
		{
			// hidden empty group - link a new EmptyGroupItem
			AddEmptyGroupItem(group);

			// but don't return it to layout
			return null;
		}
	}

	// prepare the grouping information.  Called from RemoveAll.
	void PrepareGrouping()
	{
		GroupStyle groupStyle;
		IList items;

		if (Level == 0)
		{
			groupStyle = Host.GetGroupStyle(null, 0);

			if (groupStyle == null)
			{
				items = Host.View;
			}
			else
			{
				CollectionView cv = Host.View.CollectionView;
				items = (cv == null) ? null : cv.Groups;
				if (items == null)
				{
					items = Host.View;

					// When there are no groups, we should ignore GroupStyle
					// and use the host's ItemsPanel .
					// But this breaks Nero because
					// their ItemsPanel can only be used at the leaf level of
					// a real grouping scenario.  It null-refs if used with
					// an empty collection, which happens during the first layout.
					// So for compat we let the bogus GroupStyle.Panel leak through
					// when the Items collection is empty.
					if (items.Count > 0)
					{
						groupStyle = null;
					}
				}
			}
		}
		else
		{
			GroupItem groupItem = (GroupItem)Peer;
			CollectionViewGroup group = groupItem.ReadLocalValue(ItemForItemContainerProperty) as CollectionViewGroup;

			if (group != null)
			{
				if (group.IsBottomLevel)
				{
					groupStyle = null;
				}
				else
				{
					groupStyle = Host.GetGroupStyle(group, Level);
				}

				items = group.Items;
			}
			else
			{
				// GroupItem has been recycled.
				groupStyle = null;
				items = Host.View;
			}
		}

		GroupStyle = groupStyle;
		ItemsInternal = items;

		if ((Level == 0) && (Host != null))
		{
			// Notify the host of a change in IsGrouping
			Host.SetIsGrouping(IsGrouping);
		}
	}

	void SetAlternationCount()
	{
		int alternationCount;

		if (IsGrouping && GroupStyle != null)
		{
			//if (GroupStyle.IsAlternationCountSet)
			//{
			//	alternationCount = GroupStyle.AlternationCount;
			//}
			//else
			if (_parent != null)
			{
				alternationCount = _parent._alternationCount;
			}
			else
			{
				alternationCount = Host.AlternationCount;
			}
		}
		else
		{
			alternationCount = Host.AlternationCount;
		}

		ChangeAlternationCount(alternationCount);
	}

	// should the given group be hidden?
	bool ShouldHide(CollectionViewGroup group)
	{
		return GroupStyle.HidesIfEmpty &&      // user asked to hide
				group.ItemCount == 0;           // group is empty
	}

	// create an empty-group placeholder item
	void AddEmptyGroupItem(CollectionViewGroup group)
	{
		EmptyGroupItem emptyGroupItem = new EmptyGroupItem();

		LinkContainerToItem(emptyGroupItem, group);

		emptyGroupItem.SetGenerator(new ItemContainerGenerator(this, emptyGroupItem));

		// add it to the list of placeholder items (this keeps it from being GC'd)
		if (_emptyGroupItems == null)
			_emptyGroupItems = new ArrayList();
		_emptyGroupItems.Add(emptyGroupItem);
	}

	// notification that a subgroup has become non-empty
	void OnSubgroupBecameNonEmpty(EmptyGroupItem groupItem, CollectionViewGroup group)
	{
		// Discard placeholder container.
		UnlinkContainerFromItem(groupItem, group);
		if (_emptyGroupItems != null)
			_emptyGroupItems.Remove(groupItem);

		// inform layout as if the group just got added
		if (ItemsChanged != null)
		{
			GeneratorPosition position = PositionFromIndex(ItemsInternal.IndexOf(group));
			//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Add, position, 1, 0));
		}
	}

	// notification that a subgroup has become empty
	void OnSubgroupBecameEmpty(CollectionViewGroup group)
	{
		if (ShouldHide(group))
		{
			GeneratorPosition position = PositionFromIndex(ItemsInternal.IndexOf(group));

			// if the group is realized, un-realize it and notify layout
			if (position.Offset == 0 && position.Index >= 0)
			{
				// un-realize
				this.Remove(position, 1);

				// inform layout as if the group just got removed
				if (ItemsChanged != null)
				{
					//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Remove, position, 1, 1));
				}

				// create the placeholder
				AddEmptyGroupItem(group);
			}
		}
	}

	// convert an index (into Items) into a GeneratorPosition
	GeneratorPosition PositionFromIndex(int itemIndex)
	{
		GeneratorPosition position;
		ItemBlock itemBlock;
		int offsetFromBlockStart;

		GetBlockAndPosition(itemIndex, out position, out itemBlock, out offsetFromBlockStart);

		return position;
	}


	void GetBlockAndPosition(object item, int itemIndex, bool deletedFromItems, out GeneratorPosition position, out ItemBlock block, out int offsetFromBlockStart, out int correctIndex)
	{
		if (itemIndex >= 0)
		{
			GetBlockAndPosition(itemIndex, out position, out block, out offsetFromBlockStart);
			correctIndex = itemIndex;
		}
		else
		{
			GetBlockAndPosition(item, deletedFromItems, out position, out block, out offsetFromBlockStart, out correctIndex);
		}
	}


	void GetBlockAndPosition(int itemIndex, out GeneratorPosition position, out ItemBlock block, out int offsetFromBlockStart)
	{
		position = new GeneratorPosition(-1, 0);
		block = null;
		offsetFromBlockStart = itemIndex;

		if (_itemMap == null || itemIndex < 0)
			return;

		int containerIndex = 0;

		for (block = _itemMap.Next; block != _itemMap; block = block.Next)
		{
			if (offsetFromBlockStart >= block.ItemCount)
			{
				// item belongs to a later block, increment the containerIndex
				containerIndex += block.ContainerCount;
				offsetFromBlockStart -= block.ItemCount;
			}
			else
			{
				// item belongs to this block.  Determine the container index and offset
				if (block.ContainerCount > 0)
				{
					// block has realized items
					position = new GeneratorPosition(containerIndex + offsetFromBlockStart, 0);
				}
				else
				{
					// block has unrealized items
					position = new GeneratorPosition(containerIndex - 1, offsetFromBlockStart + 1);
				}

				break;
			}
		}
	}

	void GetBlockAndPosition(object item, bool deletedFromItems, out GeneratorPosition position, out ItemBlock block, out int offsetFromBlockStart, out int correctIndex)
	{
		correctIndex = 0;
		int containerIndex = 0;
		offsetFromBlockStart = 0;
		int deletionOffset = deletedFromItems ? 1 : 0;
		position = new GeneratorPosition(-1, 0);

		if (_itemMap == null)
		{
			// handle reentrant call
			block = null;
			return;
		}

		for (block = _itemMap.Next; block != _itemMap; block = block.Next)
		{
			UnrealizedItemBlock uib;
			RealizedItemBlock rib = block as RealizedItemBlock;

			if (rib != null)
			{
				// compare realized items with item for which we are searching
				offsetFromBlockStart = rib.OffsetOfItem(item);
				if (offsetFromBlockStart >= 0)
				{
					position = new GeneratorPosition(containerIndex + offsetFromBlockStart, 0);
					correctIndex += offsetFromBlockStart;
					break;
				}
			}
			else if ((uib = block as UnrealizedItemBlock) != null)
			{
				// if the item isn't realized, we can't find it
				// directly.  Instead, look for indirect evidence that it
				// belongs to this block by checking the indices of
				// nearby realized items.

#if DEBUG
				// Sanity check - make sure data structure is OK so far.
				rib = block.Prev as RealizedItemBlock;
				if (rib != null && rib.ContainerCount > 0)
				{
					Debug.Assert(ControlHelpers.EqualsEx(rib.ItemAt(rib.ContainerCount - 1),
												ItemsInternal[correctIndex - 1]),
								"Generator data structure is corrupt");
				}
#endif

				bool itemIsInCurrentBlock = false;
				rib = block.Next as RealizedItemBlock;
				if (rib != null && rib.ContainerCount > 0)
				{
					// if the index of the next realized item is off by one,
					// the deleted item likely comes from the current
					// unrealized block.
					itemIsInCurrentBlock =
							ControlHelpers.EqualsEx(rib.ItemAt(0),
								ItemsInternal[correctIndex + block.ItemCount - deletionOffset]);
				}
				else if (block.Next == _itemMap)
				{
					// similarly if we're at the end of the list and the
					// overall count is off by one, or if the current block
					// is the only block, the deleted item likely
					// comes from the current (last) unrealized block
					itemIsInCurrentBlock = block.Prev == _itemMap ||
						(ItemsInternal.Count == correctIndex + block.ItemCount - deletionOffset);
				}

				if (itemIsInCurrentBlock)
				{
					// we don't know where it is in this block, so assume
					// it's the very first item.
					offsetFromBlockStart = 0;
					position = new GeneratorPosition(containerIndex - 1, 1);
					break;
				}
			}

			correctIndex += block.ItemCount;
			containerIndex += block.ContainerCount;
		}

		if (block == _itemMap)
		{
			// There's no way of knowing which unrealized block it belonged to, so
			// the data structure can't be updated correctly.  Sound the alarm.
			throw new InvalidOperationException("CannotFindRemovedItem");
		}
	}


	// establish the link from the container to the corresponding item
	internal static void LinkContainerToItem(DependencyObject container, object item)
	{
		// always set the ItemForItemContainer property
		container.ClearValue(ItemForItemContainerProperty);
		container.SetValue(ItemForItemContainerProperty, item);

		// for non-direct items, set the DataContext property
		if (container != item)
		{
#if DEBUG
			//// Some ancient code at this point handled the case when DataContext
			//// was set via an Expression (presumably a binding).  I don't think
			//// this actually happens any more.  Just in case...
			//DependencyProperty dp = FrameworkElement.DataContextProperty;
			//EntryIndex entryIndex = container.LookupEntry(dp.GlobalIndex);
			//Debug.Assert(!container.HasExpression(entryIndex, dp), "DataContext set by expression (unexpectedly)");
#endif

			container.SetValue(FrameworkElement.DataContextProperty, item);
		}
	}

	private void UnlinkContainerFromItem(DependencyObject container, object item)
	{
		UnlinkContainerFromItem(container, item, _host);
	}

	internal static void UnlinkContainerFromItem(DependencyObject container, object item, IGeneratorHost host)
	{
		// When a container is removed from the tree, its future takes one of
		// two forms:
		//      a) [normal mode] the container becomes eligible for GC
		//      b) [recycling mode] the container joins the recycled list, and
		//          possibly re-enters the tree at some point, usually with a
		//          different item.
		//
		// As some "subtle issues" that arose in the
		// container recycling work illustrate, it's important that the container
		// and its subtree sever their connection to the data item.  Otherwise
		// you can get aliasing - a dead container reacting to the same item as a live
		// container.  Even without aliasing, it's a perf waste for a dead container
		// to continue reacting to its former data item.
		//
		// On the other hand, it's a perf waste to spend too much effort cleaning
		// up the container and its subtree, since they will often just get GC'd
		// in the near future.
		//
		// WPF initially did a full cleanup of the container, removing all properties
		// that were set in PrepareContainerForItem.  This avoided aliasing, but
		// was deemed too expensive, especially for scrolling.  For Windows OS Bug
		// 1445288, all this cleanup work was removed.  This sped up scrolling, but
		// introduced the recycling "subtle
		// issues".  A compromise is needed.
		//
		// The compromise is tell the container to attach to a sentinel item
		// BindingExpressionBase.DisconnectedItem.  We allow this to propagate into the
		// conainer's subtree through properties like DataContext and
		// ContentControl.Content that are normally set by PrepareItemForContainer.
		// A Binding that sees the sentinel as the data item will disconnect its
		// event listeners from the former data item, but will not change its
		// own value or invalidate its target property.  This avoids the cost
		// of re-measuring most of the subtree.

		container.ClearValue(ItemForItemContainerProperty);

		// TreeView virtualization requires that we call ClearContainer before setting
		// the DataContext to "Disconnected".  This gives the TreeViewItems a chance
		// to save "Item values" in the look-aside table, before that table is
		// discarded.
		host.ClearContainerForItem(container, item);

		if (container != item)
		{
			//			DependencyProperty dp = FrameworkElement.DataContextProperty;

			//#if DEBUG
			//			// Some ancient code at this point handled the case when DataContext
			//			// was set via an Expression (presumably a binding).  I don't think
			//			// this actually happens any more.  Just in case...
			//			EntryIndex entryIndex = container.LookupEntry(dp.GlobalIndex);
			//			Debug.Assert(!container.HasExpression(entryIndex, dp), "DataContext set by expression (unexpectedly)");
			//#endif

			//			container.SetValue(dp, BindingExpressionBase.DisconnectedItem);
		}
	}

	/// <summary>
	/// Handle events from the centralized event table
	/// </summary>
	bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
	{
		return false;   // this method is no longer used (but must remain, for compat)
	}

	void OnGroupStylePropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "Panel")
		{
			OnPanelChanged();
		}
		else
		{
			OnRefresh();
		}
	}

	void ValidateAndCorrectIndex(object item, ref int index)
	{
		if (index >= 0)
		{
			// this check is expensive - Items[index] potentially iterates through
			// the collection.  So trust the sender to tell us the truth in retail bits.
			Debug.Assert(ControlHelpers.EqualsEx(item, ItemsInternal[index]), "Event contains the wrong index");
		}
		else
		{
			index = ItemsInternal.IndexOf(item);
			if (index < 0)
				throw new InvalidOperationException("CollectionAddEventMissingItem");
		}
	}

	/// <summary>
	/// Forward a CollectionChanged event
	/// </summary>
	// Called  when items collection changes.
	void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
	{
		if (sender != ItemsInternal && args.Action != NotifyCollectionChangedAction.Reset)
			return;     // ignore events (except Reset) from ItemsCollection when we're listening to group's items.

		switch (args.Action)
		{
			case NotifyCollectionChangedAction.Add:
				if (args.NewItems.Count != 1)
					throw new NotSupportedException("RangeActionsNotSupported");
				OnItemAdded(args.NewItems[0], args.NewStartingIndex);
				break;

			case NotifyCollectionChangedAction.Remove:
				if (args.OldItems.Count != 1)
					throw new NotSupportedException("RangeActionsNotSupported");
				OnItemRemoved(args.OldItems[0], args.OldStartingIndex);
				break;

			//case NotifyCollectionChangedAction.Replace:
			//	// Don't check arguments if app targets 4.0, for compat ( 726682)
			//	if (!FrameworkCompatibilityPreferences.TargetsDesktop_V4_0)
			//	{
			//		if (args.OldItems.Count != 1)
			//			throw new NotSupportedException(SR.RangeActionsNotSupported);
			//	}
			//	OnItemReplaced(args.OldItems[0], args.NewItems[0], args.NewStartingIndex);
			//	break;

			//case NotifyCollectionChangedAction.Move:
			//	// Don't check arguments if app targets 4.0, for compat ( 726682)
			//	if (!FrameworkCompatibilityPreferences.TargetsDesktop_V4_0)
			//	{
			//		if (args.OldItems.Count != 1)
			//			throw new NotSupportedException(SR.RangeActionsNotSupported);
			//	}
			//	OnItemMoved(args.OldItems[0], args.OldStartingIndex, args.NewStartingIndex);
			//	break;

			//case NotifyCollectionChangedAction.Reset:
			//	OnRefresh();
			//	break;

			default:
				throw new NotSupportedException("UnexpectedCollectionChangeAction");
		}

		PresentationTraceLevel traceLevel = PresentationTraceSources.GetTraceLevel(this);
		if (traceLevel >= PresentationTraceLevel.High)
		{
			Verify();
		}
	}

	// Called when an item is added to the items collection
	void OnItemAdded(object item, int index)
	{
		if (_itemMap == null)
		{
			// reentrant call (from RemoveAllInternal) shouldn't happen,
			// but if it does, don't crash
			Debug.Assert(false, "unexpected reentrant call to OnItemAdded");
			return;
		}

		ValidateAndCorrectIndex(item, ref index);

		GeneratorPosition position = new GeneratorPosition(-1, 0);

		// find the block containing the new item
		ItemBlock block = _itemMap.Next;
		int offsetFromBlockStart = index;
		int unrealizedItemsSkipped = 0;     // distance since last realized item
		while (block != _itemMap && offsetFromBlockStart >= block.ItemCount)
		{
			offsetFromBlockStart -= block.ItemCount;
			position.Index += block.ContainerCount;
			unrealizedItemsSkipped = (block.ContainerCount > 0) ? 0 : unrealizedItemsSkipped + block.ItemCount;
			block = block.Next;
		}

		position.Offset = unrealizedItemsSkipped + offsetFromBlockStart + 1;
		// the position is now correct, except when pointing into a realized block;
		// that case is fixed below

		// if it's an unrealized block, add the item by bumping the count
		UnrealizedItemBlock uib = block as UnrealizedItemBlock;
		if (uib != null)
		{
			MoveItems(uib, offsetFromBlockStart, 1, uib, offsetFromBlockStart + 1, 0);
			++uib.ItemCount;
		}

		// if the item can be added to a previous unrealized block, do so
		else if ((offsetFromBlockStart == 0 || block == _itemMap) &&
				((uib = block.Prev as UnrealizedItemBlock) != null))
		{
			++uib.ItemCount;
		}

		// otherwise, create a new unrealized block
		else
		{
			uib = new UnrealizedItemBlock();
			uib.ItemCount = 1;

			// split the current realized block, if necessary
			RealizedItemBlock rib;
			if (offsetFromBlockStart > 0 && (rib = block as RealizedItemBlock) != null)
			{
				RealizedItemBlock newBlock = new RealizedItemBlock();
				MoveItems(rib, offsetFromBlockStart, rib.ItemCount - offsetFromBlockStart, newBlock, 0, offsetFromBlockStart);
				newBlock.InsertAfter(rib);
				position.Index += block.ContainerCount;
				position.Offset = 1;
				block = newBlock;
			}

			uib.InsertBefore(block);
		}

		// tell generators what happened
		if (MapChanged != null)
		{
			MapChanged(null, index, +1, uib, 0, 0);
		}

		// tell layout what happened
		if (ItemsChanged != null)
		{
			//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Add, position, 1, 0));
		}
	}


	// Called when an item is removed from the items collection
	void OnItemRemoved(object item, int itemIndex)
	{
		DependencyObject container = null;    // the corresponding container
		int containerCount = 0;

		// search for the deleted item
		GeneratorPosition position;
		ItemBlock block;
		int offsetFromBlockStart;
		int correctIndex;
		GetBlockAndPosition(item, itemIndex, true, out position, out block, out offsetFromBlockStart, out correctIndex);

		RealizedItemBlock rib = block as RealizedItemBlock;
		if (rib != null)
		{
			containerCount = 1;
			container = rib.ContainerAt(offsetFromBlockStart);
		}

		// remove the item, and remove the block if it's now empty
		MoveItems(block, offsetFromBlockStart + 1, block.ItemCount - offsetFromBlockStart - 1, block, offsetFromBlockStart, 0);
		--block.ItemCount;
		if (rib != null)
		{
			// fix up the alternation index before removing an empty block, while
			// we still have a valid block and offset
			SetAlternationIndex(block, offsetFromBlockStart, GeneratorDirection.Forward);
		}
		RemoveAndCoalesceBlocksIfNeeded(block);

		// tell generators what happened
		if (MapChanged != null)
		{
			MapChanged(null, itemIndex, -1, null, 0, 0);
		}

		// tell layout what happened
		if (ItemsChanged != null)
		{
			//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Remove, position, 1, containerCount));
		}

		// unhook the container.  Do this after layout has (presumably) removed it from
		// the UI, so that it doesn't inherit DataContext falsely.
		if (container != null)
		{
			UnlinkContainerFromItem(container, item);
		}

		// detect empty groups, so they can be hidden if necessary
		if (Level > 0 && ItemsInternal.Count == 0)
		{
			GroupItem groupItem = (GroupItem)Peer;
			CollectionViewGroup group = groupItem.ReadLocalValue(ItemForItemContainerProperty) as CollectionViewGroup;

			// the group could be null if the parent generator has already
			// unhooked its container
			if (group != null)
			{
				Parent.OnSubgroupBecameEmpty(group);
			}
		}
	}

	void OnItemReplaced(object oldItem, object newItem, int index)
	{
		// search for the replaced item
		GeneratorPosition position;
		ItemBlock block;
		int offsetFromBlockStart;
		int correctIndex;
		GetBlockAndPosition(oldItem, index, false, out position, out block, out offsetFromBlockStart, out correctIndex);

		// If the item is in an UnrealizedItemBlock, then this change need not
		// be made to the _itemsMap as we are replacing an unrealized item with another unrealized
		// item in the same place.
		RealizedItemBlock rib = block as RealizedItemBlock;
		if (rib != null)
		{
			DependencyObject container = rib.ContainerAt(offsetFromBlockStart);

			if (oldItem != container && !_host.IsItemItsOwnContainer(newItem))
			{
				// if we can re-use the old container, just relink it to the
				// new item
				rib.RealizeItem(offsetFromBlockStart, newItem, container);
				LinkContainerToItem(container, newItem);
				_host.PrepareItemContainer(container, newItem);
			}
			else
			{
				// otherwise, we need a new container
				DependencyObject newContainer = _host.GetContainerForItem(newItem);
				rib.RealizeItem(offsetFromBlockStart, newItem, newContainer);
				LinkContainerToItem(newContainer, newItem);

				if (ItemsChanged != null)
				{
					//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Replace, position, 1, 1));
				}

				// after layout has removed the old container, unlink it
				UnlinkContainerFromItem(container, oldItem);
			}
		}
	}

	void OnItemMoved(object item, int oldIndex, int newIndex)
	{
		if (_itemMap == null)
		{
			// reentrant call (from RemoveAllInternal) shouldn't happen,
			// but if it does, don't crash
			Debug.Assert(false, "unexpected reentrant call to OnItemMoved");
			return;
		}

		DependencyObject container = null;    // the corresponding container
		int containerCount = 0;
		UnrealizedItemBlock uib;

		// search for the moved item
		GeneratorPosition position;
		ItemBlock block;
		int offsetFromBlockStart;
		int correctIndex;
		GetBlockAndPosition(item, oldIndex, true, out position, out block, out offsetFromBlockStart, out correctIndex);

		GeneratorPosition oldPosition = position;

		RealizedItemBlock rib = block as RealizedItemBlock;
		if (rib != null)
		{
			containerCount = 1;
			container = rib.ContainerAt(offsetFromBlockStart);
		}

		// remove the item, and remove the block if it's now empty
		MoveItems(block, offsetFromBlockStart + 1, block.ItemCount - offsetFromBlockStart - 1, block, offsetFromBlockStart, 0);
		--block.ItemCount;
		RemoveAndCoalesceBlocksIfNeeded(block);

		//
		// now insert into the new spot.
		//

		position = new GeneratorPosition(-1, 0);
		block = _itemMap.Next;
		offsetFromBlockStart = newIndex;
		while (block != _itemMap && offsetFromBlockStart >= block.ItemCount)
		{
			offsetFromBlockStart -= block.ItemCount;
			if (block.ContainerCount > 0)
			{
				position.Index += block.ContainerCount;
				position.Offset = 0;
			}
			else
			{
				position.Offset += block.ItemCount;
			}
			block = block.Next;
		}

		position.Offset += offsetFromBlockStart + 1;

		// if it's an unrealized block, add the item by bumping the count
		uib = block as UnrealizedItemBlock;
		if (uib != null)
		{
			MoveItems(uib, offsetFromBlockStart, 1, uib, offsetFromBlockStart + 1, 0);
			++uib.ItemCount;
		}

		// if the item can be added to a previous unrealized block, do so
		else if ((offsetFromBlockStart == 0 || block == _itemMap) &&
				((uib = block.Prev as UnrealizedItemBlock) != null))
		{
			++uib.ItemCount;
		}

		// otherwise, create a new unrealized block
		else
		{
			uib = new UnrealizedItemBlock();
			uib.ItemCount = 1;

			// split the current realized block, if necessary
			if (offsetFromBlockStart > 0 && (rib = block as RealizedItemBlock) != null)
			{
				RealizedItemBlock newBlock = new RealizedItemBlock();
				MoveItems(rib, offsetFromBlockStart, rib.ItemCount - offsetFromBlockStart, newBlock, 0, offsetFromBlockStart);
				newBlock.InsertAfter(rib);
				position.Index += block.ContainerCount;
				position.Offset = 1;
				offsetFromBlockStart = 0;
				block = newBlock;
			}

			uib.InsertBefore(block);
		}

		DependencyObject parent = VisualTreeHelper.GetParent(container);

		// tell layout what happened
		if (ItemsChanged != null)
		{
			//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Move, position, oldPosition, 1, containerCount));
		}

		// unhook the container.  Do this after layout has (presumably) removed it from
		// the UI, so that it doesn't inherit DataContext falsely.
		if (container != null)
		{
			if (parent == null || VisualTreeHelper.GetParent(container) != parent)
			{
				UnlinkContainerFromItem(container, item);
			}
			else
			{
				// If the container has the same visual parent as before then that means that
				// the container was just repositioned within the parent's VisualCollection.
				// we don't need to unlink the container, but we do need to re-realize the block.
				Realize(uib, offsetFromBlockStart, item, container);
			}
		}

		// fix up the AlternationIndex on containers affected by the move
		if (_alternationCount > 0)
		{
			// start with the smaller of the two positions, and proceed forward.
			// This tends to preserve the AlternatonIndex on containers at the
			// front of the list, as users expect
			int index = Math.Min(oldIndex, newIndex);
			GetBlockAndPosition(index, out position, out block, out offsetFromBlockStart);
			SetAlternationIndex(block, offsetFromBlockStart, GeneratorDirection.Forward);
		}
	}

	// Called when the items collection is refreshed
	void OnRefresh()
	{
		this.RemoveAll();

		// tell layout what happened
		if (ItemsChanged != null)
		{
			GeneratorPosition position = new GeneratorPosition(0, 0);
			//ItemsChanged(this, new ItemsChangedEventArgs(NotifyCollectionChangedAction.Reset, position, 0, 0));
		}
	}


	//------------------------------------------------------
	//
	//  Private Fields
	//
	//------------------------------------------------------

	private Generator _generator;
	private IGeneratorHost _host;
	private ItemBlock _itemMap;
	private GeneratorStatus _status;
	private int _itemsGenerated;
	private int _startIndexForUIFromItem;
	private DependencyObject _peer;
	private int _level;
	private IList _items;
	private ReadOnlyCollection<object> _itemsReadOnly;
	private GroupStyle _groupStyle;
	private ItemContainerGenerator _parent;
	private ArrayList _emptyGroupItems;
	private int _alternationCount;

	private Type _containerType;     // type of containers on the recycle queue
	private Queue<DependencyObject> _recyclableContainers = new Queue<DependencyObject>();

	private bool _generatesGroupItems; // Flag to indicate that this generates GroupItems
	private bool _isGeneratingBatches;

	event MapChangedHandler MapChanged;

	delegate void MapChangedHandler(ItemBlock block, int offset, int count,
				ItemBlock newBlock, int newOffset, int deltaCount);

#if GENERATOR_TRACE
        MS.Internal.Utility.HFTimer _timer = new MS.Internal.Utility.HFTimer();
        MS.Internal.Utility.HFTimer _creationTimer = new MS.Internal.Utility.HFTimer();
#endif

	//------------------------------------------------------
	//
	//  Private Nested Classes
	//
	//------------------------------------------------------

	// The ItemContainerGenerator uses the following data structure to maintain
	// the correspondence between items and their containers.  It's a doubly-linked
	// list of ItemBlocks, with a sentinel node serving as the header.
	// Each node maintains two counts:  the number of items it holds, and
	// the number of containers.
	//
	// There are two kinds of blocks - one holding only "realized" items (i.e.
	// items that have been generated into containers) and one holding only
	// unrealized items.  The container count of a realized block is the same
	// as its item count (one container per item);  the container count of an
	// unrealized block is zero.
	//
	// Unrealized blocks can hold any number of items.  We only need to know
	// the count.  Realized blocks have a fixed-sized array (BlockSize) so
	// they hold up to that many items and their corresponding containers.  When
	// a realized block fills up, it inserts a new (empty) realized block into
	// the list and carries on.
	//
	// This data structure was chosen with virtualization in mind.  The typical
	// state is a long block of unrealized items (the ones that have scrolled
	// off the top), followed by a moderate number (<50?) of realized items
	// (the ones in view), followed by another long block of unrealized items
	// (the ones that have not yet scrolled into view).  So the list will contain
	// an unrealized block, followed by 3 or 4 realized blocks, followed by
	// another unrealized block.  Fewer than 10 blocks altogether, so linear
	// searching won't cost that much.  Thus we don't need a more sophisticated
	// data structure.  (If profiling reveals that we do, we can always replace
	// this one.  It's totally private to the ItemContainerGenerator and its
	// Generators.)

	// represents a block of items
	private class ItemBlock
	{
		public const int BlockSize = 16;

		public int ItemCount { get { return _count; } set { _count = value; } }
		public ItemBlock Prev { get { return _prev; } set { _prev = value; } }
		public ItemBlock Next { get { return _next; } set { _next = value; } }

		public virtual int ContainerCount { get { return Int32.MaxValue; } }
		public virtual DependencyObject ContainerAt(int index) { return null; }
		public virtual object ItemAt(int index) { return null; }

		public void InsertAfter(ItemBlock prev)
		{
			Next = prev.Next;
			Prev = prev;

			Prev.Next = this;
			Next.Prev = this;
		}

		public void InsertBefore(ItemBlock next)
		{
			InsertAfter(next.Prev);
		}

		public void Remove()
		{
			Prev.Next = Next;
			Next.Prev = Prev;
		}

		public void MoveForward(ref GeneratorState state, bool allowMovePastRealizedItem)
		{
			if (IsMoveAllowed(allowMovePastRealizedItem))
			{
				state.ItemIndex += 1;
				if (++state.Offset >= ItemCount)
				{
					state.Block = Next;
					state.Offset = 0;
					state.Count += ItemCount;
				}
			}
		}

		public void MoveBackward(ref GeneratorState state, bool allowMovePastRealizedItem)
		{
			if (IsMoveAllowed(allowMovePastRealizedItem))
			{
				if (--state.Offset < 0)
				{
					state.Block = Prev;
					state.Offset = state.Block.ItemCount - 1;
					state.Count -= state.Block.ItemCount;
				}
				state.ItemIndex -= 1;
			}
		}

		public int MoveForward(ref GeneratorState state, bool allowMovePastRealizedItem, int count)
		{
			if (IsMoveAllowed(allowMovePastRealizedItem))
			{
				if (count < ItemCount - state.Offset)
				{
					state.Offset += count;
				}
				else
				{
					count = ItemCount - state.Offset;
					state.Block = Next;
					state.Offset = 0;
					state.Count += ItemCount;
				}

				state.ItemIndex += count;
			}

			return count;
		}

		public int MoveBackward(ref GeneratorState state, bool allowMovePastRealizedItem, int count)
		{
			if (IsMoveAllowed(allowMovePastRealizedItem))
			{
				if (count <= state.Offset)
				{
					state.Offset -= count;
				}
				else
				{
					count = state.Offset + 1;
					state.Block = Prev;
					state.Offset = state.Block.ItemCount - 1;
					state.Count -= state.Block.ItemCount;
				}

				state.ItemIndex -= count;
			}

			return count;
		}

		protected virtual bool IsMoveAllowed(bool allowMovePastRealizedItem)
		{
			return allowMovePastRealizedItem;
		}

		int _count;
		ItemBlock _prev, _next;
	}

	// represents a block of unrealized (ungenerated) items
	private class UnrealizedItemBlock : ItemBlock
	{
		public override int ContainerCount { get { return 0; } }

		protected override bool IsMoveAllowed(bool allowMovePastRealizedItem)
		{
			return true;
		}
	}

	// represents a block of realized (generated) items
	private class RealizedItemBlock : ItemBlock
	{
		public override int ContainerCount { get { return ItemCount; } }

		public override DependencyObject ContainerAt(int index)
		{
			return _entry[index].Container;
		}

		public override object ItemAt(int index)
		{
			return _entry[index].Item;
		}

		public void CopyEntries(RealizedItemBlock src, int offset, int count, int newOffset)
		{
			int k;
			// choose which direction to copy so as not to clobber existing
			// entries (in case the source and destination blocks are the same)
			if (offset < newOffset)
			{
				// copy right-to-left
				for (k = count - 1; k >= 0; --k)
				{
					_entry[newOffset + k] = src._entry[offset + k];
				}

				// clear vacated entries, to avoid leak
				if (src != this)
				{
					src.ClearEntries(offset, count);
				}
				else
				{
					src.ClearEntries(offset, newOffset - offset);
				}
			}
			else
			{
				// copy left-to-right
				for (k = 0; k < count; ++k)
				{
					_entry[newOffset + k] = src._entry[offset + k];
				}

				// clear vacated entries, to avoid leak
				if (src != this)
				{
					src.ClearEntries(offset, count);
				}
				else
				{
					src.ClearEntries(newOffset + count, offset - newOffset);
				}
			}
		}

		public void ClearEntries(int offset, int count)
		{
			for (int i = 0; i < count; ++i)
			{
				_entry[offset + i].Item = null;
				_entry[offset + i].Container = null;
			}
		}

		public void RealizeItem(int index, object item, DependencyObject container)
		{
			_entry[index].Item = item;
			_entry[index].Container = container;
		}

		public int OffsetOfItem(object item)
		{
			for (int k = 0; k < ItemCount; ++k)
			{
				if (ControlHelpers.EqualsEx(_entry[k].Item, item))
					return k;
			}

			return -1;
		}

		BlockEntry[] _entry = new BlockEntry[BlockSize];
	}

	// an entry in the table maintained by RealizedItemBlock
	private struct BlockEntry
	{
		public object Item { get { return _item; } set { _item = value; } }
		public DependencyObject Container { get { return _container; } set { _container = value; } }

		private object _item;
		private DependencyObject _container;
	}

	// cached state of the factory's item map (updated by factory)
	// used to speed up calls to Generate
	private struct GeneratorState
	{
		public ItemBlock Block { get { return _block; } set { _block = value; } }
		public int Offset { get { return _offset; } set { _offset = value; } }
		public int Count { get { return _count; } set { _count = value; } }
		public int ItemIndex { get { return _itemIndex; } set { _itemIndex = value; } }

		private ItemBlock _block;     // some block in the map (most recently used)
		private int _offset;    // offset with the block
		private int _count;     // cumulative item count of blocks before the cached one
		private int _itemIndex; // index of current item
	}


	// The EmptyGroupItem class is used for the HidesIfEmpty grouping feature.
	// It takes the place of a regular GroupItem for an empty group, but is never
	// returned to layout/panel as a real container.
	private class EmptyGroupItem : GroupItem
	{
		private ItemContainerGenerator? Generator;

		public void SetGenerator(ItemContainerGenerator generator)
		{
			this.Generator = generator;
			generator.ItemsChanged += new ItemsChangedEventHandler(OnItemsChanged);
		}

		private void OnItemsChanged(object sender, ItemsChangedEventArgs e)
		{
			CollectionViewGroup group = (CollectionViewGroup)GetValue(ItemContainerGenerator.ItemForItemContainerProperty);

			// if the group becomes non-empty, un-hide the UI
			if (group.ItemCount > 0)
			{
				ItemContainerGenerator generator = Generator;
				generator.ItemsChanged -= new ItemsChangedEventHandler(OnItemsChanged);
				generator.Parent.OnSubgroupBecameNonEmpty(this, group);
			}
		}
	}
}