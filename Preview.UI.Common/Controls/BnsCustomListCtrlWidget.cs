using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xylia.Preview.UI.Controls.Primitives;

namespace Xylia.Preview.UI.Controls;

[DefaultEvent("OnItemsChanged"), DefaultProperty("Items")]
[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(FrameworkElement))]
[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)] // cannot be read & localized as string
public class BnsCustomListCtrlWidget : BnsCustomBaseWidget 
{
	#region Constructors  

	/// <summary>
	///     Default BnsCustomListCtrlWidget constructor
	/// </summary>
	/// <remarks>
	///     Automatic determination of current Dispatcher. Use alternative constructor
	///     that accepts a Dispatcher for best performance.
	/// </remarks>
	public BnsCustomListCtrlWidget() : base()
	{

	}

	static BnsCustomListCtrlWidget()
	{

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
	///     The DependencyProperty for the ItemsSource property.
	///     Flags:              None
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemsSourceProperty
		= DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(BnsCustomListCtrlWidget),
			new FrameworkPropertyMetadata(null,
				new PropertyChangedCallback(OnItemsSourceChanged)));

	private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		BnsCustomListCtrlWidget ic = (BnsCustomListCtrlWidget)d;
		IEnumerable oldValue = (IEnumerable)e.OldValue;
		IEnumerable newValue = (IEnumerable)e.NewValue;

		//((IContainItemStorage)ic).Clear();

		BindingExpressionBase beb = BindingOperations.GetBindingExpressionBase(d, ItemsSourceProperty);
		if (beb != null)
		{
			// ItemsSource is data-bound.   Always go to ItemsSource mode.
			// Also, extract the source item, to supply as context to the
			// CollectionRegistering event
		    // ic.Items.SetItemsSource(newValue, (object x) => beb.GetSourceItem(x));
		}
		else if (e.NewValue != null)
		{
			// ItemsSource is non-null, but not data-bound.  Go to ItemsSource mode
			//ic.Items.SetItemsSource(newValue);
		}
		else
		{
			// ItemsSource is explicitly null.  Return to normal mode.
			//ic.Items.ClearItemsSource();
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
		get { return Items; }
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

	private void CreateItemCollectionAndGenerator()
	{
		//_items = new ItemCollection(this);

		//// ItemInfos must get adjusted before the generator's change handler is called,
		//// so that any new ItemInfos arising from the generator don't get adjusted by mistake
		//// (see Win8 690623).
		//((INotifyCollectionChanged)_items).CollectionChanged += new NotifyCollectionChangedEventHandler(OnItemCollectionChanged1);

		//// the generator must attach its collection change handler before
		//// the control itself, so that the generator is up-to-date by the
		//// time the control tries to use it (bug 892806 et al.)
		//_itemContainerGenerator = new ItemContainerGenerator(this);

		//_itemContainerGenerator.ChangeAlternationCount();

		//((INotifyCollectionChanged)_items).CollectionChanged += new NotifyCollectionChangedEventHandler(OnItemCollectionChanged2);

		//if (IsInitPending)
		//{
		//	_items.BeginInit();
		//}
		//else if (IsInitialized)
		//{
		//	_items.BeginInit();
		//	_items.EndInit();
		//}

		//   ((INotifyCollectionChanged)_groupStyle).CollectionChanged += new NotifyCollectionChangedEventHandler(OnGroupStyleChanged);
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
				//CreateItemCollectionAndGenerator();
			}

			return _itemContainerGenerator;
		}
	}

	/// <summary>
	///     The DependencyProperty for the ItemTemplate property.
	///     Flags:              none
	///     Default Value:      null
	/// </summary>
	public static readonly DependencyProperty ItemTemplateProperty =
		 DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(BnsCustomListCtrlWidget),
		  new FrameworkPropertyMetadata(
			   null,
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
		((BnsCustomListCtrlWidget)d).OnItemTemplateChanged((DataTemplate)e.OldValue, (DataTemplate)e.NewValue);
	}

	/// <summary>
	///     This method is invoked when the ItemTemplate property changes.
	/// </summary>
	/// <param name="oldItemTemplate">The old value of the ItemTemplate property.</param>
	/// <param name="newItemTemplate">The new value of the ItemTemplate property.</param>
	protected virtual void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
	{
		CheckTemplateSource();

		//if (_itemContainerGenerator != null)
		//{
		//	_itemContainerGenerator.Refresh();
		//}
	}

	/// <summary>
	/// Throw if more than one of DisplayMemberPath, xxxTemplate and xxxTemplateSelector
	/// properties are set on the given element.
	/// </summary>
	private void CheckTemplateSource()
	{
		//if (string.IsNullOrEmpty(DisplayMemberPath))
		//{
		//	Helper.CheckTemplateAndTemplateSelector("Item", ItemTemplateProperty, ItemTemplateSelectorProperty, this);
		//}
		//else
		//{
		//	if (!(this.ItemTemplateSelector is DisplayMemberTemplateSelector))
		//	{
		//		throw new InvalidOperationException(SR.ItemTemplateSelectorBreaksDisplayMemberPath);
		//	}
		//	if (Helper.IsTemplateDefined(ItemTemplateProperty, this))
		//	{
		//		throw new InvalidOperationException(SR.DisplayMemberPathAndItemTemplateDefined);
		//	}
		//}
	}

	#endregion


	#region Protected Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		//ListContainer = this.Children.OfType<BnsCustomImageWidget>().ToList();
		//ListContainer.ForEach(x =>
		//{
		//	x.Expansion = ["JobLabel"];
		//	x.MouseEnter += (_, _) => x.Expansion.Add("MouseOver");
		//	x.MouseLeave += (_, _) => x.Expansion.Remove("MouseOver");
		//	x.PreviewKeyDown += (_, _) => x.Expansion.Add("Pressed");
		//	x.PreviewKeyUp += (_, _) => x.Expansion.Remove("Pressed");
		//});
		//ListContainer[^1].ExpansionComponentList["JobLabel"]?.SetValue("灵剑士 5555555555555");
	}

	#endregion

	#region Private Fields
	private ItemCollection _items;                      // Cache for Items property
	private ItemContainerGenerator _itemContainerGenerator;
	#endregion
}