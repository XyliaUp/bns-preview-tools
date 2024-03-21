using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xylia.Preview.UI.Controls.Helpers;
using Xylia.Preview.UI.Controls.Primitives;
using Xylia.Preview.UI.Converters;
using Orientation = CUE4Parse.BNS.Assets.Exports.Orientation;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomSliderBarWidget : BnsCustomRangeBaseWidget
{
	#region Constructors
	/// <summary>
	/// This is the static constructor for the BnsCustomSliderBarWidget class.
	/// It simply registers the appropriate class handlers for the input devices, and defines a default style sheet.
	/// </summary>
	static BnsCustomSliderBarWidget()
	{
		// Initialize CommandCollection & CommandLink(s)
		InitializeCommands();

		// Register all PropertyTypeMetadata
		MinimumProperty.OverrideMetadata(Owner, new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.AffectsMeasure));
		MaximumProperty.OverrideMetadata(Owner, new FrameworkPropertyMetadata(10.0d, FrameworkPropertyMetadataOptions.AffectsMeasure));
		ValueProperty.OverrideMetadata(Owner, new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure));

		// Listen to MouseLeftButtonDown event to determine if slide should move focus to itself
		EventManager.RegisterClassHandler(Owner, Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
		EventManager.RegisterClassHandler(Owner, Mouse.MouseWheelEvent, new MouseWheelEventHandler(OnMouseWheel), true);
	}
	#endregion

	#region Commands
	public static RoutedCommand IncreaseLarge { get; private set; }
	public static RoutedCommand IncreaseSmall { get; private set; }
	public static RoutedCommand DecreaseLarge { get; private set; }
	public static RoutedCommand DecreaseSmall { get; private set; }
	public static RoutedCommand MinimizeValue { get; private set; }
	public static RoutedCommand MaximizeValue { get; private set; }

	static void InitializeCommands()
	{
		IncreaseLarge = new RoutedCommand("IncreaseLarge", Owner);
		IncreaseSmall = new RoutedCommand("IncreaseSmall", Owner);
		DecreaseLarge = new RoutedCommand("DecreaseLarge", Owner);
		DecreaseSmall = new RoutedCommand("DecreaseSmall", Owner);
		MinimizeValue = new RoutedCommand("MinimizeValue", Owner);
		MaximizeValue = new RoutedCommand("MaximizeValue", Owner);

		Owner.RegisterCommandHandler(IncreaseLarge, new ExecutedRoutedEventHandler(OnIncreaseLargeCommand),
			   new BnsCustomSliderBarWidgetGesture(Key.PageUp, Key.PageDown, false));

		Owner.RegisterCommandHandler(DecreaseLarge, new ExecutedRoutedEventHandler(OnDecreaseLargeCommand),
			   new BnsCustomSliderBarWidgetGesture(Key.PageDown, Key.PageUp, false));

		Owner.RegisterCommandHandler(IncreaseSmall, new ExecutedRoutedEventHandler(OnIncreaseSmallCommand),
			   new BnsCustomSliderBarWidgetGesture(Key.Up, Key.Down, false),
				new BnsCustomSliderBarWidgetGesture(Key.Right, Key.Left, true));

		Owner.RegisterCommandHandler(DecreaseSmall, new ExecutedRoutedEventHandler(OnDecreaseSmallCommand),
			   new BnsCustomSliderBarWidgetGesture(Key.Down, Key.Up, false),
				new BnsCustomSliderBarWidgetGesture(Key.Left, Key.Right, true));

		Owner.RegisterCommandHandler(MinimizeValue, new ExecutedRoutedEventHandler(OnMinimizeValueCommand), Key.Home);
		Owner.RegisterCommandHandler(MaximizeValue, new ExecutedRoutedEventHandler(OnMaximizeValueCommand), Key.End);
	}

	private class BnsCustomSliderBarWidgetGesture : InputGesture
	{
		public BnsCustomSliderBarWidgetGesture(Key normal, Key inverted, bool forHorizontal)
		{
			_normal = normal;
			_inverted = inverted;
			_forHorizontal = forHorizontal;
		}

		/// <summary>
		/// Sees if the InputGesture matches the input associated with the inputEventArgs
		/// </summary>
		public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
		{
			KeyEventArgs keyEventArgs = inputEventArgs as KeyEventArgs;
			Slider slider = targetElement as Slider;
			if (keyEventArgs != null && slider != null && Keyboard.Modifiers == ModifierKeys.None)
			{
				if ((int)_normal == (int)keyEventArgs.Key)
				{
					return !IsInverted(slider);
				}
				if ((int)_inverted == (int)keyEventArgs.Key)
				{
					return IsInverted(slider);
				}
			}
			return false;
		}

		private bool IsInverted(Slider slider)
		{
			if (_forHorizontal)
			{
				return slider.IsDirectionReversed != (slider.FlowDirection == FlowDirection.RightToLeft);
			}
			else
			{
				return slider.IsDirectionReversed;
			}
		}

		private Key _normal, _inverted;
		private bool _forHorizontal;
	}


	private static void OnIncreaseSmallCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider)
			slider.Value += slider.SliderStepValue;
	}

	private static void OnDecreaseSmallCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider) 
			slider.Value -= slider.SliderStepValue;
	}

	private static void OnMaximizeValueCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider)
			slider.Value = slider.Maximum;
	}

	private static void OnMinimizeValueCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider) 
			slider.Value = slider.Minimum;
	}

	private static void OnIncreaseLargeCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider) 
			slider.Value += slider.LargeChange;
	}

	private static void OnDecreaseLargeCommand(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is BnsCustomSliderBarWidget slider)
			slider.Value -= slider.LargeChange;
	}
	#endregion

	#region Properties
	private static readonly Type Owner = typeof(BnsCustomSliderBarWidget);

	public static readonly DependencyProperty SliderOrientationProperty = DependencyProperty.Register(nameof(SliderOrientation),
		typeof(Orientation), Owner, new FrameworkPropertyMetadata(Orientation.Orient_Vertical));

	public Orientation SliderOrientation
	{
		get { return (Orientation)GetValue(SliderOrientationProperty); }
		set { SetValue(SliderOrientationProperty, value); }
	}

	public static readonly DependencyProperty SliderStepValueProperty = DependencyProperty.Register(nameof(SliderStepValue),
		typeof(float), Owner, new FrameworkPropertyMetadata(1.0F));

	/// <summary>
	///     Specifies the amount of time, in milliseconds, between repeats once repeating starts.
	/// Must be non-negative
	/// </summary>
	public float SliderStepValue
	{
		get { return (float)GetValue(SliderStepValueProperty); }
		set { SetValue(SliderStepValueProperty, value); }
	}


	public static readonly DependencyProperty bReverseDirectionProperty = DependencyProperty.Register(nameof(bReverseDirection),
		typeof(bool), Owner, new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));

	public bool bReverseDirection
	{
		get => (bool)GetValue(bReverseDirectionProperty);
		set => SetValue(bReverseDirectionProperty, BooleanBoxes.Box(value));
	}
	#endregion

	#region Override Methods
	/// <summary>
	/// This is a class handler for MouseLeftButtonDown event.
	/// The purpose of this handle is to move input focus to BnsCustomSliderBarWidget when user pressed
	/// mouse left button on any part of slider that is not focusable.
	/// </summary>
	private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton != MouseButton.Left) return;

		var slider = (BnsCustomSliderBarWidget)sender;

		// When someone click on the BnsCustomSliderBarWidget's part, and it's not focusable
		// BnsCustomSliderBarWidget need to take the focus in order to process keyboard correctly
		if (!slider.IsKeyboardFocusWithin)
		{
			e.Handled = slider.Focus() || e.Handled;
		}
	}

	/// <summary>
	/// This is a class handler for MouseWheel event.
	/// </summary>
	private static void OnMouseWheel(object sender, MouseWheelEventArgs e)
	{
		var slider = (BnsCustomSliderBarWidget)sender;
		slider.Value -= slider.SliderStepValue * Math.Sign(e.Delta);
	}

	/// <summary>
	/// Perform arrangement of slider's children
	/// </summary>
	/// <param name="finalSize"></param>
	protected override Size ArrangeOverride(Size finalSize)
	{
		Size size = base.ArrangeOverride(finalSize);

		UpdateMarkerPositionAndSize();

		return size;
	}

	/// <summary>
	/// Update Marker Length.
	/// </summary>
	/// <param name="oldValue"></param>
	/// <param name="newValue"></param>
	protected override void OnValueChanged(double oldValue, double newValue)
	{
		base.OnValueChanged(oldValue, newValue);

		UpdateMarkerPositionAndSize();
	}

	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		Marker = this.GetChild<BnsCustomLabelButtonWidget>("Marker");
		Marker.PreviewMouseLeftButtonDown += Marker_OnMouseLeftButtonDown;
		Marker.PreviewMouseLeftButtonUp += Marker_OnMouseLeftButtonUp;
		Marker.PreviewMouseMove += Marker_OnMouseMove;
		Marker.LostMouseCapture += Marker_OnLostMouseCapture;
	}
	#endregion

	#region	Marker
	private BnsCustomLabelButtonWidget Marker;

	/// <summary>
	///     IsDragging indicates that left mouse button is pressed over the marker.
	/// </summary>
	private bool IsDragging;

	/// <summary>
	/// The position where the mouse was clicked down
	/// </summary>
	private Point _originPosition;

	/// <summary>
	/// The position of the mouse when the previous DragDelta event was fired
	/// </summary>
	private Point _previousPosition;

	protected void Marker_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (!IsDragging)
		{
			e.Handled = true;
			Marker.Focus();
			Marker.CaptureMouse();
			IsDragging = true;
			_originPosition = _previousPosition = e.GetPosition(this);
		}
	}

	protected void Marker_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (IsDragging)
		{
			e.Handled = true;
			IsDragging = false;
			Marker.ReleaseMouseCapture();
		}
	}

	protected void Marker_OnMouseMove(object sender, MouseEventArgs e)
	{
		if (IsDragging)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// We will fire DragDelta event only when the mouse is really moved
				var markerCoordPosition = e.GetPosition(this);
				if (markerCoordPosition != _previousPosition)
				{
					e.Handled = true;

					Value += ValueFromDistance(markerCoordPosition.X - _previousPosition.X, markerCoordPosition.Y - _previousPosition.Y);
					_previousPosition = markerCoordPosition;
				}
			}
			else
			{
				if (e.MouseDevice.Captured == (IInputElement)sender)
					ReleaseMouseCapture();
				IsDragging = false;
				_originPosition.X = 0;
				_originPosition.Y = 0;
			}
		}
	}

	private void Marker_OnLostMouseCapture(object sender, MouseEventArgs e)
	{
		if (Mouse.Captured != (IInputElement)sender)
		{
			IsDragging = false;
			Marker.ReleaseMouseCapture();
		}
	}


	private double ValueToSize()
	{
		Size trackSize = this.RenderSize;
		Size markerSize = Marker.RenderSize;
		double range = Maximum - Minimum;

		if (SliderOrientation == Orientation.Orient_Vertical)
		{
			return Math.Max(0.0, (trackSize.Height - markerSize.Height) / range);
		}
		else
		{
			return Math.Max(0.0, (trackSize.Width - markerSize.Width) / range);
		}
	}

	private double ValueFromDistance(double horizontal, double vertical)
	{
		var direction = bReverseDirection ? -1 : 1;
		var Density = ValueToSize();

		if (SliderOrientation == Orientation.Orient_Vertical)
		{
			return direction * vertical / Density;
		}
		else
		{
			return direction * horizontal / Density;
		}
	}

	private void UpdateMarkerPositionAndSize()
	{
		if (Marker is null) return;

		var offset = LayoutData.GetOffsets(Marker);
		var valueToSize = ValueToSize();

		if (SliderOrientation == Orientation.Orient_Vertical)
		{
			var pos = bReverseDirection ?
				(Maximum - Value) * valueToSize :
				(Value - Minimum) * valueToSize;

			LayoutData.SetOffsets(Marker, offset with { Top = pos });
		}
		else
		{
			var pos = bReverseDirection ?
				(Maximum - Value) * valueToSize :
				(Value - Minimum) * valueToSize;

			LayoutData.SetOffsets(Marker, offset with { Left = pos });
		}
	}
	#endregion
}