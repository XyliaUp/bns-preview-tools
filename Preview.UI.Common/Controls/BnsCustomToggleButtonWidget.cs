using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using CUE4Parse.BNS.Assets.Exports;
using Xylia.Preview.UI.Controls.Helpers;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomToggleButtonWidget : BnsCustomLabelButtonWidget
{
	#region Constructors
	static BnsCustomToggleButtonWidget()
	{
		//DefaultStyleKeyProperty.OverrideMetadata(typeof(BnsCustomToggleButtonWidget), new FrameworkPropertyMetadata(typeof(BnsCustomToggleButtonWidget)));
	}
	#endregion

	#region Properties and Events
	/// <summary>
	///     Checked event
	/// </summary>
	public static readonly RoutedEvent CheckedEvent = EventManager.RegisterRoutedEvent("Checked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BnsCustomToggleButtonWidget));

	/// <summary>
	///     Unchecked event
	/// </summary>
	public static readonly RoutedEvent UncheckedEvent = EventManager.RegisterRoutedEvent("Unchecked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BnsCustomToggleButtonWidget));

	/// <summary>
	///     Add / Remove Checked handler
	/// </summary>
	[Category("Behavior")]
	public event RoutedEventHandler Checked
	{
		add
		{
			AddHandler(CheckedEvent, value);
		}

		remove
		{
			RemoveHandler(CheckedEvent, value);
		}
	}

	/// <summary>
	///     Add / Remove Unchecked handler
	/// </summary>
	[Category("Behavior")]
	public event RoutedEventHandler Unchecked
	{
		add
		{
			AddHandler(UncheckedEvent, value);
		}

		remove
		{
			RemoveHandler(UncheckedEvent, value);
		}
	}

	/// <summary>
	///     The DependencyProperty for the IsChecked property.
	///     Flags:              BindsTwoWayByDefault
	///     Default Value:      false
	/// </summary>
	public static readonly DependencyProperty bIsCheckedProperty = DependencyProperty.Register("bIsChecked",
		typeof(bool), typeof(BnsCustomToggleButtonWidget),
		new FrameworkPropertyMetadata(BooleanBoxes.FalseBox,
			FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
			new PropertyChangedCallback(OnIsCheckedChanged)));

	/// <summary>
	///     Indicates whether the ToggleButton is checked
	/// </summary>
	[Category("Appearance")]
	[TypeConverter(typeof(NullableBoolConverter))]
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public bool bIsChecked
	{
		get { return (bool)GetValue(bIsCheckedProperty); }
		set { SetValue(bIsCheckedProperty, value); }
	}

	/// <summary>
	///     Called when IsChecked is changed on "d."
	/// </summary>
	/// <param name="d">The object on which the property was changed.</param>
	/// <param name="e">EventArgs that contains the old and new values for this property</param>
	private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var button = (BnsCustomToggleButtonWidget)d;
		bool oldValue = (bool)e.OldValue;
		bool newValue = (bool)e.NewValue;

		////doing soft casting here because the peer can be that of RadioButton and it is not derived from
		////ToggleButtonAutomationPeer - specifically to avoid implementing TogglePattern
		//ToggleButtonAutomationPeer peer = UIElementAutomationPeer.FromElement(button) as ToggleButtonAutomationPeer;
		//if (peer != null)
		//{
		//	peer.RaiseToggleStatePropertyChangedEvent(oldValue, newValue);
		//}

		if (newValue)
		{
			button.OnChecked(new RoutedEventArgs(CheckedEvent));
		}
		else
		{
			button.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
		}

		button.UpdateVisualState();
	}

	/// <summary>
	///     Called when IsChecked becomes true.
	/// </summary>
	/// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
	private void OnChecked(RoutedEventArgs e)
	{
		// If RadioButton is checked we should uncheck the others in the same group
		UpdateRadioButtonGroup();
		RaiseEvent(e);
	}

	/// <summary>
	///     Called when IsChecked becomes false.
	/// </summary>
	/// <param name="e">Event arguments for the routed event that is raised by the default implementation of this method.</param>
	private void OnUnchecked(RoutedEventArgs e)
	{
		RaiseEvent(e);
	}

	public ImageProperty PressedImageProperty { get; set; }
	#endregion

	#region Override methods
	/// <summary>
	/// Creates AutomationPeer (<see cref="UIElement.OnCreateAutomationPeer"/>)
	/// </summary>
	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return null;
		//return new ToggleButtonAutomationPeer(this);
	}

	/// <summary>
	/// This override method is called when the control is clicked by mouse or keyboard
	/// </summary>
	protected override void OnClick()
	{
		OnToggle();
		base.OnClick();
	}

	internal override void ChangeVisualState(bool useTransitions)
	{
		// Update the Check state group
		VisualStateManager.GoToState(this, bIsChecked ? VisualStates.StateChecked : VisualStates.StateUnchecked, useTransitions);
	}
	#endregion

	#region Private helpers
	/// <summary>
	/// This vitrual method is called from OnClick(). BnsCustomToggleButtonWidget toggles IsChecked property.
	/// Subclasses can override this method to implement their own toggle behavior
	/// </summary>
	protected internal void OnToggle()
	{
		SetCurrentValue(bIsCheckedProperty,
			Parent is BnsCustomRadioButtonWidget || !bIsChecked);
	}

	private void UpdateRadioButtonGroup()
	{
		// Logical parent should be the group
		DependencyObject parent = this.Parent;
		if (parent is BnsCustomRadioButtonWidget)
		{
			// Traverse logical children
			IEnumerable children = LogicalTreeHelper.GetChildren(parent);
			IEnumerator itor = children.GetEnumerator();
			while (itor.MoveNext())
			{
				if (itor.Current is BnsCustomToggleButtonWidget rb && rb != this & (rb.bIsChecked == true))
					rb.UncheckRadioButton();
			}
		}
	}

	private void UncheckRadioButton()
	{
		SetCurrentValue(bIsCheckedProperty, BooleanBoxes.FalseBox);
	}
	#endregion
}